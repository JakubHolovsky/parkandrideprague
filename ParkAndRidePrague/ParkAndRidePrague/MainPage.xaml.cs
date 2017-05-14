using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ParkAndRidePrague.Core.Apis;
using ParkAndRidePrague.Core.Common;
using ParkAndRidePrague.Core.Interfaces;
using ParkAndRidePrague.Helpers;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace ParkAndRidePrague
{
    public partial class MainPage : ContentPage
    {
		private readonly IParkingApi parkingApi;
        private readonly ILogger logger;
        private readonly ObservableCollection<IParking> parkings;
        

        public MainPage()
        {
            InitializeComponent();

            logger = new Logger();
			parkingApi = new TskApi(logger);
			// parkingApi = new TestParkingApi();

            listViewParkings.IsPullToRefreshEnabled = true;
            parkings = new ObservableCollection<IParking>();
            listViewParkings.ItemsSource = parkings;

            ((App)App.Current).OnTimerTick += OnOnTimerTick;
        }

        private async void OnOnTimerTick()
        {
            var displayLoading = parkings.Count == 0;
            await RefreshParkings(displayLoading);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listViewParkings.Refreshing += ListViewParkingsOnRefreshing;
			listViewParkings.ItemTapped += ListViewParkingsItemTapped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            listViewParkings.Refreshing -= ListViewParkingsOnRefreshing;
			listViewParkings.ItemTapped -= ListViewParkingsItemTapped;
        }

		private async void ListViewParkingsItemTapped(object sender, ItemTappedEventArgs e)
		{
			listViewParkings.SelectedItem = null;
			var parking = e.Item as IParking;
			if (parking == null)
				return;

			var parkingDetail = new ParkingDetail(parking);
			await Navigation.PushModalAsync(new NavigationPage(parkingDetail));
		}

		private async void ListViewParkingsOnRefreshing(object sender, EventArgs eventArgs)
        {
            await RefreshParkings(true);
        }

        private async Task RefreshParkings(bool displayLoading)
        {
            UpdateStatus(AppResources.updating);
            if (displayLoading)
                UpdateLoading(true);

            var hasInternetAccess = await NetworkHelper.HasInternetAccess();
            if (!hasInternetAccess)
            {
                UpdateStatus(AppResources.noNetwork);

                if (displayLoading)
                    UpdateLoading(false);

                return;
            }

            var apiResult = await parkingApi.GetParkings();

            if (apiResult.Error)
            {
                UpdateStatus(AppResources.cannotUpdateParkings);

                if (displayLoading)
                    UpdateLoading(false);

                return;
            }

            foreach (var observableParking in parkings)
            {
                var parking = apiResult.Result.SingleOrDefault(p => p.Id == observableParking.Id);
                if (parking != null)
                {
                    if (parking.LastUpdateDate == observableParking.LastUpdateDate)
                        parking.PreviousFreePlacesCount = observableParking.PreviousFreePlacesCount;
                    else
                        parking.PreviousFreePlacesCount = observableParking.FreePlacesCount;
                }
            }
            ((App)Application.Current).InvokeParkingsRefreshed(apiResult);
            var refreshedParkings = apiResult.Result;

            if (parkings.Count == 0)
            {
                // If we came to the app for the first time, show parkings then do sorting.
                RefreshParkings(refreshedParkings);
                refreshedParkings = await SortParkings(refreshedParkings, true);
                RefreshParkings(refreshedParkings);
            }
            else
            {
                refreshedParkings = await SortParkings(refreshedParkings, false);
                RefreshParkings(refreshedParkings);
            }

            if (displayLoading)
                UpdateLoading(false);
            UpdateStatus($"{AppResources.updatedAt.ToLower()} {apiResult.UpdatedAt:HH:mm:ss}");
        }

        private void RefreshParkings(List<IParking> refreshedParkings)
        {
            parkings.Clear();
            foreach (var parking in refreshedParkings)
            {
                parkings.Add(parking);
            }
        }

        private async Task<List<IParking>> SortParkings(List<IParking> refreshedParkings, bool displayMessage)
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
                {
                    if (displayMessage)
                        UpdateStatus(AppResources.sortingByLocation);
                    var position = await CrossGeolocator.Current.GetPositionAsync();
                    refreshedParkings =
                        refreshedParkings.OrderBy(p => p.GetDistance(position.Latitude, position.Longitude))
                            .ToList();
                }
                else
                {
                    refreshedParkings = refreshedParkings.OrderBy(p => p.Name).ToList();
                }
            }
            catch (Exception exception)
            {
                logger.Log(exception);
            }

            return refreshedParkings;
        }

        private void UpdateLoading(bool showLoading)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				listViewParkings.IsRefreshing = showLoading;
			});
		}

		private void UpdateStatus(string text)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				labelStatus.Text = text;
			});
		}
    }
}

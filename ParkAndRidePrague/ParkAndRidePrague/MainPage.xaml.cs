using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
        private Timer timer;
        private bool isRefreshing;

        public MainPage()
        {
            InitializeComponent();

			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            logger = new Logger();
			parkingApi = new TskApi(logger);
			// parkingApi = new TestParkingApi();

            listViewParkings.IsPullToRefreshEnabled = true;
            parkings = new ObservableCollection<IParking>();
            listViewParkings.ItemsSource = parkings;

            TimerCallback timerDelegate = TimerCallback;
            timer = new Timer(timerDelegate, null, 0, 30000);
        }

        private void TimerCallback(object state)
        {
            var displayLoading = parkings.Count == 0;
            RefreshParkings(displayLoading).Wait();
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

		void ListViewParkingsItemTapped(object sender, ItemTappedEventArgs e)
		{
			listViewParkings.SelectedItem = null;
		}

		private async void ListViewParkingsOnRefreshing(object sender, EventArgs eventArgs)
        {
            await RefreshParkings(true);
        }

        private async Task RefreshParkings(bool displayLoading)
        {
            if (isRefreshing)
                return;

			isRefreshing = true;

			UpdateStatus("updating");
			if (displayLoading)
				UpdateLoading(true);

			var hasInternetAccess = await NetworkHelper.HasInternetAccess();
			if (!hasInternetAccess)
			{
				UpdateStatus("no network");

				if (displayLoading)
					UpdateLoading(false);

				isRefreshing = false;
				return;
			}

            var refreshedParkings = await parkingApi.GetParkings();

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            {
                var position = await CrossGeolocator.Current.GetPositionAsync();
                refreshedParkings =
                    refreshedParkings.OrderBy(p => p.GetDistance(position.Latitude, position.Longitude))
                        .ToList();
            }
            parkings.Clear();
            foreach (var parking in refreshedParkings)
            {
                parkings.Add(parking);
            }

			if (displayLoading)
				UpdateLoading(false);
			UpdateStatus($"updated at {DateTime.Now.ToString("HH:mm:ss")}");

            isRefreshing = false;
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

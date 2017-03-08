using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkAndRidePrague.Core.Apis;
using ParkAndRidePrague.Core.Common;
using ParkAndRidePrague.Core.Dtos;
using ParkAndRidePrague.Helpers;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace ParkAndRidePrague
{
    public partial class MainPage : ContentPage
    {
        private readonly TskApi tskApi;
        private readonly ILogger logger;
        private readonly ObservableCollection<TskParking> parkings;
        private readonly Timer timer;
        private bool isRefreshing;

        public MainPage()
        {
            InitializeComponent();

            logger = new Logger();
            tskApi = new TskApi(logger);

            listViewParkings.IsPullToRefreshEnabled = true;
            parkings = new ObservableCollection<TskParking>();
            listViewParkings.ItemsSource = parkings;

            TimerCallback timerDelegate = TimerCallback;
            timer = new Timer(timerDelegate, null, 0, 15000);
        }

        private void TimerCallback(object state)
        {
            RefreshParkings().Wait();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listViewParkings.Refreshing += ListViewParkingsOnRefreshing;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            listViewParkings.Refreshing -= ListViewParkingsOnRefreshing;
        }

        private async void ListViewParkingsOnRefreshing(object sender, EventArgs eventArgs)
        {
            await RefreshParkings();
        }

        private async Task RefreshParkings()
        {
            if (isRefreshing)
                return;

            isRefreshing = true;

            Device.BeginInvokeOnMainThread(() =>
            {
                listViewParkings.IsRefreshing = true;
            });

            var refreshedParkings = await tskApi.GetParkings();

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

            Device.BeginInvokeOnMainThread(() =>
            {
                listViewParkings.IsRefreshing = false;
            });

            isRefreshing = false;
        }
    }
}

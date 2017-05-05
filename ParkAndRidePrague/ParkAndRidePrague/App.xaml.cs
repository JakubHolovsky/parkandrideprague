using Xamarin.Forms;
using ParkAndRidePrague.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using ParkAndRidePrague.Localization;

namespace ParkAndRidePrague
{
    public partial class App : Application
    {
        private Timer timer;
        public event OnTimerTickEventHandler OnTimerTick;
        public delegate void OnTimerTickEventHandler();

        public event OnParkingsRefreshedEventHandler OnParkingsRefreshed;
		public delegate void OnParkingsRefreshedEventHandler(IApiResult<List<IParking>> apiResult);

        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new MainPage();
        }

        private void TimerCallback(object state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OnTimerTick?.Invoke();
            });
        }

        private void StartTimer()
        {
            if (timer == null)
            {
                TimerCallback timerDelegate = TimerCallback;
                timer = new Timer(timerDelegate, null, 0, 30000);
            }
        }

        private void StopTimer()
        {
            timer = null;
        }

        public void InvokeParkingsRefreshed(IApiResult<List<IParking>> apiResult)
        {
            OnParkingsRefreshed?.Invoke(apiResult);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            StartTimer();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            StopTimer();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            StartTimer();
        }
    }
}

using Xamarin.Forms;
using ParkAndRidePrague.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;

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

            MainPage = new MainPage();
        }

        private void TimerCallback(object state)
        {
            OnTimerTick?.Invoke();
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
			if (OnParkingsRefreshed != null)
			{
				OnParkingsRefreshed.Invoke(apiResult);
			}
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

using Xamarin.Forms;
using ParkAndRidePrague.Core.Interfaces;
using System.Collections.Generic;

namespace ParkAndRidePrague
{
    public partial class App : Application
    {
		public event OnParkingsRefreshedEventHandler OnParkingsRefreshed;
		public delegate void OnParkingsRefreshedEventHandler(IApiResult<List<IParking>> apiResult);

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

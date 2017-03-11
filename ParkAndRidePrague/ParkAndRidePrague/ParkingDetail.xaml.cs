using System.Collections.Generic;
using System.Linq;
using ParkAndRidePrague.Core.Interfaces;
using ParkAndRidePrague.ViewModels;
using Xamarin.Forms;

namespace ParkAndRidePrague
{
	public partial class ParkingDetail : ContentPage
	{
		public IParking Parking { get; set; }

		public ParkingDetail(IParking parking)
		{
			InitializeComponent();

			Parking = parking;
			var parkingViewModel = new ParkingViewModel(parking);
			BindingContext = parkingViewModel;

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "Back",
				Command = new Command(() => Navigation.PopModalAsync()),
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			((App)Application.Current).OnParkingsRefreshed += HandleOnParkingsRefreshed;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			((App)Application.Current).OnParkingsRefreshed -= HandleOnParkingsRefreshed;
		}

		private void HandleOnParkingsRefreshed(IApiResult<List<IParking>> apiResult)
		{
			if (apiResult.Error)
				return;

			var parking = apiResult.Result.SingleOrDefault(p => p.Id == Parking.Id);
			if (parking == null)
				return;

			if (parking.LastUpdateDate == Parking.LastUpdateDate)
				return;

			var parkingViewModel = BindingContext as ParkingViewModel;
			if (parkingViewModel == null)
				return;

			parkingViewModel.Update(parking);

			Device.BeginInvokeOnMainThread(() => {
			if (!labelDifference.IsVisible)
					labelDifference.IsVisible = true;
				if (parkingViewModel.Difference == 0)
				{
					labelDifference.TextColor = Color.Black;
				}
				else if (parkingViewModel.Difference > 0)
				{
					labelDifference.TextColor = Color.FromHex("#4CAF50");
				}
				else
				{
					labelDifference.TextColor = Color.FromHex("#FF5722");
				}
			});

			Parking = parking;
		}
	}
}

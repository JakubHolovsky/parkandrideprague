﻿using System.Collections.Generic;
using System.Linq;
using ParkAndRidePrague.Core.Interfaces;
using ParkAndRidePrague.ViewModels;
using Plugin.ExternalMaps;
using Xamarin.Forms;

namespace ParkAndRidePrague
{
	public partial class ParkingDetail : ContentPage
	{
	    private IParking Parking { get; set; }

		public ParkingDetail(IParking parking)
		{
			InitializeComponent();

			Parking = parking;
			var parkingViewModel = new ParkingViewModel(parking);
			BindingContext = parkingViewModel;
			UpdateFreePlacesCountColorText(parkingViewModel);

            string backIcon = null;
            string mapIcon = null;
            switch(Device.RuntimePlatform)
            {
                case Device.UWP:
                    backIcon = "Images/back_arrow.png";
                    mapIcon = "Images/map_pin.png";
                    break;
                default:
                    mapIcon = "map_pin.png";
                    break;
            }

			ToolbarItems.Add(new ToolbarItem
			{
				Text = AppResources.map,
				Command = new Command(async () => await CrossExternalMaps.Current.NavigateTo(Parking.Name, Parking.Latitude, Parking.Longitude)),
				IconImageSource = mapIcon 
			});

			ToolbarItems.Add(new ToolbarItem
			{
				Text = AppResources.back,
				Command = new Command(async () => await Navigation.PopModalAsync()),
				IconImageSource = backIcon
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
			UpdateFreePlacesCountColorText(parkingViewModel);

			Parking = parking;
		}

		private void UpdateFreePlacesCountColorText(ParkingViewModel parkingViewModel)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
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
		}
	}
}

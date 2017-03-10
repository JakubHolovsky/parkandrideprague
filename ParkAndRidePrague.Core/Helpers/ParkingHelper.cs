using System;
using ParkAndRidePrague.Core.Enums;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Helpers
{
	public static class ParkingHelper
	{
		public static ParkingAvailability CalculateParkingAvailability(this IParking parking)
		{
			var onePercent = (double)parking.TotalPlacesCount / 100;
			if (parking.FreePlacesCount == 0)
				return ParkingAvailability.None;
			if (parking.FreePlacesCount <= (int)(onePercent * 10))
				return ParkingAvailability.Low;
			else if (parking.FreePlacesCount <= (int)(onePercent * 30))
				return ParkingAvailability.Medium;
			else
				return ParkingAvailability.High;
		}
	}
}

using System;
using ParkAndRidePrague.Core.Enums;
using ParkAndRidePrague.Core.Helpers;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Test
{
	public class TestParking : IParking
	{
		public TestParking()
		{
		}

		public int Id
		{
			get; set;
		}

		public int FreePlacesCount
		{
			get; set;
		}

		public double Latitude
		{
			get; set;
		}

		public double Longitude
		{
			get; set;
		}

		public DateTime LastUpdateDate 
		{ 
			get; set; 
		}

		public string Name
		{
			get; set;
		}

		public ParkingAvailability ParkingAvailability
		{
			get
			{
				return this.CalculateParkingAvailability();
			}
		}

		public int TakenPlacesCount
		{
			get; set;
		}

		public int TotalPlacesCount
		{
			get; set;
		}
	}
}

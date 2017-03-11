using System.Collections.Generic;
using System.Threading.Tasks;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Test
{
	public class TestParkingApi : IParkingApi
	{
		public TestParkingApi()
		{
		}

		public async Task<IApiResult<List<IParking>>> GetParkings()
		{
			var testParkings = new List<IParking>();
			var testParking1 = new TestParking()
			{
				Name = "Test Parking 1",
				FreePlacesCount = 0,
				Latitude = 150,
				Longitude = 150,
				TakenPlacesCount = 10,
				TotalPlacesCount = 10
			};
			testParkings.Add(testParking1);
			var testParking2 = new TestParking()
			{
				Name = "Test Parking 2",
				FreePlacesCount = 1,
				Latitude = 150,
				Longitude = 150,
				TakenPlacesCount = 9,
				TotalPlacesCount = 10
			};
			testParkings.Add(testParking2);
			var testParking3 = new TestParking()
			{
				Name = "Test Parking 3",
				FreePlacesCount = 3,
				Latitude = 150,
				Longitude = 150,
				TakenPlacesCount = 7,
				TotalPlacesCount = 10
			};
			testParkings.Add(testParking3);
			var testParking4 = new TestParking()
			{
				Name = "Test Parking 4",
				FreePlacesCount = 10,
				Latitude = 150,
				Longitude = 150,
				TakenPlacesCount = 0,
				TotalPlacesCount = 10
			};
			testParkings.Add(testParking4);
			return new ApiResult<List<IParking>>() { Error = false, Result = testParkings };
		}
	}
}

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkAndRidePrague.Core.Apis;
using ParkAndRidePrague.Core.Common;

namespace ParkAndRidePrague.Core.Tests.Apis
{
    [TestClass]
    public class TskApiTests
    {
        [TestMethod]
        public async Task GetParkingsShouldSucceed()
        {
            var logger = new Logger();
            var tskApi = new TskApi(logger);
            var parkings = await tskApi.GetParkings();
            Assert.IsNotNull(parkings);
            Assert.IsTrue(parkings.Count > 0);
        }

        [TestMethod]
        public async Task GetAndWriteParkingAvailability()
        {
            var logger = new Logger();
            var tskApi = new TskApi(logger);
            var parkings = await tskApi.GetParkings();
            Assert.IsNotNull(parkings);
            Assert.IsTrue(parkings.Count > 0);
            foreach (var parking in parkings)
                Console.WriteLine($"Parking: {parking.Name}, Availability: {parking.ParkingAvailability}, Total Places: {parking.TotalNumOfPlaces}, Free Places: {parking.NumOfFreePlaces}, Taken Places: {parking.NumOfTakenPlaces}");
        }
    }
}

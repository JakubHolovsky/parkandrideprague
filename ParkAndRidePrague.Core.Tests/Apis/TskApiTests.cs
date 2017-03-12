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
            var apiResult = await tskApi.GetParkings();
            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.Result.Count > 0);
        }

        [TestMethod]
        public async Task GetAndWriteParkingAvailability()
        {
            var logger = new Logger();
            var tskApi = new TskApi(logger);
            var apiResult = await tskApi.GetParkings();
            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.Result.Count > 0);
            foreach (var parking in apiResult.Result)
                Console.WriteLine($"Parking: {parking.Name}, Availability: {parking.ParkingAvailability}, Total Places: {parking.TotalPlacesCount}, Free Places: {parking.FreePlacesCount}, Taken Places: {parking.TakenPlacesCount}");
        }
    }
}

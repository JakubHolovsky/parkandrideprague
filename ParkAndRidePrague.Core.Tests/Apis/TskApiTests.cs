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
    }
}

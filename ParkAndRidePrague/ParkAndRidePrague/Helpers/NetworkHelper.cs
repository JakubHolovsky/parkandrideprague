using System.Threading.Tasks;
using ParkAndRidePrague.Core;
using Plugin.Connectivity;

namespace ParkAndRidePrague
{
	public static class NetworkHelper
	{
		public static async Task<bool> HasInternetAccess()
		{
			var isHostReachable = await CrossConnectivity.Current.IsRemoteReachable(Constants.Apis.TskkApiHost);
			return CrossConnectivity.Current.IsConnected && isHostReachable;
		}
	}
}

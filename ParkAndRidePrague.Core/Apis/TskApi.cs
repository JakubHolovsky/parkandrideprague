using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkAndRidePrague.Core.Dtos;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Apis
{
    public class TskApi : IParkingApi
    {
        private readonly ILogger logger;

        public TskApi(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<ApiResult<List<IParking>>> GetParkings()
        {
            try
            {
                var httpClient = new HttpClient();
                var jsonResponse = await httpClient.GetStringAsync(Constants.Apis.TskApiUrl);
                var tskResponse = JsonConvert.DeserializeObject<TskResponse>(jsonResponse);
				return new ApiResult<List<IParking>>
				{
					Error = false,
					Result = tskResponse.Parkings.Cast<IParking>().ToList()
				};
            }
            catch (Exception e)
            {
                logger.Log(e);
				return new ApiResult<List<IParking>>()
				{
					Error = true,
					Result = new List<IParking>()
				};
            }
        }
    }
}

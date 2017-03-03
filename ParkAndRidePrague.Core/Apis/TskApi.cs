using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkAndRidePrague.Core.Common;
using ParkAndRidePrague.Core.Dtos;

namespace ParkAndRidePrague.Core.Apis
{
    public class TskApi
    {
        private readonly ILogger logger;

        public TskApi(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<List<TskParking>> GetParkings()
        {
            try
            {
                var httpClient = new HttpClient();
                var jsonResponse = await httpClient.GetStringAsync(Constants.Apis.TskApiUrl);
                var tskResponse = JsonConvert.DeserializeObject<TskResponse>(jsonResponse);
                return tskResponse.Parkings;
            }
            catch (Exception e)
            {
                logger.Log(e);
                return new List<TskParking>();
            }
        }
    }
}

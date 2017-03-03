using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParkAndRidePrague.Core.Dtos
{
    public class TskResponse
    {
        [JsonProperty("desc")]
        public string Description { get; set; }
        [JsonProperty("results ")]
        public List<TskParking> Parkings { get; set; }
    }
}

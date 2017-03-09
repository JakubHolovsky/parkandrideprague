using Newtonsoft.Json;
using ParkAndRidePrague.Core.Enums;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Dtos
{
    public class TskParking : IParking
    {
        [JsonProperty("pr")]
        public bool Pr { get; set; }
        [JsonProperty("lng")]
        public double Longitude { get; set; }
        [JsonProperty("parkid")]
        public string ParkId { get; set; }
        [JsonProperty("totalnumofplaces")]
        public int TotalPlacesCount { get; set; }
        [JsonProperty("htmlweb")]
        public string Htmlweb { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastupdatedate")]
        public string LastUpdateDate { get; set; }
        [JsonProperty("numoffreeplaces")]
        public int FreePlacesCount { get; set; }
        [JsonProperty("numoftakenplaces")]
        public int TakenPlacesCount { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        #region Custom Properties

        [JsonIgnore]
        public ParkingAvailability ParkingAvailability
        {
            get
            {
                var onePercent = (double)TotalPlacesCount / 100;
                if (FreePlacesCount == 0)
                    return ParkingAvailability.None;
                if (FreePlacesCount <= (int)(onePercent * 10))
                    return ParkingAvailability.Low;
                else if (FreePlacesCount <= (int)(onePercent * 30))
                    return ParkingAvailability.Medium;
                else 
                    return ParkingAvailability.High;
            }
        }
        #endregion
    }
}

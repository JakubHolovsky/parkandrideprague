using Newtonsoft.Json;
using ParkAndRidePrague.Core.Enums;

namespace ParkAndRidePrague.Core.Dtos
{
    public class TskParking
    {
        [JsonProperty("pr")]
        public bool Pr { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
        [JsonProperty("parkid")]
        public string ParkId { get; set; }
        [JsonProperty("totalnumofplaces")]
        public int TotalNumOfPlaces { get; set; }
        [JsonProperty("htmlweb")]
        public string Htmlweb { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastupdatedate")]
        public string LastUpdateDate { get; set; }
        [JsonProperty("numoffreeplaces")]
        public int NumOfFreePlaces { get; set; }
        [JsonProperty("numoftakenplaces")]
        public int NumOfTakenPlaces { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }

        #region Custom Properties

        [JsonIgnore]
        public ParkingAvailability ParkingAvailability
        {
            get
            {
                var onePercent = (double)TotalNumOfPlaces / 100;
                if (NumOfFreePlaces == 0)
                    return ParkingAvailability.None;
                if (NumOfFreePlaces <= (int)(onePercent * 10))
                    return ParkingAvailability.Low;
                else if (NumOfFreePlaces <= (int)(onePercent * 30))
                    return ParkingAvailability.Medium;
                else 
                    return ParkingAvailability.High;
            }
        }

        #endregion
    }
}

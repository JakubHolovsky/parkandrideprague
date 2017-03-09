using ParkAndRidePrague.Core.Enums;

namespace ParkAndRidePrague.Core.Interfaces
{
    public interface IParking
    {
        string Name { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
        int TotalPlacesCount { get; set; }
        int FreePlacesCount { get; set; }
        int TakenPlacesCount { get; set; }
        ParkingAvailability ParkingAvailability { get; }
    }
}

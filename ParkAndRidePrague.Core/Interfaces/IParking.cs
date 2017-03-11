using System;
using ParkAndRidePrague.Core.Enums;

namespace ParkAndRidePrague.Core.Interfaces
{
    public interface IParking
    {
		int Id { get; set; }
        string Name { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
		DateTime LastUpdateDate { get; set; }
        int TotalPlacesCount { get; set; }
        int FreePlacesCount { get; set; }
        int TakenPlacesCount { get; set; }
        ParkingAvailability ParkingAvailability { get; }
    }
}

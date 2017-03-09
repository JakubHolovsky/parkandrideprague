using System;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Helpers
{
    public static class GeoLocationHelper
    {
        public static double GetDistance(this IParking tskParking, double location2Lat, double location2Lng)
        {
            return CalculateDistance(tskParking.Latitude, tskParking.Longitude, location2Lat, location2Lng);
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private static double CalculateDistance(double location1Lat, double location1Lng, double location2Lat, double location2Lng)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = DegreesToRadians(location1Lat);
            double longitude1Rad = DegreesToRadians(location1Lng);
            double latititude2Rad = DegreesToRadians(location2Lat);
            double longitude2Rad = DegreesToRadians(location2Lng);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                    Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }
    }
}

using System;

namespace ParkAndRidePrague.Core.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception exception);
    }
}

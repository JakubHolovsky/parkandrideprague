using System;
using System.Diagnostics;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core.Common
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            // TODO: Log somewhere remotely
            Debug.WriteLine(message);
        }

        public void Log(Exception exception)
        {
            // TODO: Log somewhere remotely
            if (exception != null)
                Debug.WriteLine(exception.Message);
        }
    }
}

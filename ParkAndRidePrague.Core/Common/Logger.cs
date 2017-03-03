using System;
using System.Diagnostics;

namespace ParkAndRidePrague.Core.Common
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception exception);
    }

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

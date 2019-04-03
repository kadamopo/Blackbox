using System;

namespace ServiceBusQueueClient.Logger
{
    public interface ILogger
    {
        void Log(string message);
    }
}
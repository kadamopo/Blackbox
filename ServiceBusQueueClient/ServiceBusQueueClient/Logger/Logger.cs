using System;

namespace ServiceBusQueueClient.Logger
{
    public abstract class Logger : ILogger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now}, {message}");
        }
    }
}
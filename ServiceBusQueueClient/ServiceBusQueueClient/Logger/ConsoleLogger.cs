using System;

namespace ServiceBusQueueClient.Logger
{
    public class ConsoleLogger : Logger, ILogger
    {
        public override void Log(string message)
        {
            Console.Write("Console logger: ");
            base.Log(message);
        }
    }
}
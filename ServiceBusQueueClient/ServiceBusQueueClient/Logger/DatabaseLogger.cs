using System;

namespace ServiceBusQueueClient.Logger
{
    public class DatabaseLogger : Logger, ILogger
    {
        // ToDo: This need proper implementation.

        public override void Log(string message)
        {
            Console.Write("Database logger: ");
            base.Log(message);
        }
    }
}
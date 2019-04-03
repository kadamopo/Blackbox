using System;

namespace ServiceBusQueueClient.Logger
{
    public class FileLogger : Logger, ILogger
    {
        // ToDo: This need proper implementation.

        public override void Log(string message)
        {
            Console.Write("File logger: ");
            base.Log(message);
        }
    }
}
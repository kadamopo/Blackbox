using ServiceBusQueueClient.QueueClient;
using ServiceBusQueueClient.DI;

namespace ServiceBusQueueClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IQueueReader queueReader = ServiceLocator.GetQueueReader();

            queueReader.ReadMessages();
        }
    }
}

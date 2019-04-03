using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace QueueHelpers
{
    public class QueueWritter : IQueueWritter
    {
        // ToDo: Move the Service Bus connection string and queue name details into a configuration file
        const string ServiceBusConnectionString = "Endpoint=sb://kostastest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=N8r5px7R+MLzUb7Dd8IfCO41OxU0SHrduWP95Z83dT0=";
        const string QueueName = "devicemonitoringdata";
        static IQueueClient queueClient;

        public void AddMessage(string messageBody)
        {
            QueueMessageAsync(messageBody).GetAwaiter().GetResult();
        }

        private async Task QueueMessageAsync(string messageBody)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            await SendMessageAsync(messageBody);

            await queueClient.CloseAsync();
        }

        private async Task SendMessageAsync(string messageBody)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                await queueClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                // ToDo: use a logger to log the exception details
                Console.WriteLine($"{DateTime.Now} :: Exception: {ex.Message}");
            }
        }
    }
}

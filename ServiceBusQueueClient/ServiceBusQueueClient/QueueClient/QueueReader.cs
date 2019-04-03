using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

using ServiceBusQueueClient.Models;
using ServiceBusQueueClient.DAL;

namespace ServiceBusQueueClient.QueueClient
{
    public class QueueReader : IQueueReader
    {
        // ToDo: Move the Service Bus connection string and queue name details into a configuration file
        const string ServiceBusConnectionString = "Endpoint=sb://kostastest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=N8r5px7R+MLzUb7Dd8IfCO41OxU0SHrduWP95Z83dT0=";
        const string QueueName = "devicemonitoringdata";
        static IQueueClient queueClient;

        private readonly IDeviceRepository deviceRepository;
        private readonly IDeviceMessageRepository deviceMessageRepository;

        public QueueReader(IDeviceRepository deviceRepository, IDeviceMessageRepository deviceMessageRepository)
        {
            this.deviceRepository = deviceRepository;
            this.deviceMessageRepository = deviceMessageRepository;
        }

        public void ReadMessages()
        {
            ReadMessagesAsync().GetAwaiter().GetResult();
        }

        private async Task ReadMessagesAsync()
        {
            queueClient = new Microsoft.Azure.ServiceBus.QueueClient(ServiceBusConnectionString, QueueName);

            RegisterOnMessageHandlerAndReceiveMessages();

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        private void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that will process messages
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            string messageSequenceNumber = message.SystemProperties.SequenceNumber.ToString();
            string messageBody = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine($"Received message: SequenceNumber:{messageSequenceNumber} Body:{messageBody}");

            JsonMessage queueMessage = JsonDeserialiser(messageBody);

            // ToDo: The following code that checks if the device is already in the
            // database (in this case only an update is required), or if it is a new
            // device (in this case a new device has to be created and added to the
            // DB), must be moved somewhere else, because this is very tightly coupled
            // and I shouldn't have random references to objects of the Model in here.

            Device device = deviceRepository.GetById(queueMessage.SerialNumber);

            if (device != null)
            {
                var deviceMessage = new DeviceMessage
                {
                    Timestamp = queueMessage.Timestamp,
                    BatteryLevel = queueMessage.BatteryLevel,
                    Uptime = queueMessage.Uptime,
                    SerialNumber = queueMessage.SerialNumber
                };

                deviceMessageRepository.Insert(deviceMessage);
            }
            else
            {
                var deviceMessage = new DeviceMessage
                {
                    Timestamp = queueMessage.Timestamp,
                    BatteryLevel = queueMessage.BatteryLevel,
                    Uptime = queueMessage.Uptime
                };

                IList<DeviceMessage> deviceMessages = new List<DeviceMessage>();

                deviceMessages.Add(deviceMessage);

                device = new Device { SerialNumber = queueMessage.SerialNumber, DeviceMessages = deviceMessages };

                deviceRepository.Insert(device);
            }

            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
            // to avoid unnecessary exceptions.
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

        // ToDo: Move this method to a service class that can be located via the
        // Service Locator or a DI container. It shouldn't be here.
        private JsonMessage JsonDeserialiser(string message)
        {
            JsonMessage jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(message);

            Console.WriteLine($"\n\tDevice message in JSON format deserialised as follows:\n");
            Console.WriteLine(jsonMessage.ToString());
            Console.WriteLine();

            return jsonMessage;
        }
    }
}

using Microsoft.EntityFrameworkCore;

using ServiceBusQueueClient.Models;
using ServiceBusQueueClient.DAL;
using ServiceBusQueueClient.QueueClient;
using ServiceBusQueueClient.Logger;

namespace ServiceBusQueueClient.DI
{
    public static class ServiceLocator
    {
        public static DbContext Context { get; private set; }
        public static IDeviceRepository DeviceRepository { get; private set; }
        public static IDeviceMessageRepository DeviceMessageRepository { get; private set; }
        public static IQueueReader QueueReader { get; private set; }
        public static ILogger Logger { get; private set; }


        static ServiceLocator()
        {
            Context = null;
            DeviceRepository = null;
        }

        public static DbContext GetContext()
        {
            if (Context == null)
            {
                Context = new DeviceContext();
            }

            return Context;
        }

        public static IDeviceRepository GetDeviceRepository()
        {
            if (DeviceRepository == null)
            {
                DeviceRepository = new DeviceRepository(GetContext());
            }

            return DeviceRepository;
        }

        public static IDeviceMessageRepository GetDeviceMessageRepository()
        {
            if (DeviceMessageRepository == null)
            {
                DeviceMessageRepository = new DeviceMessageRepository(GetContext());
            }

            return DeviceMessageRepository;
        }

        public static IQueueReader GetQueueReader()
        {
            if (QueueReader == null)
            {
                QueueReader = new QueueReader(GetDeviceRepository(), GetDeviceMessageRepository());
            }

            return QueueReader;
        }

        public static ILogger GetLogger()
        {
            if (Logger == null)
            {
                Logger = new ConsoleLogger();
            }

            return Logger;
        }
    }
}

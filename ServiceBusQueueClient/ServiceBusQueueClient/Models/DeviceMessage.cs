using System;

namespace ServiceBusQueueClient.Models
{
    public class DeviceMessage : IDeviceMessage
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public uint BatteryLevel { get; set; }
        public TimeSpan Uptime { get; set; }

        public virtual string SerialNumber { get; set; }
        public virtual Device Device { get; set; }
    }
}

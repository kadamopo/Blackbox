using System;

namespace ServiceBusQueueClient.Models
{
    public interface IDeviceMessage
    {
        uint BatteryLevel { get; set; }
        Device Device { get; set; }
        int Id { get; set; }
        string SerialNumber { get; set; }
        DateTime Timestamp { get; set; }
        TimeSpan Uptime { get; set; }
    }
}
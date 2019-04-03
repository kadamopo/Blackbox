using System;

namespace ServiceBusQueueClient.Models
{
    public interface IJsonMessage
    {
        uint BatteryLevel { get; set; }
        string SerialNumber { get; set; }
        DateTime Timestamp { get; set; }
        TimeSpan Uptime { get; set; }
    }
}
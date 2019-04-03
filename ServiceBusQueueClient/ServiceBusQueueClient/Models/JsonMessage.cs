using System;

namespace ServiceBusQueueClient.Models
{
    public class JsonMessage : IJsonMessage
    {
        public string SerialNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public uint BatteryLevel { get; set; }
        public TimeSpan Uptime { get; set; }

        public override string ToString()
        {
            return $"\tSerial Number: {SerialNumber}\n\tTimestamp: {Timestamp}\n\tBattery Level: {BatteryLevel}\n\tUptime: {Uptime}";
        }
    }
}

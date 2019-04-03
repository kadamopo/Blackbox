using System;

namespace Models
{
    public class Device : IDevice
    {
        public string SerialNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public uint BatteryLevel { get; set; }
        public TimeSpan Uptime { get; set; }
    }
}

using System;

namespace Models
{
    public interface IDevice
    {
        uint BatteryLevel { get; set; }
        string SerialNumber { get; set; }
        DateTime Timestamp { get; set; }
        TimeSpan Uptime { get; set; }
    }
}
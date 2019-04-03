using System.Collections.Generic;

namespace ServiceBusQueueClient.Models
{
    public interface IDevice
    {
        ICollection<DeviceMessage> DeviceMessages { get; set; }
        string SerialNumber { get; set; }
    }
}
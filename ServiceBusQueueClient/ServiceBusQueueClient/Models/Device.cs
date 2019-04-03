using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceBusQueueClient.Models
{
    public class Device : IDevice
    {
        [Key]
        public string SerialNumber { get; set; }

        public virtual ICollection<DeviceMessage> DeviceMessages { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using Models;
using DeviceMonitoringAPI.Context;
using QueueHelpers;
using JsonHelpers;
using BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceMonitoringAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceContext _context;
        private readonly IQueueWritter _messageQueue;
        private readonly IDeviceProcessing _processLogic;
        private readonly IJsonSerialiser<Device> _jsonSerialiser;

        public DeviceController(DeviceContext context, IQueueWritter messageQueue, IDeviceProcessing processLogic, IJsonSerialiser<Device> jsonSerialiser)
        {
            _context = context;
            _messageQueue = messageQueue;
            _processLogic = processLogic;
            _jsonSerialiser = jsonSerialiser;
        }

        // GET: api/Device
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        // GET api/Device/SBC-00004522
        [HttpGet("{serialNumber}")]
        public async Task<ActionResult<Device>> GetDevice(string serialNumber)
        {
            var device = await _context.Devices.FindAsync(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // POST api/Device
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            _processLogic.Process(device);
            _messageQueue.AddMessage(_jsonSerialiser.Serialise(device));

            return CreatedAtAction(nameof(GetDevice), new { serialNumber = device.SerialNumber }, device);
        }

        // PUT api/Device/SBC-00004522
        [HttpPut("{serialNumber}")]
        public async Task<IActionResult> PutDevice(string serialNumber, Device device)
        {
            if (serialNumber != device.SerialNumber)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _processLogic.Process(device);
            _messageQueue.AddMessage(_jsonSerialiser.Serialise(device));

            return NoContent();
        }

        // DELETE api/Device/SBC-00004522
        [HttpDelete("{serialNumber}")]
        public async Task<IActionResult> DeleteDevice(string serialNumber)
        {
            var device = await _context.Devices.FindAsync(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

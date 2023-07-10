using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gps_app.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAllDevices([FromQuery] int page = 1)
        {
            if (page < 1) page = 1;
            return Ok(await _deviceService.GetDevices(page));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Device>>> GetDeviceById(string id)
        {
            var device = await _deviceService.GetDeviceById(id);

            if (device == null) return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> AddDevice(DeviceDto device)
        {
            if (await _deviceService.GetDeviceById(device.Id) == null)
            {
                return Ok(await _deviceService.SaveDevice(device));
            }
            return Conflict("Device with id '" + device.Id + "' already exists.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteDevice(string id)
        {
            var result = await _deviceService.DeleteDevice(id);
            if (result) return NoContent();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Device>> UpdateDevice(DeviceDto updatedDevice)
        {
            if ( updatedDevice.Id == null) return NotFound("Device id must be valid.");

            var result = await _deviceService.UpdateDevice(updatedDevice);

            if (result == null) return BadRequest("Device was not found.");

            return Ok(result);

        }
    }
}

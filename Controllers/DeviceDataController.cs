using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace gps_app.Controllers
{
    [Route("api/devices/{deviceId}/data")]
    [ApiController]
    public class DeviceDataController : ControllerBase
    {
        private readonly IDeviceDataService _deviceDataService;
        public DeviceDataController(IDeviceDataService deviceDataService)
        {
            _deviceDataService = deviceDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceData>>> GetDataForDevice(string deviceId, [FromQuery] int page = 0)
        {
            if (page < 0) page = 0;
            List<DeviceDataDto> deviceData = await _deviceDataService.GetDeviceData(deviceId, page);

            if (deviceData.IsNullOrEmpty()) return NotFound();

            return Ok(deviceData);
        }

        [HttpGet("{deviceDataId}")]
        public async Task<ActionResult<List<DeviceData>>> GetDeviceDataById(string deviceId, string deviceDataId)
        {
            var deviceData = await _deviceDataService.GetDeviceDataById(deviceId, deviceDataId);

            if (deviceData == null) return NotFound();
            return Ok(deviceData);
        }

        [HttpPost]
        public async Task<ActionResult<DeviceData[]>> AddDeviceData(string deviceId, DeviceDataDto device)
        {
            if (device == null) return BadRequest();

            var deviceData = await _deviceDataService.GetDeviceDataById(deviceId, device.Id);

            if (deviceData != null)
            {
                return Conflict("Device Data with id '" + deviceData.Id + "' already exists.");
            }

            return Ok(await _deviceDataService.SaveDeviceData(deviceId, device));
        }

        [HttpDelete("{deviceDataId}")]
        public async Task<ActionResult<Device>> DeleteDeviceData(string deviceId, string deviceDataId)
        {
            var result = await _deviceDataService.DeleteDeviceData(deviceId, deviceDataId);
            if (result) return NoContent();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Device>> UpdateDataDevice(string deviceId, DeviceDataDto updatedDeviceData)
        {
            if (updatedDeviceData.Id == null || deviceId == null) return BadRequest("Device id and Device data id must be valid.");

            var deviceDataDto = await _deviceDataService.UpdateDeviceData(deviceId, updatedDeviceData);

            if (deviceDataDto == null) return BadRequest("Device data was not found.");

            return Ok(deviceDataDto);

        }
    }
}

using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace gps_app.Controllers
{
    [Route("api/{deviceId}/data")]
    [ApiController]
    public class DeviceDataController : ControllerBase
    {
        private readonly IDeviceDataService _deviceDataService;
        public DeviceDataController(IDeviceDataService deviceDataService)
        {
            _deviceDataService = deviceDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceData>>> GetDataForDevice(string deviceId)
        {
            List<DeviceDataDto> deviceData = await _deviceDataService.GetDeviceData(deviceId);

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
    }
}

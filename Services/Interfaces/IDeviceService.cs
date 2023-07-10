using gps_app.Entities.Dtos;
using gps_app.Models;
using Microsoft.Data.SqlClient;

namespace gps_app.Services.Interfaces
{
    public interface IDeviceService : IBaseService<Device, DeviceDto>
    {
        Task<List<DeviceDto>> GetDevices(int deviceCount);
        Task<DeviceDto?> GetDeviceById(string id);
        Task<DeviceDto> SaveDevice(DeviceDto device);
        Task<bool> DeleteDevice(string deviceId);
        Task<DeviceDto> UpdateDevice(DeviceDto device);
    }
}

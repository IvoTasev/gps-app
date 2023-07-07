using gps_app.Entities.Dtos;
using gps_app.Models;

namespace gps_app.Services.Interfaces
{
    public interface IDeviceDataService : IBaseService<DeviceData, DeviceDataDto>
    {
        Task<List<DeviceDataDto>> GetDeviceData(string deviceId);
        Task<DeviceDataDto> GetDeviceDataById(string deviceId, string dataId);
        Task<DeviceDataDto> SaveDeviceData(string deviceId, DeviceDataDto deviceData);
    }
}

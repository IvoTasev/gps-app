using gps_app.Entities.Dtos;
using gps_app.Models;

namespace gps_app.Services.Interfaces
{
    public interface IDeviceDataService : IBaseService<DeviceData, DeviceDataDto>
    {
        Task<List<DeviceDataDto>> GetDeviceData(string deviceId, int page);
        Task<DeviceDataDto> GetDeviceDataById(string deviceId, string dataId);
        Task<DeviceDataDto> SaveDeviceData(string deviceId, DeviceDataDto deviceData);
        Task<bool> DeleteDeviceData(string deviceId, string deviceDataId);
        Task<DeviceDataDto> UpdateDeviceData(string deviceId, DeviceDataDto deviceData);
    }
}

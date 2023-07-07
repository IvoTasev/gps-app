using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Persistency;
using gps_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gps_app.Services
{
    public class DeviceDataService : IDeviceDataService
    {

        DatabaseContext databaseContext;

        public DeviceDataService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<DeviceDataDto>> GetDeviceData(string deviceId)
        {
            var deviceDataDtos = new List<DeviceDataDto>();
            var deviceData = await databaseContext.DeviceData.Where(data => data.DeviceId == deviceId).ToListAsync();

            deviceData.ForEach(d => deviceDataDtos.Add(ToDto(d)));
            return deviceDataDtos;
        }

        public async Task<DeviceDataDto> GetDeviceDataById(string deviceId, string id)
        {
            var deviceData = await databaseContext.DeviceData
                .Where(d => d.DeviceId == deviceId)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
            if (deviceData == null)
            {
                return null;
            }
            return ToDto(deviceData);
        }

        public async Task<DeviceDataDto> SaveDeviceData(string deviceId, DeviceDataDto dto)
        {
            var device = await databaseContext.Devices.FindAsync(deviceId);
            if (device == null)
            {
                return null;
            }
            DeviceData data = FromDto(dto);
            data.Device = device;
            databaseContext.DeviceData.Add(data);

            await databaseContext.SaveChangesAsync();

            return dto;
        }

        public DeviceData FromDto(DeviceDataDto dto)
        {
            DeviceData deviceData = new DeviceData
            {
                Id = dto.Id,
                Date = dto.Date,
                Location = dto.Location,
                DeviceId = dto.DeviceId,
                Device = null!
            };
            return deviceData;
        }

        public DeviceDataDto ToDto(DeviceData entity)
        {
            DeviceDataDto dto = new DeviceDataDto
            {
                Id = entity.Id,
                Date = entity.Date,
                Location = entity.Location,
                DeviceId = entity.DeviceId
            };
            return dto;
        }
    }
}

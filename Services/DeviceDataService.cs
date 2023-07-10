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
        private static readonly float resultsPerPage = 2f;

        public DeviceDataService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<DeviceDataDto>> GetDeviceData(string deviceId, int page)
        {
            var deviceDataDtos = new List<DeviceDataDto>();
            List<DeviceData> deviceData;

            //if page is not defined all objects are fetched
            if (page != 0)
            {
                deviceData = await databaseContext.DeviceData
                    .Where(d => d.DeviceId == deviceId)
                    .Skip((page - 1) * (int)resultsPerPage)
                    .Take((int)resultsPerPage)
                    .ToListAsync();
            }
            else {
                deviceData = await databaseContext.DeviceData
                    .Where(d => d.DeviceId == deviceId)
                    .ToListAsync();
            }

            deviceData.ForEach(d => deviceDataDtos.Add(ToDto(d, true, page)));
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
                Date = (DateTime)dto.Date,
                Location = dto.Location,
                DeviceId = dto.DeviceId,
                Device = null!
            };
            return deviceData;
        }

        public DeviceDataDto ToDto(DeviceData entity, bool addPagination = false, int page = 0)
        {
            DeviceDataDto dto = new DeviceDataDto
            {
                Id = entity.Id,
                Date = entity.Date,
                Location = entity.Location,
                DeviceId = entity.DeviceId
            };

            if (addPagination)
            {
                var numberOfPages = Math.Ceiling(databaseContext.Devices.Count() / resultsPerPage);

                dto.CurrentPage = page;
                dto.Pages = (int)numberOfPages;
            }

            return dto;
        }

        public async Task<bool> DeleteDeviceData(string deviceId, string deviceDataId)
        {
            var deviceData = await databaseContext.DeviceData
                .Where(d => d.DeviceId == deviceId)
                .Where(d => d.Id == deviceDataId)
                .FirstOrDefaultAsync();

            if (deviceData != null)
            {
                databaseContext.DeviceData.Remove(deviceData);
                await databaseContext.SaveChangesAsync(true);
                return true;
            }

            return false;
        }

        public async Task<DeviceDataDto> UpdateDeviceData(string deviceId, DeviceDataDto deviceDataDto)
        {
            var deviceData = await databaseContext.DeviceData
                            .Where(d => d.DeviceId == deviceId)
                            .Where(d => d.Id == deviceDataDto.Id)
                            .FirstOrDefaultAsync(); if (deviceData == null) return null!;

            if (deviceData == null) return null!;

            if (deviceDataDto.Date != null) deviceData.Date = (DateTime)deviceDataDto.Date;
            if (deviceDataDto.Location != null) deviceData.Location = deviceDataDto.Location;

            databaseContext.DeviceData.Update(deviceData);
            await databaseContext.SaveChangesAsync();
            return deviceDataDto;
        }
    }
}

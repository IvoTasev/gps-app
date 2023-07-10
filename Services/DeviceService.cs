using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Models.Enums;
using gps_app.Persistency;
using gps_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gps_app.Services
{
    public class DeviceService : IDeviceService
    {

        DatabaseContext databaseContext;

        private static readonly float resultsPerPage = 3f;

        public DeviceService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<DeviceDto>> GetDevices(int page)
        {
            var deviceDtos = new List<DeviceDto>();

            var devices = await databaseContext.Devices
                .Skip((page - 1) * (int)resultsPerPage)
                .Take((int)resultsPerPage)
                .ToListAsync();

            devices.ForEach(async device =>
            {
                deviceDtos.Add(ToDto(device, true, page));
            });
            return deviceDtos;
        }

        public async Task<DeviceDto?> GetDeviceById(string id)
        {
            var device = await databaseContext.Devices.Include(d => d.DeviceData).FirstOrDefaultAsync(d => d.Id == id);
            
            if (device == null)
            {
                return null;
            }
            return ToDto(device);
        }

        public async Task<DeviceDto> SaveDevice(DeviceDto dto)
        {
            databaseContext.Devices.Add(FromDto(dto));
            await databaseContext.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteDevice(string deviceId)
        {
            var device = await databaseContext.Devices.SingleOrDefaultAsync(d => d.Id == deviceId);

            if (device != null)
            {
                databaseContext.Devices.Remove(device);
                await databaseContext.SaveChangesAsync(true);
                return true;
            }

            return false;
        }

        public async Task<DeviceDto> UpdateDevice(DeviceDto dto)
        {
            var device = await databaseContext.Devices.SingleOrDefaultAsync(d => d.Id == dto.Id);
            if (device == null) return null!;

            if (dto.DeviceType != null) device.DeviceType = (DeviceType)dto.DeviceType;

            databaseContext.Devices.Update(device);
            await databaseContext.SaveChangesAsync();
            return dto;
        }

        public Device FromDto(DeviceDto dto)
        {
            Device device = new Device { Id = dto.Id, DeviceType = (DeviceType)dto.DeviceType, DeviceData = new List<DeviceData> () { } };
            return device;
        }

        public DeviceDto ToDto(Device entity, bool addPagination = false, int page = 0)
        {
            DeviceDto dto = new DeviceDto
            {
                Id = entity.Id,
                DeviceType = entity.DeviceType
            };

            var latestDeviceData = databaseContext.DeviceData
                .Where(d => d.DeviceId == entity.Id)
                .OrderByDescending(d => d.Date)
                .FirstOrDefault();

            dto.latestDataDate = latestDeviceData.Date;
            dto.latestDataLocation = latestDeviceData.Location;

            if (addPagination)
            {
                var numberOfPages = Math.Ceiling(databaseContext.Devices.Count() / resultsPerPage);

                dto.CurrentPage = page;
                dto.Pages = (int)numberOfPages;
            }

            return dto;
        }

    }
}

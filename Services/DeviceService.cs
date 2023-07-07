using gps_app.Entities.Dtos;
using gps_app.Models;
using gps_app.Persistency;
using gps_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gps_app.Services
{
    public class DeviceService : IDeviceService
    {

        DatabaseContext databaseContext;

        public DeviceService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<DeviceDto>> GetDevices(int page)
        {
            var resultsPerPage = 2f;
            var numberOfPages = Math.Ceiling(databaseContext.Devices.Count() / resultsPerPage);

            var deviceDtos = new List<DeviceDto>();
            var devices = await databaseContext.Devices.Include(d => d.DeviceData)
                .Skip((page - 1) * (int) resultsPerPage)
                .Take((int) resultsPerPage)
                .ToListAsync();

            devices.ForEach(d =>
            {
                var dto = ToDto(d);
                dto.CurrentPage = page;
                dto.Pages = (int) numberOfPages;

                deviceDtos.Add(dto);
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

        public Device FromDto(DeviceDto dto)
        {
            Device device = new Device { Id = dto.Id, DeviceType = dto.DeviceType, DeviceData = dto.DeviceData };
            return device;
        }

        public DeviceDto ToDto(Device entity)
        {
            DeviceDto dto = new DeviceDto { Id = entity.Id, DeviceType = entity.DeviceType, DeviceData = entity.DeviceData };
            return dto;
        }
    }
}

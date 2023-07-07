using gps_app.Models;
using gps_app.Models.Enums;

namespace gps_app.Entities.Dtos
{
    public class DeviceDto : BaseDto
    {
        public required DeviceType DeviceType { get; set; }
        public ICollection<DeviceData> DeviceData { get; set;  } = new List<DeviceData>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
}

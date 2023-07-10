using gps_app.Models;
using gps_app.Models.Enums;

namespace gps_app.Entities.Dtos
{
    public class DeviceDto : BaseDto
    {
        public DeviceType? DeviceType { get; set; }
        public DateTime? latestDataDate { get; set;  } = null;
        public string? latestDataLocation { get; set;  } = null;

    }
}

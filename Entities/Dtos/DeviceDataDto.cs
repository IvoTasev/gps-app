using gps_app.Models;

namespace gps_app.Entities.Dtos
{
    public class DeviceDataDto : BaseDto
    {
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
        public required string DeviceId { get; set; }
    }
}

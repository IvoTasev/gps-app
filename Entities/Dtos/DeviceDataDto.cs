using gps_app.Models;

namespace gps_app.Entities.Dtos
{
    public class DeviceDataDto : BaseDto
    {
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public string? DeviceId { get; set; }
    }
}

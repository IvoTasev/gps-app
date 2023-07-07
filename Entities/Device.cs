using gps_app.Entities;
using gps_app.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace gps_app.Models
{
    public class Device : Entity
    {
        [Required]
        public required DeviceType DeviceType { get; set; }
        public ICollection<DeviceData> DeviceData { get; set; } = new List<DeviceData>();
    }
}

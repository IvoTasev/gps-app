using gps_app.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gps_app.Models
{
    public class DeviceData : Entity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override required string Id { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public required DateTime Date { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Location { get; set; }
        [Required]
        public required string DeviceId { get; set; }
        [Required]
        public Device Device { get; set; } = null!;
    }
}

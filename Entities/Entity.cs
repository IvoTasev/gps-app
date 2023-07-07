using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gps_app.Entities
{
    public abstract class Entity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual required string Id { get; set; }
    }
}

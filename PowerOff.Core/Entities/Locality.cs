using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core.Entities
{
    public class Locality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ModeratorId { get; set; }
        public User? Moderator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<Street>? Streets { get; set; } = new List<Street>();
        public virtual ICollection<PowerOffEvent>? Events  { get; set; } = new List<PowerOffEvent>();
    }
}

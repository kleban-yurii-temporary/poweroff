using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core.Entities
{
    public class Street
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public StreetType? Type { get; set; }
        public Locality? Locality { get; set; }
        public virtual ICollection<Building>? Buildings { get; set; } = new List<Building>();
        public virtual ICollection<PowerOffEvent>? Events { get; set; } = new List<PowerOffEvent>();
    }
}

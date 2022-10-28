using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core.Entities
{
    public class PowerOffEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public PowerOffEventStatus? Status { get; set; }
        public Locality? Locality { get; set; }
        public virtual ICollection<Street>? Streets { get; set; } = new List<Street>(); 
    }
}

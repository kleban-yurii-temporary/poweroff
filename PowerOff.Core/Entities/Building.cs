using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core.Entities
{
    public class Building
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string? Number { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsStreetTerminator { get; set; }
        public Street? Street { get; set; }
    }
}

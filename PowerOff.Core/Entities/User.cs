using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core.Entities
{
    public class User : IdentityUser
    {
        public Locality? ModerateLocality { get; set; }
    }
}

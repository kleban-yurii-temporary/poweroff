using Microsoft.EntityFrameworkCore;
using PowerOff.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Repositories
{
    public class AccessHelperRepository
    {
        private readonly PowerDbContext dbContext;

        public AccessHelperRepository(PowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> StreetAccessAllowed(int streetId, string username)
        {
            var street = await dbContext.Streets.Include(x=> x.Locality).FirstAsync(x=> x.Id == streetId);
            var user = await dbContext.Users.Include(x=> x.ModerateLocality).FirstAsync(x => x.UserName == username);
            return street.Locality.Id == user.ModerateLocality.Id;
        }
    }
}

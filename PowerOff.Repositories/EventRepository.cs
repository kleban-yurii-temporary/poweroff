using Microsoft.EntityFrameworkCore;
using PowerOff.Core;
using PowerOff.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Repositories
{
    public class EventRepository
    {
        private readonly PowerDbContext dbContext;
        public EventRepository(PowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PowerOffEvent>> GetListAsync(int localityId)
        {
            return await dbContext.PowerOffEvents.Include(x => x.Status).Where(x => x.Locality.Id == localityId).ToListAsync();
        }
    }
}

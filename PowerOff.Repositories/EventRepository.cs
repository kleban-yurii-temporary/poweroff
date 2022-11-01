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

        public async Task CreateAsync(int id, DateTime start, DateTime end, IEnumerable<int> streetIds)
        {
            var @event = new PowerOffEvent
            {
                EndTime = end,
                Locality = await dbContext.Localities.FindAsync(id),
                StartTime = start,
                Status = await dbContext.PowerOffEventStatuses.FindAsync(1)
            };

            foreach (var sid in streetIds)
            {
                @event.Streets.Add(await dbContext.Streets.FirstAsync(x => x.Id == sid));
            }

            await dbContext.PowerOffEvents.AddAsync(@event);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PowerOffEvent>> GetListAsync(int localityId)
        {
            return await dbContext.PowerOffEvents.Include(x => x.Status)
                .Include(x => x.Streets).ThenInclude(x => x.Type)
                .Where(x => x.Locality.Id == localityId).ToListAsync();
        }
    }
}

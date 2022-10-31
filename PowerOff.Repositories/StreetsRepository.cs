using Microsoft.EntityFrameworkCore;
using PowerOff.Core;
using PowerOff.Core.Entities;

namespace PowerOff.Repositories
{
    public class StreetsRepository
    {
        private readonly PowerDbContext dbContext;

        public StreetsRepository(PowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Street>> GetListAsync(int localityId)
        {
            return await dbContext.Streets
                .Include(x=> x.Buildings)
                .Include(x=> x.Type)
                .Where(x => x.Locality.Id == localityId).ToListAsync();
        }
    }
}
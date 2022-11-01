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
    public class LocalityRepository
    {
        private readonly PowerDbContext dbContext;

        public LocalityRepository(PowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Locality> GetLocalityAsync(int id)
        {
            return await dbContext.Localities.FirstAsync(x=> x.Id == id);
        }

        public async Task<Locality> GetLocalityByUserAsync(string? name)
        {
            return await dbContext.Localities.FirstAsync(x=> x.Moderator.UserName == name);
        }
    }
}

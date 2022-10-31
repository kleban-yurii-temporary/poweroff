using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerOff.Core;
using PowerOff.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Repositories
{
    public class SessionManager
    {
        private readonly PowerDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;       
        private ISession _session => httpContextAccessor.HttpContext.Session;

        public SessionManager(PowerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void SetLocalityId(int id)
        {
            _session.SetInt32("localityId", id);
        }

        public async Task<int> GetLocalityId()
        {
            if (!_session.Keys.Any(x => x == "localityId"))
                SetLocalityId((await dbContext.Localities.FirstAsync()).Id);

            return _session.GetInt32("localityId").Value;
        }       
    }
}

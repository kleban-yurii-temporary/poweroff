using Microsoft.EntityFrameworkCore;
using PowerOff.Core.Entities;
using System.Text.Json;

namespace PowerOff.Core.Initializer
{
    public class DataInitializer
    {
        private readonly PowerDbContext _dbContext;

        public DataInitializer(PowerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<KeyValuePair<string, int>>> InitAsync(string filepath)
        {

            var localityId = await initDefaultLocality();

            using (var file = File.OpenRead(filepath))
            {
                var data = await JsonSerializer.DeserializeAsync<IEnumerable<JsonStreetObj>>(file);
                await initStreetTypesAsync(data.Select(x => x.StreetType).Distinct().ToList());
                await initStreetsAsync(data, localityId);
            }

            await initPowerOffEventStatuses();

            return new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Населені пункти", _dbContext.Localities.Count()),
                new KeyValuePair<string, int>("Статуси подій", _dbContext.PowerOffEventStatuses.Count()),
                new KeyValuePair<string, int>("Будівлі", _dbContext.Buildings.Count()),
                     new KeyValuePair<string, int>("Вулиці", _dbContext.Streets.Count()),
                new KeyValuePair<string, int>("Тип вулиці", _dbContext.StreetTypes.Count())
            };
        }

        private async Task initPowerOffEventStatuses()
        {
            if (_dbContext.PowerOffEventStatuses.Any())
            {
                _dbContext.PowerOffEventStatuses.RemoveRange(await _dbContext.PowerOffEventStatuses.ToListAsync());
                await _dbContext.SaveChangesAsync();
            }
            await _dbContext.PowerOffEventStatuses.AddRangeAsync(
                new List<PowerOffEventStatus>
                {
                    new PowerOffEventStatus {Title = "Архівний"},
                    new PowerOffEventStatus {Title = "Відмінений"},
                    new PowerOffEventStatus {Title = "Активний"},
                    new PowerOffEventStatus {Title = "Завершений"}
                });
            await _dbContext.SaveChangesAsync();
        }

        private async Task initStreetsAsync(IEnumerable<JsonStreetObj> data, int localityId)
        {
            if (await _dbContext.Buildings.AnyAsync())
            {
                _dbContext.Buildings.RemoveRange(await _dbContext.Buildings.ToListAsync());
                await _dbContext.SaveChangesAsync();
                _dbContext.Streets.RemoveRange(await _dbContext.Streets.ToListAsync());
                await _dbContext.SaveChangesAsync();
            }

            foreach (var jo in data)
            {
                var street = new Street
                {
                    Locality = await _dbContext.Localities.FindAsync(localityId),
                    Title = jo.StreetTitle,
                    Type = await _dbContext.StreetTypes.FirstAsync(x => x.Title == jo.StreetType),
                };

                street.Buildings = jo.Buildings.Split(",").Select(x => new Building { Number = x }).ToList();

                await _dbContext.Streets.AddAsync(street);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task initStreetTypesAsync(List<string?> list)
        {
            if (await _dbContext.StreetTypes.AnyAsync())
            {
                _dbContext.StreetTypes.RemoveRange(await _dbContext.StreetTypes.ToListAsync());
                await _dbContext.SaveChangesAsync();
            }
            await _dbContext.StreetTypes.AddRangeAsync(list.Select(x => new StreetType { ShortTitle = x, Title = x }).ToList());
            await _dbContext.SaveChangesAsync();
        }

        private async Task<int> initDefaultLocality()
        {
            if (_dbContext.Localities.Any())
            {
                _dbContext.Localities.RemoveRange(await _dbContext.Localities.ToListAsync());
                await _dbContext.SaveChangesAsync();
            }

            var locality = await _dbContext.Localities.AddAsync(new Locality
            {
                Title = "м. Острог",
                Latitude = 0,
                Longitude = 0,
                Moderator = await _dbContext.Users.FirstAsync(x => x.Email == "admin@poweroff.app")
            });

            await _dbContext.SaveChangesAsync();

            return locality.Entity.Id;
        }

        private void initSreetTypes(PowerDbContext ctx, IEnumerable<string> types)
        {

        }
    }
}
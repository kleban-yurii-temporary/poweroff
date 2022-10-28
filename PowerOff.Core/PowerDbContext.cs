using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerOff.Core.Entities;
using System.Numerics;

namespace PowerOff.Core
{
    public class PowerDbContext : IdentityDbContext<User>
    {
        public PowerDbContext(DbContextOptions<PowerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(x => x.ModerateLocality)
                .WithOne(x => x.Moderator)
                .HasForeignKey<Locality>(c => c.ModeratorId); 

            builder.Entity<Street>()
                .HasMany(x => x.Events)
                .WithMany(x => x.Streets);
            
            builder.SeedUsers();

            base.OnModelCreating(builder);
        }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<StreetType> StreetTypes { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<PowerOffEvent> PowerOffEvents { get; set; }
        public DbSet<PowerOffEventStatus> PowerOffEventStatuses { get; set; }

    }
}
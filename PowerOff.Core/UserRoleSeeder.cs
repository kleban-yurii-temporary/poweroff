using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerOff.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOff.Core
{
    public static class UserRoleSeeder
    {
        public static void SeedUsers(this ModelBuilder builder)
        {
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string MODERATOR_ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = MODERATOR_ROLE_ID,
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                });

            string ADMIN_ID = Guid.NewGuid().ToString();
            string MODERATOR_ID = Guid.NewGuid().ToString();

            var admin = new User
            {
                Id = ADMIN_ID,
                UserName = "admin@poweroff.app",
                Email = "admin@poweroff.app",
                EmailConfirmed = true,
                NormalizedEmail = "admin@poweroff.app".ToUpper(),
                NormalizedUserName = "admin@poweroff.app".ToUpper()
            };
            var moderator = new User
            {
                Id = MODERATOR_ID,
                UserName = "moderator@poweroff.app",
                Email = "moderator@poweroff.app",
                EmailConfirmed = true,
                NormalizedEmail = "moderator@poweroff.app".ToUpper(),
                NormalizedUserName = "moderator@poweroff.app".ToUpper()
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            admin.PasswordHash = hasher.HashPassword(admin, "admin$Pass1");
            moderator.PasswordHash = hasher.HashPassword(moderator, "moderator$Pass1");

            builder.Entity<User>().HasData(admin, moderator);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = MODERATOR_ROLE_ID,
                    UserId = MODERATOR_ID
                });

        }
    }
}

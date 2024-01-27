using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Project.mvc.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define roles using constants to avoid magic strings
            const string adminRoleId = "2372ffbf-e16c-4e6e-947f-123fc0ec2d66";
            const string userRoleId = "a2d90cfa-fd3c-497c-9ad3-61313f89cc7e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            // Seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Define admin user using constants to avoid magic strings
            const string adminUserId = "594d733a-100c-46b9-9640-e6d349dc9d93";
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "owaiskhan",
                Email = "khanowais33@gmailcom",
                NormalizedEmail = "khanowais33@gmail.com".ToUpper(),
                NormalizedUserName = "owaiskhan".ToUpper(),
            };

            // Hash the password for the admin user
            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "Owais@1234");

            // Seed admin user
            builder.Entity<IdentityUser>().HasData(adminUser);

            // Assign roles to the admin user
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            };

            // Seed admin user roles
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}

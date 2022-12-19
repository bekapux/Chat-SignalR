using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Chat.Identity.Models.IdentityModels;

namespace Chat.Identity.Seeding
{
    public class UsersSeed : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Email = "admin",
                    NormalizedEmail = "Admin",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "asdASD123"),
                    EmailConfirmed = true,
                    IsActive = true
                },
                new ApplicationUser
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Email = "user",
                    NormalizedEmail = "USER",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    PasswordHash = hasher.HashPassword(null, "asdASD123"),
                    EmailConfirmed = true,
                    IsActive = true
                }
            );
        }
    }
}

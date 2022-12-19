using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Chat.Identity.Seeding
{
    public class UserRoleSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "11111111-1111-1111-1111-111111111111",
                    UserId = "11111111-1111-1111-1111-111111111111"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "22222222-2222-2222-2222-222222222222",
                    UserId = "22222222-2222-2222-2222-222222222222"
                }
            );
            builder.HasKey(x => new { x.RoleId, x.UserId });
        }
    }
}

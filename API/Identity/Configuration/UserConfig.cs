using Chat.Identity.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Identity.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.PersonalNumber).HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).HasMaxLength(40);
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}

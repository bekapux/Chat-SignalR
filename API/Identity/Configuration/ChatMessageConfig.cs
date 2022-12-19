using Chat.Identity.Models.IdentityModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Chat.Identity.Models.Domain;

namespace Chat.Identity.Configuration
{
    public class ChatMessageConfig : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.Property(x => x.UserId).HasMaxLength(450).IsRequired();
            builder.Property(x => x.MessageText).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.DateSent).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        }
    }
}

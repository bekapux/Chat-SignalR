using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Chat.Identity.Models.Domain;

namespace Chat.Identity.Configuration
{
    public class ChatGroupConfig : IEntityTypeConfiguration<ChatGroup>
    {
        public void Configure(EntityTypeBuilder<ChatGroup> builder)
        {
            builder.Property(x => x.DateCreated).IsRequired().HasDefaultValueSql("GetDate()");
        }
    }
}

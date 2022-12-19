using Chat.Identity.Models.IdentityModels;

namespace Chat.Identity.Models.Domain
{
    public class ChatGroup
    {
        public int Id { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}

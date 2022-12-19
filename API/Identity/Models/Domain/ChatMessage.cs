using Chat.Identity.Models.IdentityModels;

namespace Chat.Identity.Models.Domain
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime DateSent { get; set; }
        public ChatGroup ChatGroup { get; set; }
        public int ChatGroupId { get; set; }
    }
}

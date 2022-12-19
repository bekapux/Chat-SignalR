using Chat.Identity.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Chat.Identity.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public List<ChatGroup> Groups { get; set; }
    }
}

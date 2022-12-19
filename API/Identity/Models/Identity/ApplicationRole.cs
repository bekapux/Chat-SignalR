using Microsoft.AspNetCore.Identity;

namespace Chat.Identity.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsActive { get; set; }
    }
}

namespace Chat.Models.Dtos.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
        //public string RecaptchaToken { get; set; }
    }
}

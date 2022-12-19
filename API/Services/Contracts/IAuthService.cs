using Chat.Models.Dtos.Identity;

namespace Chat.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<AuthResponse> Register(RegistrationRequest request);
    }
}

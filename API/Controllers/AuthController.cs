using Chat.Models.Dtos.Identity;
using Chat.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructor

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Actions

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request) =>
            Ok(await _authService.Login(request));

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request) =>
            Ok(await _authService.Register(request));

        #endregion
    }
}

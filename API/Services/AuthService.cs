using Chat.Identity.Models.IdentityModels;
using Chat.Models.Dtos.Identity;
using Chat.Models.Utility;
using Chat.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Services
{
    public class AuthService : IAuthService
    {
        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IRecaptchaService _recaptchaService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager
            //IRecaptchaService recaptchaService
        )
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            //_recaptchaService = recaptchaService;
        }

        #endregion

        #region Methods

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new Exception($"Credentials for '{request.Email}' are not valid'.");
            if (user.IsActive == false) throw new Exception($"User with email '{request.Email}' is inactive");

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                request.Password,
                isPersistent: request.RememberMe ?? false,
                lockoutOnFailure: false
            );

            if (!result.Succeeded) throw new Exception($"Credentials for '{request.Email}' are not valid'.");

            var role = (await _userManager.GetRolesAsync(user))?.FirstOrDefault();

            var jwtSecurityToken = await GenerateToken(user, role ?? "");

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Role = role
            };

            return response;
        }

        public async Task<AuthResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
                IsActive = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
                throw new Exception($"ServiceEmail {request.Email} already exists.");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return await Login(new AuthRequest
                {
                    Email = request.Email,
                    Password = request.Password,
                    RememberMe = true
                });
            }
            throw new Exception($"{result.Errors}");
        }

        #endregion
        
        #region Private Methods

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, string roleName)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
                {
                    new Claim(CustomClaimTypes.Uid, user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(CustomClaimTypes.EmailConfirmed, user.EmailConfirmed.ToString()),
                    new Claim(ClaimTypes.Role, roleName)
                }
                .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public static class CustomClaimTypes
        {
            public const string Uid = "uid";
            public const string EmailConfirmed = "emailConfirmed";
        }

        #endregion
    }
}

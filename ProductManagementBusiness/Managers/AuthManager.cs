using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagementBusiness.Dtos.Auth;
using ProductManagementBusiness.Interfaces.Managers;
using ProductManagementBusiness.Interfaces.Services;
using ProductManagementBusiness.Services;
using ProductManagementData.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagementBusiness.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthManager(UserManager<User> userManager, IConfiguration configuration, IAuthService authService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authService = authService;
        }

        public async Task<AuthResult> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await _authService.GenerateTokens(user);
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            
            await _userManager.AddToRoleAsync(user, "User");

            return await _authService.GenerateTokens(user);
        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            return await _authService.RefreshToken(refreshToken);
        }

        public async Task LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

           
            var authService = _authService as AuthService;
            authService?.InvalidateToken(user.RefreshToken);

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            await _userManager.UpdateAsync(user);
        }
    }
}
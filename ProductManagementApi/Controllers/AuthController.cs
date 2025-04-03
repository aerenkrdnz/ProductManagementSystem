using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementBusiness.Dtos.Auth;
using ProductManagementBusiness.Interfaces.Managers;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authManager.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterDto dto)
        {
            var result = await _authManager.RegisterAsync(dto);
            return CreatedAtAction(nameof(Login), new { email = dto.Email }, result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _authManager.RefreshTokenAsync(refreshToken);
            return Ok(result);
        }
    }
}

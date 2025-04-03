using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementBusiness.Dtos.Auth;

namespace ProductManagementBusiness.Interfaces.Managers
{
    public interface IAuthManager
    {
        Task<AuthResult> LoginAsync(LoginDto dto);
        Task<AuthResult> RegisterAsync(RegisterDto dto);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
    }
}

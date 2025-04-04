using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementBusiness.Dtos.Auth;
using ProductManagementData.Entities;

namespace ProductManagementBusiness.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResult> GenerateTokens(User user);
        Task<AuthResult> RefreshToken(string refreshToken);
        bool IsTokenValid(string token);
        void InvalidateToken(string token);
    }
}

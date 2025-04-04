using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ProductManagementBusiness.Interfaces.Services;

namespace ProductManagementApi.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && !authService.IsTokenValid(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token is invalid or expired");
                return;
            }

            await _next(context);
        }
    }
}
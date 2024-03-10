using Restaurant.Application.Interfaces;
using System.Security.Claims;

namespace Restaurant.WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            var emailClaim = httpContextAccessor.HttpContext?.User?.FindFirst("Email");
            GetCurrentUserId = emailClaim?.Value ?? string.Empty;
        }

        public string? GetCurrentUserId { get; }
    }
}

using Restaurant.Application.Interfaces;
using System.Security.Claims;

namespace Restaurant.WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            var emailClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("Name");
            GetCurrentUserId = string.IsNullOrEmpty(emailClaim) ? "" : emailClaim;
        }

        public string? GetCurrentUserId { get; }
    }
}

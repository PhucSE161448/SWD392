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
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            GetCurrentUser = string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id);
        }

        public string? GetCurrentUserId { get; }
        public int GetCurrentUser { get; }
    }
}

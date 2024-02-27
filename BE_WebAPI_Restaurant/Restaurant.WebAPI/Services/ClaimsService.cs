using Restaurant.Application.Interfaces;
using System.Security.Claims;

namespace Restaurant.WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            GetCurrentUserId = string.IsNullOrEmpty(name) ? "" : name;
        }

        public string GetCurrentUserId { get; }
    }
}

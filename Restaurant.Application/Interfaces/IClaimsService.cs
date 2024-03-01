using System;
namespace Restaurant.Application.Interfaces
{
    public interface IClaimsService
    {
        public string GetCurrentUserId { get; }
    }
}

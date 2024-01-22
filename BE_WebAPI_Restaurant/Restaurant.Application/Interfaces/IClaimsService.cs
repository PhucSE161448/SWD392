using System;
namespace Restaurant.Application.Interfaces
{
    public interface IClaimsService
    {
        public int GetCurrentUserId { get; }
    }
}

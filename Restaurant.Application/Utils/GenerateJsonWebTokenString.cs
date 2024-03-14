using Restaurant.Application.Commons;
using Restaurant.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Application.Utils
{
    public static class GenerateJsonWebTokenString
    {
        public static string GenerateJsonWebToken(this Account user, AppConfiguration appSettingConfiguration, string secretKey, DateTime now)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email" ,user.Email),
                new Claim("Name", user.Username),
                new Claim("Role" , user.Role),
                new Claim(ClaimTypes.Role ,user.Role),
            };
            var token = new JwtSecurityToken(
                issuer: appSettingConfiguration.JWTSection.Issuer,
                audience: appSettingConfiguration.JWTSection.Audience,
                claims: claims,
                expires: now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

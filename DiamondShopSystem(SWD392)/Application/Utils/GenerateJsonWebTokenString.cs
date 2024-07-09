using Application.Commons;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Utils
{
    public static class GenerateJsonWebTokenString
    {
        public static string GenerateJsonWebToken(this Account user, AppConfiguration appSettingConfiguration, string secretKey, DateTime now)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("role", user.Role.Name),
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

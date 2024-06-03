using Application.Commons;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Utils
{
    public static class GenerateJsonWebTokenString
    {



        public static string GenerateJsonWebToken(this Account account, AppConfiguration appSettingConfiguration, string secretKey, DateTime now)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var claims = new[]
            //{
            //    new Claim("Id", user.Id.ToString()),
            //    new Claim("Email" ,user.Email),
            //    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            //};
            //var token = new JwtSecurityToken(
            //    issuer: appSettingConfiguration.JWTSection.Issuer,
            //    audience: appSettingConfiguration.JWTSection.Audience,
            //    claims: claims,
            //    expires: now.AddMinutes(15),
            //    signingCredentials: credentials);


            //return new JwtSecurityTokenHandler().WriteToken(token);
            // Tạo một khóa ngẫu nhiên 32 byte
            byte[] keyBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim("Id", account.Id.ToString()),
            new Claim("Email", account.Email),
            new Claim(ClaimTypes.Role, account.RoleId.ToString()),
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

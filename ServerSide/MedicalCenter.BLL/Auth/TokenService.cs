using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MedicalCenter.DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MedicalCenter.BLL.Auth
{
    public class TokenService: ITokenService
    {
        private readonly JwtSettings _settings;
        public TokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task<(string accessToken, string jti, DateTime expiresAt)> CreateJwtTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(ClaimTypes.Name, user.UserID.ToString()),
              new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
              new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var serialized = new JwtSecurityTokenHandler().WriteToken(token);
            var jti = token.Id ?? Guid.NewGuid().ToString();

            return Task.FromResult((serialized, jti, expires));
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }


    }
}

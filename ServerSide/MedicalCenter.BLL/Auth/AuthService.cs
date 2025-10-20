using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.BLL.Helpers;
using MedicalCenter.DAL.Context;
using MedicalCenter.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.BLL.Auth
{
    public class AuthService: IAuthService
    {
        private readonly MedicalCenterDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(MedicalCenterDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<(bool succeeded, string? message, string? accessToken, string? refreshToken, DateTime? accessTokenExpiry)> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return (false, "Invalid email or password", null, null, null);

            if (!user.IsActive)
                return (false, "Your account has been deactivated. Please contact support.", null, null, null);

            bool passwordOk = HashedPass.VerifyPassword(password, user.PasswordHash);
            if (!passwordOk)
                return (false, "Invalid email or password", null, null, null);

            var (accessToken, jti, expiresAt) = await _tokenService.CreateJwtTokenAsync(user);
            var refresh = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refresh,
                JwtId = jti,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(14),
                UserId = user.UserID,
                Used = false,
                Revoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return (true, "Login successful", accessToken, refresh, expiresAt);
        }
        public async Task<(bool succeeded, string? accessToken, string? refreshToken)> RefreshTokenAsync(string token, string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var jti = jwtToken.Id;

            var storedRefresh = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedRefresh == null) return (false, null, null);
            if (storedRefresh.ExpiryDate < DateTime.UtcNow) return (false, null, null);
            if ((storedRefresh.Used ?? false) || (storedRefresh.Revoked ?? false))
                return (false, null, null);
            if (storedRefresh.JwtId != jti) return (false, null, null);

            storedRefresh.Used = true;
            storedRefresh.Revoked = true; 

            var (newAccess, newJti, expiresAt) = await _tokenService.CreateJwtTokenAsync(storedRefresh.User!);
            var newRefresh = _tokenService.GenerateRefreshToken();

            var newRefreshEntity = new RefreshToken
            {
                Token = newRefresh,
                JwtId = newJti,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(14),
                UserId = storedRefresh.UserId
            };

            _context.RefreshTokens.Add(newRefreshEntity);
            await _context.SaveChangesAsync();

            return (true, newAccess, newRefresh);
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var stored = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (stored == null) return false;
            stored.Revoked = true;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}

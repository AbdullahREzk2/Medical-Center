using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MedicalCenter
{
    public static class SessionManager
    {
        public static string? AccessToken { get; set; }
        public static string? RefreshToken { get; set; }
        public static DateTime AccessTokenExpiresAt { get; set; }
        public static string? Role { get; set; }
        public static int UserId { get; set; }

        public static bool IsAccessTokenValid =>
            !string.IsNullOrEmpty(AccessToken) && DateTime.UtcNow < AccessTokenExpiresAt;

        public static void ExtractRoleFromToken()
        {
            if (string.IsNullOrEmpty(AccessToken)) return;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(AccessToken);
                // Try the common JWT claim for role
                Role = token.Claims.FirstOrDefault(c =>
                    c.Type == "role" ||
                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                )?.Value;
            }
            catch
            {
                Role = null;
            }
        }

        public static void ExtractIdFromToken()
        {
            if (string.IsNullOrEmpty(AccessToken)) return;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(AccessToken);
                var userIdClaim = token.Claims.FirstOrDefault(c =>
                c.Type == "sub" ||
                 c.Type.EndsWith("/name", StringComparison.OrdinalIgnoreCase)
                 )?.Value;
                if (int.TryParse(userIdClaim, out int id))
                    UserId = id;
            }
            catch
            {
                UserId = 0;
            }
        }

        public static void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
            Role = null;
            UserId = 0;
            AccessTokenExpiresAt = DateTime.MinValue;
        }
    }
}
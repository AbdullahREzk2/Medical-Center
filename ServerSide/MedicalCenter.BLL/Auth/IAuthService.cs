using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.BLL.Auth
{
    public interface IAuthService
    {
        Task<(bool succeeded, string? message, string? accessToken, string? refreshToken, DateTime? accessTokenExpiry)> LoginAsync(string email, string password);
        Task<(bool succeeded, string? accessToken, string? refreshToken)> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }

}

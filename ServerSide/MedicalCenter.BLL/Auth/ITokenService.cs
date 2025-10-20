using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Entities;

namespace MedicalCenter.BLL.Auth
{
    public interface ITokenService
    {
        Task<(string accessToken, string jti, DateTime expiresAt)> CreateJwtTokenAsync(User user);
        string GenerateRefreshToken();
    }


}

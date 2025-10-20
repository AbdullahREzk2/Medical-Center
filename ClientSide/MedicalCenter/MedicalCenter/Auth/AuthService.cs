using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace MedicalCenter.Auth
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://hopemedicalcenter.runasp.net/api/Auth/")
            };
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            Logout();

            var res = await _httpClient.PostAsJsonAsync("Login", new { Email = email, Password = password });
            if (!res.IsSuccessStatusCode) return false;

            var payload = await res.Content.ReadFromJsonAsync<JsonElement>();
            SessionManager.AccessToken = payload.GetProperty("accessToken").GetString();
            SessionManager.RefreshToken = payload.GetProperty("refreshToken").GetString();

            if (payload.TryGetProperty("expiresAt", out var exp))
                SessionManager.AccessTokenExpiresAt = exp.GetDateTime();
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(SessionManager.AccessToken);
                SessionManager.AccessTokenExpiresAt = jwt.ValidTo;
            }

            SessionManager.ExtractRoleFromToken();
            SessionManager.ExtractIdFromToken();

            return true;
        }

        public async Task<bool> RefreshAsync()
        {
            var res = await _httpClient.PostAsJsonAsync("api/Auth/Refresh", new { AccessToken = SessionManager.AccessToken, RefreshToken = SessionManager.RefreshToken });
            if (!res.IsSuccessStatusCode) return false;

            var payload = await res.Content.ReadFromJsonAsync<JsonElement>();
            SessionManager.AccessToken = payload.GetProperty("accessToken").GetString();
            SessionManager.RefreshToken = payload.GetProperty("refreshToken").GetString();

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(SessionManager.AccessToken);
            SessionManager.AccessTokenExpiresAt = jwt.ValidTo;

            return true;
        }

        public void Logout()
        {
            SessionManager.Clear();

            _httpClient.DefaultRequestHeaders.Authorization = null;
            _httpClient.DefaultRequestHeaders.Clear();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace MedicalCenter
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly HttpClient _refreshClient;
        private readonly string _refreshEndpoint = "https://hopemedicalcenter.runasp.net/api/Auth/Refresh";

        public AuthenticatedHttpClientHandler(HttpMessageHandler innerHandler, HttpClient refreshClient)
            : base(innerHandler)
        {
            _refreshClient = refreshClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (SessionManager.IsAccessTokenValid)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AccessToken);

            var response = await base.SendAsync(request, cancellationToken);

            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                bool refreshed = await TryRefreshTokenAsync();
                if (!refreshed) return response;

                response.Dispose();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }

        private async Task<bool> TryRefreshTokenAsync()
        {
            if (string.IsNullOrEmpty(SessionManager.RefreshToken) || string.IsNullOrEmpty(SessionManager.AccessToken))
                return false;

            var dto = new { AccessToken = SessionManager.AccessToken, RefreshToken = SessionManager.RefreshToken };
            var res = await _refreshClient.PostAsJsonAsync(_refreshEndpoint, dto);
            if (!res.IsSuccessStatusCode) return false;

            var json = await res.Content.ReadFromJsonAsync<JsonElement>();
            if (json.TryGetProperty("accessToken", out var at) && json.TryGetProperty("refreshToken", out var rt))
            {
                SessionManager.AccessToken = at.GetString();
                SessionManager.RefreshToken = rt.GetString();

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(SessionManager.AccessToken);
                var exp = jwt.ValidTo;
                SessionManager.AccessTokenExpiresAt = exp;

                return true;
            }
            return false;
        }


    }
}

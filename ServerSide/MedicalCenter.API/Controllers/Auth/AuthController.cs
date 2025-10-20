using MedicalCenter.BLL.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;


        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var (succeeded, message, accessToken, refreshToken, expiresAt) =
                await _authService.LoginAsync(dto.Email, dto.Password);

            if (!succeeded)
                return Unauthorized(new { message });

            return Ok(new
            {
                accessToken,
                refreshToken,
                expiresAt
            });
        }
        #endregion

        #region Refresh token
        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
        {
            var (succeeded, accessToken, refreshToken) = await _authService.RefreshTokenAsync(dto.AccessToken, dto.RefreshToken);

            if (!succeeded)
                return Unauthorized("Invalid tokens");

            return Ok(new 
            {
                accessToken, refreshToken
            });
        }

        #endregion

        #region Logout
        [HttpPost("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            var ok = await _authService.RevokeRefreshTokenAsync(dto.RefreshToken);
            if (!ok)
                return NotFound();

            return Ok();
        }
        #endregion

    }
}

using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;
using MovieHub.API.Utilities;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(
            LoginRequestDto loginRequestDto
        )
        {
            return Ok(
                ResponseHelper.Success(
                    data: await _authService.LoginAsync(loginRequestDto)
                )
            );
        }
    }
}

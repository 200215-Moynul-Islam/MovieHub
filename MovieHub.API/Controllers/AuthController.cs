using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(
            IUserService userService,
            IAuthService authService
        )
        {
            _userService = userService;
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> RegisterUserAsync(
            UserCreateDto userCreateDto
        )
        {
            return Created(
                String.Empty,
                await _userService.RegisterUserAsync(userCreateDto)
            );
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(
            LoginRequestDto loginRequestDto
        )
        {
            return Ok(await _authService.LoginAsync(loginRequestDto));
        }
    }
}

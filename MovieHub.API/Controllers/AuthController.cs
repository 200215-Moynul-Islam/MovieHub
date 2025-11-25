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

        public AuthController(IUserService userService)
        {
            _userService = userService;
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
    }
}

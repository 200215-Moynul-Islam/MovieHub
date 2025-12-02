using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;
using MovieHub.API.Utilities;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> RegisterUserAsync(
            UserCreateDto userCreateDto
        )
        {
            return StatusCode(
                StatusCodes.Status201Created,
                ResponseHelper.Success(
                    data: await _userService.RegisterUserAsync(userCreateDto)
                )
            );
        }
    }
}

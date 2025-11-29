using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [Authorize(
        Roles = $"{DefaultConstants.Role.AdminRoleName}, {DefaultConstants.Role.MangerRoleName}"
    )]
    [ApiController]
    [Route("api/showtimes")]
    public class ShowTimeController : ControllerBase
    {
        private readonly IMovieHallShowTimeService _movieHallShowTimeService;
        private readonly IShowTimeService _showTimeService;
        private readonly ICustomAuthorizationService _customAuthorizationService;

        public ShowTimeController(
            IMovieHallShowTimeService movieHallShowTimeService,
            IShowTimeService showTimeService,
            ICustomAuthorizationService customAuthorizationService
        )
        {
            _movieHallShowTimeService = movieHallShowTimeService;
            _showTimeService = showTimeService;
            _customAuthorizationService = customAuthorizationService;
        }

        // POST: api/showtimes
        [HttpPost]
        public async Task<ActionResult<ShowTimeReadDto>> CreateShowTimeAsync(
            [FromBody] ShowTimeCreateDto showTimeCreateDto
        )
        {
            if (
                !User.IsInRole(DefaultConstants.Role.AdminRoleName)
                && !await _customAuthorizationService.IsManagerByHallIdAsync(
                    showTimeCreateDto.HallId!.Value,
                    Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                )
            )
            {
                // Try to return an error message like $"You are not authorized to access the hall with Id {showTimeCreateDto.HallId}.". But Forbid method does not allow that.
                return Forbid();
            }

            return Created(
                String.Empty,
                await _movieHallShowTimeService.CreateShowTimeAsync(
                    showTimeCreateDto
                )
            );
        }

        // GET: api/showtimes/{id:int}
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<
            ActionResult<ShowTimeDetailsReadDto>
        > GetShowTimeDetailsByIdAsync([FromRoute] int id)
        {
            return Ok(await _showTimeService.GetShowTimeDetailsByIdAsync(id));
        }

        // PATCH: api/showtimes/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<
            ActionResult<ShowTimeReadDto>
        > UpdateShowTimeByIdAsync(
            [FromRoute] int id,
            [FromBody] ShowTimeUpdateDto showTimeUpdateDto
        )
        {
            if (
                !User.IsInRole(DefaultConstants.Role.AdminRoleName)
                && !await _customAuthorizationService.IsManagerByShowTimeIdAsync(
                    id,
                    Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                )
            )
            {
                // Try to return an error message like $"You are not authorized to access the showtime with Id {id}." But Forbid method does not allow that.
                return Forbid();
            }
            if (
                !User.IsInRole(DefaultConstants.Role.AdminRoleName)
                && showTimeUpdateDto.HallId is not null
                && !await _customAuthorizationService.IsManagerByHallIdAsync(
                    showTimeUpdateDto.HallId!.Value,
                    Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                )
            )
            {
                // Try to return an error message like $"You are not authorized to access the hall with Id {showTimeCreateDto.HallId}.". But Forbid method does not allow that.
                return Forbid();
            }

            return Ok(
                await _movieHallShowTimeService.UpdateShowTimeByIdAsync(
                    id,
                    showTimeUpdateDto
                )
            );
        }
    }
}

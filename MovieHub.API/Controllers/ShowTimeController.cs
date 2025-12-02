using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;
using MovieHub.API.Utilities;

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
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ResponseHelper.Fail()
                );
            }

            return StatusCode(
                StatusCodes.Status201Created,
                ResponseHelper.Success(
                    data: await _movieHallShowTimeService.CreateShowTimeAsync(
                        showTimeCreateDto
                    )
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
            return Ok(
                ResponseHelper.Success(
                    data: await _showTimeService.GetShowTimeDetailsByIdAsync(id)
                )
            );
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
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ResponseHelper.Fail()
                );
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
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ResponseHelper.Fail()
                );
            }

            return Ok(
                ResponseHelper.Success(
                    data: await _movieHallShowTimeService.UpdateShowTimeByIdAsync(
                        id,
                        showTimeUpdateDto
                    )
                )
            );
        }
    }
}

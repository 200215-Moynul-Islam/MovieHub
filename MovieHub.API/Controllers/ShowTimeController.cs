using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/showtimes")]
    public class ShowTimeController : ControllerBase
    {
        private readonly IMovieHallShowTimeService _movieHallShowTimeService;
        private readonly IShowTimeService _showTimeService;

        public ShowTimeController(
            IMovieHallShowTimeService movieHallShowTimeService,
            IShowTimeService showTimeService
        )
        {
            _movieHallShowTimeService = movieHallShowTimeService;
            _showTimeService = showTimeService;
        }

        // POST: api/showtimes
        [HttpPost]
        public async Task<ActionResult<ShowTimeReadDto>> CreateShowTimeAsync(
            [FromBody] ShowTimeCreateDto showTimeCreateDto
        )
        {
            return Created(
                String.Empty,
                await _movieHallShowTimeService.CreateShowTimeAsync(
                    showTimeCreateDto
                )
            );
        }

        // GET: api/showtimes/{id:int}
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
            return Ok(
                await _movieHallShowTimeService.UpdateShowTimeByIdAsync(
                    id,
                    showTimeUpdateDto
                )
            );
        }
    }
}

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
        public async Task<ActionResult<int>> CreateShowTimeAsync(
            ShowTimeCreateDto showTimeCreateDto
        )
        {
            var showTimeId =
                await _movieHallShowTimeService.CreateShowTimeAsync(
                    showTimeCreateDto
                );
            return Ok(showTimeId);
        }

        // GET: api/showtimes/{id:int}
        [HttpGet("{id:int}")]
        public async Task<
            ActionResult<ShowTimeDetailsReadDto>
        > GetShowTimeDetailsByIdAsync(int id)
        {
            return Ok(await _showTimeService.GetShowTimeDetailsByIdAsync(id));
        }

        // PATCH: api/showtimes/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateShowTimeByIdAsync(
            int id,
            ShowTimeUpdateDto showTimeUpdateDto
        )
        {
            await _movieHallShowTimeService.UpdateShowTimeByIdAsync(
                id,
                showTimeUpdateDto
            );
            return Ok();
        }
    }
}

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

        public ShowTimeController(
            IMovieHallShowTimeService movieHallShowTimeService
        )
        {
            _movieHallShowTimeService = movieHallShowTimeService;
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
    }
}

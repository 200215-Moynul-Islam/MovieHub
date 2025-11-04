using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMovieShowTimeService _movieShowTimeService;

        public MovieController(
            IMovieService movieService,
            IMovieShowTimeService movieShowTimeService
        )
        {
            _movieService = movieService;
            _movieShowTimeService = movieShowTimeService;
        }

        // POST api/movies
        [HttpPost]
        public async Task<ActionResult<int>> CreateMovieAsync(
            MovieCreateDto movieCreateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieId = await _movieService.CreateMovieAsync(movieCreateDto);
            return Created(String.Empty, movieId);
        }

        // GET: api/movies/{id:int}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieReadDto>> GetMovieByIdAsync(int id)
        {
            return Ok(await _movieService.GetMovieByIdAsync(id));
        }

        // GET: api/movies
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<MovieReadDto>>
        > GetAllMoviesAsync(
            int offset = DefaultConstants.Offset,
            int limit = DefaultConstants.Limit
        )
        {
            return Ok(await _movieService.GetAllMoviesAsync(offset, limit));
        }

        // PATCH: api/movies/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateMovieByIdAsync(
            int id,
            [FromBody] MovieUpdateDto movieUpdateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _movieService.UpdateMovieByIdAsync(id, movieUpdateDto);
            return Ok();
        }

        // DELETE: api/movies/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovieByIdAsync(int id)
        {
            await _movieShowTimeService.DeactivateMovieByIdAsync(id);
            return Ok();
        }
    }
}

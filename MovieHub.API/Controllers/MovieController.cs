using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
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
    }
}

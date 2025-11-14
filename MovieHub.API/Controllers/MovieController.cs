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
        private readonly IBranchMovieService _branchMovieService;

        public MovieController(
            IMovieService movieService,
            IMovieShowTimeService movieShowTimeService,
            IBranchMovieService branchMovieService
        )
        {
            _movieService = movieService;
            _movieShowTimeService = movieShowTimeService;
            _branchMovieService = branchMovieService;
        }

        // POST api/movies
        [HttpPost]
        public async Task<ActionResult<int>> CreateMovieAsync(
            MovieCreateDto movieCreateDto
        )
        {
            var movieId = await _movieService.CreateMovieAsync(movieCreateDto);
            return Created(String.Empty, movieId);
        }

        // GET: api/movies/{id:int}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieReadDto>> GetMovieByIdAsync(int id)
        {
            return Ok(await _movieService.GetMovieByIdAsync(id));
        }

        // GET: api/movies/{id:int}/upcoming-showtimes?branchId=123
        [HttpGet("{id:int}/upcoming-showtimes")]
        public async Task<
            ActionResult<MovieWithShowTimesReadDto>
        > GetMovieWithUpcomingShowTimesByIdForBranchAsync(
            int id,
            [FromQuery] int branchId
        )
        {
            return Ok(
                await _branchMovieService.GetMovieWithUpcomingShowTimesByIdForBranchAsync(
                    id,
                    branchId
                )
            );
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

        //GET: api/movies/upcoming?branchId=123
        [HttpGet("upcoming")]
        public async Task<
            ActionResult<IEnumerable<MovieReadDto>>
        > GetScheduledMoviesByBranchIdAsync([FromQuery] int branchId)
        {
            return Ok(
                await _branchMovieService.GetScheduledMoviesByBranchIdAsync(
                    branchId
                )
            );
        }

        // PATCH: api/movies/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateMovieByIdAsync(
            int id,
            [FromBody] MovieUpdateDto movieUpdateDto
        )
        {
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

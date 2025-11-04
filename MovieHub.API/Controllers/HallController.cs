using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/halls")]
    public class HallController : ControllerBase
    {
        private readonly IBranchHallSeatService _branchHallSeatService;

        public HallController(
            IBranchHallSeatService branchHallSeatService
        )
        {
            _branchHallSeatService = branchHallSeatService;
        }

        // POST api/halls
        [HttpPost]
        public async Task<ActionResult<int>> CreateHallAsync(
            HallCreateDto hallCreateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hallId = await _branchHallSeatService.CreateHallWithSeatsAsync(
                hallCreateDto
            );
            return Created(String.Empty, hallId);
        }
    }
}

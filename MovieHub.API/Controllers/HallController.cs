using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/halls")]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;
        private readonly IBranchHallSeatService _branchHallSeatService;

        public HallController(
            IHallService hallService,
            IBranchHallSeatService branchHallSeatService
        )
        {
            _hallService = hallService;
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

        //GET: api/halls/{id:int}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HallReadDto>> GetHallByIdAsync(int id)
        {
            return Ok(await _hallService.GetHallByIdAsync(id));
        }

        //PATCH: api/halls/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateHallByIdAsync(
            int id,
            HallUpdateDto hallUpdateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _hallService.UpdateHallByIdAsync(id, hallUpdateDto);
            return Ok();
        }
    }
}

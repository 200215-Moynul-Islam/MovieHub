using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
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
        private readonly IHallShowTimeService _hallShowTimeService;
        private readonly IBranchHallService _branchHallService;

        public HallController(
            IHallService hallService,
            IBranchHallSeatService branchHallSeatService,
            IHallShowTimeService hallShowTimeService,
            IBranchHallService branchHallService
        )
        {
            _hallService = hallService;
            _branchHallSeatService = branchHallSeatService;
            _hallShowTimeService = hallShowTimeService;
            _branchHallService = branchHallService;
        }

        // POST api/halls
        [HttpPost]
        public async Task<ActionResult<HallReadDto>> CreateHallAsync(
            [FromBody] HallCreateDto hallCreateDto
        )
        {
            return Created(
                String.Empty,
                await _branchHallSeatService.CreateHallWithSeatsAsync(
                    hallCreateDto
                )
            );
        }

        //GET: api/halls/{id:int}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HallReadDto>> GetHallByIdAsync(
            [FromRoute] int id
        )
        {
            return Ok(await _hallService.GetHallByIdAsync(id));
        }

        //PATCH: api/halls/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateHallByIdAsync(
            [FromRoute] int id,
            [FromBody] HallUpdateDto hallUpdateDto
        )
        {
            await _hallService.UpdateHallByIdAsync(id, hallUpdateDto);
            return Ok();
        }

        //GET: api/halls/?branchId={branchId}&offset={offset}&limit={limit}
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<HallReadDto>>
        > GetHallsByBranchIdAsync(
            [FromQuery] int branchId,
            [FromQuery] int offset = DefaultConstants.Offset,
            [FromQuery] int limit = DefaultConstants.Limit
        )
        {
            return Ok(
                await _branchHallService.GetHallsByBranchIdAsync(
                    branchId,
                    offset,
                    limit
                )
            );
        }

        //DELETE: api/halls/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHallByIdAsync([FromRoute] int id)
        {
            await _hallShowTimeService.DeactivateHallByIdAsync(id);
            return Ok();
        }
    }
}

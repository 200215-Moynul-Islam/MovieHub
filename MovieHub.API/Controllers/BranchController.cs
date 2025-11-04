using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/branches")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IBranchHallService _branchHallService;

        public BranchController(
            IBranchService branchService,
            IBranchHallService branchHallService
        )
        {
            _branchService = branchService;
            _branchHallService = branchHallService;
        }

        // POST: api/branches
        [HttpPost]
        public async Task<ActionResult<BranchReadDto>> CreateBranchAsync(
            [FromBody] BranchCreateDto branchCreateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBranch = await _branchService.CreateBranchAsync(
                branchCreateDto
            );
            return CreatedAtRoute(
                "GetBranchById",
                new { id = createdBranch.Id },
                createdBranch
            );
        }

        // GET: api/branches/{id:int}
        [HttpGet("{id:int}", Name = "GetBranchById")]
        public async Task<ActionResult<BranchReadDto>> GetBranchByIdAsync(
            int id
        )
        {
            return Ok(await _branchService.GetBranchByIdAsync(id));
        }

        // DELETE: api/branches/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBranchByIdAsync(int id)
        {
            await _branchHallService.DeactivateBranchWithHallsByBranchIdAsync(
                id
            );
            return Ok();
        }

        // PATCH: api/branches/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateBranchByIdAsync(
            int id,
            [FromBody] BranchUpdateDto branchUpdateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _branchService.UpdateBranchByIdAsync(id, branchUpdateDto);
            return Ok();
        }

        // GET: api/branches
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<BranchReadDto>>
        > GetAllBranchesAsync(
            int offset = DefaultConstants.Offset,
            int limit = DefaultConstants.Limit
        )
        {
            return Ok(await _branchService.GetAllBranchesAsync(offset, limit));
        }

        // PATCH: api/branches/{id:int}
        [HttpPatch("{id:int}/reset-manager")]
        public async Task<IActionResult> ResetBranchManagerByIdAsync(int id)
        {
            await _branchService.ResetBranchManagerByIdAsync(id);
            return Ok();
        }
    }
}

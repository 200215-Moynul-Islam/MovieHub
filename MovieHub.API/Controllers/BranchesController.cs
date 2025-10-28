using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/branches")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
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

            var createdBranch = await _branchService.CreateBranchAsync(branchCreateDto);
            return CreatedAtRoute("GetBranchById", new { id = createdBranch.Id }, createdBranch);
        }

        // GET: api/branches/{id:int}
        [HttpGet("{id:int}", Name = "GetBranchById")]
        public async Task<ActionResult<BranchReadDto>> GetBranchByIdAsync(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }

        // DELETE: api/branches/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBranchByIdAsync(int id)
        {
            await _branchService.DeactivateBranchByIdAsync(id);
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
    }
}

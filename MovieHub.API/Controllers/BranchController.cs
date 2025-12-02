using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;
using MovieHub.API.Utilities;

namespace MovieHub.API.Controllers
{
    [Authorize(Roles = DefaultConstants.Role.AdminRoleName)]
    [ApiController]
    [Route("api/branches")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IBranchHallService _branchHallService;
        private readonly IUserBranchService _userBranchService;

        public BranchController(
            IBranchService branchService,
            IBranchHallService branchHallService,
            IUserBranchService userBranchService
        )
        {
            _branchService = branchService;
            _branchHallService = branchHallService;
            _userBranchService = userBranchService;
        }

        // POST: api/branches
        [HttpPost]
        public async Task<ActionResult<BranchReadDto>> CreateBranchAsync(
            [FromBody] BranchCreateDto branchCreateDto
        )
        {
            var createdBranch = await _userBranchService.CreateBranchAsync(
                branchCreateDto
            );
            return StatusCode(
                StatusCodes.Status201Created,
                ResponseHelper.Success(data: createdBranch)
            );
        }

        // GET: api/branches/{id:int}
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetBranchById")]
        public async Task<ActionResult<BranchReadDto>> GetBranchByIdAsync(
            [FromRoute] int id
        )
        {
            return Ok(
                ResponseHelper.Success(
                    data: await _branchService.GetBranchByIdAsync(id)
                )
            );
        }

        // DELETE: api/branches/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBranchByIdAsync(
            [FromRoute] int id
        )
        {
            await _branchHallService.DeactivateBranchWithHallsByBranchIdAsync(
                id
            );
            return Ok(ResponseHelper.Success());
        }

        // PATCH: api/branches/{id:int}
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<BranchReadDto>> UpdateBranchByIdAsync(
            [FromRoute] int id,
            [FromBody] BranchUpdateDto branchUpdateDto
        )
        {
            return Ok(
                ResponseHelper.Success(
                    data: await _userBranchService.UpdateBranchByIdAsync(
                        id,
                        branchUpdateDto
                    )
                )
            );
        }

        // GET: api/branches?offset={offset}&limit={limit}
        [AllowAnonymous]
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<BranchReadDto>>
        > GetAllBranchesAsync(
            [FromQuery] int offset = DefaultConstants.Offset,
            [FromQuery] int limit = DefaultConstants.Limit
        )
        {
            return Ok(
                ResponseHelper.Success(
                    data: await _branchService.GetAllBranchesAsync(
                        offset,
                        limit
                    )
                )
            );
        }

        // PATCH: api/branches/{id:int}/reset-manager
        [HttpPatch("{id:int}/reset-manager")]
        public async Task<
            ActionResult<IActionResult>
        > ResetBranchManagerByIdAsync([FromRoute] int id)
        {
            await _branchService.ResetBranchManagerByIdAsync(id);
            return Ok(ResponseHelper.Success());
        }
    }
}

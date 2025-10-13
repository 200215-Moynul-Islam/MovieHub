using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.Repositories;

namespace MovieHub.API.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(MovieHubDbContext dbContext) : base(dbContext)
        {
        }

        #region Existence Checks
        public async Task<bool> BranchNameExistsAsync(string branchName)
        {
            branchName = branchName.Trim().ToLower();
            return await _dbContext.Branches
                .AnyAsync(b => b.Name.Trim().ToLower() == branchName);
        }

        public async Task<bool> IsManagerAssignedAsync(Guid managerId)
        {
            return await _dbContext.Branches
                .AnyAsync(b => b.ManagerId == managerId);
        }
        #endregion
    }
}

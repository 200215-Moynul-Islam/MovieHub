using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.DTOs;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly MovieHubDbContext _dbContext;
        private readonly IMapper _mapper;
        public BranchRepository(MovieHubDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region CRUD Operations
        #region Create
        public async Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto)
        {
            var branch = _mapper.Map<Branch>(branchCreateDto);
            await _dbContext.Branches.AddAsync(branch);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<BranchReadDto>(branch);
        }
        #endregion

        #region Read
        public async Task<BranchReadDto?> GetBranchByIdAsync(int id)
        {
            var branch = await _dbContext.Branches
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
           
            if (branch == null)
            {
                return null;
            }
            
            return _mapper.Map<BranchReadDto>(branch);
        }
        #endregion
        #endregion

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

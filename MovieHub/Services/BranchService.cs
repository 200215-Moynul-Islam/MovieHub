using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto)
        {
            // TODO: Validate if manager exists in User module when it's done
            var branchNameExists = _branchRepository.NameExistsCaseInsensitiveAsync(branchCreateDto.Name);
            var isManagerAssigned = branchCreateDto.ManagerId == null 
                ? Task.FromResult(false) 
                : _branchRepository.IsManagerAssignedAsync(branchCreateDto.ManagerId.Value);
            // Wait for both tasks to be completed
            await Task.WhenAll(branchNameExists, isManagerAssigned);
            
            if (branchNameExists.Result)
            {
                throw new Exception($"Branch with name '{branchCreateDto.Name}' already exists."); // Use specific exception and avoid magic exception message.
            }

            if (isManagerAssigned.Result)
            {
                throw new Exception($"Manager with ID '{branchCreateDto.ManagerId}' is already assigned to another branch."); // Use specific exception and avoid magic exception message.
            }

            var branch = _mapper.Map<Branch>(branchCreateDto);
            await _branchRepository.CreateAsync(branch);
            await _branchRepository.SaveChangesAync();
            return _mapper.Map<BranchReadDto>(branch);
        }

        public async Task<BranchReadDto?> GetBranchByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchReadDto>(branch);
        }
    }
}
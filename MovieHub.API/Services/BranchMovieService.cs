using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchMovieService : IBranchMovieService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IMovieRepository _movieRepository;

        public BranchMovieService(
            IMapper mapper,
            IBranchRepository branchRepository,
            IMovieRepository movieRepository
        )
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _movieRepository = movieRepository;
        }

        public async Task<
            IEnumerable<MovieReadDto>
        > GetScheduledMoviesByBranchIdAsync(int branchId)
        {
            await EnsureBranchExistsByIdOrThrowAsync(branchId);
            return _mapper.Map<IEnumerable<MovieReadDto>>(
                await _movieRepository.GetScheduledMoviesByBranchIdAsync(
                    branchId
                )
            );
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdOrThrowAsync(int branchId)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(branchId);
            if (!exists)
            {
                throw new Exception(
                    $"Branch with id '{branchId}' does not exist."
                );
            }
        }
        #endregion
    }
}

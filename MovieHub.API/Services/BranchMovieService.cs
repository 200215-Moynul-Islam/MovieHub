using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
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

        public async Task<MovieWithShowTimesReadDto> GetMovieWithUpcomingShowTimesByIdForBranchAsync(
            int movieId,
            int branchId
        )
        {
            await EnsureMovieExistsByIdOrThrowAsync(movieId);
            await EnsureBranchExistsByIdOrThrowAsync(branchId);
            return _mapper.Map<MovieWithShowTimesReadDto>(
                await _movieRepository.GetMovieWithUpcomingShowTimesByIdForBranchAsync(
                    movieId,
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
                throw new NotFoundException(
                    BusinessErrorMessages.Branch.NotFound
                );
            }
        }

        private async Task EnsureMovieExistsByIdOrThrowAsync(int movieId)
        {
            if (!await _movieRepository.ExistsByIdAsync(movieId))
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Movie.NotFound
                );
            }
        }
        #endregion
    }
}

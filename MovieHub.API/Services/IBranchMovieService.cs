using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchMovieService
    {
        Task<IEnumerable<MovieReadDto>> GetScheduledMoviesByBranchIdAsync(
            int branchId
        );
    }
}

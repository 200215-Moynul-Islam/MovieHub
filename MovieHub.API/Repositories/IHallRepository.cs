using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public interface IHallRepository : ISoftDeletableNamedRepository<Hall>
    {
        Task DeactivateByBranchIdAsync(int branchId);
    }
}

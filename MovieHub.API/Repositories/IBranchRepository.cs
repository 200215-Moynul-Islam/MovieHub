using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository : ISoftDeletableNamedRepository<Branch>
    {
        Task<bool> IsManagerAssignedAsync(Guid managerId);
        Task ResetManagerByIdAsync(int id);
    }
}

namespace MovieHub.API.Services
{
    public interface IHallService
    {
        Task DeactivateByBranchIdAsync(int branchId);
    }
}

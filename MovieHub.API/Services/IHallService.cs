namespace MovieHub.API.Services
{
    public interface IHallService
    {
        Task DeactivateHallsByBranchIdAsync(int branchId);
    }
}

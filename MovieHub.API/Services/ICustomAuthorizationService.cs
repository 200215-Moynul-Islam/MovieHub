namespace MovieHub.API.Services
{
    public interface ICustomAuthorizationService
    {
        Task<bool> IsManagerByHallIdAsync(int hallId, Guid userId);
        Task<bool> IsManagerByShowTimeIdAsync(int showTimeId, Guid userId);
    }
}

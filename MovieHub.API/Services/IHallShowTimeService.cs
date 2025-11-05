namespace MovieHub.API.Services
{
    public interface IHallShowTimeService
    {
        Task DeactivateHallByIdAsync(int id);
    }
}

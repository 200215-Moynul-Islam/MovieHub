using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IUserService
    {
        Task<UserReadDto> RegisterUserAsync(UserCreateDto userCreateDto);
    }
}

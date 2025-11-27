using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto loginRequestDto);
    }
}

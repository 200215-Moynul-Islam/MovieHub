using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserReadDto> RegisterUserAsync(
            UserCreateDto userCreateDto
        )
        {
            await EnsureEmailAndUsernameIsUniqueOrThrowAsync(userCreateDto);

            var user = _mapper.Map<User>(userCreateDto);
            user.Id = Guid.NewGuid();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                userCreateDto.Password
            );
            user.UserRoles.Add(
                new UserRole { RoleId = DefaultConstants.Role.UserRoleId }
            );

            await _userRepository.CreateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserReadDto>(user);
        }

        #region Private Methods
        private async Task EnsureEmailAndUsernameIsUniqueOrThrowAsync(
            UserCreateDto userCreateDto
        )
        {
            if (await _userRepository.ExistsByEmailAsync(userCreateDto.Email!))
            {
                throw new ConflictException(
                    BusinessErrorMessages.User.EmailUnavailable
                );
            }

            if (
                await _userRepository.ExistsByUsernameAsync(
                    userCreateDto.Username!
                )
            )
            {
                throw new ConflictException(
                    BusinessErrorMessages.User.UsernameUnavailable
                );
            }
        }
        #endregion
    }
}

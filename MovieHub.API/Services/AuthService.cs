using System.Security.Claims;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;
using MovieHub.API.Utilities;

namespace MovieHub.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IConfiguration _config;

        public AuthService(
            IUserRepository userRepository,
            IJwtHelper jwtHelper,
            IConfiguration configuration
        )
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _config = configuration;
        }

        public async Task<string> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await GetUserWithRolesByEmailOrUsernameOrThrowAsync(
                loginRequestDto.EmailOrUsername!
            );
            EnsurePasswordIsCorrectOrThrowAsync(
                loginRequestDto.Password!,
                user.PasswordHash!
            );

            // Generate the calims for the token
            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name!));
            }

            var expiredInMinutes = Convert.ToDouble(
                _config["JwtSettings:ExpiresInMinutes"]
            );
            var token = _jwtHelper.GenerateToken(claims, expiredInMinutes);

            return token;
        }

        #region Private Methods
        private async Task<User> GetUserWithRolesByEmailOrUsernameOrThrowAsync(
            string emailOrUsername
        )
        {
            var user =
                await _userRepository.GetUserWithRolesByEmailOrUsernameAsync(
                    emailOrUsername
                );
            if (user == null)
            {
                throw new InvalidCredentialsException(
                    BusinessErrorMessages.User.InvalidEmailOrUsername
                );
            }
            return user;
        }

        private void EnsurePasswordIsCorrectOrThrowAsync(
            string password,
            string passwordHash
        )
        {
            if (!BCrypt.Net.BCrypt.Verify(password, passwordHash))
            {
                throw new InvalidCredentialsException(
                    BusinessErrorMessages.User.InvalidCredential
                );
            }
            return;
        }
        #endregion
    }
}

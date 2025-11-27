using System.Security.Claims;

namespace MovieHub.API.Utilities
{
    public interface IJwtHelper
    {
        string GenerateToken(
            IEnumerable<Claim> claims,
            double expiresInMinutes
        );
    }
}

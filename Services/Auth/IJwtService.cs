using System.Security.Claims;

namespace Services.Auth;

public interface IJwtService
{
    string GenerateToken(string userId, string userName, IEnumerable<Claim>? extraClaims = null);
}

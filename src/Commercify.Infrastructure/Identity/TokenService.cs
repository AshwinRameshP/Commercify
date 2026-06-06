using Commercify.Core.Features.Users;
using Commercify.Core.Models;

namespace Commercify.Infrastructure.Identity;

public class TokenService : ITokenService
{
    public Task<string> GenerateToken(AppUser user)
    {
        // Minimal placeholder implementation for tests / DI resolution.
        return Task.FromResult(Guid.NewGuid().ToString());
    }
}

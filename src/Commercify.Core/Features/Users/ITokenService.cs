using Commercify.Core.Models;

namespace Commercify.Core.Features.Users;

public interface ITokenService
{
    Task<string> GenerateToken(AppUser user);
}

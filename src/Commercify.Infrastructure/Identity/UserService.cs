using Commercify.Core.Features.Users;
using Commercify.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Commercify.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUser?> FindByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<Result<string>> Register(AppUser user, string password)
    {
        var identityResult = await _userManager.CreateAsync(user, password);
        if (!identityResult.Succeeded)
            return Result.Error(string.Join(';', identityResult.Errors.Select(e => e.Description))?? "user Creation Failed!");
        return user.Id;
    }

    public async Task<AppUser?> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;
        var valid = await _userManager.CheckPasswordAsync(user, password);
        return valid ? user : null;
    }
}

using Commercify.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commercify.Core.Features.Users;

public  interface IUserService
{
    Task<AppUser?> FindByEmail(string email);
    Task<Result<string>> Register(AppUser user, string password);

    Task<AppUser?> Login(string email, string password);
}

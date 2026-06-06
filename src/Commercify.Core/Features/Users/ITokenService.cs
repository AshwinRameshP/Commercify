using Commercify.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercify.Core.Features.Users;

public interface ITokenService
{
    Task<string> GenerateToken(AppUser)
}

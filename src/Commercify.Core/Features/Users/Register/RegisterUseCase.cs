using Commercify.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commercify.Core.Features.Users.Register;

public class RegisterUseCase(IUserService userService, ITokenService tokenService)
{
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;
    public  async Task<Result<RegisterResponse>> Execute(RegisterRequest request)
    {
       var existingUser = await _userService.FindByEmail(request.Email);
        if (existingUser != null) 
            return Result.Conflict("User with this email already exists.");
        var user = new AppUser(request.Email, request.FirstName, request.LastName);
        var result = await _userService.Register(user, request.Password);
        if (result.IsError)
            return Result.Error(result.ErrorMessage);
        string token = await _tokenService.GenerateToken(user);
        return new RegisterResponse(user.Id,token);
    }
}
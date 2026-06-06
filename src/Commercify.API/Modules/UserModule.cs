using Commercify.API.Extensions;
using Commercify.Core.Features.Users.Register;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Commercify.API.Modules;

public class UserModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", Register)
            .WithTags("Users")
            .WithOpenApi()
            .Validator<RegisterRequest>();
    }
    private async Task<Results<Ok<RegisterResponse>, Conflict<string>, BadRequest<string>>> Register(RegisterRequest request, RegisterUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return result switch
        {
            { IsSuccess: true } => TypedResults.Ok(result.Value!),
            { IsSuccess: false, ErrorMessage: "User already exists" } => TypedResults.Conflict(result.ErrorMessage),
            { IsSuccess: false } => TypedResults.BadRequest(result.ErrorMessage),
            _ => throw new NotImplementedException()
        };
    }   
}

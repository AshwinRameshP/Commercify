using Commercify.Core.Features.Categories.Create;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Commercify.API.Modules;

public class CategoryModule
{
    public static void  MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories")
            .WithTags("Categories")
            .WithOpenApi();
        group.MapPost("/categories", CreateCategory);

    }

    private static async Task<Ok<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request,CreateCategoryUseCase useCase)
    {
        var result = await useCase.Execute(request);
        return TypedResults.Ok(result);
    }
}

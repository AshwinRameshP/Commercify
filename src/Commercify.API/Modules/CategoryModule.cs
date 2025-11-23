using Commercify.API.Extensions;
using Commercify.Core.Features.Categories.Create;
using Commercify.Core.Features.Categories.Delete;
using Commercify.Core.Features.Categories.Read;
using Commercify.Core.Features.Categories.Update;
using Commercify.Core.Features.Products.Read;
using Commercify.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Commercify.API.Modules;

public class CategoryModule
{
    public static void  MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories")
            .WithTags("Categories")
            .WithOpenApi();

        group.MapPost("/categories", CreateCategory)
            .Validator<CreateCategoryRequest>();
        group.MapGet("/categories", GetAllCategories);
        group.MapGet("/{id}", GetCategoryById);
        group.MapPut("/{id}", UpdateCategory)
            .Validator<UpdateCategoryRequest>();
        group.MapDelete("/{id}", DeleteCategory); 
    }

    private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteCategory(long id,DeleteCategoryUseCase useCase)
    {
        var result = await useCase.Execute(id);
        if (result.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        return result.Status == ResultStatus.NotFound  ? TypedResults.NotFound(result.ErrorMessage) : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<Results<Ok<UpdateCategoryResponse>, NotFound<string>>> UpdateCategory(long id, 
        UpdateCategoryRequest request, UpdateCategoryUseCase useCase)
    {
        var result = await useCase.Execute(id, request);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<Ok<CategoryResponse>, NotFound<string>>> GetCategoryById(long id, CategoryReadService service)
    {
        var result = await service.GetById(id);
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result.Value);
        }
        return TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Ok<IEnumerable<CategoryResponse>>> GetAllCategories(int? page, 
        int?pageSize,
        CategoryReadService service)
    {
        var result = await service.GetAll(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
        return TypedResults.Ok(result);
    }
    
    private static async Task<Ok<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request,CreateCategoryUseCase useCase)
    {
        var result = await useCase.Execute(request);
        return TypedResults.Ok(result);
    }

}

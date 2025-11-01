using Commercify.API.Extensions;
using Commercify.Core.Features.Products.Create;
using Commercify.Core.Features.Products.Delete;
using Commercify.Core.Features.Products.Import;
using Commercify.Core.Features.Products.Read;
using Commercify.Core.Features.Products.Update;
using Commercify.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Commercify.Api.Modules;

// ReSharper disable once UnusedType.Global
public class ProductModule
{
    private const string GetProductEndpointName = "GetProduct";

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products")
            .WithOpenApi()
        .WithTags("Products");
            //.RequireAuthorization(Security.AdminPolicy);

        group.MapGet("", GetAllProducts)
            .WithSummary("Retrieves a paginated list of all available products.")
            .AllowAnonymous();

        group.MapGet("/{id}", GetProductById)
            .WithSummary("Retrieves details for a specific product by its ID.")
            .WithName(GetProductEndpointName)
            .AllowAnonymous();

        group.MapPost("", CreateProduct)
            .WithSummary("Creates a new product with specified details, including name, price, and description.")
            .Validator<CreateProductRequest>();

        group.MapPut("/{id}", UpdateProduct)
            .WithSummary("Updates the details of an existing product based on the specified ID.")
            .Validator<UpdateProductRequest>();

        group.MapDelete("/{id}", DeleteProduct)
            .WithSummary("Deletes a specific product from the system based on its ID.");
        
        //group.MapPost("/import", ImportProducts)
        //    .WithSummary("Imports multiple products from a CSV file, allowing batch creation.")
        //    //TODO fix this properly
        //    // https://stackoverflow.com/questions/77189996/upload-files-to-a-minimal-api-endpoint-in-net-8
        //    // https://www.youtube.com/watch?v=ihwXmTE9dkk
        //    //Antiforgery protection is a security measure that prevents cross-site request forgery (CSRF) attacks.
        //    //CSRF attacks trick authenticated users into performing actions they didn't intend by submitting forms or requests on their behalf.
        //    // The risk here for cross-site request forgery is low, as the endpoint will be protected later,
        //    // so that only administrators can access it.
        //    .DisableAntiforgery();
    }

    private static async Task<Ok<IEnumerable<ProductResponse>>> GetAllProducts(
        int? page,
        int? size,
        ProductReadService service)
    {
        var result = await service.GetAll(page.GetValueOrDefault(), size.GetValueOrDefault());
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<ProductResponse>, NotFound<string>>> GetProductById(int id,
        ProductReadService service)
    {
        var result = await service.GetById(id);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<IResult> CreateProduct(CreateProductRequest request, CreateProductUseCase useCase)
    {
        var result = await useCase.Execute(request);

        // name GET product endpoint, and use it to generate the location header
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, GetProductEndpointName, new { id = result.Value.Id })
            : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<Results<Ok<UpdateProductResponse>, NotFound<string>>> UpdateProduct(
        long id,
        UpdateProductRequest request,
        UpdateProductUseCase useCase)
    {
        var result = await useCase.Execute(id, request);

        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteProduct(long id, DeleteProductUseCase useCase)
    {
        var result = await useCase.Execute(id);

        if (result.IsSuccess)
        {
            return TypedResults.NoContent();
        }

        return result.Status == ResultStatus.NotFound
            ? TypedResults.NotFound(result.ErrorMessage)
            : TypedResults.BadRequest(result.ErrorMessage);
    }

    // Method to import products from an uploaded CSV file
    private static async Task<Results<Ok, UnprocessableEntity<string>, BadRequest<string>>> ImportProducts(
        IFormFile file, // The uploaded file containing product data
        ImportProductsUseCase useCase // The use case for handling the product import
    )
    {
        // Check if the file is empty (no data was uploaded)
        if (file.Length == 0)
        {
            // Return a BadRequest response if no file was uploaded
            return TypedResults.BadRequest("No file uploaded.");
        }

        // Convert the uploaded file to a format that the use case can process
        var uploadedFile = await ConvertToUploadedFile(file);

        // Execute the import process using the use case and get the result
        var result = await useCase.Execute(uploadedFile);

        // If the import was successful, return an Ok response
        if (result.IsSuccess)
        {
            return TypedResults.Ok();
        }

        // Check the type of error and return the appropriate response
        return result.Status == ResultStatus.UnprocessableEntity
            ? TypedResults.UnprocessableEntity(result.ErrorMessage) // If an import-specific error, return UnprocessableEntity
            : TypedResults.BadRequest(result.ErrorMessage); // For other errors, return BadRequest with the error message
    }

    // Method to convert an uploaded file to a format that the use case can process
    private static async Task<UploadedFile> ConvertToUploadedFile(IFormFile formFile)
    {
        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        return new UploadedFile(formFile.FileName, formFile.ContentType, memoryStream.ToArray());
    }
}
using Commercify.Core.Models;
using Commercify.Core.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Features.Products.Import;

//C# primary constructors allow you to define and initialize class properties directly in the class declaration,
//making your code cleaner and more concise.
//Introduced in C# 12, they simplify setup by combining constructor logic and property definitions in one place.
public class ImportProductsUseCase(
    IDbContext dbContext,
    IValidator<ProductImportRequest> validator,
    IProductParser productParser)
{
    public async Task<Result> Execute(UploadedFile csvFile)
    {
        // First, we check if the uploaded file is a CSV file
        if (!csvFile.FileName.EndsWith(".csv"))
        {
            return Result.UnprocessableEntity("Invalid file format. Please upload a CSV file.");
        }

        //Then, we are using the product parser to get the imported products from the CSV file.
        // And here is an important lesson on decoupling the use case from the implementation details.
        // The IProductParser interface is defined in the Core layer,
        // And the implementation is provided by the Infrastructure layer.
        var importResult = productParser.GetImportedProducts(csvFile);
        if (importResult.IsError)
        {
            return importResult;
        }
        
        var productsToImport = importResult.Value;

        var existingCategoryIds = await dbContext
            .Set<Category>()
            .Select(x => x.Id)
            .ToListAsync();

        // We are iterating over the list of products to import and validate each product.
        for (var i = 0; i < productsToImport.Count; i++)
        {
            var productRequest = productsToImport[i];
            var productResult = ParseProduct(productRequest, existingCategoryIds, i);
            
            if (productResult.IsError)
            {
                return productResult;
            }
            dbContext.Set<Product>().Add(productResult.Value);
        }

        await dbContext.SaveChangesAsync();
        return Result.Success();
    }

    // Method to parse and validate a single product import request.
    // It returns either a valid Product or an error result
    private Result<Product> ParseProduct(
        ProductImportRequest productRequest, // The product data to be parsed
        List<long> existingCategoryIds,      // List of existing category IDs to validate against
        int i                                // Line number (or index) for error reporting
    )
    {
        // Validate the product request using a FluentValidation
        var validationResult = validator.Validate(productRequest);

        // If validation fails, we return a failure result with detailed error messages
        if (!validationResult.IsValid)
        {
            // Concatenate error messages and include the line number in the message for better debugging
            return Result.Error(
                $"Validation error on line {i + 1}: " +
                $"{string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
        }

        // We also, validate that the category ID exists in the list of valid categories
        if (!existingCategoryIds.Contains(productRequest.CategoryId))
        {
            // Return a failure result if the category ID is not found
            return Result.Error($"Category with id {productRequest.CategoryId} does not exist.");
        }

        // And finally, we create a Product object from the validated product request data
        var product = new Product
        {
            Name = productRequest.Name,
            Description = productRequest.Description,
            Price = productRequest.Price,
            CategoryId = productRequest.CategoryId,
            StockQuantity = productRequest.StockQuantity
        };

        return product;
    }
}
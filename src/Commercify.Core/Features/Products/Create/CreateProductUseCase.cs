using Commercify.Core.Models;
using Commercify.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Features.Products.Create;

public class CreateProductUseCase(IDbContext dbContext)
{
    public async Task<Result<CreateProductResponse>> Execute(CreateProductRequest request)
    {
        var categoryExists = await dbContext
            .Set<Category>()
            .AnyAsync(c => c.Id == request.CategoryId);
        
        if (!categoryExists)
        {
            return Result.NotFound($"Category with id {request.CategoryId} was not found");
        }
        
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            StockQuantity = request.StockQuantity
        };
        dbContext.Set<Product>().Add(product);
        await dbContext.SaveChangesAsync();
        
        var categoryName = await dbContext
            .Set<Category>()
            .Where(c => c.Id == request.CategoryId)
            .Select(c => c.Name)
            .FirstAsync();
        
        return new CreateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            categoryName,
            product.StockQuantity);
    }
}
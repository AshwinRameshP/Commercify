using Commercify.Core.Models;
using Commercify.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Features.Products.Update;

public class UpdateProductUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdateProductResponse>> Execute(long id, UpdateProductRequest request)
    {
        var product = await dbContext.Set<Product>().FindAsync(id);

        if (product == null)
        {
            return Result.NotFound($"Product with id {id} was not found");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        product.StockQuantity = request.StockQuantity;
        dbContext.Set<Product>().Update(product);
        await dbContext.SaveChangesAsync();

        var categoryName = await dbContext
            .Set<Category>()
            .Where(c => c.Id == request.CategoryId)
            .Select(c => c.Name)
            .FirstAsync();
        
        return new UpdateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            categoryName,
            product.StockQuantity);
    }
}
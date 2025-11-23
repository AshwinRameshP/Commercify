using Commercify.Core.Models;
using Commercify.Core.Shared;

namespace Commercify.Core.Features.Products.Delete;

public class DeleteProductUseCase(IDbContext dbContext)
{
public async Task<Result> Execute(long id)
{
    var product = await dbContext.Set<Product>().FindAsync(id);
    if (product == null)
    {
        return Result.NotFound($"Product with id {id} was not found");
    }
    /*
    var isInOrder = await dbContext.Set<OrderItem>()
        .AnyAsync(x => x.ProductId == id);
    
    if (isInOrder)
    {
        return Result.Error($"Product with id {id} is in an order and cannot be deleted");
    }
    */
    dbContext.Set<Product>().Remove(product);
    await dbContext.SaveChangesAsync();
    
    return Result.Success();
}
}
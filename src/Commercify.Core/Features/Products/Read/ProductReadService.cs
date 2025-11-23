
using Commercify.Core.Models;
using Commercify.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Features.Products.Read;

public class ProductReadService(IDbContext dbContext)
{
    public async Task<IEnumerable<ProductResponse>> GetAll(int page, int pageSize)
    {
        if (page == 0) { page = 1; }
        if (pageSize == 0) { pageSize = 10; }
        
        return await dbContext
            .Set<Product>()
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.CategoryId, p.Category.Name, p.StockQuantity))
            .ToListAsync();
    }

    public async Task<Result<ProductResponse>> GetById(long id)
    {
        var product = await dbContext
            .Set<Product>()
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.CategoryId, p.Category.Name, p.StockQuantity))
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return Result.NotFound($"Product with id {id} was not found");
        }
        return product;
    }
}
using Commercify.Core.Models;
using Commercify.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Features.Categories.Read;

public class CategoryReadService
{
    private readonly IDbContext _dbContext;
    public CategoryReadService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<CategoryResponse>> GetAll(int page, int pageSize)
    {
        if (page == 0)
            page = 1;
        if (pageSize == 0)
            pageSize = 10;
        return await _dbContext.Set<Category>()
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CategoryResponse(x.Id, x.Name, x.Description))
            .ToListAsync();
    }
    public async Task<Result<CategoryResponse>> GetById(long id)
    {
        var category = await _dbContext.Set<Category>()
            .Where(x => x.Id == id)
            .Select(x => new CategoryResponse(x.Id, x.Name, x.Description))
            .FirstOrDefaultAsync();
        
        if (category == null)
        {
            return Result.NotFound($"Category with id {id} not found.");
        }   
        return category;
    }
}


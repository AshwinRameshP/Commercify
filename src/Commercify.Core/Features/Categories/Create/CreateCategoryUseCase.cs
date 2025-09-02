using Commercify.Core.Models;
using Commercify.Core.Shared;

namespace Commercify.Core.Features.Categories.Create;

public class CreateCategoryUseCase
{
    private readonly IDbContext _dbContext;

    public CreateCategoryUseCase(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateCategoryResponse> Execute(CreateCategoryRequest request)
    {
         var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };
        _dbContext.Set<Category>().Add(category);
        await _dbContext.SaveChangesAsync();
        return new CreateCategoryResponse(category.Id,category.Name,category.Description);
    }
}

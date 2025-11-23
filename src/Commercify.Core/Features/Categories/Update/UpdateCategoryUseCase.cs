using Commercify.Core.Models;
using Commercify.Core.Shared;

namespace Commercify.Core.Features.Categories.Update;

public class UpdateCategoryUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdateCategoryResponse>> Execute(long id, UpdateCategoryRequest request)
    {
        var category = await dbContext.Set<Category>().FindAsync(id);

        if (category == null)
        {
            return Result.NotFound($"Category with id {id} was not found.");
        }

        category.Name = request.Name;
        category.Description = request.Description;

        dbContext.Set<Category>().Update(category); 
        await dbContext.SaveChangesAsync();

        return Result<UpdateCategoryResponse>.Success(new UpdateCategoryResponse(category.Id, category.Name,category.Description));
    }
}

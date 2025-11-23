using Commercify.Core.Models;
using Commercify.Core.Shared;

namespace Commercify.Core.Features.Categories.Delete
{
    public class DeleteCategoryUseCase(IDbContext dbContext)
    {
        public async Task<Result> Execute(long id)
        {
            var category = await dbContext.Set<Category>().FindAsync(id);

            if (category == null)
            {
                return Result.NotFound($"Category with id {id} was not found.");
            }

            dbContext.Set<Category>().Remove(category);
            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}

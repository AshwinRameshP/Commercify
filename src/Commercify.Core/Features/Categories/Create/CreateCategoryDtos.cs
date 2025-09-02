namespace Commercify.Core.Features.Categories.Create;

public record CreateCategoryRequest(string Name, string Description);
public record CreateCategoryResponse(long Id, string Name, string Description);

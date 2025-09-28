namespace Commercify.Core.Features.Categories.Update;

public record UpdateCategoryRequest(string Name, string Description);
public record UpdateCategoryResponse(long Id, string Name, string Description);

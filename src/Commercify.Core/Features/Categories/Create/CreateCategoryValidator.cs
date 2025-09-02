using Commercify.Core.Models;
using FluentValidation;

namespace Commercify.Core.Features.Categories.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(Category.MaxLengths.Name);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(Category.MaxLengths.Description);

    }
}

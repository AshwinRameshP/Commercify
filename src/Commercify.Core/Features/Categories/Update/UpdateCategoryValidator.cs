using FluentValidation;

namespace Commercify.Core.Features.Categories.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Models.Category.MaxLengths.Name);
        RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(Models.Category.MaxLengths.Description);    
    }    
}

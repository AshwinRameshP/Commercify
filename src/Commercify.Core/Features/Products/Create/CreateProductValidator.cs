using FluentValidation;

namespace Commercify.Core.Features.Products.Create;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Models.Product.MaxLengths.Name);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(Models.Product.MaxLengths.Description);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}
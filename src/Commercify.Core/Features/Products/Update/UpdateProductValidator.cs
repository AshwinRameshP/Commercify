using Commercify.Core.Models;
using FluentValidation;

namespace Commercify.Core.Features.Products.Update;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Product.MaxLengths.Name);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(Product.MaxLengths.Description);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}
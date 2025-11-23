using Commercify.Core.Features.Categories.Create;
using Commercify.Core.Features.Categories.Delete;
using Commercify.Core.Features.Categories.Read;
using Commercify.Core.Features.Categories.Update;
using Commercify.Core.Features.Products.Create;
using Commercify.Core.Features.Products.Delete;
using Commercify.Core.Features.Products.Read;
using Commercify.Core.Features.Products.Update;
using Commercify.Core.Shared;
using Commercify.Infrastructure.Database;
using FluentValidation;

namespace Commercify.API.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDbContext, AppDbContext>();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

        services.AddTransient<CreateCategoryUseCase>();
        services.AddTransient<UpdateCategoryUseCase>();
        services.AddTransient<DeleteCategoryUseCase>();
        services.AddTransient<CategoryReadService>();

        services.AddTransient<CreateProductUseCase>();
        services.AddTransient<UpdateProductUseCase>();
        services.AddTransient<DeleteProductUseCase>();
        services.AddTransient<ProductReadService>();

        return services;
    }
}

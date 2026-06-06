using Commercify.Core.Features.Categories.Create;
using Commercify.Core.Features.Categories.Delete;
using Commercify.Core.Features.Categories.Read;
using Commercify.Core.Features.Categories.Update;
using Commercify.Core.Features.Products.Create;
using Commercify.Core.Features.Products.Delete;
using Commercify.Core.Features.Products.Read;
using Commercify.Core.Features.Products.Update;
using Commercify.Core.Features.Users;
using Commercify.Core.Features.Users.Register;
using Commercify.Core.Shared;
using Commercify.Infrastructure.Database;
using Commercify.Infrastructure.Identity;
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

        services.AddTransient<RegisterUseCase>();

        // User and token services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}

using Commercify.Core.Features.Categories.Create;
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

        return services;
    }
}

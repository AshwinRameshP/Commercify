using Commercify.Core.Features.Categories.Create;
using Commercify.Core.Shared;
using Commercify.Infrastructure.Database;

namespace Commercify.API.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDbContext, AppDbContext>();
        services.AddTransient<CreateCategoryUseCase>();

        return services;
    }
}

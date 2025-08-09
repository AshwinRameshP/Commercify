using Commercify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Commercify.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql("connectionstring", postgreOptions => { });
        });

        return services;
    }
}

using Commercify.API.Configurations;
using Commercify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Commercify.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(dbOptions.ConnectionString, postgreOptions => {
                postgreOptions.CommandTimeout(dbOptions.CommandTimeout);
                postgreOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount);
            });
        });

        return services;
    }
}

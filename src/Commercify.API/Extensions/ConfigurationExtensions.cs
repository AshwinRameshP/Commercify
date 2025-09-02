using Commercify.API.Configurations;

namespace Commercify.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection("Database"))
            .ValidateDataAnnotations();
        return services;
    }
}

using Commercify.API.Filters;

namespace Commercify.API.Extensions;

public static class ValidatorExtensions
{
    public static RouteHandlerBuilder Validator<T>(this RouteHandlerBuilder handlerBuilder) where T : class
    {
        handlerBuilder.AddEndpointFilter<EndpointValidationFilter<T>>();
        return handlerBuilder;
    }
}

using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Commercify.API.Filters;

internal class EndpointValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    private IValidator<T> Validator => validator;   
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var inputData = context.Arguments.OfType<T>()
            .FirstOrDefault(a=> a?.GetType() == typeof(T));
        if (inputData is not null)
        {
            ValidationResult validationResult = await Validator.ValidateAsync(inputData);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode:(int)HttpStatusCode.BadRequest);
            }
        }
        return await next.Invoke(context);
    }
}

using System.Diagnostics.CodeAnalysis;

namespace Commercify.Core.Models;

public enum ResultStatus
{
    Ok,
    Error,
    Created,
    NoContent,
    Forbidden,
    Unauthorized,
    NotFound,
    Conflict,
    CriticalError,
    UnprocessableEntity
}

/*
 * The Result class is a generic class that represents the result of an operation.
 * It can be either a success or a failure, and it contains information about the error type and message.
 * The Result class is used to return the result of an operation, and it can be used to handle errors and exceptions in a consistent way.
 */

public class Result
{
    public bool IsError => !IsSuccess;
    public ResultStatus Status { get; }

    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.NoContent or ResultStatus.Created;
    public string ErrorMessage { get; }

    protected Result(ResultStatus status, string errorMessage)
    {
        Status = status;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Represents a successful operation.
    /// </summary>
    public static Result Success() => new(ResultStatus.Ok, string.Empty);

    /// <summary>
    /// Represents a failed operation and accepts an error message as the reason for the failure.
    /// </summary>
    public static Error Error(string errorMessage) => new Error(ResultStatus.Error, errorMessage);

    /// <summary>
    /// Represents a failed operation where the requested resource was not found.
    /// </summary>
    public static Error NotFound() => new Error(ResultStatus.NotFound, string.Empty);

    /// <summary>
    /// Represents a failed operation where the requested resource was not found.
    /// </summary>
    public static Error NotFound(string errorMessage) => new Error(ResultStatus.NotFound, errorMessage);

    /// <summary>
    /// Represents a failed operation where the request could not be processed because of conflict in the current state of the resource.
    /// </summary>
    public static Error Conflict() => new Error(ResultStatus.Conflict, string.Empty);

    /// <summary>
    /// Represents a failed operation where the request could not be processed because of conflict in the current state of the resource.
    /// </summary>
    public static Error Conflict(string errorMessage) => new Error(ResultStatus.Conflict, errorMessage);

    /// <summary>
    /// Represents a failed operation where the request could not be processed due to a critical error.
    /// </summary>
    public static Error CriticalError() => new Error(ResultStatus.CriticalError, string.Empty);

    /// <summary>
    /// Represents a failed operation where the request could not be processed due to a critical error.
    /// </summary>
    public static Error CriticalError(string errorMessage) => new Error(ResultStatus.CriticalError, errorMessage);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public static Error Unauthorized() => new Error(ResultStatus.Unauthorized, string.Empty);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public static Error Unauthorized(string errorMessage) => new Error(ResultStatus.Unauthorized, errorMessage);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public static Error Forbidden() => new Error(ResultStatus.Forbidden, string.Empty);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public static Error Forbidden(string errorMessage) => new Error(ResultStatus.Forbidden, errorMessage);

    /// <summary>
    /// Represents a failed operation where the entity cannot be processed.
    /// </summary>
    public static Error UnprocessableEntity() => new Error(ResultStatus.UnprocessableEntity, string.Empty);

    /// <summary>
    /// Represents a failed operation where the entity cannot be processed.
    /// </summary>
    public static Error UnprocessableEntity(string errorMessage) => new Error(ResultStatus.UnprocessableEntity, errorMessage);

    public static implicit operator Result(Error error) => new(error.Status, error.ErrorMessage);
}

public class Result<T> : Result
{
    private readonly T? _value;

    private Result(ResultStatus status, string errorMessage, T? value)
        : base(status, errorMessage)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<T>(T value) => new(ResultStatus.Ok, string.Empty, value);

    public static implicit operator Result<T>(Error error) => new(error.Status, error.ErrorMessage, default);

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation.
    /// </summary>
    public static Result<T> Success(T value) => new(ResultStatus.Ok, string.Empty, value);

    /// <summary>
    /// Represents a failed operation and accepts an error message as the reason for the failure.
    /// </summary>
    public new static Result<T> Error(string errorMessage) => new(ResultStatus.Error, errorMessage, default);

    /// <summary>
    /// Represents a successful operation where a new resource has been created.
    /// </summary>
    public static Result<T> Created(T value) => new(ResultStatus.Created, string.Empty, value);

    /// <summary>
    /// Represents a successful operation where no content is returned.
    /// </summary>
    public static Result<T> NoContent() => new(ResultStatus.NoContent, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the requested resource was not found.
    /// </summary>
    public new static Result<T> NotFound() => new(ResultStatus.NotFound, String.Empty, default);

    /// <summary>
    /// Represents a failed operation where the requested resource was not found.
    /// </summary>
    public new static Result<T> NotFound(string errorMessage) => new(ResultStatus.NotFound, errorMessage, default);

    /// <summary>
    /// Represents a failed operation where the request could not be processed because of conflict in the current state of the resource.
    /// </summary>
    public new static Result<T> Conflict() => new(ResultStatus.Conflict, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the request could not be processed because of conflict in the current state of the resource.
    /// </summary>
    public new static Result<T> Conflict(string errorMessage) => new(ResultStatus.Conflict, errorMessage, default);

    /// <summary>
    /// Represents a failed operation where the request could not be processed due to a critical error.
    /// </summary>
    public new static Result<T> CriticalError() => new(ResultStatus.CriticalError, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the request could not be processed due to a critical error.
    /// </summary>
    public new static Result<T> CriticalError(string errorMessage) => new(ResultStatus.CriticalError, errorMessage, default);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public new static Result<T> Unauthorized() => new(ResultStatus.Unauthorized, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public new static Result<T> Unauthorized(string errorMessage) => new(ResultStatus.Unauthorized, errorMessage, default);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public new static Result<T> Forbidden() => new(ResultStatus.Forbidden, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the request is not authorized.
    /// </summary>
    public new static Result<T> Forbidden(string errorMessage) => new(ResultStatus.Forbidden, errorMessage, default);

    /// <summary>
    /// Represents a failed operation where the entity cannot be processed.
    /// </summary>
    public new static Result<T> UnprocessableEntity() => new(ResultStatus.UnprocessableEntity, string.Empty, default);

    /// <summary>
    /// Represents a failed operation where the entity cannot be processed.
    /// </summary>
    public new static Result<T> UnprocessableEntity(string errorMessage) => new(ResultStatus.UnprocessableEntity, errorMessage, default);
}

public class Error
{
    public ResultStatus Status { get; }

    public string ErrorMessage { get; }

    internal Error(ResultStatus status, string errorMessage)
    {
        ErrorMessage = errorMessage;
        Status = status;
    }
}
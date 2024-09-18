using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Application.Common;
using Platform.Application.Common.Responses;

namespace Platform.Application.Extensions;

/// <summary>
/// ApiResponseResultExtensions
/// </summary>
public static partial class ApiResponseResultExtensions
{
    /// <summary>
    /// Convert a <see cref="Ardalis.Result.Result{T}"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <typeparam name="T">The value being returned</typeparam>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Microsoft.AspNetCore.Http.IResult ToPlatformApiResult<T>(this Ardalis.Result.Result<T> result) =>
        ToPlatformApiResult((Ardalis.Result.IResult)result);

    /// <summary>
    /// Convert a <see cref="Ardalis.Result.Result"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Microsoft.AspNetCore.Http.IResult ToPlatformApiResult(this Ardalis.Result.Result result) =>
        ToPlatformApiResult((Ardalis.Result.IResult)result);

    internal static Microsoft.AspNetCore.Http.IResult ToPlatformApiResult(this Ardalis.Result.IResult result) =>
        result.Status switch
        {
            Ardalis.Result.ResultStatus.Ok => Ok(result),
            Ardalis.Result.ResultStatus.Created => Created(result),
            Ardalis.Result.ResultStatus.NoContent => NoContent(result),
            Ardalis.Result.ResultStatus.NotFound => NotFoundEntity(result),
            Ardalis.Result.ResultStatus.Unauthorized => UnAuthorized(result),
            Ardalis.Result.ResultStatus.Forbidden => Forbidden(result),
            Ardalis.Result.ResultStatus.Invalid => BadRequest(result),
            Ardalis.Result.ResultStatus.Error => UnprocessableEntity(result),
            Ardalis.Result.ResultStatus.Conflict => ConflictEntity(result),
            Ardalis.Result.ResultStatus.Unavailable => UnavailableEntity(result),
            Ardalis.Result.ResultStatus.CriticalError => CriticalEntity(result),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };

    private static Microsoft.AspNetCore.Http.IResult Ok(Ardalis.Result.IResult result)
    {
        return result is Ardalis.Result.Result
            ? ToHttpResults(result, StatusCodes.Status200OK, "Ok.")
            : Results.Ok(new ApiResponse<object> { Success = true, Data = result.GetValue() });
    }

    private static Microsoft.AspNetCore.Http.IResult Created(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status201Created, "Created.");
    }

    private static Microsoft.AspNetCore.Http.IResult NoContent(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status204NoContent, "No content.");
    }

    private static Microsoft.AspNetCore.Http.IResult BadRequest(Ardalis.Result.IResult result)
    {
        Dictionary<string, string[]> groupedErrors;
        if (result.ValidationErrors.Any())
        {
            groupedErrors = result.ValidationErrors.GroupBy(e => e.Identifier)
                .ToDictionary(g => StringUtility.ToCamelCase(g.Key), g => g.Select(e => e.ErrorMessage).ToArray());
        }
        else
        {
            groupedErrors = new Dictionary<string, string[]> { { "message", new[] { result.Errors.FirstOrDefault() ?? "Bad request." } } };
        }

        return Results.Json(
            new ApiResponse<string> { Success = false, Errors = groupedErrors },
            statusCode: StatusCodes.Status400BadRequest,
            contentType: "application/json"
        );
    }

    private static Microsoft.AspNetCore.Http.IResult UnprocessableEntity(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status422UnprocessableEntity, "Unprocessable entity.");
    }

    private static Microsoft.AspNetCore.Http.IResult NotFoundEntity(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status404NotFound, "Resource not found.");
    }

    private static Microsoft.AspNetCore.Http.IResult ConflictEntity(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status409Conflict, "Resource already exists.");
    }

    private static Microsoft.AspNetCore.Http.IResult CriticalEntity(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status500InternalServerError, "Something went wrong.");
    }

    private static Microsoft.AspNetCore.Http.IResult UnavailableEntity(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status503ServiceUnavailable, "Service unavailable.");
    }

    private static Microsoft.AspNetCore.Http.IResult Forbidden(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status403Forbidden, "Forbidden.");
    }

    private static Microsoft.AspNetCore.Http.IResult UnAuthorized(Ardalis.Result.IResult result)
    {
        return ToHttpResults(result, StatusCodes.Status401Unauthorized, "Unauthorized.");
    }

    private static Microsoft.AspNetCore.Http.IResult ToHttpResults(Ardalis.Result.IResult result, int statusCode, string message)
    {
        Dictionary<string, string[]> groupedErrors;
        if (result.ValidationErrors.Any())
        {
            groupedErrors = result.ValidationErrors.GroupBy(e => e.Identifier)
                .ToDictionary(g => StringUtility.ToCamelCase(g.Key), g => g.Select(e => e.ErrorMessage).ToArray());
        }
        if (result.Errors.Any())
        {
            groupedErrors = result.Errors.ToDictionary(e => "message", e => new[] { e });
        }
        else
        {
            groupedErrors = new Dictionary<string, string[]> { { "message", new[] { message } } };
        }

        return Results.Json(new ApiResponse<string> { Success = false, Errors = groupedErrors }, statusCode: statusCode, contentType: "application/json");
    }
}

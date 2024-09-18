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
    public static Microsoft.AspNetCore.Http.IResult ToPlatformApiResult<T>(this Ardalis.Result.Result<T> result) => ToPlatformApiResult((Ardalis.Result.IResult)result);

    /// <summary>
    /// Convert a <see cref="Ardalis.Result.Result"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Microsoft.AspNetCore.Http.IResult ToPlatformApiResult(this Ardalis.Result.Result result) => ToPlatformApiResult((Ardalis.Result.IResult)result);

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
        var details = new List<Dictionary<string, string[]>> { new() { { "message", new[] { result.Errors.FirstOrDefault() ?? "Bad request." } } } };
        if (result.ValidationErrors.Any())
        {
            details = result.ValidationErrors.Select(e => new Dictionary<string, string[]> { { StringUtility.ToCamelCase(e.Identifier), new[] { e.ErrorMessage } } }).ToList();
        }
        return Results.Json(new ApiResponse<string>
        {
            Success = false,
            Errors = details
        }, statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");
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
        var details = new List<Dictionary<string, string[]>> { new() { { "message", new[] { message } } } };
        if (result.Errors.Any())
        {
            details.Clear();
            details.Add(new Dictionary<string, string[]> { { "message", result.Errors.ToArray() } });
        }

        return Results.Json(new ApiResponse<string>
        {
            Success = false,
            Errors = details
        }, statusCode: statusCode, contentType: "application/json");
    }
}


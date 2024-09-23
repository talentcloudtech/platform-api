using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public static Results<JsonHttpResult<ApiResponse<T>>, Ok<ApiResponse<T>>> ToPlatformApiResult<T>(this Ardalis.Result.Result<T> result)
        where T : class =>
        result.Status switch
        {
            Ardalis.Result.ResultStatus.Ok => TypedResults.Ok(new ApiResponse<T> { Success = true, Data = (T)result.GetValue() }),
            Ardalis.Result.ResultStatus.Created => TypedResults.Ok(new ApiResponse<T> { Success = true }),
            Ardalis.Result.ResultStatus.NoContent => TypedResults.Ok(new ApiResponse<T> { Success = true }),
            Ardalis.Result.ResultStatus.NotFound => TypedResults.Json(
                new ApiResponse<T>
                {
                    Success = false,
                    Errors = new Dictionary<string, string[]> { { "message", new[] { "Resource not found." } } },
                }
            ),
            Ardalis.Result.ResultStatus.Unauthorized => ToHttpResults<T>(result, StatusCodes.Status401Unauthorized, "Unauthorized."),
            Ardalis.Result.ResultStatus.Forbidden => ToHttpResults<T>(result, StatusCodes.Status403Forbidden, "Forbidden."),
            Ardalis.Result.ResultStatus.Invalid => BadRequest<T>(result),
            Ardalis.Result.ResultStatus.Error => ToHttpResults<T>(result, StatusCodes.Status422UnprocessableEntity, "Error."),
            Ardalis.Result.ResultStatus.Conflict => ToHttpResults<T>(result, StatusCodes.Status409Conflict, "Resource already exists."),
            Ardalis.Result.ResultStatus.Unavailable => ToHttpResults<T>(result, StatusCodes.Status503ServiceUnavailable, "Service unavailable."),
            Ardalis.Result.ResultStatus.CriticalError => ToHttpResults<T>(result, StatusCodes.Status500InternalServerError, "Something went wrong."),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };

    /// <summary>
    /// Convert a <see cref="Ardalis.Result.Result"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Results<JsonHttpResult<ApiResponse<object>>, Ok<ApiResponse<object>>> ToPlatformApiResult(this Ardalis.Result.Result result) =>
        result.Status switch
        {
            Ardalis.Result.ResultStatus.Ok => TypedResults.Ok(new ApiResponse<object> { Success = true, Data = result.GetValue() }),
            Ardalis.Result.ResultStatus.Created => TypedResults.Ok(new ApiResponse<object> { Success = true, Data = result.GetValue() }),
            Ardalis.Result.ResultStatus.NoContent => TypedResults.Ok(new ApiResponse<object> { Success = true, Data = result.GetValue() }),
            Ardalis.Result.ResultStatus.NotFound => TypedResults.Json(
                new ApiResponse<object>
                {
                    Success = false,
                    Errors = new Dictionary<string, string[]> { { "message", new[] { "Resource not found." } } },
                }
            ),
            Ardalis.Result.ResultStatus.Unauthorized => ToHttpResults<object>(result, StatusCodes.Status401Unauthorized, "Unauthorized."),
            Ardalis.Result.ResultStatus.Forbidden => ToHttpResults<object>(result, StatusCodes.Status403Forbidden, "Forbidden."),
            Ardalis.Result.ResultStatus.Invalid => BadRequest<object>(result),
            Ardalis.Result.ResultStatus.Error => ToHttpResults<object>(result, StatusCodes.Status422UnprocessableEntity, "Error."),
            Ardalis.Result.ResultStatus.Conflict => ToHttpResults<object>(result, StatusCodes.Status409Conflict, "Resource already exists."),
            Ardalis.Result.ResultStatus.Unavailable => ToHttpResults<object>(result, StatusCodes.Status503ServiceUnavailable, "Service unavailable."),
            Ardalis.Result.ResultStatus.CriticalError => ToHttpResults<object>(result, StatusCodes.Status500InternalServerError, "Something went wrong."),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };

    private static JsonHttpResult<ApiResponse<T>> BadRequest<T>(Ardalis.Result.IResult result)
        where T : class
    {
        Dictionary<string, string[]> groupedErrors;
        if (result.ValidationErrors.Any())
        {
            groupedErrors = result
                .ValidationErrors.GroupBy(e => e.Identifier)
                .ToDictionary(g => StringUtility.ToCamelCase(g.Key), g => g.Select(e => e.ErrorMessage).ToArray());
        }
        else
        {
            groupedErrors = new Dictionary<string, string[]> { { "message", new[] { result.Errors.FirstOrDefault() ?? "Bad request." } } };
        }

        return TypedResults.Json(
            new ApiResponse<T> { Success = false, Errors = groupedErrors },
            statusCode: StatusCodes.Status400BadRequest,
            contentType: "application/json"
        );
    }

    private static JsonHttpResult<ApiResponse<T>> ToHttpResults<T>(Ardalis.Result.IResult result, int statusCode, string message)
        where T : class
    {
        Dictionary<string, string[]> groupedErrors;
        if (result.ValidationErrors.Any())
        {
            groupedErrors = result
                .ValidationErrors.GroupBy(e => e.Identifier)
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

        return TypedResults.Json(new ApiResponse<T> { Success = false, Errors = groupedErrors }, statusCode: statusCode, contentType: "application/json");
    }
}

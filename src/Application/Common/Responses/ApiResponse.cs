using Ardalis.Result;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Application.Common.Responses;

/// <summary>
/// Represents an API response with a result and optional pagination information.
/// </summary>
public class ApiResponse<T> where T : class
{
    /// <summary>
    /// Gets or sets a value indicating whether the API response is successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the data of the API response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the errors of the API response.
    /// </summary>
    public List<Dictionary<string, string[]>>? Errors { get; set; } = new();

    /// <summary>
    /// Creates an ApiResponse from a Result.
    /// </summary>
    /// <param name="result">The result to create the ApiResponse from.</param>
    /// <returns>An ApiResponse.</returns>
    public static ApiResponse<T> FromResult(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new ApiResponse<T>
            {
                Data = result.Value,
                Success = true,
                Errors = null
            };
        }

        if (result.ValidationErrors.Any())
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = false,
                Errors = result.ValidationErrors.Select(e => new Dictionary<string, string[]> { { StringUtility.ToCamelCase(e.Identifier), new[] { e.ErrorMessage } } }).ToList()
            };
        }
        return new ApiResponse<T>
        {
            Data = default,
            Success = false,
            Errors = result.Errors.Select(e => new Dictionary<string, string[]> { { "message", new[] { e } } }).ToList()
        };


    }


}
using System.Collections.Generic;
using System.Linq;
using Ardalis.Result;

namespace Platform.Application.Common.Responses;

/// <summary>
/// Represents an API response with a result and optional pagination information.
/// </summary>
public class ApiResponse<T>
    where T : class
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
    public Dictionary<string, string[]>? Errors { get; set; }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Application.Common.Responses;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure global exception handling middleware for the application.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger to use for logging exceptions.</param>
    /// <param name="env">The web host environment to use for determining if the application is in development mode.</param>
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions and write the response.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            _logger.LogError(ex, message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            if (ex is UniqueConstraintException err)
            {
                message = $"Unique constraint {err.ConstraintName} violated. Duplicate value for {err.ConstraintProperties[0]}";
                context.Response.StatusCode = 409;
            }
            if (ex is CannotInsertNullException notNullConstraintException)
            {
                message = notNullConstraintException.Message;
                context.Response.StatusCode = 400;
            }
            if (ex is NumericOverflowException numericOverflowException)
            {
                message = numericOverflowException.Message;
                context.Response.StatusCode = 400;
            }
            if (ex is MaxLengthExceededException maxLengthExceededException)
            {
                message = "Max length exceeded for some field";
                context.Response.StatusCode = 400;
            }
            if (ex is ReferenceConstraintException referenceConstraintException)
            {
                message = referenceConstraintException.Message;
                context.Response.StatusCode = 400;
            }

            var errors = new Dictionary<string, string[]> { { "message", [message] } };

            if (_env.IsDevelopment())
            {
                errors.Add("stackTrace", [ex.StackTrace ?? string.Empty]);
            }

            var response = new ApiResponse<object>
            {
                Success = false,
                Data = null,
                Errors = errors,
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

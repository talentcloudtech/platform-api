using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for validating requests in MediatR, using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the request handler.</typeparam>
public class ValidationResultPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResultPipelineBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve dependencies.</param>
    public ValidationResultPipelineBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Handles the validation of the request before passing it to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="next">The delegate representing the next action in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the response.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                // Due to not wanting to use reflection, we assume that every request
                // that wants to validate something also returns a result.
                // Using implicit casts, we are able to use this same behavior for all of them.
                return (TResponse)(dynamic)Result.Invalid(result.AsErrors());
            }
        }

        return await next();
    }
}

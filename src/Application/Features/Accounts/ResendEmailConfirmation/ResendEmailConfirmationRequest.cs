using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.ResendEmailConfirmation;

/// <summary>
/// Represents a request to resend an email confirmation.
/// </summary>
public record ResendEmailConfirmationRequest(string Email) : IRequest<Result>;

using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.ForgotPassword;

/// <summary>
/// Request to initiate the password reset process.
/// </summary>
/// <param name="Email">The email address of the user requesting the password reset.</param>
public record ForgotPasswordRequest(string Email) : IRequest<Result>;

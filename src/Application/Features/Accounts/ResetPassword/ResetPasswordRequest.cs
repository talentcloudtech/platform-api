using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.ResetPassword;

/// <summary>
/// Request to reset the password for a user.
/// </summary>
/// <param name="Email">The email address of the user.</param>
/// <param name="ResetCode">The code used to verify the user's identity.</param>
/// <param name="NewPassword">The new password to set for the user.</param>
public record ResetPasswordRequest(string Email, string ResetCode, string NewPassword) : IRequest<Result>;

using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.RegisterAccount;

/// <summary>
/// Request to register a new user account.
/// </summary>
/// <param name="UserName">The username for the new account.</param>
/// <param name="Email">The email address for the new account.</param>
/// <param name="Password">The password for the new account.</param>
public record RegisterAccountRequest(string UserName, string Email, string Password) : IRequest<Result>;

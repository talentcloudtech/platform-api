using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Accounts.RegisterAccount;

/// <summary>
/// Request to register a new user account.
/// </summary>
/// <param name="UserName">The username for the new account.</param>
/// <param name="Email">The email address for the new account.</param>
/// <param name="Password">The password for the new account.</param>
/// <param name="Role">The role for the new account.</param>
/// <param name="OrganizationName">The name of the organization for the new account.</param>
public record RegisterAccountRequest(string UserName, string Email, string Password, string Role, string? OrganizationName) : IRequest<Result>;

using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.UserRegister;

/// <summary>
/// Represents a request for user registration.
/// </summary>
public record UserRegisterRequest : IRequest<Result<UserRegisterResponse>>
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the username for the account.
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the account.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets the password confirmation.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}

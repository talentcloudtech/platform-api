using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.ChangePassword;

/// <summary>
/// Represents a request to change the password of a user.
/// </summary>
public record ChangePasswordRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the current password of the user.
    /// </summary>
    public required string CurrentPassword { get; init; }

    /// <summary>
    /// Gets or sets the new password to be set for the user.
    /// </summary>
    public required string NewPassword { get; init; }
};

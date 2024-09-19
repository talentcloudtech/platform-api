using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.ChangeEmail;

/// <summary>
/// Represents a request to change the email address of a user.
/// </summary>
/// <param name="NewEmail">The new email address to be set for the user.</param>
/// <param name="Password">The password of the user.</param>
public record ChangeEmailRequest(string NewEmail, string Password) : IRequest<Result<string>>
{
    /// <summary>
    /// Gets or sets the ID of the user whose email is to be changed.
    /// </summary>
    /// <remarks>
    /// This property is automatically set to the UserId value during deserialization.
    /// </remarks>
    [JsonIgnore]
    public UserId UserId { get; init; }
}

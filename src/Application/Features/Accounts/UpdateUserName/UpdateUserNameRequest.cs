using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.UpdateUserName;

/// <summary>
/// Request to update the username.
/// </summary>
/// <param name="UserName">The new username.</param>
public record UpdateUsernameRequest(string UserName) : IRequest<Result<string>>
{
    /// <summary>
    /// Gets or sets the ID of the user to update.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }
}

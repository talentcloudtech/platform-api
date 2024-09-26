using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.UpdateUserFullName;

/// <summary>
/// Request to update the full name of a user.
/// </summary>
/// <param name="FullName">The new full name to set for the user.</param>
public record UpdateUserFullNameRequest(string FullName) : IRequest<Result<CommonUserResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the user to update.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }
}

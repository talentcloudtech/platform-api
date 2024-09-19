using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.DeleteAccount;

/// <summary>
/// Represents a request to delete a user account.
/// </summary>
/// <param name="UserName">The username of the user to delete.</param>
/// <param name="Password">The password of the user to delete.</param>
public record DeleteAccountRequest(string UserName, string Password) : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the ID of the user to delete.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }
}

using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.DeleteAccount;

/// <summary>
/// Represents a request to delete a user account.
/// </summary>
public record DeleteAccountRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the ID of the user to delete.
    /// </summary>
    public UserId UserId { get; set; }
}

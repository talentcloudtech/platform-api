using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.GetAccountDetails;

/// <summary>
/// Represents a request to get account details.
/// </summary>
public class GetAccountDetailsRequest : IRequest<Result<GetAccountDetailsResponse>>
{
    /// <summary>
    /// Gets or sets the user ID for which to retrieve account details.
    /// </summary>
    public UserId UserId { get; set; } = string.Empty;
}

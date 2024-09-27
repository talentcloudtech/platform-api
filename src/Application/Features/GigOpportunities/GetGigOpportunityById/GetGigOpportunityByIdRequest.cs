using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigOpportunities.GetGigOpportunityById;

/// <summary>
/// Request to get a gig opportunity by id
/// </summary>
/// <param name="memberRequestId">the member request id which is gig opportunity id</param>
public record GetGigOpportunityByIdRequest(MemberRequestId memberRequestId) : IRequest<Result<GigOpportunityResponse>>;

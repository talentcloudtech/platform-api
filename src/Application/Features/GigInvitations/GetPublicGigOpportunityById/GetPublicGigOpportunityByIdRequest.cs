using Ardalis.Result;
using MediatR;
using Platform.Application.Features.MemberRequests;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigInvitations.GetPublicGigOpportunityById;

/// <summary>
/// Request to get a gig opportunity by id
/// </summary>
/// <param name="memberRequestId">the member request id which is gig opportunity id</param>
public record GetPublicGigOpportunityByIdRequest(MemberRequestId memberRequestId) : IRequest<Result<MemberRequestResponse>>;

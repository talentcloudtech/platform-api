using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigInvitations.GetGigInvitationById;

/// <summary>
/// Request to get a gig invitation by its unique identifier.
/// </summary>
/// <param name="MemberRequestId">The unique identifier of the member request.</param>
/// <param name="Id">The unique identifier of the gig invitation.</param>
public record GetGigInvitationByIdRequest(MemberRequestId MemberRequestId, GigApplicantInvitationId Id) : IRequest<Result<GigInvitationResponse>>;

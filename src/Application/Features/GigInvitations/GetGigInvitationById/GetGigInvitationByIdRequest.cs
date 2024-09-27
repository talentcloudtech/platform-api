using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigInvitations.GetGigInvitationById;

/// <summary>
/// Request to get a gig invitation by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the gig invitation.</param>
public record GetGigInvitationByIdRequest(GigApplicantInvitationId Id) : IRequest<Result<GigInvitationResponse>>;

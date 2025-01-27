using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Application.Features.MemberRequests;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigInvitations.GetUserGigInvitation;

/// <summary>
/// Request to get a paginated list of gig invitations for a user.
/// </summary>
/// <param name="Search">The search query.</param>
/// <param name="SortBy">The field to sort by.</param>
/// <param name="SortOrder">The order to sort by.</param>
/// <param name="Statuses">The statuses to filter by.</param>
/// <param name="CurrentPage">The current page.</param>
/// <param name="PageSize">The page size.</param>
public record GetUserGigInvitationRequest(
    string Search = "",
    string SortBy = nameof(GigApplicantInvitation.CreatedDate),
    string SortOrder = "desc",
    GigApplicantInvitationStatus[]? Statuses = null,
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<MemberRequestResponse>>>
{
    /// <summary>
    /// A list of invitation statuses to filter the results. If empty, all statuses will be included.
    /// </summary>
    public GigApplicantInvitationStatus[] Statuses { get; set; } = Statuses ?? [];
}

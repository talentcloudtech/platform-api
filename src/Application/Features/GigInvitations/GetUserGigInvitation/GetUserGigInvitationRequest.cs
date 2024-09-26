using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
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
) : IRequest<Result<PaginatedList<GigInvitationResponse>>>
{
    /// <summary>
    /// The unique identifier of the member request.
    /// This property is ignored during JSON serialization/deserialization.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }
    
    /// <summary>
    /// The unique identifier of the user requesting their gig invitations.
    /// This property is ignored during JSON serialization/deserialization.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; set; }

    /// <summary>
    /// A list of invitation statuses to filter the results. If empty, all statuses will be included.
    /// </summary>
    public GigApplicantInvitationStatus[] Statuses { get; set; } = Statuses ?? [];
}

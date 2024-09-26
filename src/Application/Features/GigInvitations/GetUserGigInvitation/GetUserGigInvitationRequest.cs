using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.GigInvitations.GetUserGigInvitation;

/// <summary>
/// Request to get a paginated list of gig invitations for a user.
/// </summary>
public record GetUserGigInvitationRequest : IRequest<Result<PaginatedList<GigInvitationResponse>>>
{
    /// <summary>
    /// The unique identifier of the user requesting their gig invitations.
    /// This property is ignored during JSON serialization/deserialization.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; set; }

    /// <summary>
    /// The current page number for pagination. Defaults to 1.
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// The number of items to display per page. Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// A search string to filter the gig invitations. Defaults to an empty string.
    /// </summary>
    public string Search { get; set; } = string.Empty;

    /// <summary>
    /// The field name to sort the results by. Defaults to the creation date of the invitation.
    /// </summary>
    public string SortBy { get; set; } = nameof(GigApplicantInvitation.CreatedDate);

    /// <summary>
    /// The order to sort the results in ("asc" for ascending, "desc" for descending). Defaults to descending.
    /// </summary>
    public string SortOrder { get; set; } = "desc";

    /// <summary>
    /// A list of invitation statuses to filter the results. If empty, all statuses will be included.
    /// </summary>
    public List<GigApplicantInvitationStatus> Statuses { get; set; } = [];
}

using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Finance.GetUpcoming;

/// <summary>
/// Request to get upcoming finance details
/// </summary>
public record GetUpcomingFinanceRequest(
    WorkspaceId WorkspaceId,
    string? Search = null,
    string SortBy = "DueDate",
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10,
    ProjectId? ProjectId = null,
    TaskStatus[]? Statuses = null
) : IRequest<Result<PaginatedList<UpcomingFinanceResponse>>>;

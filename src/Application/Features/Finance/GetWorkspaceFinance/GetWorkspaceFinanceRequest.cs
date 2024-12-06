using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Finance.GetWorkspaceFinance;

/// <summary>
/// Request to get workspace finance details
/// </summary>
public record GetWorkspaceFinanceRequest(
    WorkspaceId WorkspaceId,
    string? Search = null,
    string SortBy = "DueDate",
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10,
    ProjectId? ProjectId = null,
    TaskStatus[]? Statuses = null
) : IRequest<Result<PaginatedList<WorkspaceFinanceResponse>>>;

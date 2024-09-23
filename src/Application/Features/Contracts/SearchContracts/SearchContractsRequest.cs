using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts.SearchContracts;

/// <summary>
/// Represents a request to search for contracts with optional filtering and pagination.
/// </summary>
/// <param name="Title">The optional title to filter contracts by. If null, no filtering by title is applied.</param>
/// <param name="Statuses">The optional status to filter contracts by. If null, no filtering by status is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <returns>A paginated list of <see cref="ContractResponse"/> matching the search criteria.</returns>
public record SearchContractsRequest(string? Title = null, WorkspaceMemberStatus[]? Statuses = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ContractResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID to filter contracts by.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the list of statuses to filter contracts by.
    /// </summary>
    public WorkspaceMemberStatus[] Statuses { get; init; } = Statuses ?? new[] { WorkspaceMemberStatus.Active };
}

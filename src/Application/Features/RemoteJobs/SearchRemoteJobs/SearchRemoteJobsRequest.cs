using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;

namespace Platform.Application.Features.RemoteJobs.SearchRemoteJobs;

/// <summary>
/// Represents a request to search for remote jobs.
/// </summary>
public record SearchRemoteJobsRequest(
    string? Search = null,
    string? Category = null,
    string? Location = null,
    string? JobType = null,
    string? Status = null,
    string SortBy = nameof(RemoteJob.CreatedDate),
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<RemoteJobResponse>>>;

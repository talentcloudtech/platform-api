using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.RemoteJobs.SearchRemoteJobs;

/// <summary>
/// Handles searching for remote jobs.
/// </summary>
public class SearchRemoteJobsHandler(IContext context) : IRequestHandler<SearchRemoteJobsRequest, Result<PaginatedList<RemoteJobResponse>>>
{

    /// <summary>
    /// Handles the request to search for remote jobs.
    /// </summary>
    public async Task<Result<PaginatedList<RemoteJobResponse>>> Handle(SearchRemoteJobsRequest request, CancellationToken cancellationToken)
    {
        IQueryable<RemoteJob> query = context.RemoteJobs;

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%") ||
                                     EF.Functions.Like(x.CompanyName.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%"));

        // Apply category filter
        if (!string.IsNullOrEmpty(request.Category))
            query = query.Where(x => x.Category == request.Category);

        // Apply location filter
        if (!string.IsNullOrEmpty(request.Location))
            query = query.Where(x => x.Location == request.Location);

        // Apply job type filter
        if (!string.IsNullOrEmpty(request.JobType))
            query = query.Where(x => x.JobType == request.JobType);

        // Apply status filter
        if (!string.IsNullOrEmpty(request.Status))
            query = query.Where(x => x.Status == request.Status);

        // Apply sorting
        query = request.SortOrder.ToLower() == "asc"
            ? query.OrderBy(i => EF.Property<object>(i, request.SortBy))
            : query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var remoteJobs = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<RemoteJobResponse>
        {
            CurrentPage = remoteJobs.CurrentPage,
            TotalPages = remoteJobs.TotalPages,
            TotalItems = remoteJobs.TotalItems,
            Result = remoteJobs.Result.Select(x => x.Adapt<RemoteJobResponse>()).ToList()
        };

        return Result.Success(paginatedResponse);
    }
}

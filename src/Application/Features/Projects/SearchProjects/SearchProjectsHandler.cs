using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Handles the request to search for projects with optional filtering and pagination.
/// </summary>
public class SearchProjectsHandler : IRequestHandler<SearchProjectsRequest, PaginatedList<ProjectResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchProjectsHandler"/> class.
    /// </summary>
    /// <param name="context">The context to access the database.</param>
    public SearchProjectsHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the request to search for projects based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating projects.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
    public async Task<PaginatedList<ProjectResponse>> Handle(
        SearchProjectsRequest request,
        CancellationToken cancellationToken
    )
    {
        var projects = _context
            .Projects.WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(request.Status != null, x => x.Status == request.Status);

        return await projects
            .ProjectToType<ProjectResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}

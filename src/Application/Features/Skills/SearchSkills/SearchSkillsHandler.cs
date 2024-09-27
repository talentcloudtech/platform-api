using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;

namespace Platform.Application.Features.Skills.SearchSkills;

/// <summary>
/// Handles the request to search for skills with optional filtering and pagination.
/// </summary>
public class SearchSkillsHandler(IContext context) : IRequestHandler<SearchSkillsRequest, Result<PaginatedList<SkillResponse>>>
{
    /// <summary>
    /// Handles the request to search for skills based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating skills.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="SkillResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<SkillResponse>>> Handle(SearchSkillsRequest request, CancellationToken cancellationToken)
    {
        var skills = context
            .Skills.WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name.ToLower().Trim(), $"%{request.Name!.ToLower().Trim()}%"))
            .AsNoTracking();

        var result = await skills.ProjectToType<SkillResponse>().OrderBy(x => x.Name).ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        return Result.Success(result);
    }
}

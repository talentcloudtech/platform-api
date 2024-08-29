using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Projects.GetProjectById;

/// <summary>
/// Handles the request to retrieve a project by its unique identifier.
/// </summary>
public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdRequest, Result<ProjectResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context used to access projects.</param>
    public GetProjectByIdHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the retrieval of a project by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the project to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="ProjectResponse"/> if the project is found,
    /// or a not found result if the project does not exist.
    /// </returns>
    public async Task<Result<ProjectResponse>> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (project is null)
            return Result.NotFound();

        return project.Adapt<ProjectResponse>();
    }
}

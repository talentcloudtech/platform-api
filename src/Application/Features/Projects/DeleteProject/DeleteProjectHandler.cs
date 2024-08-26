using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Projects.DeleteProject;

/// <summary>
/// Handles the request to delete a project.
/// </summary>
public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, Result>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public DeleteProjectHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the request to delete a project.
    /// </summary>
    /// <param name="request">The delete project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        var hero = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (hero is null)
            return Result.NotFound();
        _context.Projects.Remove(hero);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

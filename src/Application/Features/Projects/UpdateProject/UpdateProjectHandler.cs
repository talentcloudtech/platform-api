using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Projects.UpdateProject;

/// <summary>
/// Handles the update project request.
/// </summary>
public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, Result<ProjectResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UpdateProjectHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the update project request.
    /// </summary>
    /// <param name="request">The update project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<ProjectResponse>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var originalProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalProject == null)
            return Result.NotFound();

        originalProject.Name = request.Name;
        // originalProject.Description = request.Description;
        // originalProject.Status = request.Status;

        _context.Projects.Update(originalProject);

        await _context.SaveChangesAsync(cancellationToken);

        return originalProject.Adapt<ProjectResponse>();
    }
}

using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Projects.CreateProject;

/// <summary>
/// Handles the creation of a project.
/// </summary>
public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Result<ProjectResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public CreateProjectHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the creation of a project.
    /// </summary>
    /// <param name="request">The create project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the project creation.</returns>
    public async Task<Result<ProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Project>();

        _context.Projects.Add(created);
        await _context.SaveChangesAsync(cancellationToken);

        return created.Adapt<ProjectResponse>();
    }
}

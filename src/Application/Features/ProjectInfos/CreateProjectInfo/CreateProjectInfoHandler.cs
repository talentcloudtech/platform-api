using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectInfos.CreateProjectInfo;

/// <summary>
/// Handles the creation of a new project for a member profile.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class CreateProjectInfoHandler(IContext context) : IRequestHandler<CreateProjectInfoRequest, Result<ProjectInfoResponse>>
{
    /// <summary>
    /// Handles the request to create a new project.
    /// </summary>
    /// <param name="request">The request containing the project details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the created ProjectInfoDto.</returns>
    public async Task<Result<ProjectInfoResponse>> Handle(CreateProjectInfoRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(p => p.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var project = new ProjectInfo
        {
            Identifier = CustomNewId.GenerateStandardId(),
            Title = request.Title,
            Link = request.Link,
            ShortDescription = request.ShortDescription,
            Technologies = request.Technologies,
        };

        if (memberProfile.ProjectInfos == null)
            memberProfile.ProjectInfos = new List<ProjectInfo>();

        memberProfile.ProjectInfos?.Add(project);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(project.Adapt<ProjectInfoResponse>());
    }
}

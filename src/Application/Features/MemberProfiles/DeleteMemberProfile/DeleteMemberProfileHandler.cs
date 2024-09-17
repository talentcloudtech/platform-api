using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.FileStorages.DeleteFile;

namespace Platform.Application.Features.MemberProfiles.DeleteMemberProfile;

/// <summary>
/// Handles the request to delete a member profile.
/// </summary>
public class DeleteMemberProfileHandler(IContext context, IMediator mediator) : IRequestHandler<DeleteMemberProfileRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a member profile.
    /// </summary>
    /// <param name="request">The delete member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (memberProfile is null)
            return Result.NotFound();
        context.MemberProfiles.Remove(memberProfile);
        await context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(memberProfile.ProfilePictureUrl))
            await mediator.Send(new DeleteFileRequest(memberProfile.ProfilePictureUrl), cancellationToken);

        if (!string.IsNullOrWhiteSpace(memberProfile.ResumeFileUrl))
            await mediator.Send(new DeleteFileRequest(memberProfile.ResumeFileUrl), cancellationToken);

        return Result.Success();
    }
}

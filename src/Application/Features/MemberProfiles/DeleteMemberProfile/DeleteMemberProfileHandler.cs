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
public class DeleteMemberProfileHandler : IRequestHandler<DeleteMemberProfileRequest, Result>
{
    private readonly IContext _context;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>

    public DeleteMemberProfileHandler(IContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the request to delete a member profile.
    /// </summary>
    /// <param name="request">The delete member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (memberProfile is null)
            return Result.NotFound();
        _context.MemberProfiles.Remove(memberProfile);
        await _context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(memberProfile.ProfilePictureUrl))
            await _mediator.Send(new DeleteFileRequest(memberProfile.ProfilePictureUrl), cancellationToken);

        if (!string.IsNullOrWhiteSpace(memberProfile.ResumeFileUrl))
            await _mediator.Send(new DeleteFileRequest(memberProfile.ResumeFileUrl), cancellationToken);

        return Result.Success();
    }
}

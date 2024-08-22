using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.DeleteMemberProfile;

/// <summary>
/// Handles the request to delete a member profile.
/// </summary>
public class DeleteMemberProfileHandler : IRequestHandler<DeleteMemberProfileRequest, Result>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public DeleteMemberProfileHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the request to delete a member profile.
    /// </summary>
    /// <param name="request">The delete member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(
            x => x.Id == request.Id,
            cancellationToken
        );
        if (memberProfile is null)
            return Result.NotFound();
        _context.MemberProfiles.Remove(memberProfile);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.Accounts.UpdateAvatar;

/// <summary>
/// Handler for updating the avatar.
/// </summary>
/// <param name="session">The session instance.</param>
/// <param name="context">The context instance.</param>
public class UpdateAvatarHandler(ICurrentSession session, IContext context) : IRequestHandler<UpdateAvatarRequest, Result<string>>
{
    private readonly ICurrentSession _session = session;
    private readonly IContext _context = context;

    /// <summary>
    /// Handles the request to update the avatar.
    /// </summary>
    /// <param name="request">The request containing the file path of the new avatar.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    public async Task<Result<string>> Handle(UpdateAvatarRequest request, CancellationToken cancellationToken)
    {
        var userId = _session.UserId;
        var memberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (memberProfile is null)
            return Result.NotFound("Member profile not found.");

        memberProfile.ProfilePictureUrl = request.Url;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

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
using Platform.Infrastructure.LogtoSdk;
using Platform.Infrastructure.LogtoSdk.Users;

namespace Platform.Application.Features.Accounts.UpdateAvatar;

/// <summary>
/// Handler for updating the avatar.
/// </summary>
/// <param name="session">The session instance.</param>
/// <param name="context">The context instance.</param>
/// <param name="logtoEndpoint">The logto endpoint configuration.</param>
public class UpdateAvatarHandler(ISession session, IContext context, IOptions<LogtoApiOptions> logtoEndpoint)
    : IRequestHandler<UpdateAvatarRequest, Result<string>>
{
    private readonly ISession _session = session;
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
            return Result.NotFound();

        memberProfile.ProfilePictureUrl = request.FilePath;
        await _context.SaveChangesAsync(cancellationToken);

        LogtoUsersClient logtoUsersClient = new LogtoUsersClient(logtoEndpoint);
        var logtoUser = await logtoUsersClient.UpdateUserAsync(userId.ToString(), new LogtoUser { Avatar = request.FilePath });

        return Result.Success();
    }
}

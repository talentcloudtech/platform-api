using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.MemberProfiles.CreateMemberProfile;

/// <summary>
/// Handles the creation of a member profile.
/// </summary>
public class CreateMemberProfileHandler(ICurrentSession currentSession, IContext context)
    : IRequestHandler<CreateMemberProfileRequest, Result<MemberProfileResponse>>
{
    /// <summary>
    /// Handles the creation of a member profile.
    /// </summary>
    /// <param name="request">The create member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the member profile creation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(CreateMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = currentSession.UserId;
        var created = request.Adapt<MemberProfile>();
        created.Id = userId;
        context.MemberProfiles.Add(created);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(created.Adapt<MemberProfileResponse>());
    }
}

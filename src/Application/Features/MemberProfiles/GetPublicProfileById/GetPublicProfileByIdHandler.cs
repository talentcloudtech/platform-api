using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.MemberProfiles.GetPublicProfileById;

/// <summary>
/// Handles the request to retrieve a member profile by its unique identifier.
/// </summary>
public class GetPublicProfileByIdHandler(IContext context) : IRequestHandler<GetPublicProfileByIdRequest, Result<PublicProfileResponse>>
{
    /// <summary>
    /// Handles the retrieval of a member profile by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the member profile to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="PublicProfileResponse"/> if the member profile is found,
    /// or a not found result if the member profile does not exist.
    /// </returns>
    public async Task<Result<PublicProfileResponse>> Handle(GetPublicProfileByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (memberProfile is null)
            return Result.NotFound("Member profile not found.");

        return memberProfile.Adapt<PublicProfileResponse>();
    }
}

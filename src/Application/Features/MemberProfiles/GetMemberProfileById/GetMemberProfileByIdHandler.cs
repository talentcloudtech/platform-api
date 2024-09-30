using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.GetMemberProfileById;

/// <summary>
/// Handles the request to retrieve a member profile by its unique identifier.
/// </summary>
public class GetMemberProfileByIdHandler(IContext context) : IRequestHandler<GetMemberProfileByIdRequest, Result<MemberProfileResponse>>
{
    /// <summary>
    /// Handles the retrieval of a member profile by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the member profile to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="MemberProfileResponse"/> if the member profile is found,
    /// or a not found result if the member profile does not exist.
    /// </returns>
    public async Task<Result<MemberProfileResponse>> Handle(GetMemberProfileByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context
            .MemberProfiles.Include(x => x.User)
            .Include(x => x.Languages)
            .Include(x => x.Skills)
            .Include(x => x.Experiences)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (memberProfile is null)
            return Result.NotFound("Member profile not found.");

        var response = memberProfile.Adapt<MemberProfileResponse>();

        return Result.Success(response);
    }
}

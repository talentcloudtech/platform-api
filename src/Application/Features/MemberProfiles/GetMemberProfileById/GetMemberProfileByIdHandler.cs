using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.GetMemberProfileById;

/// <summary>
/// Handles the request to retrieve a member profile by its unique identifier.
/// </summary>
public class GetMemberProfileByIdHandler : IRequestHandler<GetMemberProfileByIdRequest, Result<MemberProfileResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMemberProfileByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context used to access member profile.</param>
    public GetMemberProfileByIdHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the retrieval of a member profile by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the member profile to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="MemberProfileResponse"/> if the member profile is found,
    /// or a not found result if the member profile does not exist.
    /// </returns>
    public async Task<Result<MemberProfileResponse>> Handle(
        GetMemberProfileByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var memberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(
            x => x.Id == request.Id,
            cancellationToken: cancellationToken
        );

        if (memberProfile is null)
            return Result.NotFound();

        return memberProfile.Adapt<MemberProfileResponse>();
    }
}

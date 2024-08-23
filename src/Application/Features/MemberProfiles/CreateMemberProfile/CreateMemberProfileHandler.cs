using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.MemberProfiles.CreateMemberProfile;

/// <summary>
/// Handles the creation of a member profile.
/// </summary>
public class CreateMemberProfileHandler : IRequestHandler<CreateMemberProfileRequest, Result<MemberProfileResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public CreateMemberProfileHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the creation of a member profile.
    /// </summary>
    /// <param name="request">The create member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the member profile creation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(
        CreateMemberProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var created = request.Adapt<MemberProfile>();

        var existHandleName = await _context
            .MemberProfiles.AnyAsync(
                x => x.HandleName.ToLower().Trim() == request.HandleName.ToLower().Trim(),
                cancellationToken
            )
            .ConfigureAwait(false);
        if (existHandleName)
            Result.Error("Duplicate handle name found");

        _context.MemberProfiles.Add(created);
        await _context.SaveChangesAsync(cancellationToken);

        return created.Adapt<MemberProfileResponse>();
    }
}

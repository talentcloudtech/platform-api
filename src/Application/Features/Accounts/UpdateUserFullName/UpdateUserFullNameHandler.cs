using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.UpdateUserFullName;

/// <summary>
/// Handler for updating the full name of a user.
/// </summary>
/// <param name="context">The database context.</param>
public class UpdateUserFullNameHandler(IContext context) : IRequestHandler<UpdateUserFullNameRequest, Result<CommonUserResponse>>
{
    /// <summary>
    /// Updates the full name of the user.
    /// </summary>
    /// <param name="request">The request containing the new username and user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<CommonUserResponse>> Handle(UpdateUserFullNameRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await context.Users.Include(x => x.MemberProfile).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (currentUser == null)
        {
            return Result.NotFound("User not found.");
        }

        MemberProfile? memberProfile = currentUser.MemberProfile;

        if (memberProfile is null)
        {
            memberProfile = new MemberProfile
            {
                Id = request.UserId,
                UserName = currentUser!.UserName,
                FullName = request.FullName,
            };
            context.MemberProfiles.Add(memberProfile);
        }
        else
        {
            memberProfile.FullName = request.FullName;
            context.MemberProfiles.Update(memberProfile);
        }

        await context.SaveChangesAsync(cancellationToken);

        var response = new CommonUserResponse(currentUser!, memberProfile);

        return Result.Success(response);
    }
}

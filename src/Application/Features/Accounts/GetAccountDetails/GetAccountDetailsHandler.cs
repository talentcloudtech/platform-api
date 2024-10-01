using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.MemberProfiles;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Accounts.GetAccountDetails;

/// <summary>
/// Handler for retrieving account details of the current user.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class GetAccountDetailsHandler(IContext context, ICurrentSession session) : IRequestHandler<GetAccountDetailsRequest, Result<GetAccountDetailsResponse>>
{
    /// <summary>
    /// Handles the request to get account details.
    /// </summary>
    /// <param name="request">The request containing the user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the account details if found, or a not found result.</returns>
    public async Task<Result<GetAccountDetailsResponse>> Handle(GetAccountDetailsRequest request, CancellationToken cancellationToken)
    {
        // Retrieve the user from the database, including their member profile
        var user = await context.Users.Include(u => u.MemberProfile).FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        var nameParts = user.MemberProfile?.FullName?.Split(' ');
        var accountDetails = new GetAccountDetailsResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
            FirstName = nameParts?.Length > 0 ? nameParts[0] : user.UserName,
            LastName = nameParts?.Length > 1 ? nameParts[1] : null,
            ProfilePictureUrl = user.MemberProfile?.ProfilePictureUrl,
            UserRole = session.Role!.Name,
            IsProfileComplete = user.MemberProfile?.IsProfileComplete ?? false,
            ProfileCompletenessScore = MemberProfileEvaluator.GetProfileCompletenessScore(user.MemberProfile!),
        };
        return Result.Success(accountDetails);
    }
}

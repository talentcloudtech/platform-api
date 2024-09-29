using System.Linq;
using Platform.Domain.Entities;

namespace Platform.Application.Features.MemberProfiles;

/// <summary>
/// Provides methods for calculating various scores and metrics related to member profiles.
/// </summary>
public static class ProfileScoreCalculator
{
    /// <summary>
    /// Calculates the profile score based on the completeness of the member profile.
    /// </summary>
    /// <param name="profile">The member profile to calculate the score for.</param>
    /// <returns>The profile score as an integer percentage (0-100).</returns>
    public static int CalculateProfileScore(MemberProfile profile)
    {
        int totalFields = 10;
        int completedFields = 0;

        if (!string.IsNullOrWhiteSpace(profile.FullName))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.UserName))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.ShortBio))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.Subtitle))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.ProfilePictureUrl))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.Timezone))
            completedFields++;
        if (!string.IsNullOrWhiteSpace(profile.Address))
            completedFields++;
        if (profile.Languages.Any())
            completedFields++;
        if (profile.Skills.Any())
            completedFields++;
        if (profile.Experiences.Any())
            completedFields++;

        return (int)((double)completedFields / totalFields * 100);
    }
}

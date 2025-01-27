using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Domain.Entities;

namespace Platform.Application.Features.MemberProfiles;

/// <summary>
/// Provides methods for calculating various scores and metrics related to member profiles.
/// </summary>
public static class MemberProfileEvaluator
{
    /// <summary>
    /// A list of checks to determine if a profile field is completed.
    /// </summary>
    private static readonly List<Func<MemberProfile, bool>> ProfileFieldChecks =
        new()
        {
            profile => !string.IsNullOrWhiteSpace(profile.FullName),
            profile => !string.IsNullOrWhiteSpace(profile.ShortBio),
            profile => !string.IsNullOrWhiteSpace(profile.Subtitle),
            // profile => !string.IsNullOrWhiteSpace(profile.ProfilePictureUrl),
            profile => !string.IsNullOrWhiteSpace(profile.Timezone),
            profile => !string.IsNullOrWhiteSpace(profile.Address),
            profile => profile.Languages.Any(),
            profile => profile.Skills.Any(),
        };

    /// <summary>
    /// Checks if the member profile is fully completed.
    /// </summary>
    /// <param name="profile">The member profile to check for completion.</param>
    /// <returns>True if the profile is complete, false otherwise.</returns>
    public static bool IsProfileFullyCompleted(MemberProfile profile)
    {
        return GetCompletedFieldsCount(profile) == ProfileFieldChecks.Count;
    }

    /// <summary>
    /// Calculates the profile score based on the completeness of the member profile.
    /// </summary>
    /// <param name="profile">The member profile to check for completion.</param>
    /// <returns>The profile score as an integer percentage (0-100).</returns>
    public static int GetProfileCompletenessScore(MemberProfile profile)
    {
        int completedFields = GetCompletedFieldsCount(profile);
        return (int)((double)completedFields / ProfileFieldChecks.Count * 100);
    }

    /// <summary>
    /// Counts the number of completed profile fields.
    /// </summary>
    /// <param name="profile">The member profile to check for completion.</param>
    /// <returns>The number of completed fields.</returns>
    private static int GetCompletedFieldsCount(MemberProfile profile)
    {
        return ProfileFieldChecks.Count(check => check(profile));
    }
}

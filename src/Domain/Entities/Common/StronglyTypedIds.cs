using System;
using StronglyTypedIds;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a strongly typed identifier for a Guid.
/// </summary>
public interface IGuid { }

/// <summary>
/// Represents a strongly typed identifier for a user.
/// </summary>
[StronglyTypedId]
public partial struct UserId : IGuid
{
    /// <summary>
    /// Implicitly converts a Guid to a UserId.
    /// </summary>
    /// <param name="guid">The Guid value.</param>
    /// <returns>A new instance of UserId.</returns>
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a project.
/// </summary>
[StronglyTypedId]
public partial struct ProjectId : IGuid
{
    /// <summary>
    /// Implicitly converts a Guid to a ProjectId.
    /// </summary>
    /// <param name="guid">The Guid value.</param>
    /// <returns>A new instance of ProjectId.</returns>
    public static implicit operator ProjectId(Guid guid)
    {
        return new ProjectId(guid);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a member profile.
/// </summary>
[StronglyTypedId]
public partial struct MemberProfileId : IGuid
{
    /// <summary>
    /// Implicitly converts a Guid to a member profile id.
    /// </summary>
    /// <param name="guid">The Guid value.</param>
    /// <returns>A new instance of ProjectId.</returns>
    public static implicit operator MemberProfileId(Guid guid)
    {
        return new MemberProfileId(guid);
    }
}

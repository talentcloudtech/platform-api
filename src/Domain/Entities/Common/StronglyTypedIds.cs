using System;
using StronglyTypedIds;

[assembly: StronglyTypedIdDefaults(Template.String, "string-efcore")]

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a strongly typed identifier for a string.
/// </summary>
public interface IStringId { }

/// <summary>
/// Represents a strongly typed identifier for a user.
/// </summary>
[StronglyTypedId]
public partial struct UserId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a UserId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of UserId.</returns>
    public static implicit operator UserId(string id)
    {
        return new UserId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a user.
/// </summary>
[StronglyTypedId]
public partial struct RoleId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a UserId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of UserId.</returns>
    public static implicit operator RoleId(string id)
    {
        return new RoleId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a project.
/// </summary>
[StronglyTypedId]
public partial struct ProjectId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a ProjectId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ProjectId.</returns>
    public static implicit operator ProjectId(string id)
    {
        return new ProjectId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a refresh token.
/// </summary>
[StronglyTypedId]
public partial struct RefreshTokenId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a RefreshTokenId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of RefreshTokenId.</returns>
    public static implicit operator RefreshTokenId(string id)
    {
        return new RefreshTokenId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a skill.
/// </summary>
[StronglyTypedId]
public partial struct SkillId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a SkillId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of  SkillId.</returns>
    public static implicit operator SkillId(string id)
    {
        return new SkillId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a experience.
/// </summary>
[StronglyTypedId]
public partial struct ExperienceId : IStringId
{
    internal static ExperienceId New()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Implicitly converts a id to a ExperienceId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ExperienceId.</returns>
    public static implicit operator ExperienceId(string id)
    {
        return new ExperienceId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a education.
/// </summary>
[StronglyTypedId]
public partial struct EducationId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a EducationId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of EducationId.</returns>
    public static implicit operator EducationId(string id)
    {
        return new EducationId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a certification.
/// </summary>
[StronglyTypedId]
public partial struct CertificationId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a CertificationId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of CertificationId.</returns>
    public static implicit operator CertificationId(string id)
    {
        return new CertificationId(id);
    }
}

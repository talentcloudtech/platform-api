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
/// Represents a strongly typed identifier for a role.
/// </summary>
[StronglyTypedId]
public partial struct RoleId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a RoleId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of RoleId.</returns>
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
/// Represents a strongly typed identifier for a workspace.
/// </summary>
[StronglyTypedId]
public partial struct WorkspaceId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a WorkspaceId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of WorkspaceId.</returns>
    public static implicit operator WorkspaceId(string id)
    {
        return new WorkspaceId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a workspace member.
/// </summary>
[StronglyTypedId]
public partial struct WorkspaceMemberId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a WorkspaceMemberId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of WorkspaceMemberId.</returns>
    public static implicit operator WorkspaceMemberId(string id)
    {
        return new WorkspaceMemberId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a contract.
/// </summary>
[StronglyTypedId]
public partial struct ContractId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a ContractId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ContractId.</returns>
    public static implicit operator ContractId(string id)
    {
        return new ContractId(id);
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
    /// <returns>A new instance of SkillId.</returns>
    public static implicit operator SkillId(string id)
    {
        return new SkillId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for an experience.
/// </summary>
[StronglyTypedId]
public partial struct ExperienceId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to an ExperienceId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ExperienceId.</returns>
    public static implicit operator ExperienceId(string id)
    {
        return new ExperienceId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for an education.
/// </summary>
[StronglyTypedId]
public partial struct EducationId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to an EducationId.
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

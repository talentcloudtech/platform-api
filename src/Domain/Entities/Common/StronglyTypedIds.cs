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
/// Represents a strongly typed identifier for a language.
/// </summary>
[StronglyTypedId]
public partial struct LanguageId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a LanguageId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of LanguageId.</returns>
    public static implicit operator LanguageId(string id)
    {
        return new LanguageId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a member skill.
/// </summary>
[StronglyTypedId]
public partial struct MemberSkillId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MemberSkillId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MemberSkillId.</returns>
    public static implicit operator MemberSkillId(string id)
    {
        return new MemberSkillId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a member's project info.
/// </summary>
[StronglyTypedId]
public partial struct ProjectInfoId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a ProjectInfoId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ProjectInfoId.</returns>
    public static implicit operator ProjectInfoId(string id)
    {
        return new ProjectInfoId(id);
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

/// <summary>
/// Represents a strongly typed identifier for a message reply.
/// </summary>
[StronglyTypedId]
public partial struct MessageReplyId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MessageReplyId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MessageReplyId.</returns>
    public static implicit operator MessageReplyId(string id)
    {
        return new MessageReplyId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a message attachment.
/// </summary>
[StronglyTypedId]
public partial struct MessageAttachmentId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MessageAttachmentId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MessageAttachmentId.</returns>
    public static implicit operator MessageAttachmentId(string id)
    {
        return new MessageAttachmentId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a task.
/// </summary>
[StronglyTypedId]
public partial struct ProjectTaskId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a TaskId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of TaskId.</returns>
    public static implicit operator ProjectTaskId(string id)
    {
        return new ProjectTaskId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a task assignment.
/// </summary>
[StronglyTypedId]
public partial struct ProjectTaskAssignmentId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a TaskAssignmentId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of TaskAssignmentId.</returns>
    public static implicit operator ProjectTaskAssignmentId(string id)
    {
        return new ProjectTaskAssignmentId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a project member.
/// </summary>
[StronglyTypedId]
public partial struct ProjectMemberId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a ProjectMemberId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ProjectMemberId.</returns>
    public static implicit operator ProjectMemberId(string id)
    {
        return new ProjectMemberId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a message.
/// </summary>
[StronglyTypedId]
public partial struct MessageId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MessageId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MessageId.</returns>
    public static implicit operator MessageId(string id)
    {
        return new MessageId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a message recipient.
/// </summary>
[StronglyTypedId]
public partial struct MessageRecipientId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MessageRecipientId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MessageRecipientId.</returns>
    public static implicit operator MessageRecipientId(string id)
    {
        return new MessageRecipientId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a member request.
/// </summary>
[StronglyTypedId]
public partial struct MemberRequestId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a MemberRequestId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of MemberRequestId.</returns>
    public static implicit operator MemberRequestId(string id)
    {
        return new MemberRequestId(id);
    }
}

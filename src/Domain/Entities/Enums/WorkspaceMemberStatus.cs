namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Enum representing the different statuses of a workspace member.
/// </summary>
public enum WorkspaceMemberStatus
{
    /// <summary>
    /// Represents an active workspace member.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Represents an inactive workspace member.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Represents a workspace member who has sent a proposal.
    /// </summary>
    ProposalSent = 3,

    /// <summary>
    /// Represents a workspace member who has been cancelled.
    /// </summary>
    Cancelled = 4,
}

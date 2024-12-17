namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Enum representing the different statuses of a proposal.
/// </summary>
public enum ProposalStatus
{
    /// <summary>
    /// Represents a proposal with pending status.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Represents a proposal with approved status.
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Represents a proposal with rejected status.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// Represents a proposal with expired status.
    /// </summary>
    Expired = 4,
}

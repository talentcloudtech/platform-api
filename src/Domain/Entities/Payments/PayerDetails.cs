using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a payer details entity in the system.
/// </summary>

public class PayerDetails
{
    /// <summary>
    /// Gets or sets the workspace id
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the project id
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the contract id
    /// </summary>
    public required ContractId ContractId { get; set; }

    /// <summary>
    /// Gets or sets the task id
    /// </summary>
    public required ProjectTaskId TaskId { get; set; }
}

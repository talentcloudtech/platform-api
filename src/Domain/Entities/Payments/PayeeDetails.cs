using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a payee details entity in the system.
/// </summary>
public class PayeeDetails
{
    /// <summary>
    /// Gets or sets the order type
    /// </summary>
    public OrderType OrderType { get; set; }

    /// <summary>
    /// Gets or sets the task id
    /// </summary>
    public ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the contract id
    /// </summary>
    public ContractId ContractId { get; set; }

    /// <summary>
    /// Gets or sets the opportunity id
    /// </summary>
    public MemberRequestId OpportunityId { get; set; }
}

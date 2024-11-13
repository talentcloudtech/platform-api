using System.Collections.Generic;
using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a workspace transaction entity in the system.
/// </summary>
public class WorkspaceTransaction : AuditableEntity<WorkspaceTransactionId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace transaction.
    /// </summary>
    public override WorkspaceTransactionId Id { get; set; } = CustomNewId.GenerateStandardId();


    /// <summary>
    /// Gets or sets the project ID associated with the workspace transaction.
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the workspace id
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the transaction type
    /// </summary>
    public required TransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the transaction amount
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the payment method
    /// </summary>
    public required PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets the transaction description
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the transaction id
    /// </summary>
    public required string TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the transaction status
    /// </summary>
    public required TransactionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the payee details
    /// </summary>
    public List<PayeeDetails> PayeeDetails { get; set; } = new List<PayeeDetails>();

    /// <summary>
    /// Gets or sets the user associated with the member profile.
    /// </summary>
    public ApplicationUser? User { get; set; }

    /// <summary>
    /// Gets or sets the created by
    /// </summary>
    public Workspace? Workspace { get; set; }
}

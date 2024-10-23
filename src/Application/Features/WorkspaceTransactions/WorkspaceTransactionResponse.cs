using System;
using Platform.Domain.Entities.Common;
using Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceTransactions;

/// <summary>
/// Represents the response for a workspace transaction operation.
/// </summary>
public class WorkspaceTransactionResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace transaction.
    /// </summary>
    public WorkspaceTransactionId Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the workspace associated with this transaction.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the amount of the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the payment method used for the transaction.
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets the type of the transaction (Credit or Debit).
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the description of the transaction.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the external transaction identifier.
    /// </summary>
    public string? TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the status of the transaction.
    /// </summary>
    public TransactionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the transaction was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}


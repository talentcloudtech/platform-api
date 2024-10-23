using System.Collections.Generic;
using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member transaction entity in the system.
/// </summary>
public class MemberTransaction : AuditableEntity<MemberTransactionId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the member transaction.
    /// </summary>
    public override MemberTransactionId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the user id
    /// </summary>
    public required UserId UserId { get; set; }

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
    /// Gets or sets the payer details
    /// </summary>
    public List<PayerDetails> PayerDetails { get; set; } = new List<PayerDetails>();

    /// <summary>
    /// Gets or sets the user associated with the transaction
    /// </summary>
    public ApplicationUser? User { get; set; }

    /// <summary>
    /// Gets or sets the created by user
    /// </summary>
    public ApplicationUser? CreatedByUser { get; set; }
}

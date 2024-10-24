using System;
using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;
using Platform.Application.Common.Responses;
using System.Collections.Generic;

namespace Platform.Application.Features.MemberTransactions;

/// <summary>
/// Represents the response model for a member transaction.
/// </summary>
public class MemberTransactionResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the member transaction.
    /// </summary>
    public MemberTransactionId Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the member associated with this transaction.
    /// </summary>
    public UserId MemberId { get; set; }

    /// <summary>
    /// Gets or sets the name of the member associated with this transaction.
    /// </summary>
    public required string MemberName { get; set; }

    /// <summary>
    /// Gets or sets the type of the transaction (Credit or Debit).
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the amount of the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the payment method used for the transaction.
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets the description of the transaction.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the external transaction identifier.
    /// </summary>
    public required string TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the status of the transaction.
    /// </summary>
    public TransactionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the transaction was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the transaction.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who created the transaction.
    /// </summary>
    public required string CreatedByName { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated workspace, if applicable.
    /// </summary>
    public WorkspaceId? WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the name of the associated workspace, if applicable.
    /// </summary>
    public required string WorkspaceName { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated project, if applicable.
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the associated project, if applicable.
    /// </summary>
    public required string ProjectName { get; set; }

    /// <summary>
    /// Gets or sets the payer details associated with the transaction.
    /// </summary>
    public List<PayerDetailsResponse> PayerDetails { get; set; } = new List<PayerDetailsResponse>();

}

/// <summary>
/// Represents the response model for a payer details.
/// </summary>
public class PayerDetailsResponse
{
    /// <summary>
    /// Gets or sets the name of the payer.
    /// </summary>
    public string? PayerName { get; set; }

    /// <summary>
    /// Gets or sets the email of the payer.
    /// </summary>
    public string? PayerEmail { get; set; }
}

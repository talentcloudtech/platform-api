namespace Domain.Entities.Enums;

/// <summary>
/// Transaction status enum
/// </summary>
public enum TransactionStatus
{
    /// <summary>
    /// Pending transaction status
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Paid transaction status
    /// </summary>
    Paid = 2,

    /// <summary>
    /// Canceled transaction status
    /// </summary>
    Canceled = 3,

    /// <summary>
    /// Refunded transaction status
    /// </summary>
    Refunded = 4,

    /// <summary>
    /// Success transaction status
    /// </summary>
    Success = 5,

    /// <summary>
    /// Owned transaction status
    /// </summary>
    Owned = 6,

    /// <summary>
    /// Hold transaction status
    /// </summary>
    Hold = 7,

    /// <summary>
    /// Enter into payment transaction status
    /// </summary>
    EnterIntoPayment = 8,
}

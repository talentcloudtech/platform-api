namespace Domain.Entities.Enums;

/// <summary>
/// Payment method enum
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Transfer payment method
    /// </summary>
    Transfer = 1,

    /// <summary>
    /// Stripe payment method
    /// </summary>
    Stripe = 2,

    /// <summary>
    /// Bank payment method
    /// </summary>
    Bank = 3,

    /// <summary>
    /// Withdraw payment method
    /// </summary>
    Withdraw = 4,
}

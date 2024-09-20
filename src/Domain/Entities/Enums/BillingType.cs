namespace Platform.Domain.Enums;

/// <summary>
/// Enum representing the different billing types.
/// </summary>
public enum BillingType
{
    /// <summary>
    /// Represents a fixed billing type, where a set amount is charged regardless of time or usage.
    /// </summary>
    Fixed = 1,

    /// <summary>
    /// Represents an hourly billing type, where charges are based on the number of hours worked or used.
    /// </summary>
    Hourly = 2,

    /// <summary>
    /// Represents a weekly billing type, where charges are applied on a weekly basis.
    /// </summary>
    Weekly = 3,

    /// <summary>
    /// Represents a monthly billing type, where charges are applied on a monthly basis.
    /// </summary>
    Monthly = 4
}
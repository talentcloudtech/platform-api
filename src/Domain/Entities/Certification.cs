using System;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a certification.
/// </summary>
public class Certification
{
    /// <summary>
    /// Gets or sets the name of the certification.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the organization issuing the certification.
    /// </summary>
    public string? OrganizationName { get; set; }

    /// <summary>
    /// Gets or sets the date the certification was issued.
    /// </summary>
    public DateTime IssuedDate { get; set; }

    /// <summary>
    /// Gets or sets the expiry date of the certification, if applicable.
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}

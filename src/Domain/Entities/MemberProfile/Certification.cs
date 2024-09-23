using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a certification.
/// </summary>
public class Certification
{
    /// <summary>
    /// Gets or sets the unique identifier of the certification.
    /// </summary>
    public string Identifier { get; set; } = null!;

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

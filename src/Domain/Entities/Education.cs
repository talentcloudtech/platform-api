using System;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an educational background entry.
/// </summary>
public class Education
{
    /// <summary>
    /// Gets or sets the name of the educational institute.
    /// </summary>
    public string? InstituteName { get; set; }

    /// <summary>
    /// Gets or sets the degree with major.
    /// </summary>
    public string? DegreeWithMajor { get; set; }

    /// <summary>
    /// Gets or sets the start date of the education.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the education.
    /// </summary>
    public DateTime EndDate { get; set; }
}

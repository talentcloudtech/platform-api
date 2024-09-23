using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an educational background entry.
/// </summary>
public class Education
{
    /// <summary>
    /// Gets or sets the unique identifier of the education.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the educational institute.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// Gets or sets the degree with major.
    /// </summary>
    public string? Degree { get; set; }

    /// <summary>
    /// Gets or sets the field of study.
    /// </summary>
    public string? FieldOfStudy { get; set; }

    /// <summary>
    /// Gets or sets the start date of the education.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the education.
    /// </summary>
    public DateTime? EndDate { get; set; }
}

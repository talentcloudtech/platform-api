using System;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a work experience entry.
/// </summary>
public class Experience
{
    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// Gets or sets the industry of the experience.
    /// </summary>
    public string? Industry { get; set; }

    /// <summary>
    /// Gets or sets the start date of the experience.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the experience. Null if currently working.
    /// </summary>
    public DateTime? EndDate { get; set; }
}

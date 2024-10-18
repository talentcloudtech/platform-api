using System.Collections.Generic;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a remote job listing.
/// </summary>
public class RemoteJob : AuditableEntity<RemoteJobId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the remote job.
    /// </summary>
    public override RemoteJobId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the title of the remote job.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the name of the company offering the remote job.
    /// </summary>
    public required string CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the remote job.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the remote job (e.g., Software Development, Marketing).
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the location associated with the remote job (can indicate global, country-specific, etc.).
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the salary range for the remote job.
    /// </summary>
    public string? SalaryRange { get; set; }

    /// <summary>
    /// Gets or sets the type of job (e.g., full-time, part-time, contract).
    /// </summary>
    public string? JobType { get; set; }

    /// <summary>
    /// Gets or sets the URL for applying to the remote job.
    /// </summary>
    public string? ApplyUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the job is remote.
    /// </summary>
    public bool Remote { get; set; } = true;

    /// <summary>
    /// Gets or sets the status of the job (e.g., active, closed).
    /// </summary>
    public string? Status { get; set; } = "active";
}

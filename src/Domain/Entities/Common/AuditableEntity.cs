using System;

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents an auditable entity.
/// </summary>
public class AuditableEntity<TId> : Entity<TId>
{
    /// <summary>
    /// Gets or sets the date when the entity was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the contract was last updated. Null if never updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the contract. Null if never updated.
    /// </summary>
    public UserId? UpdatedBy { get; set; }
}

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a base entity with a generic identifier.
/// </summary>
/// <typeparam name="T">The type of the identifier.</typeparam>
public abstract class Entity<T>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public virtual T Id { get; set; } = default!;
}

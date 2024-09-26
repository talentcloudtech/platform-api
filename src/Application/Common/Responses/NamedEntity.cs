namespace Platform.Application.Common.Responses;

/// <summary>
/// Represents a named entity.
/// </summary>
public class NamedEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    public string Name { get; set; } = null!;
}

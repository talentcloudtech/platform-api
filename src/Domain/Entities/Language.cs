using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a language proficiency entry for a member profile.
/// </summary>
public class Language
{
    /// <summary>
    /// Gets or sets the unique identifier of the language entry.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the proficiency level of the language.
    /// </summary>
    public LanguageLevel ProficiencyLevel { get; set; }
}

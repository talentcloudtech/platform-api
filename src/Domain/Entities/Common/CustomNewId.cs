using NanoidDotNet;

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a custom NewId implementation.
/// </summary>
public class CustomNewId
{
    /// <summary>
    /// The lowercase alphabet used for generating the identifier.
    /// </summary>
    const string lowerCaseAlphabets = "123456789abcdefghijkmnopqrstuvwxyz";

    /// <summary>
    /// English alphabet letters and digits without lookalikes: l, I, 0, O.
    /// </summary>
    const string alphabets = $"{lowerCaseAlphabets}ABCDEFGHJKLMNPQRSTUVWXYZ";

    /// <summary>
    /// Generates a new unique identifier.
    /// </summary>
    /// <returns></returns>
    public static string GenerateStandardId() => NewId(21, false);

    /// <summary>
    /// Generates a new unique identifier with a short length.
    /// </summary>
    /// <returns>The new identifier.</returns>
    public static string GenerateShortId() => NewId(12, false);

    /// <summary>
    /// Generate a standard secret with 32 characters, including uppercase letters, lowercase.
    /// </summary>
    /// <returns>The new identifier.</returns>
    public static string GenerateStandardSecret() => NewId(32, false);

    /// <summary>
    /// Generates a new unique identifier with a long length.
    /// </summary>
    /// <param name="size">The length of the identifier.</param>
    /// <param name="useUpperCase">True to use lowercase letters, false to use uppercase letters.</param>
    /// <returns>The new identifier.</returns>
    private static string NewId(int size, bool useUpperCase)
    {
        var alphabet = useUpperCase ? alphabets : lowerCaseAlphabets;
        return Nanoid.Generate(alphabet, size);
    }
}

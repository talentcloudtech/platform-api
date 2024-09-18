namespace Platform.Application.Common;

/// <summary>
/// Provides utility methods for working with strings.
/// </summary>
public static class StringUtility
{
    /// <summary>
    /// Converts a string to camel case.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The camel case version of the string.</returns>
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        return char.ToLower(str[0]) + str.Substring(1);
    }
}
using System.Collections.Generic;
using System.Text.Json;

namespace Platform.Application.Extensions;

/// <summary>
/// Provides extension methods for working with JSON elements.
/// </summary>
public static class JsonExtension
{
    /// <summary>
    /// Tries to get a property in a JSON element.
    /// </summary>
    /// <param name="element">The JSON element.</param>
    /// <param name="propertyPath">The property path.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>True if the property is found, false otherwise.</returns>
    public static bool TryGetProperty(this JsonElement element, string propertyPath, out JsonElement value)
    {
        value = element;
        foreach (var propertyName in propertyPath.Split('.'))
        {
            if (value.TryGetProperty(propertyName, out JsonElement temp))
            {
                value = temp;
            }
            else
            {
                value = default;
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Gets the value of a string property in a JSON element.
    /// </summary>
    /// <param name="element">The JSON element.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The string value, or null if the property is not found or is not a string.</returns>
    public static string? GetStringValue(this JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out JsonElement propertyValue))
        {
            return propertyValue.GetString();
        }
        return null;
    }

    /// <summary>
    /// Gets the values of an array property in a JSON element.
    /// </summary>
    /// <param name="element">The JSON element.</param>
    /// <param name="arrayName">The name of the array property.</param>
    /// <param name="propertyName">The name of the property in the array.</param>
    /// <returns>A list of string values, or null if the property is not found or is not an array.</returns>
    public static List<string>? GetArrayValues(this JsonElement element, string arrayName, string propertyName)
    {
        if (
            element.TryGetProperty(arrayName, out JsonElement arrayElement)
            && arrayElement.ValueKind == JsonValueKind.Array
        )
        {
            var values = new List<string>();
            foreach (JsonElement item in arrayElement.EnumerateArray())
            {
                if (item.TryGetProperty(propertyName, out JsonElement value))
                {
                    var stringValue = value.GetString();
                    if (stringValue != null)
                    {
                        values.Add(stringValue);
                    }
                }
            }
            return values;
        }
        return null;
    }
}

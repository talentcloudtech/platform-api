using System.Web;

namespace Platform.Application.Common;

/// <summary>
/// Provides utility methods for working with URIs.
/// </summary>
public static class UriUtils
{
    // List of common SAS query parameters to check
    private static readonly string[] SasQueryParameters =
    {
        "sv", // Service version
        "ss", // Services allowed
        "srt", // Resource types
        "sp", // Permissions
        "se", // Expiry time
        "st", // Start time
        "spr", // Protocol
        "sig" // Signature
    };

    /// <summary>
    /// Checks whether the given URI is a SAS (Shared Access Signature) URI.
    /// </summary>
    /// <param name="fileUri">The file URI to check.</param>
    /// <returns>
    /// <c>true</c> if the URI is a SAS URI; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="fileUri"/> is <c>null</c>.
    /// </exception>
    public static bool IsSasUri(string fileUri)
    {
        if (fileUri == null)
        {
            throw new ArgumentNullException(nameof(fileUri));
        }
        var uri = new Uri(fileUri);

        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        return SasQueryParameters.Any(param => queryParameters.AllKeys.Contains(param));
    }
}

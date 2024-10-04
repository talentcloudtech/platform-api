using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace Platform.Application.Common;

/// <summary>
/// Provides helper methods for file-related operations.
/// </summary>
public static class FileHelper
{
    private static readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    /// <summary>
    /// Gets the content type for a given file name.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The content type of the file, or "application/octet-stream" if the type cannot be determined.</returns>
    public static string GetContentType(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
        }

        if (!_contentTypeProvider.TryGetContentType(fileName, out string? contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}

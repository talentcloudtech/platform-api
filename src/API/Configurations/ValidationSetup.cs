using FluentValidation;

namespace Platform.Api.Configurations;

/// <summary>
/// Represents the setup for validation.
/// </summary>
public static class ValidationSetup
{
    /// <summary>
    /// Adds the validation setup.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddValidationSetup(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
    }
}

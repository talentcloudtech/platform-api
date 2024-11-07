using FluentValidation;

namespace Platform.Application.Features.WorkspaceAssets.UpdateWorkspaceAsset;

/// <summary>
/// Validator for UpdateWorkspaceAssetRequest
/// </summary>
public class UpdateWorkspaceAssetValidator : AbstractValidator<UpdateWorkspaceAssetRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWorkspaceAssetValidator"/> class.
    /// </summary>
    public UpdateWorkspaceAssetValidator()
    {
        RuleFor(x => x.FieldsToUpdate).NotEmpty().WithMessage("At least one field must be specified for update");

        When(
            x => x.FieldsToUpdate.Contains("fileName"),
            () =>
            {
                RuleFor(x => x.FileName)
                    .NotEmpty()
                    .WithMessage("File name is required when updating fileName")
                    .MaximumLength(256)
                    .WithMessage("File name cannot exceed 256 characters");
            }
        );
    }
}

using FluentValidation;

namespace Platform.Application.Features.Projects.DeleteProject;

/// <summary>
/// Represents a validator for the DeleteProjectRequest class.
/// </summary>
public class DeleteProjectValidator : AbstractValidator<DeleteProjectRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectValidator"/> class.
    /// </summary>
    public DeleteProjectValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

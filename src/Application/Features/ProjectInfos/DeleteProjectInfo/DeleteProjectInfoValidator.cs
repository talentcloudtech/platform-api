using FluentValidation;

namespace Platform.Application.Features.ProjectInfos.DeleteProjectInfo;

/// <summary>
/// Represents a validator for the DeleteProjectInfoRequest class.
/// </summary>
public class DeleteProjectInfoValidator : AbstractValidator<DeleteProjectInfoRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectInfoValidator"/> class.
    /// </summary>
    public DeleteProjectInfoValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.ProjectInfoId).NotEmpty();
    }
}

using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.ProjectMembers.UpdateProjectMember;

/// <summary>
/// Validator for the <see cref="UpdateProjectMemberRequest"/> class.
/// </summary>
public class UpdateProjectMemberValidator : AbstractValidator<UpdateProjectMemberRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectMemberValidator"/> class.
    /// </summary>
    public UpdateProjectMemberValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.AllocatedMinMinutePerDay).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1440); // 1440 minutes in a day
    }
}

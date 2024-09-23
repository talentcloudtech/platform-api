using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.ProjectMembers.CreateProjectMember;

/// <summary>
/// Validator for the <see cref="CreateProjectMemberRequest"/> class.
/// </summary>
public class CreateProjectMemberValidator : AbstractValidator<CreateProjectMemberRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectMemberValidator"/> class.
    /// </summary>
    public CreateProjectMemberValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.AllocatedMinMinutePerDay).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1440); // 1440 minutes in a day
    }
}

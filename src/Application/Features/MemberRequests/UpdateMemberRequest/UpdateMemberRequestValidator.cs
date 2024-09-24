using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberRequests.UpdateMemberRequest;

/// <summary>
/// Represents a validator for the UpdateMemberRequestRequest class.
/// </summary>
public class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberRequestRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberRequestValidator"/> class.
    /// </summary>
    public UpdateMemberRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
        RuleFor(x => x.ExpectedBudget).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MinutesPerDay).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Timezone).NotEmpty().MaximumLength(StringSizes.Short);
        RuleFor(x => x.RequiredSkills).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
    }
}

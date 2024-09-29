using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberRequests.CreateMemberRequest;

/// <summary>
/// Validator for the <see cref="CreateMemberRequestRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateMemberRequestValidator : AbstractValidator<CreateMemberRequestRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberRequestValidator"/> class.
    /// </summary>
    public CreateMemberRequestValidator()
    {
        RuleFor(x => x.WorkspaceId).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
        RuleFor(x => x.StartDate).NotNull().NotEmpty();
        RuleFor(x => x.EndDate).NotNull().NotEmpty().GreaterThan(x => x.StartDate);
        RuleFor(x => x.ExpectedBudget).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MinutesPerDay).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Timezone).NotNull().NotEmpty().MaximumLength(StringSizes.Short);
        RuleFor(x => x.RequiredSkills).NotNull().NotEmpty();
    }
}

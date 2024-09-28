using FluentValidation;

namespace Platform.Application.Features.GigOpportunities.UpdateGigOpportunityCandidate;

/// <summary>
/// Validator for the <see cref="UpdateGigOpportunityCandidateRequest"/>.
/// </summary>
public class UpdateGigOpportunityCandidateValidator : AbstractValidator<UpdateGigOpportunityCandidateRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGigOpportunityCandidateValidator"/> class.
    /// </summary>
    public UpdateGigOpportunityCandidateValidator()
    {
        RuleFor(x => x.MemberRequestId).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotNull().NotEmpty();
        RuleFor(x => x.Status).NotNull().IsInEnum();
    }
}

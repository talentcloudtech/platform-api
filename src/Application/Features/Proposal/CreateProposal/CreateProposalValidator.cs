using FluentValidation;

namespace Platform.Application.Features.Proposal.CreateProposal;

/// <summary>
/// Validator for AssignProjectMembersRequest
/// </summary>
public class CreateProposalValidator : AbstractValidator<CreateProposalRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProposalValidator"/> class
    /// </summary>
    public CreateProposalValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("Task ID is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.CostEstimation).NotEmpty().WithMessage("Cost estimation is required");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        RuleFor(x => x.EndDate)
            .Must((request, endDate) => !endDate.HasValue || endDate.Value >= request.StartDate)
            .WithMessage("End date must be greater than or equal to the start date.");
    }
}

using FluentValidation;

namespace Platform.Application.Features.GigInvitations.UpdateGigInvitationStatus;

/// <summary>
/// Validator for the <see cref="UpdateGigInvitationStatusRequest"/> class.
/// </summary>
public class UpdateGigInvitationStatusValidator : AbstractValidator<UpdateGigInvitationStatusRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGigInvitationStatusValidator"/> class.
    /// </summary>
    public UpdateGigInvitationStatusValidator()
    {
        RuleFor(x => x.GigInvitationId).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
    }
}

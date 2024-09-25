using FluentValidation;

namespace Platform.Application.Features.WorkspaceMemberInvitations.UpdateWorkspaceMemberInvitation;

/// <summary>
/// Validator for the <see cref="UpdateWorkspaceMemberInvitationRequest"/>.
/// </summary>
public class UpdateWorkspaceMemberInvitationValidator : AbstractValidator<UpdateWorkspaceMemberInvitationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWorkspaceMemberInvitationValidator"/> class.
    /// </summary>
    public UpdateWorkspaceMemberInvitationValidator()
    {
        RuleFor(x => x.WorkspaceMemberInvitationId).NotEmpty();
        RuleFor(x => x.Accept).NotNull();
    }
}

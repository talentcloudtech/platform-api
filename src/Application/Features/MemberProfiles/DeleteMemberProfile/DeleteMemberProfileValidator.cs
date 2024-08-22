using FluentValidation;

namespace Platform.Application.Features.MemberProfiles.DeleteMemberProfile;

/// <summary>
/// Represents a validator for the DeleteProjectRequest class.
/// </summary>
public class DeleteMemberProfileValidator : AbstractValidator<DeleteMemberProfileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMemberProfileValidator"/> class.
    /// </summary>
    public DeleteMemberProfileValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

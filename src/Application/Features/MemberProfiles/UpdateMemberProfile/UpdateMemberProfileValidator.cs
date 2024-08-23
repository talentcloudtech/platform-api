using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.UpdateMemberProfile;

/// <summary>
/// Represents a validator for the UpdateMemberProfileRequest class.
/// </summary>
public class UpdateMemberProfileValidator : AbstractValidator<UpdateMemberProfileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberProfileValidator"/> class.
    /// </summary>
    public UpdateMemberProfileValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.HandleName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.ShortBio).NotEmpty().MaximumLength(StringSizes.Bio);
        RuleFor(x => x.Timezone).NotEmpty();
    }
}

using Platform.Domain.Entities.Common;

namespace Platform.Application.Dto;

/// <summary>
/// DTO for gig applicant invitation send email.
/// </summary>
/// <param name="Id">The ID of the gig applicant invitation.</param>
/// <param name="Email">The email of the invited user.</param>
/// <param name="UserName">The name of the invited user.</param>
public record GigInvitationSendEmailDto(GigApplicantInvitationId Id, string Email, string UserName);

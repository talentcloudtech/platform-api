using Ardalis.Result;
using MediatR;
using Platform.Application.Features.Languages;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberSkills.GetMemberSkillById;

/// <summary>
/// Represents a request to get a member skill by its ID.
/// </summary>
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="MemberSkillId">The unique identifier of the member skill.</param>
public record GetMemberSkillByIdRequest(UserId MemberProfileId, string MemberSkillId) : IRequest<Result<MemberSkillResponse>>;

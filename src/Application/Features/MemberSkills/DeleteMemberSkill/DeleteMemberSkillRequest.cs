using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberSkills.DeleteMemberSkill;

/// <summary>
/// Represents a request to delete a member skill entry.
/// </summary>
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="MemberSkillId">The unique identifier of the member skill entry to delete.</param>
public record DeleteMemberSkillRequest(UserId MemberProfileId, string MemberSkillId) : IRequest<Result>;

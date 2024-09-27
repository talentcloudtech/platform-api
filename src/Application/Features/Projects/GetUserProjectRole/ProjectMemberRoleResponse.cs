using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.GetUserProjectRole;

/// <summary>
/// Response for project member role.
/// </summary>
public class ProjectMemberRoleResponse
{
    /// <summary>
    /// The role of the project member.
    /// </summary>
    public ProjectRole Role { get; set; }
}

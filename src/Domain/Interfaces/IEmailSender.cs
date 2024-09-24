using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Interfaces;

/// <summary>
/// Defines the contract for an email sender.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends a workspace invitation email to the specified email addresses.
    /// </summary>
    /// <param name="workspaceId">The ID of the workspace. </param>
    /// <param name="workspaceName">The name of the workspace.</param>
    /// <param name="emails">The email addresses to send the invitation to. </param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendWorkspaceInvitationAsync(WorkspaceId workspaceId, string workspaceName, List<string> emails);
}

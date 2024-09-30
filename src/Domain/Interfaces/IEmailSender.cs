using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Application.Dto;
using Platform.Domain.Entities;

namespace Platform.Domain.Interfaces;

/// <summary>
/// Defines the contract for an email sender.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends a workspace invitation email to the specified email addresses.
    /// </summary>
    /// <param name="workspaceName">The name of the workspace.</param>
    /// <param name="invitations"> The invitations to send to the users. </param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendWorkspaceInvitationAsync(string workspaceName, List<WorkspaceMemberInvitation> invitations);

    /// <summary>
    /// Sends a gig invitation email to the specified email addresses.
    /// </summary>
    /// <param name="gigTitle">The title of the gig.</param>
    /// <param name="invitations"> The invitations to send to the users. </param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendGigInvitationAsync(string gigTitle, List<GigInvitationSendEmailDto> invitations);
}

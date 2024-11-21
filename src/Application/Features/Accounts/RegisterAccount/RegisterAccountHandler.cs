using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.API.Configurations;
using Platform.Application.Common;
using Platform.Application.Features.Workspaces.CreateWorkspace;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Accounts.RegisterAccount;

/// <summary>
/// Handler for registering a new user account.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="userManager">The user manager to use for creating the user.</param>
/// <param name="emailSender">The email sender to use for sending the confirmation email.</param>
/// <param name="mediator">The mediator to use for sending the request to create a new workspace.</param>
public class RegisterAccountHandler(IContext context, IMediator mediator, UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser> emailSender)
    : IRequestHandler<RegisterAccountRequest, Result>
{
    /// <summary>
    /// Handles the request to register a new user account.
    /// </summary>
    /// <param name="request">The request to register a new user account.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the registration process.</returns>
    public async Task<Result> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName.Trim(),
            Email = request.Email.Trim(),
            CreatedAt = DateTime.UtcNow,
        };

        var role = UserRole.FromString(request.Role);

        if (role == null)
            return Result.Invalid(new ValidationError { Identifier = "root", ErrorMessage = "Invalid role." });

        if (role.Name == UserRole.Admin.Name)
            return Result.Invalid(new ValidationError { Identifier = "root", ErrorMessage = "Admin role is not allowed to be registered by this endpoint." });

        if (role.Name == UserRole.Customer.Name && string.IsNullOrWhiteSpace(request.OrganizationName))
            return Result.Invalid(new ValidationError { Identifier = "root", ErrorMessage = "Organization name is required for enterprise users." });

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result.Invalid(result.Errors.Select(e => new ValidationError { Identifier = "root", ErrorMessage = e.Description }));

        await userManager.AddToRoleAsync(user, role.Name);

        if (role.Name == UserRole.Customer.Name && !string.IsNullOrWhiteSpace(request.OrganizationName))
            await mediator.Send(
                new CreateWorkspaceRequest
                {
                    UserId = user.Id,
                    Role = role,
                    Name = request.OrganizationName,
                    IsDefault = true,
                },
                cancellationToken
            );

        var memberProfile = new MemberProfile
        {
            Id = user.Id,
            UserName = user.UserName,
            FullName = request.FullName?.Trim(),
        };

        await context.MemberProfiles.AddAsync(memberProfile, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // send email to user
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);
        var confirmationLink = $"{EnvironmentVariables.BackendUrl}/api/accounts/confirmEmail?userId={user.Id}&code={encodedToken}";
        await emailSender.SendConfirmationLinkAsync(user, user.Email, confirmationLink);

        return Result.Success();
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Application.Features.FileStorages.MoveFile;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.CreateAccount;

/// <summary>
/// Handles the creation of a member profile.
/// </summary>
public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, Result>
{
    private readonly IContext _context;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CreateAccountHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAccountHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="userManager">The user manager.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="logger">The logger.</param>
    public CreateAccountHandler(
        IContext context,
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger<CreateAccountHandler> logger
    )
    {
        _context = context;
        _mediator = mediator;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Handles the creation of a member profile.
    /// </summary>
    /// <param name="request">The create member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the member profile creation.</returns>
    public async Task<Result> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var context = request.Context;
        if (context is null)
            return Result.Error("Context is null");

        var receivedSecret = context.Request.Headers["logto-signature-sha-256"].FirstOrDefault();
        var singingKey = _configuration["WebhookSecret:LogtoSecretKey"];
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

        if (
            string.IsNullOrEmpty(receivedSecret)
            || string.IsNullOrEmpty(singingKey)
            || !SecurityHelper.VerifyHMACSHA256Signature(singingKey, requestBody, receivedSecret)
        )
        {
            return Result.Unauthorized();
        }
        var webhookRequestBody = JsonSerializer.Deserialize<LogtoWebhookRequest>(requestBody);
        switch (webhookRequestBody?.Event)
        {
            case "PostSignIn":
                var user = webhookRequestBody?.User;
                if (user is null)
                    return Result.Error("User is null");

                // add Identity user table as well if not exists
                var userExists = await _userManager.FindByIdAsync(user.Id);
                if (userExists is null)
                {
                    var identityUser = new ApplicationUser
                    {
                        Id = user.Id,
                        UserName = user.Username,
                        Email = user.PrimaryEmail,
                    };
                    await _userManager.CreateAsync(identityUser);
                }

                var memberProfile = new MemberProfile
                {
                    Id = user.Id,
                    FullName = user.Name,
                    ProfilePictureUrl = user.Avatar
                };
                _context.MemberProfiles.Add(memberProfile);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            case "user.created":
                break;
            case "user.suspended":
                break;
            case "user.updated":
                break;
            case "user.deleted":
                break;
        }

        return Result.Success();
    }
}

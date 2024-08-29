using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Platform.Application.Common;
using Platform.Application.Extensions;
using Platform.Domain.Entities;

namespace Platform.Application.Features.WebhookProcessors.LogtoWebhook;

/// <summary>
/// Handles the creation of a member profile.
/// </summary>
public class LogtoWebhookHandler : IRequestHandler<LogtoWebhookRequest, Result>
{
    private readonly IContext _context;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LogtoWebhookHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogtoWebhookHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="userManager">The user manager.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="logger">The logger.</param>
    public LogtoWebhookHandler(
        IContext context,
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger<LogtoWebhookHandler> logger
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
    public async Task<Result> Handle(LogtoWebhookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received webhook request");
        _logger.LogInformation("event received: {rawBody}", request.RawBody);
        _logger.LogInformation(request.Signature);

        var singingKey = _configuration["WebhookSecret:LogtoSecretKey"];
        _logger.LogInformation(singingKey);
        var createSignature = SecurityHelper.GenerateHMACSHA256Signature(singingKey, request.RawBody);
        if (
            string.IsNullOrEmpty(request.Signature)
            || string.IsNullOrEmpty(singingKey)
            || createSignature != request.Signature
        )
        {
            _logger.LogError(
                "Invalid signature received {signature} and computed {computed}",
                request.Signature,
                createSignature
            );
            return Result.Unauthorized();
        }
        _logger.LogInformation("Signature is valid");

        var webhookRequestBody = JsonSerializer.Deserialize<LogtoWebhookRequestBody>(
            request.RawBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        _logger.LogInformation("Received webhook request body {body}", JsonSerializer.Serialize(webhookRequestBody));

        switch (webhookRequestBody?.Event)
        {
            case "PostSignIn":
                _logger.LogInformation("PostSignIn event received");
                _logger.LogInformation(webhookRequestBody?.UserId);
                _logger.LogInformation("Received user {User}", JsonSerializer.Serialize(webhookRequestBody?.User));
                var userExists = await _userManager.FindByIdAsync(webhookRequestBody.UserId);
                var user = webhookRequestBody?.User;
                if (user is null)
                    return Result.Error("User is null");

                if (userExists is not null)
                {
                    return Result.Success();
                }

                var identityUser = new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.Username ?? user.Id,
                    Email = user.PrimaryEmail,
                };

                var memberProfile = new MemberProfile
                {
                    Id = user.Id,
                    FullName = user.Name,
                    ProfilePictureUrl = user.Avatar,
                };

                EnhanceUserDetails(identityUser, memberProfile, user);

                var result = await _userManager.CreateAsync(identityUser);
                if (!result.Succeeded)
                    return Result.Error("Failed to create user");

                _context.MemberProfiles.Add(memberProfile);
                await _context.SaveChangesAsync(cancellationToken);

                break;
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

    /// <summary>
    /// Enhances the user details in the application user and member profile entities.
    /// </summary>
    /// <param name="applicationUser">The application user entity.</param>
    /// <param name="memberProfile">The member profile entity.</param>
    /// <param name="userEntity">The user entity from Logto.</param>
    private void EnhanceUserDetails(
        ApplicationUser applicationUser,
        MemberProfile memberProfile,
        LogtoUserEntityResponse userEntity
    )
    {
        try
        {
            if (userEntity.Identities == null)
            {
                return;
            }
            var jsonString = userEntity.Identities.ToString();
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return;
            }
            using JsonDocument document = JsonDocument.Parse(jsonString);

            var root = document.RootElement;
            if (root.TryGetProperty("github.details.rawData.userInfo", out JsonElement githubRawUserInfo))
            {
                var githubLink = githubRawUserInfo.GetStringValue("html_url");
                memberProfile.ExternalLinks = !string.IsNullOrEmpty(githubLink) ? [githubLink] : Array.Empty<string>();

                var githubUsername = githubRawUserInfo.GetStringValue("login");

                applicationUser.UserName = !string.IsNullOrEmpty(githubUsername)
                    ? githubUsername
                    : applicationUser.UserName;

                // assume email is confirmed
                applicationUser.EmailConfirmed = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enhance user details");
            return;
        }
    }
}

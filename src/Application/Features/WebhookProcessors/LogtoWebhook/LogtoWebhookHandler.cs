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
/// Handles webhook events related to logto.
/// </summary>
public class LogtoWebhookHandler : IRequestHandler<LogtoWebhookRequest, Result>
{
    private readonly IContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LogtoWebhookHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogtoWebhookHandler"/> class.
    /// </summary>
    public LogtoWebhookHandler(IContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<LogtoWebhookHandler> logger)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Handles the webhook request to create or update a member profile.
    /// </summary>
    /// <param name="request">The webhook request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(LogtoWebhookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Signature: {Signature}", request.Signature);

        var signingKey = _configuration["WebhookSecret:LogtoSecretKey"];

        if (string.IsNullOrEmpty(request.Signature) || string.IsNullOrEmpty(signingKey) || string.IsNullOrEmpty(request.RawBody))
        {
            _logger.LogDebug("Signature is empty or signing key or body is empty");
            return Result.Invalid();
        }

        var computedSignature = SecurityHelper.GenerateHMACSHA256Signature(signingKey, request.RawBody);
        if (computedSignature != request.Signature)
        {
            _logger.LogError("Invalid signature received: {ReceivedSignature}, computed: {ComputedSignature}", request.Signature, computedSignature);
            return Result.Unauthorized();
        }

        _logger.LogInformation("Signature validated successfully");

        var webhookRequestBody = JsonSerializer.Deserialize<LogtoWebhookRequestBody>(
            request.RawBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        _logger.LogInformation("Received webhook event: {event}", webhookRequestBody?.Event);

        if (webhookRequestBody?.Event == "PostSignIn")
        {
            return await HandlePostSignInEvent(webhookRequestBody, cancellationToken);
        }

        return Result.Success();
    }

    /// <summary>
    /// Handles the PostSignIn event by creating or updating the user profile.
    /// </summary>
    /// <param name="webhookRequestBody">The webhook request body.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    private async Task<Result> HandlePostSignInEvent(LogtoWebhookRequestBody webhookRequestBody, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing PostSignIn event for User ID: {UserId}", webhookRequestBody?.UserId);
        if (string.IsNullOrEmpty(webhookRequestBody?.UserId))
        {
            return Result.Error("User ID is null or empty");
        }

        var userExists = await _userManager.FindByIdAsync(webhookRequestBody.UserId);

        // already exists
        if (userExists is not null)
        {
            return Result.Success();
        }

        var user = webhookRequestBody?.User;

        if (user is null)
            return Result.Error("User is null");

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

        var creationResult = await _userManager.CreateAsync(identityUser);
        if (!creationResult.Succeeded)
            return Result.Error("Failed to create user");

        _context.MemberProfiles.Add(memberProfile);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Enhances the application user and member profile entities with additional details.
    /// </summary>
    /// <param name="applicationUser">The application user entity to enhance.</param>
    /// <param name="memberProfile">The member profile entity to enhance.</param>
    /// <param name="userEntity">The user entity from Logto to enhance.</param>
    private void EnhanceUserDetails(ApplicationUser applicationUser, MemberProfile memberProfile, LogtoUserEntityResponse userEntity)
    {
        try
        {
            if (userEntity.Identities == null)
                return;
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
                memberProfile.ExternalLinks = !string.IsNullOrEmpty(githubLink) ? new[] { githubLink } : Array.Empty<string>();

                var githubUsername = githubRawUserInfo.GetStringValue("login");
                applicationUser.UserName = !string.IsNullOrEmpty(githubUsername) ? githubUsername : applicationUser.UserName;

                applicationUser.EmailConfirmed = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enhance user details");
        }
    }
}

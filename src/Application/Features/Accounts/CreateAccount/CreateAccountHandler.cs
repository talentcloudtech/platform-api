using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        // var receivedSecret = context.Request.Headers["logto-signature-sha-256"].FirstOrDefault();
        var singingKey = _configuration["WebhookSecret:LogtoSecretKey"];
        var requestBody = JsonSerializer.Serialize(request.LogtoWebhookRequest);

        if (
            string.IsNullOrEmpty(request.Signature)
            || string.IsNullOrEmpty(singingKey)
            || !SecurityHelper.VerifyHMACSHA256Signature(singingKey, requestBody, request.Signature)
        )
        {
            // return Result.Unauthorized();
        }
        // var webhookRequestBody = JsonSerializer.Deserialize<LogtoWebhookRequest>(requestBody);
        var webhookRequestBody = request.LogtoWebhookRequest;
        switch (webhookRequestBody?.Event)
        {
            case "PostSignIn":
                var userExists = await _userManager.FindByIdAsync(webhookRequestBody.UserId);
                var user = webhookRequestBody?.User;
                if (user is null)
                    return Result.Error("User is null");

                // add Identity user table as well if not exists

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
                    ProfilePictureUrl = user.Avatar
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

    private void EnhanceUserDetails(
        ApplicationUser applicationUser,
        MemberProfile memberProfile,
        LogtoUserEntityResponse userEntity
    )
    {
        if (userEntity.Identities == null)
        {
            throw new ArgumentNullException(nameof(userEntity.Identities));
        }

        try
        {
            using (
                JsonDocument document = JsonDocument.Parse(
                    json: userEntity.Identities.ToString()
                        ?? throw new ArgumentNullException(nameof(userEntity.Identities))
                )
            )
            {
                var root = document.RootElement;
                if (TryGetProperty(root, "github.details.rawData.userInfo", out JsonElement githubRawUserInfo))
                {
                    var githubLink = GetStringValue(githubRawUserInfo, "html_url");
                    memberProfile.ExternalLinks = !string.IsNullOrEmpty(githubLink)
                        ? [githubLink]
                        : Array.Empty<string>();

                    var githubUsername = GetStringValue(githubRawUserInfo, "login");

                    applicationUser.UserName = !string.IsNullOrEmpty(githubUsername)
                        ? githubUsername
                        : applicationUser.UserName;

                    // assume email is confirmed
                    applicationUser.EmailConfirmed = true;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enhance user details");
        }
    }

    private bool TryGetProperty(JsonElement element, string propertyPath, out JsonElement value)
    {
        value = element;
        foreach (var propertyName in propertyPath.Split('.'))
        {
            if (value.TryGetProperty(propertyName, out JsonElement temp))
            {
                value = temp;
            }
            else
            {
                value = default;
                return false;
            }
        }
        return true;
    }

    private string? GetStringValue(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out JsonElement propertyValue))
        {
            return propertyValue.GetString();
        }
        return null;
    }

    private List<string>? GetArrayValues(JsonElement element, string arrayName, string propertyName)
    {
        if (
            element.TryGetProperty(arrayName, out JsonElement arrayElement)
            && arrayElement.ValueKind == JsonValueKind.Array
        )
        {
            var values = new List<string>();
            foreach (JsonElement item in arrayElement.EnumerateArray())
            {
                if (item.TryGetProperty(propertyName, out JsonElement value))
                {
                    var stringValue = value.GetString();
                    if (stringValue != null)
                    {
                        values.Add(stringValue);
                    }
                }
            }
            return values;
        }
        return null;
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.UserRegister;

/// <summary>
/// Handles the user registration process.
/// </summary>
public class UserRegisterHandler : IRequestHandler<UserRegisterRequest, Result<UserRegisterResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IContext _context;
    private readonly IEmailSender<ApplicationUser> _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisterHandler"/> class.
    /// </summary>
    /// <param name="userManager">The user manager for creating and managing users.</param>
    /// <param name="context">The application context for database operations.</param>
    /// <param name="emailSender">The email sender service for sending confirmation emails.</param>
    /// <param name="httpContextAccessor">The URL helper for generating confirmation URLs.</param>
    public UserRegisterHandler(
        UserManager<ApplicationUser> userManager,
        IContext context,
        IEmailSender<ApplicationUser> emailSender,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userManager = userManager;
        _context = context;
        _emailSender = emailSender;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles the user registration request.
    /// </summary>
    /// <param name="request">The user registration request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the UserRegisterResponse if successful, or an error message if failed.</returns>
    public async Task<Result<UserRegisterResponse>> Handle(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await CreateUserAsync(request);
            var memberProfile = await CreateMemberProfileAsync(user, request);

            await transaction.CommitAsync(cancellationToken);
            await SendConfirmationEmailAsync(user);

            return Result.Success(CreateUserRegisterResponse(user, memberProfile));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Error($"Registration failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new ApplicationUser based on the registration request.
    /// </summary>
    /// <param name="request">The user registration request.</param>
    /// <returns>The newly created ApplicationUser.</returns>
    /// <exception cref="InvalidOperationException">Thrown when user creation fails.</exception>
    private async Task<ApplicationUser> CreateUserAsync(UserRegisterRequest request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(", ", result.Errors.Select(x => x.Description));
            throw new InvalidOperationException($"User creation failed: {errorMessage}");
        }

        await _userManager.SetEmailAsync(user, request.Email);

        return user;
    }

    /// <summary>
    /// Creates a new MemberProfile for the registered user.
    /// </summary>
    /// <param name="user">The ApplicationUser for whom to create the profile.</param>
    /// <param name="request">The user registration request containing profile information.</param>
    /// <returns>The newly created MemberProfile.</returns>
    private async Task<MemberProfile> CreateMemberProfileAsync(ApplicationUser user, UserRegisterRequest request)
    {
        var memberProfile = new MemberProfile
        {
            Id = user.Id,
            FullName = request.FullName,
            UserName = request.UserName,
        };

        _context.MemberProfiles.Add(memberProfile);
        await _context.SaveChangesAsync();

        return memberProfile;
    }

    /// <summary>
    /// Sends a confirmation email to the newly registered user.
    /// </summary>
    /// <param name="user">The ApplicationUser to whom the confirmation email will be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentException("User email is required for sending confirmation email.", nameof(user));
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var httpContext = _httpContextAccessor.HttpContext;

        var request = httpContext?.Request;
        var host = request?.Host.Value;
        var scheme = request?.Scheme;
        var userId = user.Id;

        var confirmationLink = $"{scheme}://{host}/api/identity/confirmEmail?userId={userId}&code={code}";
        await _emailSender.SendConfirmationLinkAsync(user, user.Email, confirmationLink).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a UserRegisterResponse based on the newly created user and member profile.
    /// </summary>
    /// <param name="user">The ApplicationUser that was created.</param>
    /// <param name="memberProfile">The MemberProfile that was created.</param>
    /// <returns>A UserRegisterResponse containing user registration details.</returns>
    private static UserRegisterResponse CreateUserRegisterResponse(ApplicationUser user, MemberProfile memberProfile)
    {
        return new UserRegisterResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            FullName = memberProfile.FullName,
        };
    }
}

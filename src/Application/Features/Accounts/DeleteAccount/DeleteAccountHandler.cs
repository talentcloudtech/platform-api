using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Accounts.DeleteAccount;

/// <summary>
/// Handles the deletion of user accounts.
/// </summary>
/// <param name="session">The current session.</param>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger.</param>
public class DeleteAccountHandler(ICurrentSession session, IContext context, ILogger<DeleteAccountHandler> logger)
    : IRequestHandler<DeleteAccountRequest, Result>
{
    private readonly ICurrentSession _session = session;
    private readonly IContext _context = context;
    private readonly ILogger<DeleteAccountHandler> _logger = logger;

    /// <summary>
    /// Handles the deletion of user accounts.
    /// </summary>
    /// <param name="request">The request containing the user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _session.UserId;

        if (currentUserId != request.UserId)
        {
            return Result.Forbidden();
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        try
        {
            // Delete user from the database
            _context.Users.Remove(user);

            // Save changes after both operations
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User account {UserId} successfully deleted", request.UserId);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting account for user {UserId}", request.UserId);
            return Result.Error("An error occurred while deleting the account. Please try again later.");
        }
    }
}

using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.UpdateUserName;

/// <summary>
/// Request to update the username.
/// </summary>
/// <param name="UserName">The new username.</param>
/// <param name="UserId">The user id.</param>
public record UpdateUsernameRequest(string UserName, UserId UserId) : IRequest<Result<string>>;

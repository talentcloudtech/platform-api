using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Accounts.Logout;

/// <summary>
/// Request to logout a user.
/// </summary>
public record LogoutRequest(string RefreshToken, UserId UserId, bool LogoutFromAllDevices) : IRequest<Result>;

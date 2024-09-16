using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.Login;

/// <summary>
/// Request to login a user.
/// </summary>
/// <param name="UserName">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
public record LoginRequest(string UserName, string Password) : IRequest<Result<LoginResponse>>;

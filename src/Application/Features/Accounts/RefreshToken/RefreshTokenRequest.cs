using Ardalis.Result;
using MediatR;
using Platform.Application.Features.Accounts.Login;

namespace Platform.Application.Features.Accounts.RefreshToken;

/// <summary>
/// Request to refresh a token.
/// </summary>
public record RefreshTokenRequest(string Token) : IRequest<Result<LoginResponse>>;

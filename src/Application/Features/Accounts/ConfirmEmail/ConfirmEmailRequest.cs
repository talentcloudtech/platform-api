using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.ConfirmEmail;

/// <summary>
/// Represents a request to confirm an email address.
/// </summary>
public record ConfirmEmailRequest(string UserId, string Code) : IRequest<Result>;

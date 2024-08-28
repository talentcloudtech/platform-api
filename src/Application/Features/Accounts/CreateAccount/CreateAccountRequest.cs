using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.CreateAccount;

/// <summary>
/// Represents a request to create a member profile.
/// </summary>
public record CreateAccountRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the user entity.
    /// </summary>
    public HttpContext? Context { get; init; }
}

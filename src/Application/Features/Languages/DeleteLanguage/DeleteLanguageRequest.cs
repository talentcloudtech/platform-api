using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Languages.DeleteLanguage;

/// <summary>
/// Represents a request to delete a language entry.
/// </summary>
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="LanguageId">The unique identifier of the language entry to delete.</param>
public record DeleteLanguageRequest(UserId MemberProfileId, LanguageId LanguageId) : IRequest<Result>;

using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Languages.GetLanguageById;

/// <summary>
/// Represents a request to get a language by its identifier.
/// </summary>
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="LanguageId">The unique identifier of the language.</param>
public record GetLanguageByIdRequest(UserId MemberProfileId, string LanguageId) : IRequest<Result<LanguageResponse>>;

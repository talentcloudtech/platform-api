using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Heroes.GetAllHeroes;

public record GetAllHeroesRequest(
    string? Name = null,
    string? Nickname = null,
    int? Age = null,
    string? Individuality = null,
    HeroType? HeroType = null,
    string? Team = null,
    int CurrentPage = 1,
    int PageSize = 15
) : IRequest<PaginatedList<GetHeroResponse>>;

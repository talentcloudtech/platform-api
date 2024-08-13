using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;

namespace Platform.Application.Features.Heroes.CreateHero;

public class CreateHeroHandler : IRequestHandler<CreateHeroRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;

    public CreateHeroHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetHeroResponse>> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Hero>();
        _context.Heroes.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetHeroResponse>();
    }
}

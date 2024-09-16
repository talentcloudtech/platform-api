using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.MemberProfiles.CreateMemberProfile;

/// <summary>
/// Handles the creation of a member profile.
/// </summary>
public class CreateMemberProfileHandler : IRequestHandler<CreateMemberProfileRequest, Result<MemberProfileResponse>>
{
    private readonly ICurrentSession _session;
    private readonly IContext _context;
    private readonly IMediator _mediator;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage options.</param>
    /// <param name="session">The session.</param>
    public CreateMemberProfileHandler(IContext context, IMediator mediator, IOptions<AzureBlobStorageOptions> azureBlobStorageOptions, ICurrentSession session)
    {
        _context = context;
        _mediator = mediator;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
        _session = session;
    }

    /// <summary>
    /// Handles the creation of a member profile.
    /// </summary>
    /// <param name="request">The create member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the member profile creation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(CreateMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = _session.UserId;
        var created = request.Adapt<MemberProfile>();
        created.Id = userId;
        _context.MemberProfiles.Add(created);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(created.Adapt<MemberProfileResponse>());
    }
}

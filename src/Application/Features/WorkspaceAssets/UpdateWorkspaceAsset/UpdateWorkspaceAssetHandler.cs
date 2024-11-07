using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;
using FluentValidation;

namespace Platform.Application.Features.WorkspaceAssets.UpdateWorkspaceAsset;

/// <summary>
/// Handles the update of workspace assets
/// </summary>
public class UpdateWorkspaceAssetHandler : IRequestHandler<UpdateWorkspaceAssetRequest, Result<WorkspaceAssetResponse>>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;
    private readonly IValidator<UpdateWorkspaceAssetRequest> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWorkspaceAssetHandler"/> class
    /// </summary>
    public UpdateWorkspaceAssetHandler(
        IContext context,
        ICurrentSession session,
        IValidator<UpdateWorkspaceAssetRequest> validator)
    {
        _context = context;
        _session = session;
        _validator = validator;
    }

    /// <summary>
    /// Handles the update request
    /// </summary>
    public async Task<Result<WorkspaceAssetResponse>> Handle(
        UpdateWorkspaceAssetRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage, x.ErrorCode, ValidationSeverity.Error)));

        var workspace = await _context.Workspaces
            .Include(x => x.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        // Check if user is a member with appropriate permissions
        var member = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == _session.UserId);
        if (member == null || (member.Role != WorkspaceRole.WorkspaceOwner && member.Role != WorkspaceRole.WorkspaceManager))
            return Result.Forbidden("You don't have permission to update assets in this workspace.");

        var asset = await _context.WorkspaceAssets
            .Include(x => x.Workspace)
            .Include(x => x.Project)
            .Include(x => x.Task)
            .FirstOrDefaultAsync(x => x.Id == request.AssetId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (asset == null)
            return Result.NotFound("Asset not found.");

        try
        {
            foreach (var field in request.FieldsToUpdate)
            {
                switch (field.ToLower())
                {
                    case "filename":
                        asset.FileName = request.FileName!;
                        break;
                }
            }

            asset.UpdatedBy = _session.UserId;
            asset.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var response = asset.Adapt<WorkspaceAssetResponse>();
            response.WorkspaceName = asset.Workspace.Name;
            response.ProjectName = asset.Project?.Title;
            response.TaskName = asset.Task?.Title;

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Error($"Failed to update asset: {ex.Message}");
        }
    }
}
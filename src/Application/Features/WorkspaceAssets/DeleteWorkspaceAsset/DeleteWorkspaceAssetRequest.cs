using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceAssets.DeleteWorkspaceAsset;

/// <summary>
/// Request to delete a workspace asset
/// </summary>
public record DeleteWorkspaceAssetRequest(WorkspaceId WorkspaceId, WorkspaceAssetId AssetId) : IRequest<Result>;

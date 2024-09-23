using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Contracts.DeleteContract;

/// <summary>
/// Represents a request to delete a contract.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace containing the contract.</param>
/// <param name="Id">The unique identifier of the contract to be deleted.</param>
public record DeleteContractRequest(WorkspaceId WorkspaceId, ContractId Id) : IRequest<Result>;

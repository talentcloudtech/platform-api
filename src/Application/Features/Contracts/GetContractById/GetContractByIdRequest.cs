using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Contracts.GetContractById;

/// <summary>
/// Represents a request to get a contract identified by its <see cref="ContractId"/>.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace containing the contract.</param>
/// <param name="Id">The unique identifier of the contract to be retrieved.</param>
public record GetContractByIdRequest(WorkspaceId WorkspaceId, ContractId Id) : IRequest<Result<ContractResponse>>;

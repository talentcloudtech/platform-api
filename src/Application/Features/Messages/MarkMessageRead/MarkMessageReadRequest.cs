using MediatR;
using Ardalis.Result;
using Platform.Domain.Entities.Common;

/// <summary>
/// Request to mark a message as read
/// </summary>
/// <param name="MessageId">The ID of the message to mark as read</param>
public record MarkMessageReadRequest(MessageId MessageId) : IRequest<Result>;
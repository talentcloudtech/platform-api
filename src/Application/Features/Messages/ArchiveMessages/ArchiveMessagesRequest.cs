using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

/// <summary>
/// Request to archive messages
/// </summary>
public record ArchiveMessagesRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the list of message IDs to archive
    /// </summary>
    public required List<MessageId> MessageIds { get; init; }
}

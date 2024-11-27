using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

/// <summary>
/// Request to unarchive messages
/// </summary>
public record UnarchiveMessagesRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the list of message IDs to unarchive
    /// </summary>
    public required List<MessageId> MessageIds { get; init; }
}

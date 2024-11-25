using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

/// <summary>
/// Request to get replies for a specific message
/// </summary>
/// <param name="MessageId">The ID of the message to get replies for</param>
/// <param name="CurrentPage">Current page number</param>
/// <param name="PageSize">Number of items per page</param>
public record GetMessageRepliesRequest(MessageId MessageId, int CurrentPage = 1, int PageSize = 20) : IRequest<Result<PaginatedList<MessageReplyResponse>>>;

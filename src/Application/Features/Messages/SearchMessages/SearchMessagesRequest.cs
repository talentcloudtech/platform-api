using System;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

/// <summary>
/// Request to search for messages in user's inbox with filters
/// </summary>
/// <param name="WorkspaceId">Optional workspace ID to filter messages</param>
/// <param name="ProjectId">Optional project ID to filter messages</param>
/// <param name="Archived">Filter by archived status</param>
/// <param name="Unread">Filter by unread status</param>
/// <param name="Search">Search text to filter messages by subject or body</param>
/// <param name="MessageTypes">Filter by message types</param>
/// <param name="StartDate">Filter messages after this date</param>
/// <param name="EndDate">Filter messages before this date</param>
/// <param name="SortBy">Field to sort by</param>
/// <param name="SortOrder">Sort order (asc/desc)</param>
/// <param name="CurrentPage">Current page number</param>
/// <param name="PageSize">Number of items per page</param>
public record SearchMessagesRequest(
    WorkspaceId? WorkspaceId = null,
    ProjectId? ProjectId = null,
    bool? Archived = false,
    bool? Unread = null,
    string? Search = null,
    MessageType[]? MessageTypes = null,
    DateTimeOffset? StartDate = null,
    DateTimeOffset? EndDate = null,
    string SortBy = "CreatedAt",
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 20
) : IRequest<Result<PaginatedList<MessageResponse>>>;

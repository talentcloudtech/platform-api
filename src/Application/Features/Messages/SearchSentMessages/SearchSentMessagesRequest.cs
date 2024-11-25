using System;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

/// <summary>
/// Request to search for messages sent by the current user
/// </summary>
/// <param name="WorkspaceId">Optional workspace ID to filter messages</param>
/// <param name="ProjectId">Optional project ID to filter messages</param>
/// <param name="SearchText">Search text to filter messages by subject or body</param>
/// <param name="MessageTypes">Filter by message types</param>
/// <param name="StartDate">Filter messages after this date</param>
/// <param name="EndDate">Filter messages before this date</param>
/// <param name="SortBy">Field to sort by</param>
/// <param name="SortOrder">Sort order (asc/desc)</param>
/// <param name="CurrentPage">Current page number</param>
/// <param name="PageSize">Number of items per page</param>
public record SearchSentMessagesRequest(
    WorkspaceId? WorkspaceId = null,
    ProjectId? ProjectId = null,
    string? SearchText = null,
    MessageType[]? MessageTypes = null,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    string SortBy = "CreatedAt",
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 20
) : IRequest<Result<PaginatedList<MessageResponse>>>;

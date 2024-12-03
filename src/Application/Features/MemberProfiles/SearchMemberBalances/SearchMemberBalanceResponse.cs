using Platform.Application.Common.Responses;

namespace Platform.Application.Features.MemberProfiles.SearchMemberBalances;

/// <summary>
/// Represents a response for searching member balances.
/// </summary>
public record SearchMemberBalanceResponse
{
    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public CommonUserResponse User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the current balance of the member.
    /// </summary>
    public decimal CurrentBalance { get; set; }
}

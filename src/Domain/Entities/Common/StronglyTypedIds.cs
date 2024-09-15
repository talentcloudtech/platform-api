using StronglyTypedIds;

[assembly: StronglyTypedIdDefaults(Template.String, "string-efcore")]

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a strongly typed identifier for a string.
/// </summary>
public interface IStringId { }

/// <summary>
/// Represents a strongly typed identifier for a user.
/// </summary>
[StronglyTypedId]
public partial struct UserId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a UserId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of UserId.</returns>
    public static implicit operator UserId(string id)
    {
        return new UserId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a user.
/// </summary>
[StronglyTypedId]
public partial struct RoleId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a UserId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of UserId.</returns>
    public static implicit operator RoleId(string id)
    {
        return new RoleId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a project.
/// </summary>
[StronglyTypedId]
public partial struct ProjectId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a ProjectId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of ProjectId.</returns>
    public static implicit operator ProjectId(string id)
    {
        return new ProjectId(id);
    }
}

/// <summary>
/// Represents a strongly typed identifier for a refresh token.
/// </summary>
[StronglyTypedId]
public partial struct RefreshTokenId : IStringId
{
    /// <summary>
    /// Implicitly converts a id to a RefreshTokenId.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns>A new instance of RefreshTokenId.</returns>
    public static implicit operator RefreshTokenId(string id)
    {
        return new RefreshTokenId(id);
    }
}

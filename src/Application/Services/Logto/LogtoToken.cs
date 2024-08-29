using System;

namespace Platform.Domain.Services.Logto;

/// <summary>
/// Represents a token for the Logto API.
/// </summary>
internal record Token
{
    private readonly DateTimeOffset _expiredAt = DateTimeOffset.Now;

    public string Value { get; private set; }

    public bool IsExpired => DateTimeOffset.Now >= _expiredAt;

    public Token(string value, int expiresIn)
    {
        Value = value;
        _expiredAt = DateTimeOffset.Now.AddSeconds(expiresIn);
    }
}

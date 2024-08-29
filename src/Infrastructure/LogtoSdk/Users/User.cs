using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Platform.Infrastructure.LogtoSdk.Users;

/// <summary>
/// Represents a user in the Logto API.
/// </summary>
public class LogtoUser
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    /// <summary>
    /// The username of the user.
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// The primary email of the user.
    /// </summary>
    [JsonPropertyName("primaryEmail")]
    public string PrimaryEmail { get; set; } = null!;

    /// <summary>
    /// The primary phone number of the user.
    /// </summary>
    [JsonPropertyName("primaryPhone")]
    public string? PrimaryPhone { get; set; }

    /// <summary>
    /// The name of the user.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The avatar URL of the user.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    /// The identities associated with the user.
    /// </summary>
    [JsonPropertyName("identities")]
    public Identities? Identities { get; set; }

    /// <summary>
    /// The last sign-in timestamp of the user.
    /// </summary>
    [JsonPropertyName("lastSignInAt")]
    public double? LastSignInAt { get; set; }

    /// <summary>
    /// The creation timestamp of the user.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public double? CreatedAt { get; set; }

    /// <summary>
    /// The last updated timestamp of the user.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public double? UpdatedAt { get; set; }

    /// <summary>
    /// The profile details of the user.
    /// </summary>
    [JsonPropertyName("profile")]
    public Profile Profile { get; set; } = null!;

    /// <summary>
    /// The application ID associated with the user.
    /// </summary>
    [JsonPropertyName("applicationId")]
    public string? ApplicationId { get; set; }

    /// <summary>
    /// Indicates whether the user is suspended.
    /// </summary>
    [JsonPropertyName("isSuspended")]
    public bool? IsSuspended { get; set; }

    /// <summary>
    /// Indicates whether the user has a password.
    /// </summary>
    [JsonPropertyName("hasPassword")]
    public bool? HasPassword { get; set; }

    /// <summary>
    /// The SSO identities associated with the user.
    /// </summary>
    [JsonPropertyName("ssoIdentities")]
    public List<SsoIdentity> SsoIdentities { get; set; } = [];
}

/// <summary>
/// Represents the address of the user.
/// </summary>
public class Address
{
    /// <summary>
    /// The formatted address.
    /// </summary>
    [JsonPropertyName("formatted")]
    public string? Formatted { get; set; }

    /// <summary>
    /// The street address.
    /// </summary>
    [JsonPropertyName("streetAddress")]
    public string? StreetAddress { get; set; }

    /// <summary>
    /// The locality.
    /// </summary>
    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    /// <summary>
    /// The region.
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; set; }

    /// <summary>
    /// The postal code.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// The country.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }
}

/// <summary>
///  Represents the identities associated with the user.
/// </summary>
public class Identities
{
    /// <summary>
    /// The user ID.
    /// </summary>
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = null!;
}

/// <summary>
/// Represents the profile information of the user.
/// </summary>
public class Profile
{
    /// <summary>
    /// The family name.
    /// </summary>
    [JsonPropertyName("familyName")]
    public string? FamilyName { get; set; }

    /// <summary>
    /// The given name.
    /// </summary>
    [JsonPropertyName("givenName")]
    public string? GivenName { get; set; }

    /// <summary>
    /// The middle name.
    /// </summary>
    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// The nickname.
    /// </summary>
    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }

    /// <summary>
    /// The preferred username.
    /// </summary>
    [JsonPropertyName("preferredUsername")]
    public string? PreferredUsername { get; set; }

    /// <summary>
    /// The website.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// The gender.
    /// </summary>
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    /// <summary>
    /// The birth date.
    /// </summary>
    [JsonPropertyName("birthdate")]
    public string? Birthdate { get; set; }

    /// <summary>
    /// The zone info.
    /// </summary>
    [JsonPropertyName("zoneinfo")]
    public string? Zoneinfo { get; set; }

    /// <summary>
    /// The locale.
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    /// <summary>
    /// The address.
    /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; set; }
}

/// <summary>
/// Represents an SSO identity associated with the user.
/// </summary>
public class SsoIdentity
{
    /// <summary>
    /// The tenant ID.
    /// </summary>
    [JsonPropertyName("tenantId")]
    public string TenantId { get; set; } = null!;

    /// <summary>
    /// The ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    /// <summary>
    /// The user ID.
    /// </summary>
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// The issuer.
    /// </summary>
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// The identity ID.
    /// </summary>
    [JsonPropertyName("identityId")]
    public string IdentityId { get; set; } = null!;

    /// <summary>
    /// The creation timestamp.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public double? CreatedAt { get; set; } = null!;

    /// <summary>
    /// The SSO connector ID.
    /// </summary>
    [JsonPropertyName("ssoConnectorId")]
    public string SsoConnectorId { get; set; } = null!;
}

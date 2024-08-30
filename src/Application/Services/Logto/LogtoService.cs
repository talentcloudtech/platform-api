using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Domain.Interfaces;

namespace Platform.Domain.Services.Logto;

/// <summary>
/// Represents a service for interacting with the Logto API.
/// </summary>
public class LogtoService : ILogtoService
{
    /// <summary>
    /// The HTTP client used to send requests to the Logto API.
    /// </summary>
    public readonly HttpClient _httpClient;

    /// <summary>
    /// The configuration options for the Logto API.
    /// </summary>
    public readonly LogtoApiOptions _logtoApiOptions;
    private Token? _currentToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogtoService"/> class.
    /// </summary>
    /// <param name="logtoEndpoint">The configuration options for the Logto API.</param>
    public LogtoService(IOptions<LogtoApiOptions> logtoEndpoint)
    {
        _logtoApiOptions = logtoEndpoint.Value;
        _httpClient = new HttpClient { BaseAddress = new Uri(_logtoApiOptions.Url) };
    }

    /// <summary>
    /// See https://openapi.logto.io/authentication.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task AuthenticateAsync()
    {
        var values = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("resource", new Uri(_logtoApiOptions.Url).AbsoluteUri + "api"),
            new KeyValuePair<string, string>("scope", "all"),
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "/oidc/token") { Content = new FormUrlEncodedContent(values) };

        var authenticationString = $"{_logtoApiOptions.ClientId}:{_logtoApiOptions.ClientSecret}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var authenticationResponse =
            JsonSerializer.Deserialize<AuthenticationResponse>(responseContent) ?? throw new InvalidOperationException("Authentication failed.");

        _currentToken = new Token(authenticationResponse.AccessToken, authenticationResponse.ExpiresIn);
    }

    /// <summary>
    /// Sends a request to the Logto API and deserializes the response to the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the response.</typeparam>
    /// <param name="request">The request to send.</param>
    /// <returns>The deserialized response.</returns>
    public async Task<T?> SendRequestAsync<T>(HttpRequestMessage request)
    {
        if (_currentToken is null || _currentToken.IsExpired)
        {
            await AuthenticateAsync();
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _currentToken!.Value);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("logto-app-dotnet-sdk", Assembly.GetExecutingAssembly().GetName().Version?.ToString()));
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("dotnet-version", Environment.Version.ToString()));

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseContent);
    }

    /// <summary>
    /// Gets the user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to get.</param>
    /// <returns>The user with the specified ID.</returns>
    public Task<LogtoUser?> GetUserAsync(string userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{userId}");

        return SendRequestAsync<LogtoUser>(request);
    }

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <returns>The users.</returns>
    public Task<IEnumerable<LogtoUser>?> GetUsersAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users");

        return SendRequestAsync<IEnumerable<LogtoUser>>(request);
    }

    /// <summary>
    /// Updates the user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The user to update.</param>
    /// <returns>The updated user.</returns>
    public Task<LogtoUser?> UpdateUserAsync(string userId, LogtoUser user)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/users/{userId}")
        {
            Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"),
        };

        return SendRequestAsync<LogtoUser>(request);
    }

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>The deleted user.</returns>
    public async Task DeleteUserAsync(string userId)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/users/{userId}");

            await SendRequestAsync<object>(request);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Failed to delete user from Logto: {ex.Message}", ex);
        }
    }
}

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
using Platform.Infrastructure.LogtoSdk.Authentication;

namespace Platform.Infrastructure.LogtoSdk;

/// <summary>
/// Represents a client for the Logto API.
/// </summary>
public class LogtoApiClient
{
    /// <summary>
    /// The HTTP client used to send requests to the Logto API.
    /// </summary>
    protected readonly HttpClient _httpClient;

    /// <summary>
    /// The configuration options for the Logto API.
    /// </summary>
    protected readonly LogtoApiOptions _logtoApiOptions;
    private Token? _currentToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogtoApiClient"/> class.
    /// </summary>
    /// <param name="logtoEndpoint">The configuration options for the Logto API.</param>
    public LogtoApiClient(IOptions<LogtoApiOptions> logtoEndpoint)
    {
        _logtoApiOptions = logtoEndpoint.Value;
        _httpClient = new HttpClient { BaseAddress = new Uri(_logtoApiOptions.Url) };
    }

    /// <summary>
    /// See https://openapi.logto.io/authentication.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    protected async Task AuthenticateAsync()
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
    protected async Task<T?> SendRequestAsync<T>(HttpRequestMessage request)
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
}

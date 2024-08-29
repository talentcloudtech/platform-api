using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;

namespace Platform.Infrastructure.LogtoSdk.Users;

/// <summary>
/// Client for interacting with Logto Users API.
/// </summary>
public class LogtoUsersClient : LogtoApiClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogtoUsersClient"/> class.
    /// </summary>
    /// <param name="logtoEndpoint">The configuration options for the Logto API.</param>
    public LogtoUsersClient(IOptions<LogtoApiOptions> logtoEndpoint)
        : base(logtoEndpoint) { }

    /// <summary>
    /// https://openapi.logto.io/operation/operation-listusers
    /// </summary>
    public async Task<IEnumerable<LogtoUser>?> GetUsersAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users");

        return await SendRequestAsync<IEnumerable<LogtoUser>>(request);
    }

    /// <summary>
    /// https://openapi.logto.io/operation/operation-getuser
    /// </summary>
    public async Task<LogtoUser?> GetUserAsync(string userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{userId}");

        return await SendRequestAsync<LogtoUser>(request);
    }

    /// <summary>
    /// https://openapi.logto.io/operation/operation-updateuser
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The user to update.</param>
    /// <returns>The updated user.</returns>
    public async Task<LogtoUser?> UpdateUserAsync(string userId, LogtoUser user)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/users/{userId}")
        {
            Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"),
        };

        return await SendRequestAsync<LogtoUser>(request);
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist authorization management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IAuthorizationService" />
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal AuthorizationService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public Task RevokeAccessTokenAsync(string clientId, string clientSecret, string accessToken, CancellationToken cancellationToken = default)
        {
            var parameters = CreateParameters(clientId, clientSecret, accessToken);

            return _todoistClient.PostRawAsync("access_tokens/revoke", parameters, cancellationToken);
        }


        private static List<KeyValuePair<string, string>> CreateParameters(string clientId, string clientSecret, string accessToken)
        {
            ThrowIfNull(clientId, nameof(clientId));
            ThrowIfNull(clientSecret, nameof(clientSecret));
            ThrowIfNull(accessToken, nameof(accessToken));

            var parameters =
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("access_token", accessToken)
                };
            return parameters;
        }

        private static void ThrowIfNull(object value, string name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}

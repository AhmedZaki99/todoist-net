using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist authorization management.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Revokes the access token.
        /// </summary>
        /// <param name="clientId">The unique Client ID of the Todoist application registered.</param>
        /// <param name="clientSecret">The unique Client Secret of the Todoist application registered.</param>
        /// <param name="accessToken">Access token obtained from OAuth authentication.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />. The task object representing the asynchronous operation.</returns>
        Task RevokeAccessTokenAsync(string clientId, string clientSecret, string accessToken, CancellationToken cancellationToken = default);
    }
}

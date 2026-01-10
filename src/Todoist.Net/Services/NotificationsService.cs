using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class NotificationsService : NotificationsCommandService, INotificationsService
    {
        internal NotificationsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Notification>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Notifications).ConfigureAwait(false);

            return response.Notifications;
        }
    }
}

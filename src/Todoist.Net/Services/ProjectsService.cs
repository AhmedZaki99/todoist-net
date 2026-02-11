using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ProjectsCommandService" />
    /// <seealso cref="Todoist.Net.Services.IProjectsService" />
    internal class ProjectsService : ProjectsCommandService, IProjectsService
    {
        internal ProjectsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Project>> GetArchivedAsync(
            string cursor = null,
            int? limit = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(cursor))
            {
                parameters.Add(new KeyValuePair<string, string>("cursor", cursor));
            }

            if (limit.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            }

            return TodoistClient.GetAsync<PaginatedResponse<Project>>(
                "projects/archived",
                parameters,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Project>> GetAsync(
            string cursor = null,
            int? limit = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(cursor))
            {
                parameters.Add(new KeyValuePair<string, string>("cursor", cursor));
            }

            if (limit.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            }

            return TodoistClient.GetAsync<PaginatedResponse<Project>>("projects", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Project> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }

            return TodoistClient.GetAsync<Project>(
                $"projects/{id}",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectFull> GetFullAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }

            return TodoistClient.GetAsync<ProjectFull>(
                $"projects/{id}/full",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }
    }
}

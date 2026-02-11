using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.TasksCommandService" />
    /// <seealso cref="Todoist.Net.Services.ITasksService" />
    internal class TasksService : TasksCommandService, ITasksService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksService"/> class.
        /// </summary>
        /// <param name="todoistClient">The todoist client.</param>
        internal TasksService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<DetailedTask>> GetAsync(TasksQuery query = null, CancellationToken cancellationToken = default)
        {
            var parameters = query?.ToParameters() ?? new List<KeyValuePair<string, string>>();

            return TodoistClient.GetAsync<PaginatedResponse<DetailedTask>>("tasks", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DetailedTask> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }

            return TodoistClient.GetAsync<DetailedTask>(
                $"tasks/{id}",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedItemsResponse<DetailedTask>> GetCompletedByCompletionDateAsync(
            CompletedTasksByCompletionDateFilter filter,
            CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var parameters = filter.ToParameters();

            return TodoistClient.GetAsync<PaginatedItemsResponse<DetailedTask>>(
                "tasks/completed/by_completion_date",
                parameters,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedItemsResponse<DetailedTask>> GetCompletedByDueDateAsync(
            CompletedTasksByDueDateFilter filter,
            CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var parameters = filter.ToParameters();

            return TodoistClient.GetAsync<PaginatedItemsResponse<DetailedTask>>(
                "tasks/completed/by_due_date",
                parameters,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<DetailedTask>> GetByFilterAsync(
            string filter,
            string lang = null,
            string cursor = null,
            int? limit = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filter))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(filter));
            }

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("filter", filter)
            };

            if (!string.IsNullOrEmpty(lang))
            {
                parameters.Add(new KeyValuePair<string, string>("lang", lang));
            }

            if (!string.IsNullOrEmpty(cursor))
            {
                parameters.Add(new KeyValuePair<string, string>("cursor", cursor));
            }

            if (limit.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            }

            return TodoistClient.GetAsync<PaginatedResponse<DetailedTask>>("tasks/filter", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DetailedTask> QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default)
        {
            if (quickAddTask == null)
            {
                throw new ArgumentNullException(nameof(quickAddTask));
            }

            return TodoistClient.PostJsonAsync<DetailedTask>("tasks/quick", quickAddTask, cancellationToken);
        }
    }
}

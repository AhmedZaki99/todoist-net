using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.TasksCommandService" />
    public interface ITasksService : ITasksCommandService
    {
        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The tasks.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<DetailedTask>> GetAsync(TasksQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<DetailedTask> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user's completed tasks by completion date.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The completed tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<PaginatedItemsResponse<DetailedTask>> GetCompletedByCompletionDateAsync(
            CompletedTasksByCompletionDateFilter filter,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user's completed tasks by due date.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The completed tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<PaginatedItemsResponse<DetailedTask>> GetCompletedByDueDateAsync(
            CompletedTasksByDueDateFilter filter,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets tasks matching a filter string.
        /// </summary>
        /// <param name="filter">The filter string.</param>
        /// <param name="lang">The filter language.</param>
        /// <param name="cursor">The pagination cursor.</param>
        /// <param name="limit">The page size.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The filtered tasks.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<DetailedTask>> GetByFilterAsync(
            string filter,
            string lang = null,
            string cursor = null,
            int? limit = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a task. Implementation of the Quick Add Task available in the official clients.
        /// </summary>
        /// <param name="quickAddTask">The quick add task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="quickAddTask"/> is <see langword="null"/></exception>
        Task<DetailedTask> QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default);
    }
}

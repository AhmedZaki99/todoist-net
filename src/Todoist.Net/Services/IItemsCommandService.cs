using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management which can be executed in a transaction.
    /// </summary>
    public interface IItemsCommandService
    {
        /// <summary>
        /// Adds a new task to a project asynchronous. By default the task is added to the user’s Inbox project.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The temporary ID of the item.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Closes a task asynchronous.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// A simplified version of item_complete / item_update_date_complete.
        /// The command does exactly what official clients do when you close a task: regular task is completed and moved to history,
        /// subtask is checked (marked as done, but not moved to history), recurring task is moved forward (due date is updated).
        /// </remarks>
        Task CloseAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Completes a recurring task. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="id">The recurring task ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task CompleteRecurringAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Completes a recurring task. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeRecurringItemArgument">The complete recurring item argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeRecurringItemArgument"/> is <see langword="null"/></exception>
        Task CompleteRecurringAsync(CompleteRecurringItemArgument completeRecurringItemArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="id">List of the IDs of the tasks to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects.
        /// </summary>
        /// <param name="id">The ids of the tasks.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UncompleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(UpdateItem item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the day orders of multiple items at once.
        /// </summary>
        /// <param name="idsToOrder">The ids to order.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrder"/> is <see langword="null"/></exception>
        Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder);

        /// <summary>
        /// Updates the day orders of multiple items at once.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="idsToOrder">The ids to order.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrder"/> is <see langword="null"/></exception>
        Task UpdateDayOrdersAsync(CancellationToken cancellationToken, params OrderEntry[] idsToOrder);

        /// <summary>
        /// Moves task to a different location asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move entry.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveAsync(ItemMoveArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the orders and indents of multiple items at once asynchronous.
        /// </summary>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(params ReorderEntry[] reorderEntries);

        /// <summary>
        /// Update the orders and indents of multiple items at once asynchronous.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(CancellationToken cancellationToken, params ReorderEntry[] reorderEntries);

        /// <summary>
        /// Completes tasks and optionally move them to history. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeItemArgument">The complete item argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeItemArgument"/> is <see langword="null"/></exception>
        Task CompleteAsync(CompleteItemArgument completeItemArgument, CancellationToken cancellationToken = default);
    }
}

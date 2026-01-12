# Todoist.Net Migration Plan: Sync API v9 → Todoist API v1

**Version:** 11.0.0  
**Date:** January 12, 2026  
**Target:** Major Breaking Release (No Backward Compatibility)

---

## Table of Contents

1. [Executive Summary](#executive-summary)
2. [Breaking Changes Summary](#breaking-changes-summary)
3. [Foundation Changes](#foundation-changes)
4. [Model Renames and Updates](#model-renames-and-updates)
5. [Service Renames and Updates](#service-renames-and-updates)
6. [Endpoint Updates](#endpoint-updates)
7. [Pagination Implementation](#pagination-implementation)
8. [New Features](#new-features)
9. [Error Handling Updates](#error-handling-updates)
10. [Testing Updates](#testing-updates)
11. [Documentation Updates](#documentation-updates)
12. [File Change Summary](#file-change-summary)

---

## Executive Summary

This document outlines the migration plan for updating Todoist.Net from **Sync API v9** to the new unified **Todoist API v1**. This is a major breaking release (v10.0.0 → v11.0.0) with **no backward compatibility**.

### Key Changes

| Category | Change |
|----------|--------|
| **Base URL** | `https://api.todoist.com/sync/v9/` → `https://api.todoist.com/api/v1/` |
| **Object Renames** | `items` → `tasks`, `notes` → `comments` |
| **ID System** | Numeric IDs → Opaque string IDs (v2 IDs) |
| **Pagination** | New cursor-based pagination via `IAsyncEnumerable<T>` |
| **Error Format** | Unified JSON error responses |
| **API Pattern** | Support both REST and Sync endpoints |

---

## Breaking Changes Summary

### Type/Model Renames

| Old Name | New Name |
|----------|----------|
| `Item` | `DetailedTask` |
| `AddItem` | `AddTask` |
| `UpdateItem` | `UpdateTask` |
| `BaseItem` | `BaseTask` |
| `ItemInfo` | `DetailedTaskInfo` |
| `ItemFilter` | `TaskFilter` |
| `ItemMoveArgument` | `TaskMoveArgument` |
| `CompletedItem` | `CompletedTask` |
| `CompletedItemsInfo` | `CompletedTasksInfo` |
| `CompleteItemArgument` | `CompleteTaskArgument` |
| `CompleteRecurringItemArgument` | `CompleteRecurringTaskArgument` |
| `QuickAddItem` | `QuickAddTask` |
| `Note` | `Comment` |
| `NotesInfo` | `CommentsInfo` |

### Service Renames

| Old Name | New Name |
|----------|----------|
| `IItemsService` | `ITasksService` |
| `IItemsCommandService` | `ITasksCommandService` |
| `ItemsService` | `TasksService` |
| `ItemsCommandService` | `TasksCommandService` |
| `INotesServices` | `ICommentsService` |
| `INotesCommandServices` | `ICommentsCommandService` |
| `NotesService` | `CommentsService` |
| `NotesCommandService` | `CommentsCommandService` |

### Client Property Renames

| Old Property | New Property |
|--------------|--------------|
| `client.Items` | `client.Tasks` |
| `client.Notes` | `client.Comments` |

### Resources Model Property Renames

| Old Property | New Property |
|--------------|--------------|
| `Resources.Items` | `Resources.Tasks` |
| `Resources.Notes` | `Resources.Comments` |

### Property Changes

| Model | Old Property | New Property/Change |
|-------|--------------|---------------------|
| `Section` | `[JsonPropertyName("collapsed")]` | `[JsonPropertyName("is_collapsed")]` |
| `User` | `IsBizAdmin` | **Removed** |

### Method Signature Changes

All GET methods that previously returned `IEnumerable<T>` or `Task<IEnumerable<T>>` now return `IAsyncEnumerable<T>`:

```csharp
// Old
Task<IEnumerable<Item>> GetAsync(CancellationToken cancellationToken = default);

// New
IAsyncEnumerable<DetailedTask> GetAsync(CancellationToken cancellationToken = default);
```

---

## Foundation Changes

### Update Base URL

**File:** `TodoistRestClient.cs`

```csharp
// Old
_httpClient.BaseAddress = new Uri("https://api.todoist.com/sync/v9/");

// New
_httpClient.BaseAddress = new Uri("https://api.todoist.com/api/v1/");
```

### Add REST API Request Methods

**File:** `ITodoistRestClient.cs` - Add new methods:

```csharp
Task<HttpResponseMessage> DeleteAsync(string resource, CancellationToken cancellationToken = default);
Task<HttpResponseMessage> PutAsync(string resource, string jsonContent, CancellationToken cancellationToken = default);
Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default);
```

**File:** `TodoistRestClient.cs` - Implement new methods:

```csharp
public async Task<HttpResponseMessage> DeleteAsync(
    string resource,
    CancellationToken cancellationToken = default)
{
    if (string.IsNullOrEmpty(resource))
    {
        throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
    }

    return await _httpClient.DeleteAsync(resource, cancellationToken).ConfigureAwait(false);
}

public async Task<HttpResponseMessage> PutAsync(
    string resource,
    string jsonContent,
    CancellationToken cancellationToken = default)
{
    if (string.IsNullOrEmpty(resource))
    {
        throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
    }

    using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    return await _httpClient.PutAsync(resource, content, cancellationToken).ConfigureAwait(false);
}

public async Task<HttpResponseMessage> PostJsonAsync(
    string resource,
    string jsonContent,
    CancellationToken cancellationToken = default)
{
    if (string.IsNullOrEmpty(resource))
    {
        throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
    }

    using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    return await _httpClient.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
}
```

### Update IAdvancedTodoistClient Interface

Add new methods for REST-style operations:

```csharp
Task<T> DeleteAsync<T>(string resource, CancellationToken cancellationToken = default);
Task DeleteAsync(string resource, CancellationToken cancellationToken = default);
Task<T> PutAsync<T>(string resource, object body, CancellationToken cancellationToken = default);
Task<T> PostJsonAsync<T>(string resource, object body, CancellationToken cancellationToken = default);
```

---

## Model Renames and Updates

### Task Models

#### `Item` → `DetailedTask`

**File:** `Models/Item.cs` → `Models/DetailedTask.cs`

```csharp
namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a detailed Todoist task returned from API responses.
    /// </summary>
    public class DetailedTask : UpdateTask
    {
        // ... existing properties plus new ones:
        
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; internal set; }
        
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }
    }
}
```

#### `AddItem` → `AddTask`

**File:** `Models/AddItem.cs` → `Models/AddTask.cs`

#### `UpdateItem` → `UpdateTask`

**File:** `Models/UpdateItem.cs` → `Models/UpdateTask.cs`

#### `BaseItem` → `BaseTask`

**File:** `Models/BaseItem.cs` → `Models/BaseTask.cs`

#### Other Task-Related Models

| Old File | New File |
|----------|----------|
| `Models/ItemInfo.cs` | `Models/DetailedTaskInfo.cs` |
| `Models/ItemFilter.cs` | `Models/TaskFilter.cs` |
| `Models/ItemMoveArgument.cs` | `Models/TaskMoveArgument.cs` |
| `Models/CompletedItem.cs` | `Models/CompletedTask.cs` |
| `Models/CompletedItemsInfo.cs` | `Models/CompletedTasksInfo.cs` |
| `Models/CompleteItemArgument.cs` | `Models/CompleteTaskArgument.cs` |
| `Models/CompleteRecurringItemArgument.cs` | `Models/CompleteRecurringTaskArgument.cs` |
| `Models/QuickAddItem.cs` | `Models/QuickAddTask.cs` |
| `Models/ReorderItemsArgument.cs` | `Models/ReorderTasksArgument.cs` |

### Comment Models

#### `Note` → `Comment`

**File:** `Models/Note.cs` → `Models/Comment.cs`

```csharp
namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist comment.
    /// </summary>
    public class Comment : BaseUnsetEntity, IWithRelationsArgument
    {
        // Rename ItemId to TaskId
        [JsonPropertyName("item_id")]
        public ComplexId? TaskId { get; set; }
        
        // ... rest of properties
    }
}
```

#### `NotesInfo` → `CommentsInfo`

**File:** `Models/NotesInfo.cs` → `Models/CommentsInfo.cs`

### Resources Model Update

**File:** `Models/Resources.cs`

```csharp
public class Resources
{
    // Old: Items → New: Tasks
    [JsonPropertyName("items")]
    public IReadOnlyCollection<DetailedTask> Tasks { get; internal set; }

    // Old: Notes → New: Comments
    [JsonPropertyName("notes")]
    public IReadOnlyCollection<Comment> Comments { get; internal set; }

    // Old: ProjectNotes → New: ProjectComments
    [JsonPropertyName("project_notes")]
    public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }

    // ... rest unchanged
}
```

### Section Model Update

**File:** `Models/Section.cs`

```csharp
// Update JSON property name
[JsonPropertyName("is_collapsed")]  // Changed from "collapsed"
public bool IsCollapsed { get; set; }
```

### New Models to Create

#### `Models/PaginatedResponse.cs`

```csharp
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated API response.
    /// </summary>
    /// <typeparam name="T">The type of items in the results.</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Gets or sets the results for the current page.
        /// </summary>
        [JsonPropertyName("results")]
        public IReadOnlyList<T> Results { get; set; }

        /// <summary>
        /// Gets or sets the cursor for the next page, or null if no more pages.
        /// </summary>
        [JsonPropertyName("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets a value indicating whether there are more pages available.
        /// </summary>
        public bool HasMore => !string.IsNullOrEmpty(NextCursor);
    }
}
```

#### `Models/PaginationOptions.cs`

```csharp
namespace Todoist.Net.Models
{
    /// <summary>
    /// Options for paginated API requests.
    /// </summary>
    public class PaginationOptions
    {
        /// <summary>
        /// Gets or sets the cursor for pagination. Null for first page.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items per page.
        /// Default: 50, Maximum: 200
        /// </summary>
        public int? Limit { get; set; }
    }
}
```

---

## Service Renames and Updates

### Tasks Service (formerly Items Service)

#### File Renames

| Old File | New File |
|----------|----------|
| `Services/IItemsService.cs` | `Services/ITasksService.cs` |
| `Services/IItemsCommandService.cs` | `Services/ITasksCommandService.cs` |
| `Services/ItemsService.cs` | `Services/TasksService.cs` |
| `Services/ItemsCommandService.cs` | `Services/TasksCommandService.cs` |

#### Interface Updates

**File:** `Services/ITasksService.cs`

```csharp
namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist tasks management.
    /// </summary>
    public interface ITasksService : ITasksCommandService
    {
        /// <summary>
        /// Gets all active tasks.
        /// </summary>
        IAsyncEnumerable<DetailedTask> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a task by ID.
        /// </summary>
        Task<DetailedTaskInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed tasks.
        /// </summary>
        IAsyncEnumerable<CompletedTask> GetCompletedAsync(TaskFilter filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets tasks matching a filter query.
        /// </summary>
        IAsyncEnumerable<DetailedTask> GetByFilterAsync(string filter, string lang = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Quickly adds a task using natural language.
        /// </summary>
        Task<DetailedTask> QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default);
    }
}
```

### Comments Service (formerly Notes Service)

#### File Renames

| Old File | New File |
|----------|----------|
| `Services/INotesServices.cs` | `Services/ICommentsService.cs` |
| `Services/INotesCommandServices.cs` | `Services/ICommentsCommandService.cs` |
| `Services/NotesService.cs` | `Services/CommentsService.cs` |
| `Services/NotesCommandService.cs` | `Services/CommentsCommandService.cs` |

#### Interface Updates

**File:** `Services/ICommentsService.cs`

```csharp
namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for comments management.
    /// </summary>
    public interface ICommentsService : ICommentsCommandService
    {
        /// <summary>
        /// Gets all comments.
        /// </summary>
        Task<CommentsInfo> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets comments for a specific task.
        /// </summary>
        IAsyncEnumerable<Comment> GetForTaskAsync(ComplexId taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets comments for a specific project.
        /// </summary>
        IAsyncEnumerable<Comment> GetForProjectAsync(ComplexId projectId, CancellationToken cancellationToken = default);
    }
}
```

**File:** `Services/ICommentsCommandService.cs`

```csharp
namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains command operations for comments.
    /// </summary>
    public interface ICommentsCommandService
    {
        /// <summary>
        /// Adds a comment to a task.
        /// </summary>
        Task<ComplexId> AddToTaskAsync(Comment comment, ComplexId taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a comment to a project.
        /// </summary>
        Task<ComplexId> AddToProjectAsync(Comment comment, ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a comment.
        /// </summary>
        Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
```

### Remove NotificationsService

The `NotificationsService` and related models should be removed as live notifications are deprecated. Keep only the `Reminder` and `RemindersService`.

**Files to Remove:**
- `Services/INotificationsService.cs`
- `Services/INotificationsCommandService.cs`
- `Services/NotificationsService.cs`
- `Services/NotificationsCommandService.cs`
- `Models/Notification.cs`
- `Models/NotificationService.cs`
- `Models/NotificationSetting.cs`
- `Models/NotificationSettings.cs`
- `Models/NotificationType.cs`

### ITodoistClient Interface Update

**File:** `ITodoistClient.cs`

```csharp
public interface ITodoistClient
{
    // Renamed from Items
    ITasksService Tasks { get; }

    // Renamed from Notes
    ICommentsService Comments { get; }

    // Remove Notifications property
    // INotificationsService Notifications { get; }  // REMOVED

    // ... rest of properties unchanged
}
```

---

## Endpoint Updates

### REST Endpoint Mapping

| Old Endpoint | New Endpoint | HTTP Method | Notes |
|--------------|--------------|-------------|-------|
| `sync` | `sync` | POST | Unchanged |
| `quick/add` | `tasks/quick` | POST | JSON body |
| `completed/get_all` | `tasks/completed/by_completion_date` | GET | Paginated |
| `completed/get_stats` | `tasks/completed/stats` | GET | |
| `items/get` | `tasks/{id}` | GET | Path param |
| `projects/get_archived` | `projects/archived` | GET | Paginated |
| `backups/get` | `backups` | GET | |
| `activity/get` | `activities` | GET | Paginated |
| `uploads/add` | `uploads` | POST | |
| `uploads/get` | `uploads` | GET | |
| `uploads/delete` | `uploads` | DELETE | |
| `emails/get_or_create` | `emails` | PUT | |
| `emails/disable` | `emails` | DELETE | |
| `templates/import_into_project` | `templates/import_into_project_from_file` | POST | |
| `templates/export_as_file` | `templates/file` | GET | |
| `templates/export_as_url` | `templates/url` | GET | |
| N/A | `tasks` | GET | New, paginated |
| N/A | `tasks/filter` | GET | New, paginated |
| N/A | `projects` | GET | New, paginated |
| N/A | `labels` | GET | New, paginated |
| N/A | `labels/shared` | GET | New, paginated |
| N/A | `sections` | GET | New, paginated |
| N/A | `sections/archived` | GET | New, paginated |
| N/A | `comments` | GET | New, paginated |

### Sync Endpoint Commands (Unchanged)

The `/sync` endpoint still uses legacy command names:

| Operation | Command Type |
|-----------|--------------|
| Add Task | `item_add` |
| Update Task | `item_update` |
| Delete Task | `item_delete` |
| Move Task | `item_move` |
| Complete Task | `item_complete` |
| Close Task | `item_close` |
| Reorder Tasks | `item_reorder` |
| Add Comment | `note_add` |
| Update Comment | `note_update` |
| Delete Comment | `note_delete` |

---

## Pagination Implementation

### IAsyncEnumerable Pattern

All GET endpoints that return collections now use `IAsyncEnumerable<T>`:

```csharp
public interface ITasksService : ITasksCommandService
{
    IAsyncEnumerable<DetailedTask> GetAsync(CancellationToken cancellationToken = default);
}
```

### Implementation Helper

**Add to `CommandServiceBase.cs`:**

```csharp
protected async IAsyncEnumerable<T> GetAllPagesAsync<T>(
    string endpoint,
    ICollection<KeyValuePair<string, string>> baseParameters = null,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
{
    string cursor = null;
    baseParameters ??= new List<KeyValuePair<string, string>>();

    do
    {
        var parameters = new List<KeyValuePair<string, string>>(baseParameters);
        
        if (!string.IsNullOrEmpty(cursor))
        {
            parameters.Add(new KeyValuePair<string, string>("cursor", cursor));
        }
        
        parameters.Add(new KeyValuePair<string, string>("limit", "200"));

        var page = await TodoistClient.GetAsync<PaginatedResponse<T>>(endpoint, parameters, cancellationToken)
            .ConfigureAwait(false);

        foreach (var item in page.Results)
        {
            yield return item;
        }

        cursor = page.NextCursor;
    }
    while (!string.IsNullOrEmpty(cursor));
}
```

### Paginated Endpoints

| Endpoint | Returns |
|----------|---------|
| `GET /tasks` | `IAsyncEnumerable<DetailedTask>` |
| `GET /tasks/filter` | `IAsyncEnumerable<DetailedTask>` |
| `GET /tasks/completed/by_completion_date` | `IAsyncEnumerable<CompletedTask>` |
| `GET /tasks/completed/by_due_date` | `IAsyncEnumerable<CompletedTask>` |
| `GET /projects` | `IAsyncEnumerable<Project>` |
| `GET /projects/archived` | `IAsyncEnumerable<Project>` |
| `GET /labels` | `IAsyncEnumerable<Label>` |
| `GET /labels/shared` | `IAsyncEnumerable<string>` |
| `GET /sections` | `IAsyncEnumerable<Section>` |
| `GET /sections/archived` | `IAsyncEnumerable<Section>` |
| `GET /comments` | `IAsyncEnumerable<Comment>` |
| `GET /activities` | `IAsyncEnumerable<Activity>` |
| `GET /projects/{id}/collaborators` | `IAsyncEnumerable<Collaborator>` |

---

## New Features

### New Endpoints to Implement

#### Tasks

| Endpoint | Method | Description |
|----------|--------|-------------|
| `GET /tasks/filter` | `GetByFilterAsync` | Filter tasks with query |
| `GET /tasks/completed/by_completion_date` | `GetCompletedByCompletionDateAsync` | Get completed by completion date |
| `GET /tasks/completed/by_due_date` | `GetCompletedByDueDateAsync` | Get completed by due date |
| `GET /tasks/completed/stats` | `GetProductivityStatsAsync` | Get productivity statistics |
| `POST /tasks` | `CreateAsync` (REST) | Create task via REST |
| `POST /tasks/{id}` | `UpdateAsync` (REST) | Update task via REST |
| `DELETE /tasks/{id}` | `DeleteAsync` (REST) | Delete task via REST |
| `POST /tasks/{id}/close` | `CloseAsync` (REST) | Close task via REST |
| `POST /tasks/{id}/reopen` | `ReopenAsync` (REST) | Reopen task via REST |
| `POST /tasks/{id}/move` | `MoveAsync` (REST) | Move task via REST |

#### Labels

| Endpoint | Method | Description |
|----------|--------|-------------|
| `GET /labels/shared` | `GetSharedAsync` | Get shared labels |
| `DELETE /labels/shared` | `RemoveSharedAsync` | Remove shared label |
| `POST /labels/shared/rename` | `RenameSharedAsync` | Rename shared label |

#### Sections

| Endpoint | Method | Description |
|----------|--------|-------------|
| `GET /sections/archived` | `GetArchivedAsync` | Get archived sections |

#### Projects

| Endpoint | Method | Description |
|----------|--------|-------------|
| `GET /projects/{id}/collaborators` | `GetCollaboratorsAsync` | Get project collaborators |

#### ID Mapping

| Endpoint | Method | Description |
|----------|--------|-------------|
| `GET /ids_mapping/{object}/{id}` | `GetMappedIdAsync` | Map old numeric ID to new string ID |

### New Models

| Model | Purpose |
|-------|---------|
| `ProductivityStats` | User productivity statistics |
| `IdMapping` | ID mapping response |

---

## Error Handling Updates

### Unified Error Response Format

The new API returns consistent JSON error responses:

```json
{
  "error": "Task not found",
  "error_code": 478,
  "error_extra": {"event_id": "<hash>", "retry_after": 3},
  "error_tag": "NOT_FOUND",
  "http_code": 404
}
```

### Update TodoistException

**File:** `Exceptions/TodoistException.cs`

```csharp
public sealed class TodoistException : Exception
{
    public int Code { get; }
    public string ErrorTag { get; }  // NEW
    public int HttpCode { get; }     // NEW
    public CommandError RawError { get; }

    public TodoistException(int code, string message, CommandError rawError)
        : base(message)
    {
        Code = code;
        ErrorTag = rawError?.ErrorTag;
        HttpCode = rawError?.HttpCode ?? 0;
        RawError = rawError;
    }
}
```

### Update CommandError Model

**File:** `Models/CommandError.cs`

```csharp
public class CommandError
{
    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    [JsonPropertyName("error_tag")]
    public string ErrorTag { get; set; }  // NEW
    
    [JsonPropertyName("http_code")]
    public int HttpCode { get; set; }  // NEW
    
    [JsonPropertyName("error_extra")]
    public Dictionary<string, object> ErrorExtra { get; set; }  // NEW
}
```

---

## Testing Updates

### Test File Renames

| Old File | New File |
|----------|----------|
| `Services/ItemsServiceTests.cs` | `Services/TasksServiceTests.cs` |
| `Services/NotesServiceTests.cs` | `Services/CommentsServiceTests.cs` |
| `Services/NotificationsServiceTests.cs` | **Remove** |

### New Test Cases

```csharp
// Pagination tests
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task GetAllTasksAsync_IteratesAllPages()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    var tasks = new List<DetailedTask>();
    
    await foreach (var task in client.Tasks.GetAsync())
    {
        tasks.Add(task);
    }
    
    Assert.NotNull(tasks);
}

// Filter tasks test
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task GetTasksByFilter_ReturnsFilteredTasks()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    var tasks = new List<DetailedTask>();
    
    await foreach (var task in client.Tasks.GetByFilterAsync("today"))
    {
        tasks.Add(task);
    }
    
    Assert.NotNull(tasks);
}

// Quick add test
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task QuickAddTask_Success()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    DetailedTask task = null;
    
    try
    {
        task = await client.Tasks.QuickAddAsync(new QuickAddTask("Test task tomorrow"));
        Assert.NotNull(task);
        Assert.NotNull(task.Id);
    }
    finally
    {
        if (task != null)
        {
            await client.Tasks.DeleteAsync(task.Id);
        }
    }
}
```

---

## Documentation Updates

### README.md Updates

1. Update API version reference from "Sync API v9" to "Todoist API v1"
2. Update all code examples:
   - `client.Items` → `client.Tasks`
   - `client.Notes` → `client.Comments`
   - `Item` → `DetailedTask`
   - `AddItem` → `AddTask`
   - `Note` → `Comment`
3. Add section on `IAsyncEnumerable` usage for pagination
4. Update NuGet version to v11.0.0

### Version Update

**File:** `Todoist.Net.csproj`

```xml
<PropertyGroup>
    <VersionPrefix>11.0.0</VersionPrefix>
    <PackageReleaseNotes>
        Major release: Migration to Todoist API v1.
        
        BREAKING CHANGES:
        - Types renamed: Item→DetailedTask, AddItem→AddTask, Note→Comment
        - Service renamed: Items→Tasks, Notes→Comments
        - GET methods now return IAsyncEnumerable for pagination
        - Notifications service removed
        - Section.collapsed renamed to is_collapsed
        
        NEW FEATURES:
        - Filter tasks endpoint
        - Completed tasks by date endpoints
        - Shared labels endpoints
        - Archived sections endpoint
        - REST endpoints for tasks
        - ID mapping endpoint
    </PackageReleaseNotes>
</PropertyGroup>
```

---

## File Change Summary

### Files to Rename

| Old Path | New Path |
|----------|----------|
| `Services/ItemsService.cs` | `Services/TasksService.cs` |
| `Services/ItemsCommandService.cs` | `Services/TasksCommandService.cs` |
| `Services/IItemsService.cs` | `Services/ITasksService.cs` |
| `Services/IItemsCommandService.cs` | `Services/ITasksCommandService.cs` |
| `Services/NotesService.cs` | `Services/CommentsService.cs` |
| `Services/NotesCommandService.cs` | `Services/CommentsCommandService.cs` |
| `Services/INotesServices.cs` | `Services/ICommentsService.cs` |
| `Services/INotesCommandServices.cs` | `Services/ICommentsCommandService.cs` |
| `Models/Item.cs` | `Models/DetailedTask.cs` |
| `Models/AddItem.cs` | `Models/AddTask.cs` |
| `Models/UpdateItem.cs` | `Models/UpdateTask.cs` |
| `Models/BaseItem.cs` | `Models/BaseTask.cs` |
| `Models/ItemInfo.cs` | `Models/DetailedTaskInfo.cs` |
| `Models/ItemFilter.cs` | `Models/TaskFilter.cs` |
| `Models/ItemMoveArgument.cs` | `Models/TaskMoveArgument.cs` |
| `Models/CompletedItem.cs` | `Models/CompletedTask.cs` |
| `Models/CompletedItemsInfo.cs` | `Models/CompletedTasksInfo.cs` |
| `Models/CompleteItemArgument.cs` | `Models/CompleteTaskArgument.cs` |
| `Models/CompleteRecurringItemArgument.cs` | `Models/CompleteRecurringTaskArgument.cs` |
| `Models/QuickAddItem.cs` | `Models/QuickAddTask.cs` |
| `Models/ReorderItemsArgument.cs` | `Models/ReorderTasksArgument.cs` |
| `Models/Note.cs` | `Models/Comment.cs` |
| `Models/NotesInfo.cs` | `Models/CommentsInfo.cs` |
| `Tests/Services/ItemsServiceTests.cs` | `Tests/Services/TasksServiceTests.cs` |
| `Tests/Services/NotesServiceTests.cs` | `Tests/Services/CommentsServiceTests.cs` |

### Files to Delete

| Path | Reason |
|------|--------|
| `Services/INotificationsService.cs` | Notifications deprecated |
| `Services/INotificationsCommandService.cs` | Notifications deprecated |
| `Services/NotificationsService.cs` | Notifications deprecated |
| `Services/NotificationsCommandService.cs` | Notifications deprecated |
| `Models/Notification.cs` | Notifications deprecated |
| `Models/NotificationService.cs` | Notifications deprecated |
| `Models/NotificationSetting.cs` | Notifications deprecated |
| `Models/NotificationSettings.cs` | Notifications deprecated |
| `Models/NotificationType.cs` | Notifications deprecated |
| `Tests/Services/NotificationsServiceTests.cs` | Notifications deprecated |

### New Files to Create

| Path | Purpose |
|------|---------|
| `Models/PaginatedResponse.cs` | Generic pagination response |
| `Models/PaginationOptions.cs` | Pagination request options |
| `Models/ProductivityStats.cs` | Productivity statistics |
| `Models/IdMapping.cs` | ID mapping response |

### Files to Modify

| File | Changes |
|------|---------|
| `TodoistRestClient.cs` | Base URL + new HTTP methods |
| `ITodoistRestClient.cs` | New interface methods |
| `TodoistClient.cs` | Service property renames |
| `ITodoistClient.cs` | Interface updates, remove Notifications |
| `IAdvancedTodoistClient.cs` | New REST methods |
| `Models/Resources.cs` | Property renames |
| `Models/Section.cs` | Property attribute update |
| `Models/CommandError.cs` | New properties |
| `Exceptions/TodoistException.cs` | New properties |
| `Todoist.Net.csproj` | Version update |
| `README.md` | Full documentation update |

---

*End of Migration Plan*

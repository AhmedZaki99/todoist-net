# Migration: Todoist.Net v10 → v11 (Sync API v9 → Todoist API v1)

## Overview

Migrate Todoist.Net from the deprecated **Todoist Sync API v9** to the new unified **Todoist API v1**. This is a major breaking release with no backward compatibility.

> **Why?** Todoist has deprecated Sync API v9 and released a unified API v1 with improved consistency, pagination, and standardized naming.

---

## Summary of Changes

### 🔴 Breaking Changes

| Category | Change |
|----------|--------|
| **Base URL** | `/sync/v9/` → `/api/v1/` |
| **Task Models** | `Item` → `DetailedTask`, `AddItem` → `AddTask`, etc. |
| **Comment Models** | `Note` → `Comment`, `NotesInfo` → `CommentsInfo` |
| **Services** | `ItemsService` → `TasksService`, `NotesService` → `CommentsService` |
| **Client Properties** | `client.Items` → `client.Tasks`, `client.Notes` → `client.Comments` |
| **Resources Properties** | `Resources.Items` → `Resources.Tasks`, `Resources.Notes` → `Resources.Comments` |
| **Pagination** | GET methods return `IAsyncEnumerable<T>` instead of `Task<IEnumerable<T>>` |
| **Removed** | `NotificationsService` and all notification-related models |
| **Property Renames** | `Section.collapsed` → `Section.is_collapsed` |
| **Removed Properties** | `User.IsBizAdmin` |

### 🟢 New Features

- Cursor-based pagination with `IAsyncEnumerable<T>` support
- Filter tasks endpoint (`GET /tasks/filter`)
- Completed tasks by completion date endpoint
- Completed tasks by due date endpoint
- Archived sections endpoint
- Shared labels endpoints
- REST endpoints for task CRUD operations
- ID mapping endpoint for migrating old numeric IDs
- Unified JSON error response format

---

## Type Renames

### Task Models

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
| `ReorderItemsArgument` | `ReorderTasksArgument` |

### Comment Models

| Old Name | New Name |
|----------|----------|
| `Note` | `Comment` |
| `NotesInfo` | `CommentsInfo` |

---

## Service Renames

| Old Service | New Service |
|-------------|-------------|
| `IItemsService` | `ITasksService` |
| `IItemsCommandService` | `ITasksCommandService` |
| `ItemsService` | `TasksService` |
| `ItemsCommandService` | `TasksCommandService` |
| `INotesServices` | `ICommentsService` |
| `INotesCommandServices` | `ICommentsCommandService` |
| `NotesService` | `CommentsService` |
| `NotesCommandService` | `CommentsCommandService` |

---

## Files to Change

### Renames (26 files)

**Services:**
- `IItemsService.cs` → `ITasksService.cs`
- `IItemsCommandService.cs` → `ITasksCommandService.cs`
- `ItemsService.cs` → `TasksService.cs`
- `ItemsCommandService.cs` → `TasksCommandService.cs`
- `INotesServices.cs` → `ICommentsService.cs`
- `INotesCommandServices.cs` → `ICommentsCommandService.cs`
- `NotesService.cs` → `CommentsService.cs`
- `NotesCommandService.cs` → `CommentsCommandService.cs`

**Models:**
- `Item.cs` → `DetailedTask.cs`
- `AddItem.cs` → `AddTask.cs`
- `UpdateItem.cs` → `UpdateTask.cs`
- `BaseItem.cs` → `BaseTask.cs`
- `ItemInfo.cs` → `DetailedTaskInfo.cs`
- `ItemFilter.cs` → `TaskFilter.cs`
- `ItemMoveArgument.cs` → `TaskMoveArgument.cs`
- `CompletedItem.cs` → `CompletedTask.cs`
- `CompletedItemsInfo.cs` → `CompletedTasksInfo.cs`
- `CompleteItemArgument.cs` → `CompleteTaskArgument.cs`
- `CompleteRecurringItemArgument.cs` → `CompleteRecurringTaskArgument.cs`
- `QuickAddItem.cs` → `QuickAddTask.cs`
- `ReorderItemsArgument.cs` → `ReorderTasksArgument.cs`
- `Note.cs` → `Comment.cs`
- `NotesInfo.cs` → `CommentsInfo.cs`

**Tests:**
- `ItemsServiceTests.cs` → `TasksServiceTests.cs`
- `NotesServiceTests.cs` → `CommentsServiceTests.cs`

### Deletions (10 files)

- `Services/INotificationsService.cs`
- `Services/INotificationsCommandService.cs`
- `Services/NotificationsService.cs`
- `Services/NotificationsCommandService.cs`
- `Models/Notification.cs`
- `Models/NotificationService.cs`
- `Models/NotificationSetting.cs`
- `Models/NotificationSettings.cs`
- `Models/NotificationType.cs`
- `Tests/Services/NotificationsServiceTests.cs`

### New Files (4 files)

- `Models/PaginatedResponse.cs`
- `Models/PaginationOptions.cs`
- `Models/ProductivityStats.cs`
- `Models/IdMapping.cs`

### Major Modifications

- `TodoistRestClient.cs` - Base URL + new HTTP methods (DELETE, PUT, PostJson)
- `ITodoistRestClient.cs` - New interface methods
- `TodoistClient.cs` - Service property renames
- `ITodoistClient.cs` - Interface updates, remove Notifications
- `IAdvancedTodoistClient.cs` - New REST methods
- `Models/Resources.cs` - Property renames (Items→Tasks, Notes→Comments)
- `Models/Section.cs` - Property attribute update (collapsed→is_collapsed)
- `Models/CommandError.cs` - New properties (ErrorTag, HttpCode, ErrorExtra)
- `Exceptions/TodoistException.cs` - New properties (ErrorTag, HttpCode)
- `Todoist.Net.csproj` - Version 11.0.0
- `README.md` - Full documentation update

---

## Endpoint Updates

| Old Endpoint | New Endpoint | Method |
|--------------|--------------|--------|
| `sync` | `sync` | POST |
| `quick/add` | `tasks/quick` | POST |
| `completed/get_all` | `tasks/completed/by_completion_date` | GET |
| `completed/get_stats` | `tasks/completed/stats` | GET |
| `items/get` | `tasks/{id}` | GET |
| `projects/get_archived` | `projects/archived` | GET |
| `backups/get` | `backups` | GET |
| `activity/get` | `activities` | GET |
| `uploads/add` | `uploads` | POST |
| `uploads/get` | `uploads` | GET |
| `uploads/delete` | `uploads` | DELETE |
| `emails/get_or_create` | `emails` | PUT |
| `emails/disable` | `emails` | DELETE |

---

## Acceptance Criteria

- [ ] All type renames completed
- [ ] All service renames completed
- [ ] All endpoint updates completed
- [ ] Pagination implemented with `IAsyncEnumerable<T>`
- [ ] New features implemented
- [ ] Notifications service removed
- [ ] All tests pass
- [ ] README.md updated
- [ ] Version updated to 11.0.0

---

## Resources

- 📖 [Detailed Migration Plan](./MigrationPlan.md)
- 📖 [Todoist API v1 Documentation](./documentation/docs/)
- 📖 [Migration Guide from Todoist](./documentation/docs/38-migration-v9.md)

---

**Labels:** `enhancement`, `breaking-change`, `api-migration`, `v11.0`

**Milestone:** v11.0.0 Release

# Todoist.Net

[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A strongly-typed .NET client library for the [Todoist Sync API v9](https://developer.todoist.com/sync/v9/).

## Features

- ✅ Full coverage of Todoist Sync API v9
- ✅ Strongly-typed models for all Todoist entities
- ✅ Transaction support for batching multiple operations
- ✅ Async/await pattern with cancellation token support
- ✅ Dependency injection support via `IHttpClientFactory`
- ✅ Cross-platform: .NET Standard 2.0 and .NET Framework 4.6.2+

## Installation

Install via [NuGet](https://www.nuget.org/packages/Todoist.Net/):

```powershell
# Package Manager
Install-Package Todoist.Net

# .NET CLI
dotnet add package Todoist.Net
```

## Quick Start

### Creating a Client

```csharp
using Todoist.Net;

// Create a client with your API token
ITodoistClient client = new TodoistClient("your-api-token");
```

> 💡 Get your API token from [Todoist Integrations Settings](https://todoist.com/prefs/integrations).

### Quick Add (Natural Language)

Use natural language to create tasks, just like in the official Todoist apps:

```csharp
// Parse labels, projects, and assignees from natural language
var quickAddItem = new QuickAddItem("Buy milk tomorrow @shopping #Groceries");
var task = await client.Items.QuickAddAsync(quickAddItem);
```

## Usage Examples

### Working with Tasks

```csharp
// Get all tasks
var tasks = await client.Items.GetAsync();

// Get a specific task
var taskInfo = await client.Items.GetAsync("task-id");

// Create a new task
var newTask = new AddItem("Complete project documentation")
{
    DueDate = DueDate.FromText("next monday"),
    Priority = Priority.High,
    Description = "Include API examples and diagrams"
};
var taskId = await client.Items.AddAsync(newTask);

// Update a task
var updateTask = new UpdateItem(taskId)
{
    Content = "Updated task title",
    Priority = Priority.Medium
};
await client.Items.UpdateAsync(updateTask);

// Complete a task
await client.Items.CloseAsync(taskId);

// Delete a task
await client.Items.DeleteAsync(taskId);
```

### Working with Due Dates

```csharp
// Natural language due date
var task1 = new AddItem("Task 1")
{
    DueDate = DueDate.FromText("every friday", Language.English)
};

// Specific date (full day)
var task2 = new AddItem("Task 2")
{
    DueDate = DueDate.CreateFullDay(new DateTime(2025, 12, 25))
};

// Floating date/time (user's local timezone)
var task3 = new AddItem("Task 3")
{
    DueDate = DueDate.CreateFloating(DateTime.Now.AddDays(1))
};

// Fixed timezone date/time
var task4 = new AddItem("Task 4")
{
    DueDate = DueDate.CreateFixedTimeZone(DateTime.UtcNow.AddHours(2), "America/New_York")
};
```

### Working with Projects

```csharp
// Get all projects
var projects = await client.Projects.GetAsync();

// Create a project
var project = new Project("My New Project")
{
    Color = "red",
    IsFavorite = true
};
var projectId = await client.Projects.AddAsync(project);

// Archive a project
await client.Projects.ArchiveAsync(projectId);

// Delete a project
await client.Projects.DeleteAsync(projectId);
```

### Working with Labels

```csharp
// Get all labels
var labels = await client.Labels.GetAsync();

// Create a label
var label = new Label("urgent") { Color = "red" };
await client.Labels.AddAsync(label);

// Use labels when creating tasks
var task = new AddItem("Important task");
task.Labels.Add("urgent");
task.Labels.Add("work");
await client.Items.AddAsync(task);
```

### Working with Sections

```csharp
// Create a section in a project
var section = new Section("In Progress", projectId);
await client.Sections.AddAsync(section);

// Add a task to a section
var task = new AddItem("Task in section")
{
    ProjectId = projectId,
    Section = section.Id.ToString()
};
await client.Items.AddAsync(task);
```

### Working with Notes/Comments

```csharp
// Add a note to a task
var note = new Note("This is a comment on the task");
await client.Notes.AddToItemAsync(note, taskId);

// Add a note to a project
var projectNote = new Note("Project-level comment");
await client.Notes.AddToProjectAsync(projectNote, projectId);
```

### Getting All Resources

```csharp
// Get all resources at once
var resources = await client.GetResourcesAsync();

// Access individual resource types
var allProjects = resources.Projects;
var allTasks = resources.Items;
var allLabels = resources.Labels;
var allSections = resources.Sections;

// Get specific resource types only
var projectsAndLabels = await client.GetResourcesAsync(
    ResourceType.Projects, 
    ResourceType.Labels
);

// Incremental sync (get only changes since last sync)
var changes = await client.GetResourcesAsync(
    resources.SyncToken,  // Token from previous sync
    ResourceType.Items
);
```

## Transactions (Batching)

Batch multiple operations into a single HTTP request for better performance:

```csharp
// Create a transaction
var transaction = client.CreateTransaction();

// Queue multiple operations (these don't execute immediately)
var projectId = await transaction.Project.AddAsync(new Project("New Project"));
var taskId = await transaction.Items.AddAsync(new AddItem("New Task", projectId));
await transaction.Notes.AddToItemAsync(new Note("Task note"), taskId);

// Execute all operations in a single request
await transaction.CommitAsync();

// After commit, IDs are updated to their permanent values
Console.WriteLine($"Project ID: {projectId.PersistentId}");
Console.WriteLine($"Task ID: {taskId.PersistentId}");
```

> 💡 **Transaction Benefits:**
> - Reduced API calls and latency
> - Atomic operations (all succeed or all fail)
> - Automatic resolution of temporary IDs to persistent IDs

## Updating Entities

### Standard Update

```csharp
var task = new UpdateItem("existing-task-id")
{
    Content = "Updated content",
    Priority = Priority.High
};
await client.Items.UpdateAsync(task);
```

### Clearing Property Values (Unset Pattern)

When updating entities, null values are excluded by default (PATCH semantics). To explicitly clear a property, use the `Unset` extension method:

```csharp
// Remove a task's due date
var task = new UpdateItem("task-id");
task.Unset(t => t.DueDate);
await client.Items.UpdateAsync(task);

// Remove a task's duration (Premium feature)
var task2 = new UpdateItem("task-id");
task2.Unset(t => t.Duration);
await client.Items.UpdateAsync(task2);
```

## Dependency Injection

For ASP.NET Core and other DI-enabled applications:

```csharp
// In Startup.cs or Program.cs
services.AddTodoistClient();

// In your service
public class MyService
{
    private readonly ITodoistClientFactory _todoistFactory;
    
    public MyService(ITodoistClientFactory todoistFactory)
    {
        _todoistFactory = todoistFactory;
    }
    
    public async Task DoWorkAsync(string userToken)
    {
        var client = _todoistFactory.CreateClient(userToken);
        var projects = await client.Projects.GetAsync();
        // ...
    }
}
```

## Premium Features

Some features require a Todoist Premium subscription:

| Feature | Service |
|---------|---------|
| Reminders | `client.Reminders` |
| Filters | `client.Filters` |
| Templates | `client.Templates` |
| Task Duration | `Duration` property on tasks |
| Completed Tasks | `client.Items.GetCompletedAsync()` |

```csharp
// Example: Working with reminders (Premium)
var reminder = new Reminder(taskId)
{
    DueDate = DueDate.CreateFloating(DateTime.Now.AddHours(1))
};
await client.Reminders.AddAsync(reminder);

// Example: Task with duration (Premium)
var task = new AddItem("Meeting")
{
    DueDate = DueDate.FromText("tomorrow at 2pm"),
    Duration = new Duration(60, DurationUnit.Minute)
};
await client.Items.AddAsync(task);
```

## Available Services

| Service | Description |
|---------|-------------|
| `client.Items` | Tasks (items) management |
| `client.Projects` | Projects management |
| `client.Labels` | Labels management |
| `client.Sections` | Sections management |
| `client.Notes` | Comments/notes management |
| `client.Filters` | Filters management (Premium) |
| `client.Reminders` | Reminders management (Premium) |
| `client.Templates` | Templates management (Premium) |
| `client.Users` | User management |
| `client.Sharing` | Project sharing |
| `client.Notifications` | Live notifications |
| `client.Activity` | Activity log |
| `client.Backups` | Account backups |
| `client.Uploads` | File uploads |
| `client.Emails` | Email integration (Premium) |

## Error Handling

```csharp
try
{
    await client.Items.AddAsync(new AddItem("New task"));
}
catch (AggregateException ex) when (ex.InnerException is TodoistException todoistEx)
{
    Console.WriteLine($"Todoist API Error: {todoistEx.Message}");
    Console.WriteLine($"Error Code: {todoistEx.Code}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP Error: {ex.Message}");
}
```

## Cancellation Support

All async methods support cancellation:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var projects = await client.Projects.GetAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was cancelled");
}
```

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Links

- [NuGet Package](https://www.nuget.org/packages/Todoist.Net/)
- [Todoist Sync API Documentation](https://developer.todoist.com/sync/v9/)
- [GitHub Repository](https://github.com/olsh/todoist-net)
- [SonarCloud Analysis](https://sonarcloud.io/dashboard?id=todoist-net)

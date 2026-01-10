# Todoist.Net - Project Architecture

This document provides a comprehensive overview of the Todoist.Net library architecture, design patterns, coding conventions, and structural organization. It serves as the authoritative reference for the upcoming migration from Todoist Sync API v9 to the Todoist Unified API v1.

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Solution Structure](#solution-structure)
3. [Design Patterns](#design-patterns)
4. [Service Architecture](#service-architecture)
5. [Model Architecture](#model-architecture)
6. [Serialization Strategy](#serialization-strategy)
7. [Client Architecture](#client-architecture)
8. [Coding Conventions](#coding-conventions)
9. [Naming Conventions](#naming-conventions)
10. [File Organization](#file-organization)
11. [Dependency Injection Support](#dependency-injection-support)
12. [Error Handling](#error-handling)
13. [Migration Considerations](#migration-considerations)

---

## Project Overview

**Todoist.Net** is a .NET client library for the Todoist Sync API v9. It provides a strongly-typed, asynchronous API for interacting with Todoist services.

### Target Frameworks
- `netstandard2.0` - For broad .NET compatibility
- `net462` - For .NET Framework 4.6.2+ support

### Key Dependencies
- `System.Text.Json` (v8.0.6) - JSON serialization
- `Microsoft.Extensions.Http` (v8.0.1) - HttpClientFactory support (netstandard2.0 only)
- `Microsoft.CSharp` (v4.7.0) - Dynamic support

### Build Configuration
- `TreatWarningsAsErrors` is enabled
- `GenerateDocumentationFile` is enabled for XML documentation
- `InternalsVisibleTo` exposes internals to `Todoist.Net.Tests`

---

## Solution Structure

```
Todoist.Net/
├── src/
│   ├── Todoist.Net/                    # Main library
│   │   ├── Exceptions/                 # Custom exception types
│   │   ├── Extensions/                 # Extension methods
│   │   ├── Models/                     # Entity models and DTOs
│   │   ├── Properties/                 # Assembly info
│   │   ├── Serialization/
│   │   │   ├── Converters/             # Custom JSON converters
│   │   │   └── Resolvers/              # JSON resolver modifiers
│   │   ├── Services/                   # Service implementations
│   │   ├── TodoistClient.cs            # Main client implementation
│   │   ├── TodoistClientFactory.cs     # Factory for DI scenarios
│   │   ├── TodoistRestClient.cs        # Low-level HTTP client
│   │   └── Interfaces (I*.cs)          # Public interfaces
│   │
│   └── Todoist.Net.Tests/              # Test project
│       ├── Extensions/                 # Test constants and helpers
│       ├── Helpers/                    # Test utilities
│       ├── Models/                     # Test models
│       ├── Services/                   # Service integration tests
│       └── Settings/                   # Test configuration
│
├── build/                              # NUKE build configuration
└── documentation/                      # API documentation (v1)
```

---

## Design Patterns

### 1. Service-Per-Resource Pattern

Each Todoist entity type has its own dedicated service, following a consistent inheritance hierarchy:

```
I{Resource}CommandService (interface) ← write operations
        ↑
I{Resource}Service (interface) ← read + write operations (extends command service)
        ↑
{Resource}CommandService (class) ← implements write operations
        ↑
{Resource}Service (class) ← adds read operations
```

**Example: Items Service**
```csharp
// Interface hierarchy
public interface IItemsCommandService { /* Add, Update, Delete, etc. */ }
public interface IItemsService : IItemsCommandService { /* GetAsync, QuickAddAsync, etc. */ }

// Implementation hierarchy
internal class ItemsCommandService : CommandServiceBase, IItemsCommandService { }
internal class ItemsService : ItemsCommandService, IItemsService { }
```

**Rationale**: This separation allows:
- Transaction services to expose only command operations
- Full services to expose both read and write operations
- Clear distinction between operations that can be batched vs. immediate reads

### 2. Command Pattern (for Write Operations)

All write operations are encapsulated as `Command` objects that can be executed immediately or queued for batch execution.

```csharp
internal class Command
{
    public CommandType CommandType { get; }      // The operation type
    public ICommandArgument Argument { get; }    // The operation arguments
    public Guid? TempId { get; }                 // Temporary ID for new entities
    public Guid Uid { get; }                     // Unique command identifier
}
```

### 3. Factory Pattern

The `TodoistClientFactory` creates `TodoistClient` instances with proper `HttpClient` management for DI scenarios.

```csharp
public interface ITodoistClientFactory
{
    TodoistClient CreateClient(string token);
}
```

### 4. Facade Pattern

`TodoistClient` acts as a facade, exposing all services through a unified interface:

```csharp
public interface ITodoistClient
{
    IItemsService Items { get; }
    IProjectsService Projects { get; }
    ILabelsService Labels { get; }
    // ... other services
}
```

### 5. Transaction Pattern

The `Transaction` class enables batching multiple commands into a single HTTP request:

```csharp
public interface ITransaction
{
    IItemsCommandService Items { get; }
    IProjectCommandService Project { get; }
    // ... command services only
    Task<string> CommitAsync(CancellationToken cancellationToken = default);
}
```

### 6. StringEnum Pattern

Type-safe string constants using a custom `StringEnum` base class:

```csharp
public abstract class StringEnum : IEquatable<StringEnum>
{
    protected StringEnum(string value) { Value = value; }
    internal string Value { get; }
    public static bool TryParse<T>(string value, out T result) where T : StringEnum;
}

// Usage
internal class CommandType : StringEnum
{
    public static CommandType AddItem { get; } = new CommandType("item_add");
    public static CommandType DeleteItem { get; } = new CommandType("item_delete");
}
```

### 7. ComplexId Pattern

Handles both persistent (string from API) and temporary (Guid for transactions) entity identifiers:

```csharp
public struct ComplexId : IEquatable<ComplexId>
{
    public string PersistentId { get; }
    public Guid TempId { get; }
    public bool IsEmpty { get; }
    
    // Implicit conversions for ease of use
    public static implicit operator ComplexId(string i);
    public static implicit operator ComplexId(Guid g);
}
```

### 8. Unset Pattern

Allows explicit null values to be sent to the API for property removal:

```csharp
public interface IUnsettableProperties
{
    HashSet<PropertyInfo> UnsetProperties { get; }
}

// Extension method for usage
public static void Unset<T, TProp>(this T entity, Expression<Func<T, TProp>> propertyExpression)
    where T : IUnsettableProperties;

// Usage
item.Unset(i => i.DueDate);  // Explicitly send null to API
```

---

## Service Architecture

### Base Service Class

All services inherit from `CommandServiceBase`:

```csharp
internal abstract class CommandServiceBase
{
    private readonly ICollection<Command> _queue;  // For transactions
    
    protected CommandServiceBase(IAdvancedTodoistClient todoistClient);  // Direct execution
    protected CommandServiceBase(ICollection<Command> queue);             // Transaction mode
    
    internal IAdvancedTodoistClient TodoistClient { get; }
    
    // Helper methods for creating commands
    internal Command CreateAddCommand<T>(CommandType commandType, T entity) where T : BaseEntity;
    internal Command CreateEntityCommand(CommandType commandType, ComplexId id);
    internal Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken = default);
}
```

### Service Implementation Template

**Command Service (Write Operations)**:
```csharp
internal class {Resource}CommandService : CommandServiceBase, I{Resource}CommandService
{
    // Two constructors: one for direct execution, one for transactions
    internal {Resource}CommandService(IAdvancedTodoistClient todoistClient) : base(todoistClient) { }
    internal {Resource}CommandService(ICollection<Command> queue) : base(queue) { }
    
    public async Task<ComplexId> AddAsync({Resource} entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        
        var command = CreateAddCommand(CommandType.Add{Resource}, entity);
        await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
        return entity.Id;
    }
    
    public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
    {
        var command = CreateEntityCommand(CommandType.Delete{Resource}, id);
        return ExecuteCommandAsync(command, cancellationToken);
    }
    
    public Task UpdateAsync({Resource} entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        
        var command = new Command(CommandType.Update{Resource}, entity);
        return ExecuteCommandAsync(command, cancellationToken);
    }
}
```

**Full Service (Read + Write Operations)**:
```csharp
internal class {Resource}Service : {Resource}CommandService, I{Resource}Service
{
    // Only one constructor - full services aren't used in transactions
    internal {Resource}Service(IAdvancedTodoistClient todoistClient) : base(todoistClient) { }
    
    public async Task<IEnumerable<{Resource}>> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.{Resources})
            .ConfigureAwait(false);
        return response.{Resources};
    }
    
    public Task<{Resource}Info> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
    {
        return TodoistClient.PostAsync<{Resource}Info>(
            "{resources}/get",
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("{resource}_id", id.ToString())
            },
            cancellationToken);
    }
}
```

### Service Interface Conventions

**Command Service Interface**:
```csharp
public interface I{Resource}CommandService
{
    Task<ComplexId> AddAsync({AddResource} entity, CancellationToken cancellationToken = default);
    Task UpdateAsync({UpdateResource} entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);
    // Resource-specific operations...
}
```

**Full Service Interface**:
```csharp
public interface I{Resource}Service : I{Resource}CommandService
{
    Task<IEnumerable<{Resource}>> GetAsync(CancellationToken cancellationToken = default);
    Task<{Resource}Info> GetAsync(ComplexId id, CancellationToken cancellationToken = default);
    // Additional read operations...
}
```

---

## Model Architecture

### Model Hierarchy

```
ICommandArgument (marker interface)
        ↓
BaseEntity (Id property)
        ↓
BaseUnsetEntity (adds IUnsettableProperties)
        ↓
Domain-specific models (Item, Project, Note, etc.)
```

### Model Categories

1. **Read Models** - Returned from API (e.g., `Item`, `Project`)
   - Have `internal set` on read-only properties
   - Include all API response fields

2. **Add Models** - For creating entities (e.g., `AddItem`)
   - Include only fields valid for creation
   - May implement `IWithRelationsArgument` for temp ID resolution

3. **Update Models** - For updating entities (e.g., `UpdateItem`)
   - Extend `BaseUnsetEntity` for null handling
   - Include only updateable fields

4. **Argument Models** - For complex command arguments
   - Implement `ICommandArgument`
   - Examples: `ItemMoveArgument`, `CompleteItemArgument`

5. **Info Models** - Extended response data (e.g., `ItemInfo`, `ProjectInfo`)
   - Contain the entity plus related data

### Model Template

**Base Entity**:
```csharp
public class BaseEntity : ICommandArgument
{
    internal BaseEntity(ComplexId id) { Id = id; }
    [JsonConstructor] internal BaseEntity() { }
    
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ComplexId Id { get; set; }
}
```

**Domain Model**:
```csharp
public class {Resource} : BaseEntity  // or BaseUnsetEntity
{
    public {Resource}(string requiredField)
    {
        RequiredField = requiredField;
    }
    
    [JsonPropertyName("required_field")]
    public string RequiredField { get; set; }
    
    [JsonPropertyName("optional_field")]
    public string OptionalField { get; set; }
    
    // Read-only properties have internal set
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; internal set; }
}
```

### Property Conventions

- **Required properties**: Set via constructor
- **Optional properties**: Public get/set
- **Read-only properties**: `internal set` (set by API response)
- **Nullable types**: Use `?` for optional value types
- **Collections**: Use `ICollection<T>` for mutable, `IReadOnlyCollection<T>` for read-only
- **JSON mapping**: Always use `[JsonPropertyName("snake_case")]`

---

## Serialization Strategy

### JSON Serializer Configuration

```csharp
private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers =
        {
            JsonResolverModifiers.SerializeInternalSetters,
            JsonResolverModifiers.FilterSerializationByType,
            JsonResolverModifiers.IncludeUnsetProperties
        }
    },
    Converters =
    {
        new StringEnumTypeConverter(),
        new ComplexIdConverter(),
        new CommandResultConverter(),
        new CommandArgumentConverter()
    }
};
```

### Custom Converters

| Converter | Purpose |
|-----------|---------|
| `ComplexIdConverter` | Handles both nullable and non-nullable `ComplexId` serialization |
| `StringEnumTypeConverter` | Converts `StringEnum` subclasses to/from their string values |
| `CommandArgumentConverter` | Serializes `ICommandArgument` using concrete type |
| `CommandResultConverter` | Deserializes command execution results |
| `BoolConverter` | Handles API's boolean representations (0/1, "0"/"1", true/false) |
| `DateOnlyConverter` | Handles date-only values |

### Resolver Modifiers

| Modifier | Purpose |
|----------|---------|
| `SerializeInternalSetters` | Allows deserialization into properties with internal setters |
| `FilterSerializationByType` | Excludes certain types from serialization |
| `IncludeUnsetProperties` | Includes explicitly unset properties as null in serialization |

---

## Client Architecture

### Interface Hierarchy

```csharp
public interface ITodoistClient
{
    // All service accessors
    IItemsService Items { get; }
    IProjectsService Projects { get; }
    // ...
    
    // Transaction creation
    ITransaction CreateTransaction();
    
    // Resource fetching
    Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes);
}

internal interface IAdvancedTodoistClient : ITodoistClient
{
    // Low-level operations for services
    Task<string> ExecuteCommandsAsync(params Command[] commands);
    Task<T> PostAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters, ...);
    Task<T> GetAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters, ...);
    // ...
}
```

### REST Client

```csharp
public interface ITodoistRestClient : IDisposable
{
    Task<HttpResponseMessage> GetAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters, ...);
    Task<HttpResponseMessage> PostAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters, ...);
    Task<HttpResponseMessage> PostFormAsync(string resource, ..., IEnumerable<UploadFile> files, ...);
}
```

### Client Initialization Options

```csharp
// Basic usage
var client = new TodoistClient("api_token");

// With proxy
var client = new TodoistClient("api_token", webProxy);

// With custom REST client (for testing)
var client = new TodoistClient(customRestClient);

// Via DI factory
services.AddTodoistClient();
var client = factory.CreateClient("api_token");
```

---

## Coding Conventions

### General Style

- **Indentation**: 4 spaces (2 for .csproj, .json, .config)
- **Charset**: UTF-8
- **Line endings**: Insert final newline, trim trailing whitespace
- **Using directives**: System namespaces first, separated from other namespaces

### Async/Await Conventions

- All async methods end with `Async` suffix
- Always use `ConfigureAwait(false)` in library code
- All async methods accept optional `CancellationToken cancellationToken = default`

```csharp
public async Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default)
{
    // ...
    await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
    return item.Id;
}
```

### Null Checking

- Use `ArgumentNullException` for null parameters
- Check null at method entry point

```csharp
if (item == null)
{
    throw new ArgumentNullException(nameof(item));
}
```

### Access Modifiers

- **Services**: `internal class`, `internal` constructors
- **Interfaces**: `public interface`
- **Models**: `public class` with mixture of access levels
- **Properties**: Explicit access modifiers on getters/setters when different

### Constructor Patterns

- Use `[JsonConstructor]` for parameterless constructors used by deserialization
- Mark internal constructors appropriately
- Use `private protected` for abstract base classes

```csharp
public class Project : BaseEntity
{
    public Project(string name) { Name = name; }
    
    [JsonConstructor]
    internal Project() { }  // For deserialization
}
```

### Expression Body vs Block Body

- Use expression body for simple single-line operations
- Use block body for multi-statement methods

```csharp
// Expression body
public Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes) =>
    GetResourcesAsync("*", resourceTypes);

// Block body
public async Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default)
{
    if (item == null)
        throw new ArgumentNullException(nameof(item));
    
    var command = CreateAddCommand(CommandType.AddItem, item);
    await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
    
    return item.Id;
}
```

---

## Naming Conventions

### Classes and Interfaces

| Pattern | Example |
|---------|---------|
| `{Resource}Service` | `ItemsService`, `ProjectsService` |
| `{Resource}CommandService` | `ItemsCommandService` |
| `I{Resource}Service` | `IItemsService` |
| `I{Resource}CommandService` | `IItemsCommandService` |
| `{Resource}` | `Item`, `Project`, `Label` |
| `{Resource}Info` | `ItemInfo`, `ProjectInfo` |
| `Add{Resource}` | `AddItem` (for creation DTOs) |
| `Update{Resource}` | `UpdateItem` (for update DTOs) |
| `{Action}{Resource}Argument` | `ItemMoveArgument`, `CompleteItemArgument` |

### Methods

| Pattern | Example |
|---------|---------|
| `GetAsync` | Retrieve single/collection |
| `AddAsync` | Create new entity |
| `UpdateAsync` | Update existing entity |
| `DeleteAsync` | Remove entity |
| `{Action}Async` | Domain-specific actions (e.g., `CloseAsync`, `ArchiveAsync`) |

### Properties

- Use PascalCase for .NET properties
- Use `[JsonPropertyName("snake_case")]` for API mapping
- Prefix boolean properties with `Is` when appropriate

```csharp
[JsonPropertyName("is_deleted")]
public bool? IsDeleted { get; internal set; }

[JsonPropertyName("child_order")]
public int? ChildOrder { get; set; }
```

### Parameters

- Use camelCase
- Be descriptive but concise
- Use `cancellationToken` (not `ct` or `token`)

---

## File Organization

### One Type Per File

- Each public/internal class or interface gets its own file
- Exception: Nested private classes can be in parent file

### File Naming

- Filename matches the type name exactly
- Interfaces: `I{TypeName}.cs`
- Classes: `{TypeName}.cs`

### Folder Structure (Services)

```
Services/
├── I{Resource}Service.cs          # Full service interface
├── I{Resource}CommandService.cs   # Command service interface
├── {Resource}Service.cs           # Full service implementation
├── {Resource}CommandService.cs    # Command service implementation
├── CommandServiceBase.cs          # Base class
├── Transaction.cs                 # Transaction implementation
└── ITransaction.cs                # Transaction interface
```

### Folder Structure (Models)

```
Models/
├── BaseEntity.cs                  # Base entity class
├── BaseUnsetEntity.cs             # Unsettable properties base
├── ICommandArgument.cs            # Marker interface
├── IUnsettableProperties.cs       # Unset interface
├── ComplexId.cs                   # ID struct
├── Command.cs                     # Command class
├── CommandType.cs                 # Command types
├── {Resource}.cs                  # Domain models
├── {Resource}Info.cs              # Info wrapper models
├── Add{Resource}.cs               # Creation DTOs
├── Update{Resource}.cs            # Update DTOs
└── {Action}Argument.cs            # Action arguments
```

---

## Dependency Injection Support

### Registration

```csharp
// netstandard2.0 only
public static IServiceCollection AddTodoistClient(this IServiceCollection services)
{
    services.AddHttpClient();
    services.AddSingleton<ITodoistClientFactory, TodoistClientFactory>();
    return services;
}
```

### Factory Usage

```csharp
public class MyService
{
    private readonly ITodoistClientFactory _todoistFactory;
    
    public MyService(ITodoistClientFactory todoistFactory)
    {
        _todoistFactory = todoistFactory;
    }
    
    public async Task DoWork(string userToken)
    {
        var client = _todoistFactory.CreateClient(userToken);
        // Use client...
    }
}
```

---

## Error Handling

### Exception Types

```csharp
[Serializable]
public sealed class TodoistException : Exception
{
    public int Code { get; }
    public CommandError RawError { get; }
}
```

### Error Propagation

- API errors are thrown as `HttpRequestException` for HTTP-level failures
- Command execution errors are aggregated into `AggregateException` containing `TodoistException` instances
- Null argument errors throw `ArgumentNullException`
- Invalid argument errors throw `ArgumentException`

### Exception Documentation

Always document exceptions in XML comments:

```csharp
/// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
/// <exception cref="AggregateException">Command execution exception.</exception>
/// <exception cref="HttpRequestException">API exception.</exception>
```

---

## Migration Considerations

### Key Areas for Migration

1. **API Endpoints**: Update `TodoistRestClient` base URL and endpoint paths
2. **Command Types**: Review and update `CommandType` values for new API
3. **Models**: Update JSON property names to match new API schema
4. **Services**: Add/remove service methods based on new API capabilities
5. **Response Handling**: Update deserialization for new response formats

### Preserved Patterns

The following patterns should be preserved during migration:

- Service-per-resource architecture
- Command/Transaction pattern for write operations
- ComplexId handling for entity references
- Unset pattern for null property handling
- Interface separation (command vs. full service)
- Async/await conventions with CancellationToken support

### Breaking Change Considerations

- Model property changes may require new model classes (vX.AddItem vs AddItem)
- New API features may require new service methods
- Removed API features should result in method deprecation with appropriate attributes
- Consider providing migration guides for consumers

---

## Appendix: Complete Service List

| Service | Description | Premium |
|---------|-------------|---------|
| `Items` | Task management | No |
| `Projects` | Project management | No |
| `Labels` | Label management | No |
| `Sections` | Section management | No |
| `Notes` | Comment/note management | No |
| `Filters` | Filter management | Yes |
| `Reminders` | Reminder management | Yes |
| `Templates` | Template management | Yes |
| `Notifications` | Live notification management | No |
| `Activity` | Activity log access | No |
| `Backups` | Backup management | No |
| `Uploads` | File upload management | No |
| `Users` | User management | No |
| `Sharing` | Project sharing | No |
| `Emails` | Email integration | Yes |

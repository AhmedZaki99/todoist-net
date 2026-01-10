# Todoist.Net - AI Coding Instructions

## Project Overview
A .NET client library for the [Todoist Sync API v9](https://developer.todoist.com/sync/v9/). Targets `netstandard2.0` and `net462` for broad compatibility.

## Architecture

### Service Pattern
The library uses a **service-per-resource** pattern where each Todoist entity type has its own service:
- **Interface pairs**: `I{Resource}Service` (read operations) + `I{Resource}CommandService` (write operations)
- **Implementation hierarchy**: `{Resource}CommandService` → `{Resource}Service` inherits and adds read operations
- All services inherit from `CommandServiceBase` which handles command execution and queuing

Example: [ItemsService.cs](../src/Todoist.Net/Services/ItemsService.cs) inherits from `ItemsCommandService`

### ComplexId Pattern
Entity IDs use `ComplexId` struct to handle both:
- **Persistent IDs** (string from API) - for existing entities
- **Temporary IDs** (Guid) - for entities created in transactions before commit

Implicit conversions exist from `string` and `Guid`, so you can pass either directly.

### Model Inheritance for CRUD Operations
- `BaseEntity` → base for all entities with `ComplexId`
- `AddItem` → for creating new items (includes `ProjectId`, `Labels`, etc.)
- `UpdateItem` → for updates (extends `BaseUnsetEntity`)
- `Item` → read model returned from API (extends `UpdateItem`)

### Unset Pattern for Nullable Properties
When updating entities, properties with `null` values are excluded from requests by default. To explicitly set a property to `null`:
```csharp
item.Unset(i => i.DueDate);  // This will send null to API
await client.Items.UpdateAsync(item);
```
This pattern requires models to inherit from `BaseUnsetEntity` and uses custom JSON serialization in [JsonResolverModifiers.cs](../src/Todoist.Net/Serialization/Resolvers/JsonResolverModifiers.cs).

### Transactions (Batching)
Multiple commands can be batched into a single HTTP request:
```csharp
var transaction = client.CreateTransaction();
var projectId = await transaction.Project.AddAsync(new Project("name"));
var itemId = await transaction.Items.AddAsync(new Item("task", projectId));
await transaction.CommitAsync();  // Single HTTP request
```
Transaction services use the command queue instead of executing immediately.

## Build System
Uses [NUKE](https://nuke.build/) build automation. Key commands:
```powershell
.\build.cmd                          # Default: Compile + UnitTest + NugetPack
.\build.cmd compile                  # Build only
.\build.cmd unit-test                # Unit tests only (trait=unit)
.\build.cmd test                     # All tests except MFA-required
.\build.cmd nuget-pack               # Create NuGet package
.\build.cmd sonar                    # SonarQube analysis (requires SONAR_TOKEN)
```

## Testing Conventions
- Test framework: **xUnit**
- Test traits for filtering:
  - `unit` - Unit tests (no API calls)
  - `integration-free` - Integration tests for free Todoist accounts
  - `integration-premium` - Integration tests requiring Todoist Premium
  - `mfa-required` - Tests that don't work with MFA enabled
- API token via environment variable: `todoist:token`
- Tests use `RateLimitAwareRestClient` wrapper for API rate limiting
- Integration tests create/cleanup their own resources in try/finally blocks

## Code Conventions
- All async methods follow `{Operation}Async` naming
- All services support `CancellationToken` (default parameter)
- Properties with `internal set` are read-only from API responses
- Use `[JsonPropertyName("snake_case")]` for API field mapping
- XML documentation comments required (generates NuGet docs)
- `TreatWarningsAsErrors` is enabled

## Key Directories
- `src/Todoist.Net/Services/` - Service interfaces and implementations
- `src/Todoist.Net/Models/` - Entity models and DTOs
- `src/Todoist.Net/Serialization/` - Custom JSON converters and resolvers
- `src/Todoist.Net.Tests/Services/` - Integration tests per service
- `build/` - NUKE build configuration

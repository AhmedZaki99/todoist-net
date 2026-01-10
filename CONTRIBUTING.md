# Contributing to Todoist.Net

Thank you for your interest in contributing to Todoist.Net! This document provides guidelines and instructions for contributing to this project.

---

## Table of Contents

1. [Code of Conduct](#code-of-conduct)
2. [Getting Started](#getting-started)
3. [Development Environment](#development-environment)
4. [Project Structure](#project-structure)
5. [Coding Standards](#coding-standards)
6. [Adding New Features](#adding-new-features)
7. [Writing Tests](#writing-tests)
8. [Submitting Changes](#submitting-changes)
9. [Documentation](#documentation)
10. [Questions and Support](#questions-and-support)

---

## Code of Conduct

By participating in this project, you agree to maintain a respectful and inclusive environment. Please be considerate of others and focus on constructive feedback.

---

## Getting Started

### Prerequisites

- **.NET SDK 8.0** or later (for building and testing)
- **.NET Framework 4.6.2** development tools (for multi-targeting)
- **Visual Studio 2022**, **VS Code with C# Dev Kit**, or **JetBrains Rider**
- **Git** for version control
- **Todoist Account** with API token (for integration testing)

### Clone the Repository

```bash
git clone https://github.com/olsh/todoist-net.git
cd todoist-net
```

### Build the Project

Using the NUKE build system:

```powershell
# Windows
.\build.cmd

# Linux/macOS
./build.sh
```

Or using the .NET CLI:

```bash
dotnet build
```

### Run Tests

```powershell
# Unit tests only
.\build.cmd unit-test

# All tests (requires API token)
$env:todoist:token = "your-api-token"
.\build.cmd test
```

---

## Development Environment

### EditorConfig

The project uses `.editorconfig` to enforce consistent formatting:

```properties
root = true

[*]
charset = utf-8
indent_size = 4
indent_style = space
insert_final_newline = true
trim_trailing_whitespace = true

[{*.csproj,*.json,*.config}]
indent_size = 2

[*.cs]
dotnet_separate_import_directive_groups = true
dotnet_sort_system_directives_first = true
```

### Project Settings

- **TreatWarningsAsErrors**: Enabled - all warnings must be resolved
- **GenerateDocumentationFile**: Enabled - XML documentation is required
- **Target Frameworks**: `netstandard2.0` and `net462`

### Setting Up Your API Token

For integration testing, set the `todoist:token` environment variable:

```powershell
# PowerShell
$env:todoist:token = "your-api-token"

# Command Prompt
set todoist:token=your-api-token

# Bash
export todoist:token=your-api-token
```

> **Note**: Use a test Todoist account or a dedicated project for testing to avoid affecting your personal data.

---

## Project Structure

```
src/
├── Todoist.Net/                    # Main library
│   ├── Services/                   # Service implementations
│   ├── Models/                     # Entity models
│   ├── Serialization/              # JSON handling
│   ├── Exceptions/                 # Exception types
│   ├── Extensions/                 # Extension methods
│   └── *.cs                        # Core classes
│
└── Todoist.Net.Tests/              # Test project
    ├── Services/                   # Service integration tests
    ├── Extensions/                 # Test constants
    ├── Helpers/                    # Test utilities
    └── Settings/                   # Test configuration
```

For detailed architecture information, see [ProjectArchitecture.md](ProjectArchitecture.md).

---

## Coding Standards

### General Guidelines

1. **Follow existing patterns**: Study existing code before making changes
2. **Keep it simple**: Prefer clarity over cleverness
3. **Be consistent**: Match the style of surrounding code
4. **Document public APIs**: All public members need XML documentation

### Naming Conventions

| Element | Convention | Example |
|---------|------------|---------|
| Services | `{Resource}Service` | `ItemsService` |
| Command Services | `{Resource}CommandService` | `ItemsCommandService` |
| Interfaces | `I{Name}` | `IItemsService` |
| Models | `{Resource}` | `Item`, `Project` |
| Add DTOs | `Add{Resource}` | `AddItem` |
| Update DTOs | `Update{Resource}` | `UpdateItem` |
| Info wrappers | `{Resource}Info` | `ItemInfo` |
| Arguments | `{Action}{Resource}Argument` | `ItemMoveArgument` |
| Async methods | `{Name}Async` | `GetAsync`, `AddAsync` |

### Async/Await Rules

```csharp
// Always use Async suffix
public async Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default)
{
    // Always use ConfigureAwait(false) in library code
    await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
    return item.Id;
}

// Always support CancellationToken with default value
Task<Item> GetAsync(ComplexId id, CancellationToken cancellationToken = default);
```

### Null Checking

```csharp
// Check at method entry
public Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
{
    if (item == null)
    {
        throw new ArgumentNullException(nameof(item));
    }
    
    // ... implementation
}
```

### JSON Property Mapping

```csharp
// Always use JsonPropertyName for API mapping
[JsonPropertyName("due_date")]
public DueDate DueDate { get; set; }

// Use internal set for read-only properties
[JsonPropertyName("created_at")]
public DateTime? CreatedAt { get; internal set; }
```

### XML Documentation

```csharp
/// <summary>
/// Adds a new task to a project asynchronously.
/// </summary>
/// <param name="item">The item to add.</param>
/// <param name="cancellationToken">A cancellation token.</param>
/// <returns>The ID of the created item.</returns>
/// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
/// <exception cref="HttpRequestException">API exception.</exception>
public Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default);
```

---

## Adding New Features

### Adding a New Service

1. **Create interfaces** in `Services/`:
   ```csharp
   // I{Resource}CommandService.cs - Write operations
   public interface I{Resource}CommandService
   {
       Task<ComplexId> AddAsync({Resource} entity, CancellationToken cancellationToken = default);
       Task UpdateAsync({Resource} entity, CancellationToken cancellationToken = default);
       Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);
   }
   
   // I{Resource}Service.cs - Read + Write operations
   public interface I{Resource}Service : I{Resource}CommandService
   {
       Task<IEnumerable<{Resource}>> GetAsync(CancellationToken cancellationToken = default);
       Task<{Resource}Info> GetAsync(ComplexId id, CancellationToken cancellationToken = default);
   }
   ```

2. **Create implementations** in `Services/`:
   ```csharp
   // {Resource}CommandService.cs
   internal class {Resource}CommandService : CommandServiceBase, I{Resource}CommandService
   {
       internal {Resource}CommandService(IAdvancedTodoistClient todoistClient)
           : base(todoistClient) { }
       
       internal {Resource}CommandService(ICollection<Command> queue)
           : base(queue) { }
       
       // Implement methods...
   }
   
   // {Resource}Service.cs
   internal class {Resource}Service : {Resource}CommandService, I{Resource}Service
   {
       internal {Resource}Service(IAdvancedTodoistClient todoistClient)
           : base(todoistClient) { }
       
       // Implement read methods...
   }
   ```

3. **Register in `TodoistClient`**:
   ```csharp
   public class TodoistClient
   {
       public I{Resource}Service {Resources} { get; }
       
       public TodoistClient(ITodoistRestClient restClient)
       {
           // ... existing code ...
           {Resources} = new {Resource}Service(this);
       }
   }
   ```

4. **Add to `ITodoistClient` interface**:
   ```csharp
   public interface ITodoistClient
   {
       I{Resource}Service {Resources} { get; }
   }
   ```

5. **Add to `Transaction`** (if command service needed):
   ```csharp
   public interface ITransaction
   {
       I{Resource}CommandService {Resources} { get; }
   }
   
   internal class Transaction : ITransaction
   {
       public I{Resource}CommandService {Resources} { get; }
       
       internal Transaction(IAdvancedTodoistClient todoistClient)
       {
           {Resources} = new {Resource}CommandService(_commands);
       }
   }
   ```

### Adding a New Model

1. **Create the model** in `Models/`:
   ```csharp
   public class {Resource} : BaseEntity  // or BaseUnsetEntity
   {
       public {Resource}(string name)
       {
           Name = name;
       }
       
       [JsonConstructor]
       internal {Resource}() { }
       
       [JsonPropertyName("name")]
       public string Name { get; set; }
       
       [JsonPropertyName("is_active")]
       public bool IsActive { get; internal set; }
   }
   ```

2. **Add command type** (if needed) in `Models/CommandType.cs`:
   ```csharp
   public static CommandType Add{Resource} { get; } = new CommandType("{resource}_add");
   public static CommandType Update{Resource} { get; } = new CommandType("{resource}_update");
   public static CommandType Delete{Resource} { get; } = new CommandType("{resource}_delete");
   ```

3. **Add resource type** (if needed) in `Models/ResourceType.cs`:
   ```csharp
   public static ResourceType {Resources} { get; } = new ResourceType("{resources}");
   ```

---

## Writing Tests

### Test File Location

- Service tests: `src/Todoist.Net.Tests/Services/{Resource}ServiceTests.cs`
- Unit tests: `src/Todoist.Net.Tests/{Component}Tests.cs`

### Test Structure

```csharp
using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class {Resource}ServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public {Resource}ServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateAndDelete_Success()
        {
            // Arrange
            var client = TodoistClientFactory.Create(_outputHelper);
            var name = Guid.NewGuid().ToString();
            
            // Act
            var id = await client.{Resources}.AddAsync(new {Resource}(name));
            
            try
            {
                // Assert
                var items = await client.{Resources}.GetAsync();
                Assert.Contains(items, x => x.Name == name);
            }
            finally
            {
                // Cleanup - ALWAYS clean up resources
                await client.{Resources}.DeleteAsync(id);
            }
        }
    }
}
```

### Test Traits

| Trait | When to Use |
|-------|-------------|
| `unit` | Tests without API calls |
| `integration-free` | Tests for free Todoist features |
| `integration-premium` | Tests requiring Todoist Premium |
| `mfa-required` | Tests incompatible with MFA |

### Required Test Coverage

For new services, include tests for:
- [ ] Create (Add)
- [ ] Read (Get single)
- [ ] Read (Get all)
- [ ] Update
- [ ] Delete
- [ ] Error handling
- [ ] Transaction support (if applicable)

For detailed testing guidelines, see [TestingDesign.md](TestingDesign.md).

---

## Submitting Changes

### Before Submitting

1. **Build successfully**: `.\build.cmd compile`
2. **Pass unit tests**: `.\build.cmd unit-test`
3. **Pass integration tests** (if API changes): `.\build.cmd test`
4. **No warnings**: TreatWarningsAsErrors is enabled
5. **Documentation**: All public APIs have XML comments

### Pull Request Process

1. **Fork** the repository
2. **Create a feature branch**: `git checkout -b feature/your-feature`
3. **Make your changes** following the coding standards
4. **Write tests** for new functionality
5. **Update documentation** if needed
6. **Commit** with clear, descriptive messages
7. **Push** to your fork
8. **Create a Pull Request** against `master`

### Commit Message Guidelines

```
Short summary (50 chars or less)

More detailed description if needed. Wrap at 72 characters.
Explain what and why, not how.

- Bullet points are fine
- Use imperative mood: "Add feature" not "Added feature"
```

### Pull Request Description

Include:
- **What**: Brief description of changes
- **Why**: Motivation for the changes
- **How**: High-level approach (if complex)
- **Testing**: How you tested the changes
- **Breaking Changes**: Any backward compatibility concerns

---

## Documentation

### Required Documentation

1. **XML Comments**: All public classes, methods, and properties
2. **README.md**: Usage examples for new features
3. **ProjectArchitecture.md**: Architecture changes
4. **TestingDesign.md**: New test patterns

### XML Comment Template

```csharp
/// <summary>
/// Brief description of what this does.
/// </summary>
/// <param name="paramName">Description of parameter.</param>
/// <returns>Description of return value.</returns>
/// <exception cref="ArgumentNullException">
/// <paramref name="paramName"/> is <see langword="null"/>.
/// </exception>
/// <exception cref="HttpRequestException">API exception.</exception>
/// <remarks>Additional notes if needed.</remarks>
/// <example>
/// <code>
/// var result = await service.MethodAsync(param);
/// </code>
/// </example>
```

---

## Questions and Support

- **GitHub Issues**: For bug reports and feature requests
- **Pull Request Comments**: For code review discussions
- **Project Wiki**: For extended documentation (if available)

### Getting Help

If you're unsure about anything:

1. Check existing code for examples
2. Read [ProjectArchitecture.md](ProjectArchitecture.md)
3. Read [TestingDesign.md](TestingDesign.md)
4. Open a GitHub issue to discuss before implementing

---

## Quick Reference

### Build Commands

```powershell
.\build.cmd                  # Default: Compile + UnitTest + NugetPack
.\build.cmd compile          # Build only
.\build.cmd unit-test        # Unit tests only
.\build.cmd test             # All tests except MFA-required
.\build.cmd nuget-pack       # Create NuGet package
```

### Common Patterns Checklist

- [ ] Service inherits from `CommandServiceBase`
- [ ] Full service extends command service
- [ ] Two constructors (client + queue) for command services
- [ ] `ConfigureAwait(false)` on all awaits
- [ ] `CancellationToken` parameter with default value
- [ ] Null checking with `ArgumentNullException`
- [ ] `[JsonPropertyName("snake_case")]` on properties
- [ ] `internal set` for read-only properties
- [ ] Try/finally for test cleanup
- [ ] Appropriate test traits

Thank you for contributing to Todoist.Net!

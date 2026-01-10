# Todoist.Net - Testing Design

This document provides a comprehensive overview of the testing architecture, patterns, conventions, and best practices used in the Todoist.Net library. It serves as the authoritative reference for writing new tests and refactoring existing tests during the migration to Todoist Unified API v1.

---

## Table of Contents

1. [Testing Overview](#testing-overview)
2. [Test Project Structure](#test-project-structure)
3. [Test Framework and Dependencies](#test-framework-and-dependencies)
4. [Test Categories and Traits](#test-categories-and-traits)
5. [Test Class Patterns](#test-class-patterns)
6. [Test Method Patterns](#test-method-patterns)
7. [Test Infrastructure](#test-infrastructure)
8. [Integration Test Conventions](#integration-test-conventions)
9. [Unit Test Conventions](#unit-test-conventions)
10. [Test Data Management](#test-data-management)
11. [Assertion Patterns](#assertion-patterns)
12. [Resource Cleanup](#resource-cleanup)
13. [Build Integration](#build-integration)
14. [Migration Testing Guidelines](#migration-testing-guidelines)

---

## Testing Overview

The Todoist.Net test suite consists of:

- **Unit Tests**: Test isolated components without API calls
- **Integration Tests**: Test against the live Todoist API
- **Premium Integration Tests**: Tests requiring Todoist Premium features

### Key Characteristics

- All integration tests are designed to be **self-contained** (create/cleanup their own resources)
- Tests use **xUnit** as the testing framework
- A custom **rate-limiting wrapper** handles API throttling
- Tests are categorized using **traits** for selective execution
- **Collection fixtures** ensure proper test isolation and serialization

---

## Test Project Structure

```
src/Todoist.Net.Tests/
├── Extensions/
│   └── Constants.cs                    # Test trait constants
├── Helpers/
│   ├── FakeLocalTimeZone.cs            # Timezone testing utilities
│   └── FakeLocalTimeZoneTests.cs       # Tests for helpers
├── Models/
│   └── (test-specific models)
├── Services/
│   ├── ActivityServiceTests.cs         # Service-specific tests
│   ├── BackupServiceTests.cs
│   ├── EmailServiceTests.cs
│   ├── FiltersServiceTests.cs
│   ├── ItemsServiceTests.cs
│   ├── LabelsServiceTests.cs
│   ├── NotesServiceTests.cs
│   ├── NotificationsServiceTests.cs
│   ├── ProjectsServiceTests.cs
│   ├── RemindersServiceTests.cs
│   ├── SectionServiceTests.cs
│   ├── SharingServiceTests.cs
│   ├── TemplateServiceTests.cs
│   ├── TransactionTests.cs
│   ├── UploadServiceTests.cs
│   └── UsersServiceTests.cs
├── Settings/
│   └── SettingsProvider.cs             # Test configuration
├── JsonResolverModifiersTests.cs       # Serialization unit tests
├── RateLimitAwareRestClient.cs         # API throttling wrapper
├── TodoistClientFactory.cs             # Test client factory
└── TodoistClientTests.cs               # Client-level tests
```

---

## Test Framework and Dependencies

### Dependencies

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.5">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
</ItemGroup>
```

### Target Framework

```xml
<TargetFramework>net8.0</TargetFramework>
```

### Build Settings

```xml
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```

---

## Test Categories and Traits

### Trait Constants

```csharp
internal static class Constants
{
    public const string TodoistApiTestCollectionName = "todoist-api-tests";
    
    public const string TraitName = "trait";
    
    public const string UnitTraitValue = "unit";
    public const string IntegrationFreeTraitValue = "integration-free";
    public const string IntegrationPremiumTraitValue = "integration-premium";
    public const string MfaRequiredTraitValue = "mfa-required";
}
```

### Trait Descriptions

| Trait Value | Description | When to Use |
|-------------|-------------|-------------|
| `unit` | Unit tests with no API calls | Testing serialization, converters, local logic |
| `integration-free` | Integration tests for free Todoist accounts | Testing standard API features |
| `integration-premium` | Integration tests requiring Todoist Premium | Testing premium features (filters, reminders, etc.) |
| `mfa-required` | Tests incompatible with MFA-enabled accounts | Tests that use password-based auth |

### Applying Traits

```csharp
[Fact]
[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public void MyUnitTest() { }

[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task MyIntegrationTest() { }

[Fact]
[Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
public async Task MyPremiumTest() { }
```

---

## Test Class Patterns

### Integration Test Class Structure

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
        public async Task OperationName_Condition_ExpectedResult()
        {
            // Arrange
            var client = TodoistClientFactory.Create(_outputHelper);
            
            // Act & Assert
            // ...
        }
    }
}
```

### Unit Test Class Structure

```csharp
using Todoist.Net.Tests.Extensions;
using Xunit;

namespace Todoist.Net.Tests
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class {Component}Tests
    {
        [Fact]
        public void MethodName_Condition_ExpectedResult()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}
```

### Key Patterns

1. **Collection Attribute**: Integration tests use `[Collection(Constants.TodoistApiTestCollectionName)]` to:
   - Ensure tests run sequentially (no parallel execution)
   - Share collection fixtures if needed
   - Prevent rate limiting issues from concurrent tests

2. **Output Helper Injection**: `ITestOutputHelper` is injected via constructor for:
   - Logging rate limit events
   - Debugging test execution
   - Providing context in test output

3. **Class-Level Traits**: Unit test classes can apply traits at class level

---

## Test Method Patterns

### Naming Convention

```
{MethodOrAction}_{Scenario}_{ExpectedBehavior}
```

**Examples**:
- `CreateItemCompleteGetCloseAsync_Success`
- `CreateItem_InvalidDueDate_ThrowsException`
- `MoveItemsToProject_Success`
- `IncludeUnsetProperties_WithUnsetProperty_IncludeNull`

### Method Structure

```csharp
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task CreateAndDelete_Success()
{
    // Arrange - Create client and prepare test data
    var client = TodoistClientFactory.Create(_outputHelper);
    var resourceName = Guid.NewGuid().ToString();
    
    // Act - Perform the operation
    var resourceId = await client.{Resource}.AddAsync(new {Resource}(resourceName));
    
    try
    {
        // Assert - Verify the result
        var resources = await client.{Resource}.GetAsync();
        Assert.Contains(resources, r => r.Name == resourceName);
    }
    finally
    {
        // Cleanup - Always clean up created resources
        await client.{Resource}.DeleteAsync(resourceId);
    }
}
```

### Exception Testing

```csharp
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public async Task CreateItem_InvalidDueDate_ThrowsException()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    var item = new AddItem("bad task")
    {
        DueDate = DueDate.FromText("Invalid date string")
    };

    var aggregateException = await Assert.ThrowsAsync<AggregateException>(
        async () =>
        {
            await client.Items.AddAsync(item);
        });

    Assert.IsType<TodoistException>(aggregateException.InnerExceptions.First());
}
```

---

## Test Infrastructure

### TodoistClientFactory

Creates test clients with rate-limiting support:

```csharp
public static class TodoistClientFactory
{
    public static ITodoistClient Create(ITestOutputHelper outputHelper)
    {
        var token = SettingsProvider.GetToken();
        return new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }
}
```

### SettingsProvider

Retrieves test configuration from environment variables:

```csharp
public static class SettingsProvider
{
    public static string GetToken()
    {
        return Environment.GetEnvironmentVariable("todoist:token");
    }
}
```

**Environment Variable**: `todoist:token` must contain a valid Todoist API token.

### RateLimitAwareRestClient

Wraps the REST client with automatic retry logic for rate limiting:

```csharp
public sealed class RateLimitAwareRestClient : ITodoistRestClient
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly TodoistRestClient _restClient;

    public RateLimitAwareRestClient(string token, ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _restClient = new TodoistRestClient(token);
    }

    public async Task<HttpResponseMessage> ExecuteRequest(Func<Task<HttpResponseMessage>> request)
    {
        HttpResponseMessage result;
        const int maxRetryCount = 35;
        int retryCount = 0;
        
        do
        {
            result = await request().ConfigureAwait(false);
            
            if ((int)result.StatusCode != 429 && (int)result.StatusCode < 500)
            {
                return result;
            }

            var cooldown = await GetRateLimitCooldown(result).ConfigureAwait(false);
            retryCount++;

            _outputHelper.WriteLine(
                "[{0:G}] Received [{1}] status code from Todoist API, retry #{2} in {3}",
                DateTime.UtcNow, result.StatusCode, retryCount, cooldown);
            
            await Task.Delay(cooldown);
        }
        while (retryCount < maxRetryCount);

        return result;
    }

    public async Task<TimeSpan> GetRateLimitCooldown(HttpResponseMessage response)
    {
        var defaultCooldown = TimeSpan.FromSeconds(30);
        
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                JObject json = JObject.Parse(content);
                return TimeSpan.FromSeconds(json["error_extra"]!["retry_after"]!.Value<double>());
            }
            catch
            {
                return defaultCooldown;
            }
        }
        
        return defaultCooldown;
    }
    
    // Implement ITodoistRestClient methods delegating to _restClient
    // with ExecuteRequest wrapper
}
```

---

## Integration Test Conventions

### Resource Lifecycle

**Always follow this pattern**:

```csharp
[Fact]
public async Task TestOperation_Success()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    
    // Create resource
    var resource = new {Resource}(Guid.NewGuid().ToString());
    await client.{Resources}.AddAsync(resource);
    
    try
    {
        // Test operations on the resource
        // ...assertions...
    }
    finally
    {
        // ALWAYS clean up
        await client.{Resources}.DeleteAsync(resource.Id);
    }
}
```

### Nested Resource Cleanup

For parent-child relationships:

```csharp
[Fact]
public async Task CreateProjectWithItem_Success()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    
    var project = new Project(Guid.NewGuid().ToString());
    await client.Projects.AddAsync(project);
    
    try
    {
        var item = new AddItem("Test task", project.Id);
        await client.Items.AddAsync(item);
        
        try
        {
            // Test operations
        }
        finally
        {
            await client.Items.DeleteAsync(item.Id);
        }
    }
    finally
    {
        await client.Projects.DeleteAsync(project.Id);
    }
}
```

### Transaction Testing

```csharp
[Fact]
public async Task CreateProjectAndItem_ViaTransaction_Success()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    
    var transaction = client.CreateTransaction();
    
    var project = new Project("Shopping List");
    var projectId = await transaction.Project.AddAsync(project);
    
    var item = new AddItem("Buy milk") { ProjectId = projectId };
    await transaction.Items.AddAsync(item);
    
    await transaction.CommitAsync();
    
    try
    {
        // Verify temp IDs were resolved to persistent IDs
        Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
        Assert.False(string.IsNullOrEmpty(item.Id.PersistentId));
        
        // Verify resources were created
        var itemInfo = await client.Items.GetAsync(item.Id);
        Assert.Equal(item.Content, itemInfo.Item.Content);
        Assert.Equal(project.Id.PersistentId, itemInfo.Project.Id.PersistentId);
    }
    finally
    {
        await client.Projects.DeleteAsync(project.Id);
    }
}
```

### Premium Feature Testing

```csharp
[Fact]
[Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
public async Task GetCompletedItems_WithFilter_Success()
{
    var client = TodoistClientFactory.Create(_outputHelper);
    
    // Create and complete an item
    var item = new AddItem("temp task");
    
    var transaction = client.CreateTransaction();
    await transaction.Items.AddAsync(item);
    await transaction.Items.CloseAsync(item.Id);
    await transaction.CommitAsync();
    
    try
    {
        // Premium feature: GetCompletedAsync with annotations
        var completedTasks = await client.Items.GetCompletedAsync(
            new ItemFilter()
            {
                AnnotateItems = true,
                AnnotateNotes = true,
                Limit = 5,
                Since = DateTime.Today.AddDays(-1)
            });

        Assert.True(completedTasks.Items.Count > 0);
        Assert.All(completedTasks.Items, i => Assert.NotNull(i.ItemObject));
    }
    finally
    {
        await client.Items.DeleteAsync(item.Id);
    }
}
```

---

## Unit Test Conventions

### Testing Serialization

```csharp
[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class JsonResolverModifiersTests
{
    private class UnsettablePropertiesModel : IUnsettableProperties
    {
        HashSet<PropertyInfo> IUnsettableProperties.UnsetProperties { get; } = [];

        [JsonPropertyName("first_property")]
        public string Property1 { get; set; }

        [JsonPropertyName("second_property")]
        public int? Property2 { get; set; }
    }

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = { JsonResolverModifiers.IncludeUnsetProperties }
        }
    };

    [Fact]
    public void IncludeUnsetProperties_NoUnsetProperties_HasNoNulls()
    {
        var model = new UnsettablePropertiesModel { Property1 = "Test" };

        var json = JsonSerializer.Serialize(model, _serializerOptions);

        Assert.Contains("\"first_property\":\"Test\"", json);
        Assert.DoesNotContain("second_property", json);
        Assert.DoesNotContain("null", json);
    }

    [Fact]
    public void IncludeUnsetProperties_WithUnsetProperty_IncludeNull()
    {
        var model = new UnsettablePropertiesModel { Property1 = "Test" };
        model.Unset(x => x.Property2);

        var json = JsonSerializer.Serialize(model, _serializerOptions);

        Assert.Contains("\"first_property\":\"Test\"", json);
        Assert.Contains("\"second_property\":null", json);
    }
}
```

### Testing Helpers

```csharp
[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class FakeLocalTimeZoneTests
{
    [Fact]
    public void FakeLocalTimeZone_SetsTimeZone_Success()
    {
        var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        
        using (new FakeLocalTimeZone(targetTimeZone))
        {
            Assert.Equal(targetTimeZone, TimeZoneInfo.Local);
        }
        
        // Verify it's restored
        Assert.NotEqual(targetTimeZone.Id, TimeZoneInfo.Local.Id);
    }
}
```

---

## Test Data Management

### Unique Resource Names

Use GUIDs for unique resource names to avoid conflicts:

```csharp
var projectName = Guid.NewGuid().ToString();
var project = new Project(projectName);
```

### Test Constants

For repeated test values:

```csharp
private const string TestItemContent = "Test Task";
private const string TestProjectPrefix = "Test_";
```

### Date/Time Testing

```csharp
// Use relative dates for reproducibility
var dueDate = DateTime.Today.AddDays(7);

// Use specific formats for verification
var item = new AddItem("task")
{
    DueDate = DueDate.FromText("22 Dec 2021", Language.English)
};

var itemInfo = await client.Items.GetAsync(item.Id);
Assert.Equal("2021-12-22", itemInfo.Item.DueDate.StringDate);
```

---

## Assertion Patterns

### Common Assertions

```csharp
// Existence
Assert.Contains(collection, item => item.Name == expectedName);
Assert.DoesNotContain(collection, item => item.Name == expectedName);

// Equality
Assert.Equal(expected, actual);
Assert.NotEqual(unexpected, actual);

// Null checks
Assert.Null(value);
Assert.NotNull(value);

// Boolean
Assert.True(condition);
Assert.False(condition);

// Type checking
Assert.IsType<ExpectedType>(value);

// Collection assertions
Assert.Single(collection);
Assert.Empty(collection);
Assert.All(collection, item => Assert.NotNull(item.Property));

// String assertions
Assert.Contains("substring", stringValue);
Assert.DoesNotContain("substring", stringValue);
Assert.StartsWith("prefix", stringValue);
```

### Exception Assertions

```csharp
// Sync exception
Assert.Throws<ArgumentNullException>(() => new SomeClass(null));

// Async exception
var exception = await Assert.ThrowsAsync<AggregateException>(
    async () => await client.Items.AddAsync(invalidItem));

// Verify inner exception
Assert.IsType<TodoistException>(exception.InnerExceptions.First());
```

### Verification After Operations

```csharp
// Verify creation
var created = await client.Projects.GetAsync(projectId);
Assert.NotNull(created);
Assert.Equal(expectedName, created.Project.Name);

// Verify update
await client.Items.UpdateAsync(item);
var updated = await client.Items.GetAsync(item.Id);
Assert.Equal(newValue, updated.Item.Property);

// Verify deletion
await client.Projects.DeleteAsync(projectId);
var remaining = await client.Projects.GetAsync();
Assert.DoesNotContain(remaining, p => p.Id.PersistentId == projectId.PersistentId);
```

---

## Resource Cleanup

### Best Practices

1. **Always use try/finally**: Even if assertions fail, cleanup should run
2. **Handle already-deleted resources**: Parent deletion may cascade to children
3. **Order matters**: Delete children before parents when possible
4. **Use transactions for bulk cleanup**: More efficient for multiple resources

### Cleanup Pattern with Error Handling

```csharp
finally
{
    try
    {
        await client.Projects.DeleteAsync(project.Id);
    }
    catch
    {
        // Parent project removes child projects too
        // So the project may be deleted already
        // ignored
    }
}
```

### Transaction-Based Cleanup

```csharp
finally
{
    var deleteTransaction = client.CreateTransaction();
    
    await deleteTransaction.Notes.DeleteAsync(note.Id);
    await deleteTransaction.Project.DeleteAsync(project.Id);
    
    await deleteTransaction.CommitAsync();
}
```

---

## Build Integration

### NUKE Build Targets

```csharp
Target UnitTest => _ => _
    .DependsOn(Compile)
    .Executes(() =>
    {
        DotNetTest(s => s
            .SetProjectFile(Solution.src.Todoist_Net_Tests)
            .SetConfiguration(Configuration)
            .SetFilter("trait=unit")
            .SetNoBuild(true));
    });

Target Test => _ => _
    .DependsOn(Compile)
    .Executes(() =>
    {
        DotNetTest(s => s
            .SetProjectFile(Solution.src.Todoist_Net_Tests)
            .SetConfiguration(Configuration)
            .SetLoggers("console;verbosity=detailed")
            .SetFilter("trait!=mfa-required")
            .SetNoBuild(true));
    });
```

### Running Tests via Build Script

```powershell
# Unit tests only
.\build.cmd unit-test

# All tests except MFA-required
.\build.cmd test

# Compile only (before testing)
.\build.cmd compile
```

### Running Tests via dotnet CLI

```powershell
# Unit tests
dotnet test --filter "trait=unit"

# Integration tests (free features)
dotnet test --filter "trait=integration-free"

# Integration tests (premium features)
dotnet test --filter "trait=integration-premium"

# All tests except MFA-required
dotnet test --filter "trait!=mfa-required"
```

### Test Environment Setup

Set the environment variable before running integration tests:

```powershell
# PowerShell
$env:todoist:token = "your-api-token"

# Command Prompt
set todoist:token=your-api-token

# Bash
export todoist:token=your-api-token
```

---

## Migration Testing Guidelines

### When Adding New API Features

1. Create new test file or add to existing service test file
2. Follow existing naming conventions
3. Apply appropriate trait for the feature tier
4. Include setup, action, assertion, and cleanup phases
5. Handle rate limiting via `RateLimitAwareRestClient`

### When Modifying Existing Features

1. Keep existing tests as regression tests
2. Update assertions to match new behavior
3. Consider adding new test cases for changed behavior
4. Document breaking changes in test comments

### When Deprecating Features

1. Mark tests with appropriate comments
2. Consider moving to a "legacy" test category
3. Ensure new API alternatives have test coverage

### Test Coverage Checklist for New Services

- [ ] `AddAsync` - Create entity
- [ ] `GetAsync` (single) - Retrieve by ID
- [ ] `GetAsync` (collection) - Retrieve all
- [ ] `UpdateAsync` - Update entity
- [ ] `DeleteAsync` - Remove entity
- [ ] Error handling - Invalid inputs
- [ ] Transaction support - If applicable
- [ ] Premium features - Separate trait if applicable
- [ ] Property unsetting - If supported

### Test Coverage Checklist for New Models

- [ ] JSON serialization round-trip
- [ ] Required property validation
- [ ] Optional property handling
- [ ] Null/default value handling
- [ ] `Unset` functionality (if applicable)
- [ ] `ComplexId` handling (if applicable)

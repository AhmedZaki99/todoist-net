# URL Schemes

## Mobile app URL schemes

Our applications for [Android](https://play.google.com/store/apps/details?id=com.todoist) and [iOS](https://apps.apple.com/us/app/todoist-to-do-list-calendar/id572688855) support custom URL schemes for launching to specific views and initiating some common actions.

## Views

The following schemes are available to open a specific view:

| Scheme | Description |
|--------|-------------|
| todoist:// | Opens Todoist to the user's default view. |
| todoist://today | Opens the today view. |
| todoist://upcoming | Opens the Upcoming view. |
| todoist://profile | Opens the profile view. |
| todoist://inbox | Opens the inbox view. |
| todoist://teaminbox | Opens the team inbox view. If the user doesn't have a business account it will show an alert and redirect automatically to the inbox view. |
| todoist://notifications | Opens notifications view. |

### Tasks

> Example of adding a task:

```text
todoist://addtask?content=mytask&date=tomorrow&priority=4
```

> Here's an example of a content value:

```text
Create document about URL Schemes!
```

> And how it should be supplied using Percent-encoding:

```text
Create&20document%20about%20URL%20Schemes%21
```

> Here's an example of a date value:

```text
Tomorrow @ 14:00
```

> And how it should be supplied using Percent-encoding:

```text
Tomorrow%20@%2014:00
```

The following schemes are available for tasks:

| Scheme | Description |
|--------|-------------|
| todoist://task?id={id} | Opens a task by ID. |
| todoist://addtask | Opens the add task view to add a new task to Todoist. |

The `todoist://addtask` scheme accepts the following optional values:

| Value | Description |
|-------|-------------|
| content *URL encoding* | The content of the task, which should be a string that is in `Percent-encoding` (also known as URL encoding). |
| date *URL encoding* | The due date of the task, which should be a string that is in `Percent-encoding` (also known as URL encoding). Look at our reference to see [which formats are supported](https://www.todoist.com/help/articles/introduction-to-due-dates-and-due-times-q7VobO). |
| priority *Integer* | The priority of the task (a number between `1` and `4`, `4` for very urgent and `1` for natural). <br>**Note**: Keep in mind that `very urgent` is the priority 1 on clients. So, `p1` will return `4` in the API. |

This URL scheme will not automatically submit the task to Todoist, it will just open and pre-fill the add task view. If no values are passed, the add task view will just be opened.

## Projects

The following schemes are available for tasks:

| Scheme | Description |
|--------|-------------|
| todoist://projects | Opens the projects view (shows all projects). |
| todoist://project?id={id} | Opens a specific project by ID. |

> Example of opening a specific project:

```text
todoist://project?id=128501470
```

The `todoist://project` scheme accepts the following required value:

| Value | Description |
|-------|-------------|
| id *Integer* | The ID of the project to view. If the ID doesn't exist, you don't have access to the project, or the value is empty, an alert will be showed and the user will be redirected to the projects view. |

### Labels

The following schemes are available for labels:

| Scheme | Description |
|--------|-------------|
| todoist://labels | Opens the labels view (shows all labels) |
| todoist://label?name={name} | Opens a specific label by name. |

> Example of opening a specific label:

```text
todoist://label?name=Urgent
```

The `todoist://label` scheme accepts the following required value:

| Value | Description |
|-------|-------------|
| name *String* | The name of the label to view. If the label doesn't exist, you don't have access to the label, or the value is empty, an alert will be shown. |

### Filters

The following schemes are available for filters:

| Scheme | Description |
|--------|-------------|
| todoist://filters | Opens the filters view (shows all filters) |
| todoist://filter?id={id} | Opens a specific filter by ID. |

> Example of opening a specific filter:

```text
todoist://filter?id=9
```

The `todoist://filter` scheme accepts the following required value:

| Value | Description |
|-------|-------------|
| id *Integer* | The ID of the filter to view. If the ID doesn't exist, you don't have access to the filter, or the value is empty, an alert will be showed and the user will be redirected to the filters view. |

### Search

The following scheme is available for searching (Android only):

| Scheme | Description |
|--------|-------------|
| todoist://search?query={query} | Used to search in the Todoist application. |

> Example of searching for "Test & Today":

```text
todoist://search?query=Test%20%26%20Today
```

The `todoist://search` scheme accepts the following required value:

| Value | Description |
|-------|-------------|
| query *URL encoding* | The query to search in the Todoist application, which should be a string that is in `Percent-encoding` (also known as URL encoding). |

## Desktop app URL schemes

Our [Desktop](https://todoist.com/downloads) applications support custom URL schemes for launching to specific views and initiating some common actions. This can be useful for integrating Todoist with other applications or services, as browsers and other applications can open these URLs to interact with Todoist. As an example, you could create a link in your application that opens a specific project in Todoist, or a link that adds a task to Todoist.

### Views

The following schemes are available to open a specific view:

| Scheme | Description | minimum version requirement |
|--------|-------------|----------------------------|
| todoist:// | Opens Todoist. | 9.2.0 |
| todoist://inbox | Opens the inbox view. | 9.2.0 |
| todoist://today | Opens the today view. | 9.2.0 |
| todoist://upcoming | Opens the Upcoming view. | 9.2.0 |
| todoist://project?id={id} | Opens the project detail view for a given project ID. | 9.2.0 |
| todoist://task?id={id} | Opens the task detail view for a given task ID. | 9.2.0 |
| todoist://openquickadd?content={content}&description={description} | Opens the global quick add, optionally refilled. | 9.2.0 |
| todoist://notifications | Opens the notifications view. | 9.10.0 |
| todoist://filters-labels | Opens the filters & labels view. | 9.10.0 |
| todoist://filter?id={id} | Opens the filter view for a given filter ID. | 9.10.0 |
| todoist://label?id={id} | Opens the label view for a given label ID. | 9.10.0 |
| todoist://search?query={query} | Opens the search view for the specified query. | 9.10.0 |
| todoist://projects | Opens my projects view. | 9.10.0 |
| todoist://projects?workspaceId={id} | Opens the projects view for the given workspace ID. | 9.10.0 |
| todoist://templates | Opens the templates view. | 9.10.0 |
| todoist://templates?id={id} | Opens the template view for the given template ID. | 9.10.0 |

### Tasks

> Example of adding a task:

*Note that this will not add the task but open the Global Quick Add refilled with given values.*

```text
todoist://openquickadd?content=mytask&description=%20is%20a%20description
```

The following schemes are available for tasks:

| Scheme | Description |
|--------|-------------|
| todoist://task?id={id} | Opens a task by ID. |
| todoist://openquickadd | Opens the global quick add to add a new task to Todoist. |

The `todoist://openquickadd` scheme accepts the following optional values:

| Value | Description |
|-------|-------------|
| content *URL encoding* | The content of the task, which should be a string that is in `Percent-encoding` (also known as URL encoding). |
| description *URL encoding* | The content of the task, which should be a string that is in `Percent-encoding` (also known as URL encoding). |

This URL scheme will not automatically submit the task to Todoist, it will just open and pre-fill the global quick add panel. If no values are passed, the global quick add will just be open.

### Projects

The following schemes are available for projects:

| Scheme | Description |
|--------|-------------|
| todoist://project?id={id} | Opens a specific project by ID. |

> Example of opening a specific project:

```text
todoist://project?id=128501470
```

The `todoist://project` scheme accepts the following required value:

| Value | Description |
|-------|-------------|
| id *Integer* | The ID of the project to view. If the ID doesn't exist it will just open Todoist. If you don't have access to the project, or the project does not exist, an error message will be shown to the user. |

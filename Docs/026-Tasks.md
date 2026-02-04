# Tasks

# Tasks

---

# Create Task

`POST` `/api/v1/tasks`

Base URL: `https://api.todoist.com`

Create a new task.

## Request

### Request Body

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `content` | string (Content) | Yes | non-empty<br><br>Task content |
| `description` | string or null (Description) | No | Task description |
| `project_id` | string or integer or null (Project Id) | No | ID of the project to add the task to. If omitted or null, the task will be added to the user's Inbox. |
| `section_id` | string or integer or null (Section Id) | No | ID of the section to add the task to |
| `parent_id` | string or integer or null (Parent Id) | No | ID of the parent task |
| `order` | integer or null (Order) | No | [ -2147483648 .. 2147483647 ]<br><br>Position of the task in the project or section |
| `labels` | array of strings (Labels) or null | No | List of label names |
| `priority` | integer or null (Priority) | No | [ 1 .. 4 ]<br><br>Task priority (1-4, where 1 is highest) |
| `assignee_id` | integer or null (Assignee Id) | No | ID of the user to assign the task to |
| `due_string` | string or null (Due String) | No | Human-readable representation of the due date. See the [Due dates](031-Due-dates) section for more details. |
| `due_date` | string or null (Due Date) | No | Due date in RFC 3339 format or similar. See the [Due dates](031-Due-dates) section for more details. |
| `due_datetime` | string or null (Due Datetime) | No | Due date and time. See the [Due dates](031-Due-dates) section for more details. |
| `due_lang` | string or null (Due Lang) | No | Due date language code. See the [Due dates](031-Due-dates) section for more details. |
| `duration` | integer or null (Duration) | No | Task duration, in either minutes or days. Only used if `duration_unit` is also provided. |
| `duration_unit` | string or null (Duration Unit) | No | Enum: `"minute"` `"day"`<br><br>Unit of time for duration |
| `deadline_date` | string `<date>` or null (Deadline Date) | No | Deadline date in YYYY-MM-DD format |

#### Request Sample

```json
{
  "content": "string",
  "description": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "section_id": "6fFPHV272WWh3gpW",
  "parent_id": "6XGgmFVcrG5RRjVr",
  "order": 12,
  "labels": [
    "string"
  ],
  "priority": 2,
  "assignee_id": 123456789,
  "due_string": "string",
  "due_date": "string",
  "due_datetime": "string",
  "due_lang": "string",
  "duration": 30,
  "duration_unit": "minute",
  "deadline_date": "2025-02-12"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "user_id": "string",
  "id": "string",
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "added_by_uid": "string",
  "assigned_by_uid": "string",
  "responsible_uid": "string",
  "labels": [
    "string"
  ],
  "deadline": {
    "property1": "string",
    "property2": "string"
  },
  "duration": {
    "property1": 0,
    "property2": 0
  },
  "checked": false,
  "is_deleted": false,
  "added_at": "string",
  "completed_at": "string",
  "completed_by_uid": "string",
  "updated_at": "string",
  "due": { },
  "priority": 0,
  "child_order": 0,
  "content": "string",
  "description": "string",
  "note_count": 0,
  "day_order": 0,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Tasks

`GET` `/api/v1/tasks`

Base URL: `https://api.todoist.com`

Get all active tasks for the user.

All provided parameters are used to narrow down the list of tasks.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer or null (Project Id) | No | Examples: `project_id=6XGgm6PHrGgMpCFX`<br><br>String ID of the project to get tasks from |
| `section_id` | string or integer or null (Section Id) | No | Examples: `section_id=6fFPHV272WWh3gpW`<br><br>String ID of the section to get tasks from |
| `parent_id` | string or integer or null (Parent Id) | No | Examples: `parent_id=6fFPHRxcmVqm4C84`<br><br>String ID of the parent task to get sub-tasks from |
| `label` | string or null (Label) | No | Filter tasks by label name |
| `ids` | string or null (Ids) | No | Examples: `ids=6XGgmFVcrG5RRjVr,6fFPHV272WWh3gpW`<br><br>A list of the task IDs to retrieve, this should be a comma separated list |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of objects (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "results": [
    {
      "user_id": "string",
      "id": "string",
      "project_id": "string",
      "section_id": "string",
      "parent_id": "string",
      "added_by_uid": "string",
      "assigned_by_uid": "string",
      "responsible_uid": "string",
      "labels": [
        "string"
      ],
      "deadline": {
        "property1": "string",
        "property2": "string"
      },
      "duration": {
        "property1": 0,
        "property2": 0
      },
      "checked": false,
      "is_deleted": false,
      "added_at": "string",
      "completed_at": "string",
      "completed_by_uid": "string",
      "updated_at": "string",
      "due": { },
      "priority": 0,
      "child_order": 0,
      "content": "string",
      "description": "string",
      "note_count": 0,
      "day_order": 0,
      "is_collapsed": true
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Tasks Completed By Completion Date

`GET` `/api/v1/tasks/completed/by_completion_date`

Base URL: `https://api.todoist.com`

Retrieves a list of completed tasks strictly limited by the specified completion
date range (up to 3 months).

It can retrieve completed items:

- From all the projects the user has joined in a workspace
- From all the projects of the user
- That match many [supported filters](https://todoist.com/help/articles/introduction-to-filters-V98wIH)

By default, the response is limited to a page containing a maximum of 50 items
(configurable using `limit`).

Subsequent pages of results can be fetched by using the `next_cursor` value from the
response as the `cursor` value for the next request.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `since` | string &lt;date-time&gt; (Since) | Yes |  |
| `until` | string &lt;date-time&gt; (Until) | Yes |  |
| `workspace_id` | integer or null (Workspace Id) | No |  |
| `project_id` | string or integer or null (Project Id) | No |  |
| `section_id` | string or integer or null (Section Id) | No |  |
| `parent_id` | string or integer or null (Parent Id) | No |  |
| `filter_query` | string or null (Filter Query) | No | [ 1 .. 1024 ] characters |
| `filter_lang` | string or null (Filter Lang) | No |  |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50` |
| `public_key` | string or null (Public Key) | No |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `items` | array of objects (Items) | No |  |
| `next_cursor` | string or null (Next Cursor) | No |  |

#### `items[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "items": [
    {
      "user_id": "string",
      "id": "string",
      "project_id": "string",
      "section_id": "string",
      "parent_id": "string",
      "added_by_uid": "string",
      "assigned_by_uid": "string",
      "responsible_uid": "string",
      "labels": [
        "string"
      ],
      "deadline": {
        "property1": "string",
        "property2": "string"
      },
      "duration": {
        "property1": 0,
        "property2": 0
      },
      "checked": false,
      "is_deleted": false,
      "added_at": "string",
      "completed_at": "string",
      "completed_by_uid": "string",
      "updated_at": "string",
      "due": { },
      "priority": 0,
      "child_order": 0,
      "content": "string",
      "description": "string",
      "note_count": 0,
      "day_order": 0,
      "is_collapsed": true
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Tasks Completed By Due Date

`GET` `/api/v1/tasks/completed/by_due_date`

Base URL: `https://api.todoist.com`

Retrieves a list of completed items strictly limited by the specified due date range
(up to 6 weeks).

It can retrieve completed items:

- From within a project, section, or parent item
- From all the projects the user has joined in a workspace
- From all the projects of the user
- That match many [supported filters](https://todoist.com/help/articles/introduction-to-filters-V98wIH)

By default, the response is limited to a page containing a maximum of 50 items
(configurable using `limit`).

Subsequent pages of results can be fetched by using the `next_cursor` value from the
response as the `cursor` value for the next request.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `since` | string &lt;date-time&gt; (Since) | Yes |  |
| `until` | string &lt;date-time&gt; (Until) | Yes |  |
| `workspace_id` | integer or null (Workspace Id) | No |  |
| `project_id` | string or integer or null (Project Id) | No |  |
| `section_id` | string or integer or null (Section Id) | No |  |
| `parent_id` | string or integer or null (Parent Id) | No |  |
| `filter_query` | string or null (Filter Query) | No | [ 1 .. 1024 ] characters |
| `filter_lang` | string or null (Filter Lang) | No |  |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50` |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `items` | array of objects (Items) | No |  |
| `next_cursor` | string or null (Next Cursor) | No |  |

#### `items[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "items": [
    {
      "user_id": "string",
      "id": "string",
      "project_id": "string",
      "section_id": "string",
      "parent_id": "string",
      "added_by_uid": "string",
      "assigned_by_uid": "string",
      "responsible_uid": "string",
      "labels": [
        "string"
      ],
      "deadline": {
        "property1": "string",
        "property2": "string"
      },
      "duration": {
        "property1": 0,
        "property2": 0
      },
      "checked": false,
      "is_deleted": false,
      "added_at": "string",
      "completed_at": "string",
      "completed_by_uid": "string",
      "updated_at": "string",
      "due": { },
      "priority": 0,
      "child_order": 0,
      "content": "string",
      "description": "string",
      "note_count": 0,
      "day_order": 0,
      "is_collapsed": true
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Tasks By Filter

`GET` `/api/v1/tasks/filter`

Base URL: `https://api.todoist.com`

Get all tasks matching the filter.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `query` | string (Query) | Yes | [ 1 .. 1024 ] characters<br><br>Filter by any [supported filter](https://todoist.com/help/articles/introduction-to-filters-V98wIH). Multiple filters (using the comma `,` operator) are not supported. |
| `lang` | string or null (Lang) | No | Examples: `lang=en` `lang=de` `lang=fr`<br><br>IETF language tag defining what language filter is written in, if differs from default English |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of objects (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "results": [
    {
      "user_id": "string",
      "id": "string",
      "project_id": "string",
      "section_id": "string",
      "parent_id": "string",
      "added_by_uid": "string",
      "assigned_by_uid": "string",
      "responsible_uid": "string",
      "labels": [
        "string"
      ],
      "deadline": {
        "property1": "string",
        "property2": "string"
      },
      "duration": {
        "property1": 0,
        "property2": 0
      },
      "checked": false,
      "is_deleted": false,
      "added_at": "string",
      "completed_at": "string",
      "completed_by_uid": "string",
      "updated_at": "string",
      "due": { },
      "priority": 0,
      "child_order": 0,
      "content": "string",
      "description": "string",
      "note_count": 0,
      "day_order": 0,
      "is_collapsed": true
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Quick Add

`POST` `/api/v1/tasks/quick`

Base URL: `https://api.todoist.com`

Add a new task using the Quick Add implementation similar to that used in
the official clients

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `text` | string (Text) | Yes | The text of the task that is parsed. It can include a due date in free form text, a project name starting with the `#` character (without spaces), a label starting with the `@` character, an assignee starting with the `+` character, a priority (e.g., `p1`), a deadline between `{}` (e.g. {in 3 days}), or a description starting from `//` until the end of the text. |
| `note` | string or null (Note) | No |  |
| `reminder` | string or null (Reminder) | No | The reminder date in free form text. |
| `auto_reminder` | boolean (Auto Reminder) | No | Default: `false`<br><br>When this option is enabled, the default reminder will be added to the new item if it has a due date with time set. See also the [auto_reminder user option](008-Sync-User) for more info about the default reminder. |
| `meta` | boolean (Meta) | No | Default: `false` |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{ }
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Reopen Task

`POST` `/api/v1/tasks/{task_id}/reopen`

Base URL: `https://api.todoist.com`

Reopens a task.

Any ancestor tasks or sections will also be marked as uncomplete and restored from history.

The reinstated tasks and sections will appear at the end of the list within their parent, after any previously active tasks.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string or integer (Task Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the task |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | any | No |  |

#### Response Sample

```json
null
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Close Task

`POST` `/api/v1/tasks/{task_id}/close`

Base URL: `https://api.todoist.com`

Closes a task.

The command performs in the same way as our official clients:

Regular tasks are marked complete and moved to history, along with their subtasks. Tasks with [recurring due dates](https://todoist.com/help/articles/introduction-to-recurring-dates-YUYVJJAV) will be scheduled to their next occurrence.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string or integer (Task Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the task |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | any | No |  |

#### Response Sample

```json
null
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Move Task

`POST` `/api/v1/tasks/{task_id}/move`

Base URL: `https://api.todoist.com`

Moves task to another project, section or parent.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string (Task Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>ID of the task to move |

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or null (Project Id) | No | ID of the project to move the task to |
| `section_id` | string or null (Section Id) | No | ID of the section to move the task to |
| `parent_id` | string or null (Parent Id) | No | ID of the parent task to move the task under |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "user_id": "string",
  "id": "string",
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "added_by_uid": "string",
  "assigned_by_uid": "string",
  "responsible_uid": "string",
  "labels": [
    "string"
  ],
  "deadline": {
    "property1": "string",
    "property2": "string"
  },
  "duration": {
    "property1": 0,
    "property2": 0
  },
  "checked": false,
  "is_deleted": false,
  "added_at": "string",
  "completed_at": "string",
  "completed_by_uid": "string",
  "updated_at": "string",
  "due": { },
  "priority": 0,
  "child_order": 0,
  "content": "string",
  "description": "string",
  "note_count": 0,
  "day_order": 0,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Task

`GET` `/api/v1/tasks/{task_id}`

Base URL: `https://api.todoist.com`

Returns a single active (non-completed) task by ID

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string or integer (Task Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the task |

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `public_key` | string or null (Public Key) | No |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "user_id": "string",
  "id": "string",
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "added_by_uid": "string",
  "assigned_by_uid": "string",
  "responsible_uid": "string",
  "labels": [
    "string"
  ],
  "deadline": {
    "property1": "string",
    "property2": "string"
  },
  "duration": {
    "property1": 0,
    "property2": 0
  },
  "checked": false,
  "is_deleted": false,
  "added_at": "string",
  "completed_at": "string",
  "completed_by_uid": "string",
  "updated_at": "string",
  "due": { },
  "priority": 0,
  "child_order": 0,
  "content": "string",
  "description": "string",
  "note_count": 0,
  "day_order": 0,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Task

`POST` `/api/v1/tasks/{task_id}`

Base URL: `https://api.todoist.com`

Updates an existing task.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string or integer (Task Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the task |

### Request Body

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `content` | string (Content) | No | Updated task content. Omit this field to keep it unchanged. |
| `description` | string (Description) | No | Updated task description. Omit this field to keep it unchanged. |
| `labels` | array of strings (Labels) | No | Updated list of label names. Omit this field to keep it unchanged. |
| `priority` | integer (Priority) | No | [ 1 .. 4 ]<br><br>Updated task priority (1-4, where 1 is highest). Omit this field to keep it unchanged. |
| `due_string` | string (Due String) | No | Updated human-readable representation of the due date. See the [Due dates](031-Due-dates) section for more details. Omit this field to keep it unchanged. |
| `due_date` | string (Due Date) | No | Updated due date in RFC 3339 format or similar. See the [Due dates](031-Due-dates) section for more details. Omit this field to keep it unchanged. |
| `due_datetime` | string (Due Datetime) | No | Updated due date and time. See the [Due dates](031-Due-dates) section for more details. Omit this field to keep it unchanged. |
| `due_lang` | string (Due Lang) | No | Updated due date language code. See the [Due dates](031-Due-dates) section for more details. Omit this field to keep it unchanged. |
| `assignee_id` | integer or null (Assignee Id) | No | ID of the user to assign the task to. Pass null to clear the value. Omit this field to keep it unchanged. |
| `duration` | integer or null (Duration) | No | Updated task duration, in either minutes or days. Only used if `duration_unit` is also provided. Pass null to clear the value. Omit this field to keep it unchanged. |
| `duration_unit` | string or null (Duration Unit) | No | Enum: `"minute"` `"day"`<br><br>Unit of time for duration. Must be provided to update the task duration. Pass null to clear the value. Omit this field to keep it unchanged. |
| `deadline_date` | string &lt;date&gt; or null (Deadline Date) | No | Updated deadline date in YYYY-MM-DD format. Pass null to clear the value. Omit this field to keep it unchanged. |

#### Request Sample

```json
{
  "content": "string",
  "description": "string",
  "labels": [
    "string"
  ],
  "priority": 2,
  "due_string": "string",
  "due_date": "string",
  "due_datetime": "string",
  "due_lang": "string",
  "assignee_id": 123456789,
  "duration": 30,
  "duration_unit": "minute",
  "deadline_date": "2025-02-12"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `id` | string (Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `section_id` | string or null (Section Id) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `added_by_uid` | string or null (Added By Uid) | Yes |  |
| `assigned_by_uid` | string or null (Assigned By Uid) | Yes |  |
| `responsible_uid` | string or null (Responsible Uid) | Yes |  |
| `labels` | array of strings (Labels) | Yes |  |
| `deadline` | object (Deadline) or null | Yes |  |
| `duration` | object (Duration) or null | Yes |  |
| `checked` | boolean (Checked) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `added_at` | string or null (Added At) | Yes |  |
| `completed_at` | string or null (Completed At) | Yes |  |
| `completed_by_uid` | string or null (Completed By Uid) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `due` | object (Due) or null | Yes |  |
| `priority` | integer (Priority) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `content` | string (Content) | Yes |  |
| `description` | string (Description) | Yes |  |
| `note_count` | integer (Note Count) | Yes | **Deprecated**: only returning 0 and is marked for removal |
| `day_order` | integer (Day Order) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `section_id`

Any of:

- Section Id (string)
- Section Id (null)

##### `parent_id`

Any of:

- Parent Id (string)
- Parent Id (null)

##### `added_by_uid`

Any of:

- Added By Uid (string)
- Added By Uid (null)

##### `assigned_by_uid`

Any of:

- Assigned By Uid (string)
- Assigned By Uid (null)

##### `responsible_uid`

Any of:

- Responsible Uid (string)
- Responsible Uid (null)

##### `deadline`

Any of:

- Deadline (object)
- Deadline (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `duration`

Any of:

- Duration (object)
- Duration (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | integer or string | No | additional property |

##### `added_at`

Any of:

- Added At (string)
- Added At (null)

##### `completed_at`

Any of:

- Completed At (string)
- Completed At (null)

##### `completed_by_uid`

Any of:

- Completed By Uid (string)
- Completed By Uid (null)

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `due`

Any of:

- Due (object)
- Due (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "user_id": "string",
  "id": "string",
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "added_by_uid": "string",
  "assigned_by_uid": "string",
  "responsible_uid": "string",
  "labels": [
    "string"
  ],
  "deadline": {
    "property1": "string",
    "property2": "string"
  },
  "duration": {
    "property1": 0,
    "property2": 0
  },
  "checked": false,
  "is_deleted": false,
  "added_at": "string",
  "completed_at": "string",
  "completed_by_uid": "string",
  "updated_at": "string",
  "due": { },
  "priority": 0,
  "child_order": 0,
  "content": "string",
  "description": "string",
  "note_count": 0,
  "day_order": 0,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Delete Task

`DELETE` `/api/v1/tasks/{task_id}`

Base URL: `https://api.todoist.com`

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `task_id` | string or integer (Task Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the task |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | any | No |  |

#### Response Sample

```json
null
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
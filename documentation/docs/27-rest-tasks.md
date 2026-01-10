# Tasks

## Create Task

`POST /api/v1/tasks`

Creates a new task and returns it.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `content` | string | Yes | Task content. Range: [1..65536] characters |
| `description` | string or null | No | Task description. Range: [0..16384] characters |
| `project_id` | string, integer, or null | No | String ID of the project |
| `section_id` | string, integer, or null | No | String ID of the section |
| `parent_id` | string, integer, or null | No | String ID of the parent task |
| `order` | integer or null | No | Non-zero integer for ordering |
| `labels` | array of strings or null | No | Task labels |
| `priority` | integer or null | No | Task priority from 1 (normal) to 4 (urgent) |
| `due_string` | string or null | No | Human-defined due date (e.g., "tomorrow", "next Monday") |
| `due_date` | string or null | No | Specific date in YYYY-MM-DD format |
| `due_datetime` | string or null | No | Specific datetime in RFC3339 format in UTC |
| `due_lang` | string or null | No | 2-letter code for due_string language |
| `assignee_id` | string or integer or null | No | ID of the user responsible for task |
| `duration` | integer or null | No | Duration of the task in minutes |
| `duration_unit` | string or null | No | Unit of duration. Enum: "minute", "day" |

### Request sample

Content type: `application/json`

```json
{
  "content": "string",
  "description": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "section_id": "6VfWjjjFg2xqX6Pa",
  "parent_id": "6XGgm6PHrGgMpCFX",
  "order": 0,
  "labels": [
    "string"
  ],
  "priority": 1,
  "due_string": "string",
  "due_date": "2019-08-24",
  "due_datetime": "2019-08-24T14:15:22Z",
  "due_lang": "en",
  "assignee_id": "6VfWjjjFg2xqX6Pa",
  "duration": 0,
  "duration_unit": "minute"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `creator_id` | string (required) | |
| `created_at` | string (required) | |
| `assignee_id` | string or null (required) | |
| `assigner_id` | string or null (required) | |
| `comment_count` | integer (required) | |
| `is_completed` | boolean (required) | |
| `content` | string (required) | |
| `description` | string (required) | |
| `due` | object or null (required) | Due date object |
| `duration` | object or null (required) | Duration object |
| `id` | string (required) | |
| `labels` | Array of strings (required) | |
| `order` | integer (required) | |
| `priority` | integer (required) | Priority from 1-4 |
| `project_id` | string (required) | |
| `section_id` | string or null (required) | |
| `parent_id` | string or null (required) | |
| `url` | string (required) | |

Response sample:
```json
{
  "creator_id": "string",
  "created_at": "string",
  "assignee_id": "string",
  "assigner_id": "string",
  "comment_count": 0,
  "is_completed": true,
  "content": "string",
  "description": "string",
  "due": {
    "date": "2019-08-24",
    "is_recurring": true,
    "datetime": "2019-08-24T14:15:22Z",
    "string": "string",
    "timezone": "string"
  },
  "duration": {
    "amount": 0,
    "unit": "minute"
  },
  "id": "string",
  "labels": [
    "string"
  ],
  "order": 0,
  "priority": 1,
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "url": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Tasks

`GET /api/v1/tasks`

Returns all tasks for a given project, section, or label. By default returns only active (non-completed) tasks.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | String ID of the project |
| `section_id` | string, integer, or null | No | String ID of the section |
| `label` | string or null | No | Label name |
| `ids` | array of strings or null | No | Array of task IDs to filter by |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of task objects

Response sample:
```json
[
  {
    "creator_id": "string",
    "created_at": "string",
    "assignee_id": "string",
    "assigner_id": "string",
    "comment_count": 0,
    "is_completed": true,
    "content": "string",
    "description": "string",
    "due": {
      "date": "2019-08-24",
      "is_recurring": true,
      "datetime": "2019-08-24T14:15:22Z",
      "string": "string",
      "timezone": "string"
    },
    "duration": {
      "amount": 0,
      "unit": "minute"
    },
    "id": "string",
    "labels": [
      "string"
    ],
    "order": 0,
    "priority": 1,
    "project_id": "string",
    "section_id": "string",
    "parent_id": "string",
    "url": "string"
  }
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Tasks Completed By Completion Date

`GET /api/v1/tasks/completed/by_completion_date`

Returns completed tasks filtered by completion date.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | String ID of the project |
| `section_id` | string, integer, or null | No | String ID of the section |
| `completed_since` | string or null | No | ISO 8601 date format (YYYY-MM-DD) |
| `completed_before` | string or null | No | ISO 8601 date format (YYYY-MM-DD) |
| `cursor` | string or null | No | Pagination cursor |
| `limit` | integer | No | Default: 50, Range: (0..200] |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects (required) | Array of task objects |
| `next_cursor` | string | Cursor for pagination |

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Tasks Completed By Due Date

`GET /api/v1/tasks/completed/by_due_date`

Returns completed tasks filtered by due date.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | String ID of the project |
| `section_id` | string, integer, or null | No | String ID of the section |
| `due_since` | string or null | No | ISO 8601 date format (YYYY-MM-DD) |
| `due_before` | string or null | No | ISO 8601 date format (YYYY-MM-DD) |
| `cursor` | string or null | No | Pagination cursor |
| `limit` | integer | No | Default: 50, Range: (0..200] |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects (required) | Array of task objects |
| `next_cursor` | string | Cursor for pagination |

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Tasks By Filter

`GET /api/v1/tasks/filter`

Returns tasks matching a filter string.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `filter` | string | Yes | Filter string (e.g., "today", "p1", "#Work") |
| `lang` | string or null | No | Language code for filter interpretation |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of task objects

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Quick Add

`POST /api/v1/tasks/quick`

Quickly add a task using natural language text.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `text` | string | Yes | Task text in natural language |
| `meta` | object or null | No | Metadata to include |
| `auto_parse_labels` | boolean or null | No | Automatically parse labels |
| `auto_reminder` | boolean or null | No | Automatically set reminder |

### Request sample

Content type: `application/json`

```json
{
  "text": "string",
  "meta": {},
  "auto_parse_labels": true,
  "auto_reminder": true
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Task object with same structure as Create Task response

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Reopen Task

`POST /api/v1/tasks/{task_id}/reopen`

Reopens a completed task.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Close Task

`POST /api/v1/tasks/{task_id}/close`

Marks a task as completed.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Move Task

`POST /api/v1/tasks/{task_id}/move`

Moves a task to a different project or section.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | Target project ID |
| `section_id` | string, integer, or null | No | Target section ID |
| `parent_id` | string, integer, or null | No | Target parent task ID |

### Request sample

Content type: `application/json`

```json
{
  "project_id": "6XGgm6PHrGgMpCFX",
  "section_id": "6VfWjjjFg2xqX6Pa",
  "parent_id": "6XGgm6PHrGgMpCFX"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Task

`GET /api/v1/tasks/{task_id}`

Returns a task by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Same as Create Task response

Response sample:
```json
{
  "creator_id": "string",
  "created_at": "string",
  "assignee_id": "string",
  "assigner_id": "string",
  "comment_count": 0,
  "is_completed": true,
  "content": "string",
  "description": "string",
  "due": {
    "date": "2019-08-24",
    "is_recurring": true,
    "datetime": "2019-08-24T14:15:22Z",
    "string": "string",
    "timezone": "string"
  },
  "duration": {
    "amount": 0,
    "unit": "minute"
  },
  "id": "string",
  "labels": [
    "string"
  ],
  "order": 0,
  "priority": 1,
  "project_id": "string",
  "section_id": "string",
  "parent_id": "string",
  "url": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Task

`POST /api/v1/tasks/{task_id}`

Updates a task by ID and returns it.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `content` | string or null | No | Task content |
| `description` | string or null | No | Task description |
| `labels` | array of strings or null | No | Task labels |
| `priority` | integer or null | No | Task priority from 1-4 |
| `due_string` | string or null | No | Human-defined due date |
| `due_date` | string or null | No | Specific date in YYYY-MM-DD |
| `due_datetime` | string or null | No | Specific datetime in RFC3339 |
| `due_lang` | string or null | No | 2-letter language code |
| `assignee_id` | string, integer, or null | No | ID of responsible user |
| `duration` | integer or null | No | Duration in minutes |
| `duration_unit` | string or null | No | Unit of duration |

### Request sample

Content type: `application/json`

```json
{
  "content": "string",
  "description": "string",
  "labels": [
    "string"
  ],
  "priority": 1,
  "due_string": "string",
  "due_date": "2019-08-24",
  "due_datetime": "2019-08-24T14:15:22Z",
  "due_lang": "en",
  "assignee_id": "6VfWjjjFg2xqX6Pa",
  "duration": 0,
  "duration_unit": "minute"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Same as Create Task response

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Delete Task

`DELETE /api/v1/tasks/{task_id}`

Deletes a task by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `task_id` | string or integer | Yes | String ID of the task |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

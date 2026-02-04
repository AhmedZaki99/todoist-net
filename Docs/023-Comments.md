# Comments

# Comments

---

# Create Comment

`POST` `/api/v1/comments`

Base URL: `https://api.todoist.com`

Creates a new comment on a project or task and returns it.

Exactly one of `task_id` or `project_id` arguments is required. Providing
neither or both will return an error.

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `content` | string (Content) | Yes | [ 1 .. 15000 ] characters<br><br>Content of the comment |
| `project_id` | string or integer or null (Project Id) | No | String ID of the project |
| `task_id` | string or integer or null (Task Id) | No | String ID of the task |
| `attachment` | object (Attachment) or null | No | A [File attachment](013-Sync-Comments) object |
| `uids_to_notify` | array of integers (Uids To Notify) or null | No | Optional list of user IDs to notify about this comment. |

#### Request Sample

```json
{
  "content": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "task_id": "6XGgmFVcrG5RRjVr",
  "attachment": {
    "file_name": "File.pdf",
    "file_type": "application/pdf",
    "file_url": "https://s3.amazonaws.com/domorebetter/Todoist+Setup+Guide.pdf",
    "resource_type": "file"
  },
  "uids_to_notify": [
    12345678,
    23456789
  ]
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `posted_uid` | string or null (Posted Uid) | Yes |  |
| `content` | string (Content) | No | Default: `""` |
| `file_attachment` | object (File Attachment) or null | Yes |  |
| `uids_to_notify` | array of strings (Uids To Notify) or null | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `posted_at` | string or null (Posted At) | Yes |  |
| `reactions` | object (Reactions) or null | Yes |  |

##### `posted_uid`

Any of:

- Posted Uid (string)
- Posted Uid (null)

##### `file_attachment`

Any of:

- File Attachment (object)
- File Attachment (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or integer or array of any or object or null | No | additional property |

##### `uids_to_notify`

Any of:

- array of strings
- null

##### `posted_at`

Any of:

- Posted At (string)
- Posted At (null)

##### `reactions`

Any of:

- Reactions (object)
- Reactions (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | array of strings | No | additional property |

#### Response Sample

```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "",
  "file_attachment": {
    "property1": "string",
    "property2": "string"
  },
  "uids_to_notify": [
    "string"
  ],
  "is_deleted": true,
  "posted_at": "string",
  "reactions": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Comments

`GET` `/api/v1/comments`

Base URL: `https://api.todoist.com`

Get all comments for a given task or project.

Exactly one of `task_id` or `project_id` arguments is required. Providing
neither or both will return an error.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer or null (Project Id) | No | Examples: `project_id=6XGgm6PHrGgMpCFX`<br><br>String ID of the project |
| `task_id` | string or integer or null (Task Id) | No | Examples: `task_id=6XGgmFVcrG5RRjVr`<br><br>String ID of the task |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | ( 0 .. 200 ]<br><br>Default: `50`<br><br>The number of objects to return in a page |
| `public_key` | string or null (Public Key) | No |  |

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
| `id` | string (Id) | Yes |  |
| `posted_uid` | string or null (Posted Uid) | Yes |  |
| `content` | string (Content) | No | Default: `""` |
| `file_attachment` | object (File Attachment) or null | Yes |  |
| `uids_to_notify` | array of strings (Uids To Notify) or null | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `posted_at` | string or null (Posted At) | Yes |  |
| `reactions` | object (Reactions) or null | Yes |  |

##### `posted_uid`

Any of:

- Posted Uid (string)
- Posted Uid (null)

##### `file_attachment`

Any of:

- File Attachment (object)
- File Attachment (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or integer or array of any or object or null | No | additional property |

##### `uids_to_notify`

Any of:

- array of strings
- null

##### `posted_at`

Any of:

- Posted At (string)
- Posted At (null)

##### `reactions`

Any of:

- Reactions (object)
- Reactions (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | array of strings | No | additional property |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "results": [
    {
      "id": "string",
      "posted_uid": "string",
      "content": "",
      "file_attachment": {
        "property1": "string",
        "property2": "string"
      },
      "uids_to_notify": [
        "string"
      ],
      "is_deleted": true,
      "posted_at": "string",
      "reactions": {
        "property1": [
          "string"
        ],
        "property2": [
          "string"
        ]
      }
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

# Get Comment

`GET` `/api/v1/comments/{comment_id}`

Base URL: `https://api.todoist.com`

Returns a single comment by ID

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `comment_id` | string or integer or null (Comment Id) | Yes | Examples: `6XGgmFVcrG5RRjVr`<br><br>String ID of the comment |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `posted_uid` | string or null (Posted Uid) | Yes |  |
| `content` | string (Content) | No | Default: `""` |
| `file_attachment` | object (File Attachment) or null | Yes |  |
| `uids_to_notify` | array of strings (Uids To Notify) or null | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `posted_at` | string or null (Posted At) | Yes |  |
| `reactions` | object (Reactions) or null | Yes |  |

##### `posted_uid`

Any of:

- Posted Uid (string)
- Posted Uid (null)

##### `file_attachment`

Any of:

- File Attachment (object)
- File Attachment (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or integer or array of any or object or null | No | additional property |

##### `uids_to_notify`

Any of:

- array of strings
- null

##### `posted_at`

Any of:

- Posted At (string)
- Posted At (null)

##### `reactions`

Any of:

- Reactions (object)
- Reactions (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | array of strings | No | additional property |

#### Response Sample

```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "",
  "file_attachment": {
    "property1": "string",
    "property2": "string"
  },
  "uids_to_notify": [
    "string"
  ],
  "is_deleted": true,
  "posted_at": "string",
  "reactions": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Comment

`POST` `/api/v1/comments/{comment_id}`

Base URL: `https://api.todoist.com`

Update a comment by ID and returns its content

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `comment_id` | string or integer (Comment Id) | Yes | Examples: `6XGgmFQrx44wfGHr`<br><br>String ID of the comment |

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `content` | string or null (Content) | Yes | [ 1 .. 15000 ] characters<br><br>New content for the comment. If null or an empty string, no update is performed. |

#### Request Sample

```json
{
  "content": "string"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- NoteSyncView
- Response Update Comment Api V1 Comments  Comment Id  Post

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `posted_uid` | string or null (Posted Uid) | Yes |  |
| `content` | string (Content) | No | Default: `""` |
| `file_attachment` | object (File Attachment) or null | Yes |  |
| `uids_to_notify` | array of strings (Uids To Notify) or null | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `posted_at` | string or null (Posted At) | Yes |  |
| `reactions` | object (Reactions) or null | Yes |  |

##### `posted_uid`

Any of:

- Posted Uid (string)
- Posted Uid (null)

##### `file_attachment`

Any of:

- File Attachment (object)
- File Attachment (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or integer or array of any or object or null | No | additional property |

##### `uids_to_notify`

Any of:

- array of strings
- null

##### `posted_at`

Any of:

- Posted At (string)
- Posted At (null)

##### `reactions`

Any of:

- Reactions (object)
- Reactions (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | array of strings | No | additional property |

#### Response Sample

```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "",
  "file_attachment": {
    "property1": "string",
    "property2": "string"
  },
  "uids_to_notify": [
    "string"
  ],
  "is_deleted": true,
  "posted_at": "string",
  "reactions": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Delete Comment

`DELETE` `/api/v1/comments/{comment_id}`

Base URL: `https://api.todoist.com`

Delete a comment by ID

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `comment_id` | string or integer (Comment Id) | Yes | Examples: `6XGgmFQrx44wfGHr`<br><br>String ID of the comment |

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
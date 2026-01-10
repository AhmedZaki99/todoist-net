# Comments

## Create Comment

`POST /api/v1/comments`

Creates a new comment on a project or task and returns it.

Exactly one of `task_id` or `project_id` arguments is required. Providing neither or both will return an error.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `content` | string | Yes | Content of the comment. Range: [1..15000] characters |
| `project_id` | string, integer, or null | No | String ID of the project |
| `task_id` | string, integer, or null | No | String ID of the task |
| `attachment` | object or null | No | A File attachment object |
| `uids_to_notify` | array of integers or null | No | Optional list of user IDs to notify about this comment |

### Request sample

Content type: `application/json`

```json
{
  "content": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "task_id": "6VfWjjjFg2xqX6Pa",
  "attachment": {
    "file_name": "string",
    "file_type": "string",
    "file_url": "string",
    "resource_type": "string"
  },
  "uids_to_notify": [
    0
  ]
}
```

### Responses

**200** Successful Response

Content type: `application/json`

Response sample:
```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "string",
  "file_attachment": {
    "file_name": "string",
    "file_type": "string",
    "file_url": "string",
    "resource_type": "string"
  },
  "uids_to_notify": [
    0
  ],
  "posted_at": "string",
  "reactions": {
    "❤️": [
      "2671362"
    ],
    "👍": [
      "2671362",
      "2671366"
    ]
  }
}
```  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Comments

`GET /api/v1/comments`

Get all comments for a given task or project.

Exactly one of `task_id` or `project_id` arguments is required. Providing neither or both will return an error.

This is a paginated endpoint. See the Pagination guide for details on using cursor-based pagination.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | String ID of the project |
| `task_id` | string, integer, or null | No | String ID of the task |
| `cursor` | string or null | No | |
| `limit` | integer | No | The number of objects to return in a page. Default: 50, Range: (0..200] |
| `public_key` | string or null | No | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects | Array of comment objects |
| `next_cursor` | string | Cursor for pagination |

Response sample:
```json
{
  "results": [
    {
      "id": "string",
      "posted_uid": "string",
      "content": "string",
      "file_attachment": {
        "file_name": "string",
        "file_type": "string",
        "file_url": "string",
        "resource_type": "string"
      },
      "uids_to_notify": [
        0
      ],
      "posted_at": "string",
      "reactions": {
        "❤️": [
          "2671362"
        ],
        "👍": [
          "2671362",
          "2671366"
        ]
      }
    }
  ],
  "next_cursor": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Comment

`GET /api/v1/comments/{comment_id}`

Returns a single comment by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `comment_id` | string, integer, or null | Yes | String ID of the comment |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `posted_uid` | string | |
| `content` | string | |
| `file_attachment` | object | File attachment object |
| `uids_to_notify` | Array of integers | |
| `posted_at` | string | |
| `reactions` | object | Reactions object with emoji keys |

Response sample:
```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "string",
  "file_attachment": {
    "file_name": "string",
    "file_type": "string",
    "file_url": "string",
    "resource_type": "string"
  },
  "uids_to_notify": [
    0
  ],
  "posted_at": "string",
  "reactions": {
    "❤️": [
      "2671362"
    ],
    "👍": [
      "2671362",
      "2671366"
    ]
  }
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Comment

`POST /api/v1/comments/{comment_id}`

Update a comment by ID and returns its content. New content for the comment. If null or an empty string, no update is performed.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `comment_id` | string, integer, or null | Yes | String ID of the comment |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `content` | string | Yes | |

### Request sample

Content type: `application/json`

```json
{
  "content": "string"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

Response sample:
```json
{
  "id": "string",
  "posted_uid": "string",
  "content": "string",
  "file_attachment": {
    "file_name": "string",
    "file_type": "string",
    "file_url": "string",
    "resource_type": "string"
  },
  "uids_to_notify": [
    0
  ],
  "posted_at": "string",
  "reactions": {
    "❤️": [
      "2671362"
    ],
    "👍": [
      "2671362",
      "2671366"
    ]
  }
}
```  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Delete Comment

`DELETE /api/v1/comments/{comment_id}`

Delete a comment by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `comment_id` | string, integer, or null | Yes | String ID of the comment |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

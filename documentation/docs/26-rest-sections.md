# Sections

## Create Section

`POST /api/v1/sections`

Create a new section.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | Name of the new section |
| `project_id` | string or integer | Yes | ID of the project to add the section to |
| `order` | integer or null | No | Position of the new section in the project |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "order": 0
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|----------------|
| `id` | string (required) | |
| `user_id` | string (required) | |
| `project_id` | string (required) | |
| `added_at` | string (required) | |
| `updated_at` | string or null (required) | |
| `archived_at` | string or null (required) | |
| `name` | string (required) | |
| `is_archived` | boolean (required) | |
| `is_deleted` | boolean (required) | |
| `section_order` | integer (required) | |

Response sample:
```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "is_archived": true,
  "is_deleted": true,
  "section_order": 0
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Sections

`GET /api/v1/sections`

Returns all sections for a given project.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | String ID of the project |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of objects

Each object in the array contains:

| Field | Type | Description |
|-------|------|----------------|
| `id` | string (required) | |
| `user_id` | string (required) | |
| `project_id` | string (required) | |
| `added_at` | string (required) | |
| `updated_at` | string or null (required) | |
| `archived_at` | string or null (required) | |
| `name` | string (required) | |
| `is_archived` | boolean (required) | |
| `is_deleted` | boolean (required) | |
| `section_order` | integer (required) | |

Response sample:
```json
[
  {
    "id": "string",
    "user_id": "string",
    "project_id": "string",
    "added_at": "string",
    "updated_at": "string",
    "archived_at": "string",
    "name": "string",
    "is_archived": true,
    "is_deleted": true,
    "section_order": 0
  }
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Section

`GET /api/v1/sections/{section_id}`

Returns a section by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `section_id` | string or integer | Yes | String ID of the section |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|----------------|
| `id` | string (required) | |
| `user_id` | string (required) | |
| `project_id` | string (required) | |
| `added_at` | string (required) | |
| `updated_at` | string or null (required) | |
| `archived_at` | string or null (required) | |
| `name` | string (required) | |
| `is_archived` | boolean (required) | |
| `is_deleted` | boolean (required) | |
| `section_order` | integer (required) | |

Response sample:
```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "is_archived": true,
  "is_deleted": true,
  "section_order": 0
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Section

`POST /api/v1/sections/{section_id}`

Updates a section by ID and returns it.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `section_id` | string or integer | Yes | String ID of the section |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string or null | No | New name for the section |

### Request sample

Content type: `application/json`

```json
{
  "name": "string"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|----------------|
| `id` | string (required) | |
| `user_id` | string (required) | |
| `project_id` | string (required) | |
| `added_at` | string (required) | |
| `updated_at` | string or null (required) | |
| `archived_at` | string or null (required) | |
| `name` | string (required) | |
| `is_archived` | boolean (required) | |
| `is_deleted` | boolean (required) | |
| `section_order` | integer (required) | |

Response sample:
```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "is_archived": true,
  "is_deleted": true,
  "section_order": 0
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Delete Section

`DELETE /api/v1/sections/{section_id}`

Deletes a section by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `section_id` | string or integer | Yes | String ID of the section |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

# Labels

## Shared Labels

`GET /api/v1/labels/shared`

Returns all shared labels.

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of label name strings

Response sample:
```json
[
  "string"
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Labels

`GET /api/v1/labels`

Returns all personal labels.

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of label objects

| Field | Type | Description |
|-------|------|-------------|
| `id` | string (required) | Label ID |
| `name` | string (required) | Label name |
| `color` | string (required) | Label color |
| `order` | integer (required) | Order for sorting |
| `is_favorite` | boolean (required) | Whether label is favorited |

Response sample:
```json
[
  {
    "id": "string",
    "name": "string",
    "color": "string",
    "order": 0,
    "is_favorite": true
  }
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Create Label

`POST /api/v1/labels`

Creates a new personal label and returns it.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | Label name. Range: [1..255] characters |
| `color` | string or null | No | Label color |
| `order` | integer or null | No | Order for sorting |
| `is_favorite` | boolean or null | No | Whether label is favorited |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string (required) | Label ID |
| `name` | string (required) | Label name |
| `color` | string (required) | Label color |
| `order` | integer (required) | Order for sorting |
| `is_favorite` | boolean (required) | Whether label is favorited |

Response sample:
```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Shared Labels Remove

`POST /api/v1/labels/shared/remove`

Removes a shared label from all tasks.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | Label name to remove |

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

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Shared Labels Rename

`POST /api/v1/labels/shared/rename`

Renames a shared label across all tasks.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | Current label name |
| `new_name` | string | Yes | New label name |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "new_name": "string"
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

## Delete Label

`DELETE /api/v1/labels/{label_id}`

Deletes a personal label by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `label_id` | string or integer | Yes | String ID of the label |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Label

`GET /api/v1/labels/{label_id}`

Returns a label by ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `label_id` | string or integer | Yes | String ID of the label |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string (required) | Label ID |
| `name` | string (required) | Label name |
| `color` | string (required) | Label color |
| `order` | integer (required) | Order for sorting |
| `is_favorite` | boolean (required) | Whether label is favorited |

Response sample:
```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Label

`POST /api/v1/labels/{label_id}`

Updates a label by ID and returns it.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `label_id` | string or integer | Yes | String ID of the label |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string or null | No | Label name |
| `color` | string or null | No | Label color |
| `order` | integer or null | No | Order for sorting |
| `is_favorite` | boolean or null | No | Whether label is favorited |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string (required) | Label ID |
| `name` | string (required) | Label name |
| `color` | string (required) | Label color |
| `order` | integer (required) | Order for sorting |
| `is_favorite` | boolean (required) | Whether label is favorited |

Response sample:
```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

# Sections

# Sections

---

# Search Sections

`GET` `/api/v1/sections/search`

Base URL: `https://api.todoist.com`

Search active sections by name, optionally filtered by project.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `query` | string (Query) | Yes | [ 1 .. 1024 ] characters<br><br>Examples: `query=To Do` `query=Week *` `query=Q* 2026` `query=Draft\*`<br><br>Search query to match section names. Matching is case-insensitive. Queries are matched literally unless `*` (wildcard) is included. Use `\*` for literal asterisk and `\\` for literal backslash. |
| `project_id` | string or integer or null (Project Id) | No | Examples: `project_id=6XGgm6PHrGgMpCFX`<br><br>String ID of the project to search sections from. If omitted or null, search sections from all projects. |
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
| `id` | string (Id) | Yes |  |
| `user_id` | string (User Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `added_at` | string (Added At) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `archived_at` | string or null (Archived At) | Yes |  |
| `name` | string (Name) | Yes |  |
| `section_order` | integer (Section Order) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `archived_at`

Any of:

- Archived At (string)
- Archived At (null)

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
      "user_id": "string",
      "project_id": "string",
      "added_at": "string",
      "updated_at": "string",
      "archived_at": "string",
      "name": "string",
      "section_order": 0,
      "is_archived": true,
      "is_deleted": true,
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

# Create Section

`POST` `/api/v1/sections`

Base URL: `https://api.todoist.com`

Create a new section

## Request

### Request Body

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes | Name of the new section |
| `project_id` | string or integer (Project Id) | Yes | ID of the project to add the section to |
| `order` | integer or null (Order) | No | Position of the new section in the project |

##### `project_id`

Any of:

- Project Id (string)
- Project Id (integer)

##### `order`

Any of:

- Order (integer)
- Order (null)

### Request Sample

```json
{
  "name": "string",
  "project_id": "6XGgm6PHrGgMpCFX",
  "order": 12
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `user_id` | string (User Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `added_at` | string (Added At) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `archived_at` | string or null (Archived At) | Yes |  |
| `name` | string (Name) | Yes |  |
| `section_order` | integer (Section Order) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `archived_at`

Any of:

- Archived At (string)
- Archived At (null)

#### Response Sample

```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "section_order": 0,
  "is_archived": true,
  "is_deleted": true,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Sections

`GET` `/api/v1/sections`

Base URL: `https://api.todoist.com`

Get all active sections for the user, optionally filtered by project.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer or null (Project Id) | No | Examples: `project_id=6XGgm6PHrGgMpCFX`<br><br>String ID of the project to get sections from. If omitted or null, get sections from all projects. |
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
| `id` | string (Id) | Yes |  |
| `user_id` | string (User Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `added_at` | string (Added At) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `archived_at` | string or null (Archived At) | Yes |  |
| `name` | string (Name) | Yes |  |
| `section_order` | integer (Section Order) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `archived_at`

Any of:

- Archived At (string)
- Archived At (null)

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
      "user_id": "string",
      "project_id": "string",
      "added_at": "string",
      "updated_at": "string",
      "archived_at": "string",
      "name": "string",
      "section_order": 0,
      "is_archived": true,
      "is_deleted": true,
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

# Get Section

`GET` `/api/v1/sections/{section_id}`

Base URL: `https://api.todoist.com`

Return the section for the given section ID

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `section_id` | string or integer (Section Id) | Yes | Examples: `6fFPHV272WWh3gpW`<br><br>String ID of the section |

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `public_key` | string or null (Public Key) | No |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `user_id` | string (User Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `added_at` | string (Added At) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `archived_at` | string or null (Archived At) | Yes |  |
| `name` | string (Name) | Yes |  |
| `section_order` | integer (Section Order) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `archived_at`

Any of:

- Archived At (string)
- Archived At (null)

#### Response Sample

```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "section_order": 0,
  "is_archived": true,
  "is_deleted": true,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Section

`POST` `/api/v1/sections/{section_id}`

Base URL: `https://api.todoist.com`

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `section_id` | string or integer (Section Id) | Yes | Examples: `6fFPHV272WWh3gpW`<br><br>String ID of the section |

### Request Body

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string or null (Name) | No | Updated section name. Passing null or omitting this field will leave it unchanged. |

##### `name`

Any of:

- Name (string)
- Name (null)

### Request Sample

```json
{
  "name": "string"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `user_id` | string (User Id) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `added_at` | string (Added At) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `archived_at` | string or null (Archived At) | Yes |  |
| `name` | string (Name) | Yes |  |
| `section_order` | integer (Section Order) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |

##### `updated_at`

Any of:

- Updated At (string)
- Updated At (null)

##### `archived_at`

Any of:

- Archived At (string)
- Archived At (null)

#### Response Sample

```json
{
  "id": "string",
  "user_id": "string",
  "project_id": "string",
  "added_at": "string",
  "updated_at": "string",
  "archived_at": "string",
  "name": "string",
  "section_order": 0,
  "is_archived": true,
  "is_deleted": true,
  "is_collapsed": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Delete Section

`DELETE` `/api/v1/sections/{section_id}`

Base URL: `https://api.todoist.com`

Delete the section and all of its tasks

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `section_id` | string or integer (Section Id) | Yes | Examples: `6fFPHV272WWh3gpW`<br><br>String ID of the section |

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
# Labels

# Labels

---

# Search Labels

`GET` `/api/v1/labels/search`

Base URL: `https://api.todoist.com`

Search user labels by name.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `query` | string (Query) | Yes | [ 1 .. 1024 ] characters<br><br>Examples: `query=urgent` `query=priority-*` `query=*-review` `query=5\*`<br><br>Search query to match label names. Matching is case-insensitive. Queries are matched literally unless `*` (wildcard) is included. Use `\*` for literal asterisk and `\\` for literal backslash. |
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
| `name` | string (Name) | Yes |  |
| `color` | string (Color) | Yes |  |
| `order` | integer or null (Order) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |

##### `order`

Any of:

- Order (integer)
- Order (null)

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
      "name": "string",
      "color": "string",
      "order": 0,
      "is_favorite": true
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

# Shared Labels

`GET` `/api/v1/labels/shared`

Base URL: `https://api.todoist.com`

Returns a set of unique strings containing labels from active tasks.

By default, the names of a user's personal labels will also be included. These can be excluded by passing the `omit_personal` parameter.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `omit_personal` | boolean (Omit Personal) | No | Default: `false` |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of strings (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

##### `next_cursor`

Any of:

- Next Cursor (string)
- Next Cursor (null)

#### Response Sample

```json
{
  "results": [
    "string"
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Labels

`GET` `/api/v1/labels`

Base URL: `https://api.todoist.com`

Get all user labels.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
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
| `name` | string (Name) | Yes |  |
| `color` | string (Color) | Yes |  |
| `order` | integer or null (Order) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |

##### `order`

Any of:

- Order (integer)
- Order (null)

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
      "name": "string",
      "color": "string",
      "order": 0,
      "is_favorite": true
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

# Create Label

`POST` `/api/v1/labels`

Base URL: `https://api.todoist.com`

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes | <= 128 characters<br><br>Name of the new label |
| `order` | integer or null (Order) | No | [ -32768 .. 32767 ]<br><br>Position of the new label in the label list |
| `color` | string or integer (Color) | No | Default: `"charcoal"`<br><br>Enum: `"berry_red"`, `"red"`, `"orange"`, `"yellow"`, `"olive_green"`, `"lime_green"`, `"green"`, `"mint_green"`, `"teal"`, `"sky_blue"`, `"light_blue"`, `"blue"`, `"grape"`, `"violet"`, `"lavender"`, `"magenta"`, `"salmon"`, `"charcoal"`, `"grey"`, `"taupe"`<br><br>Label color |
| `is_favorite` | boolean (Is Favorite) | No | Default: `false`<br><br>Whether the label is marked as a favorite |

#### Request Sample

```json
{
  "name": "string",
  "order": 12,
  "color": "charcoal",
  "is_favorite": false
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `name` | string (Name) | Yes |  |
| `color` | string (Color) | Yes |  |
| `order` | integer or null (Order) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |

##### `order`

Any of:

- Order (integer)
- Order (null)

#### Response Sample

```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Shared Labels Remove

`POST` `/api/v1/labels/shared/remove`

Base URL: `https://api.todoist.com`

Remove the given shared label from all active tasks

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes |  |

#### Request Sample

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

# Shared Labels Rename

`POST` `/api/v1/labels/shared/rename`

Base URL: `https://api.todoist.com`

Rename the given shared label from all active tasks

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes |  |
| `new_name` | string (New Name) | Yes |  |

#### Request Sample

```json
{
  "name": "string",
  "new_name": "string"
}
```

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

# Delete Label

`DELETE` `/api/v1/labels/{label_id}`

Base URL: `https://api.todoist.com`

Deletes a personal label. All instances of the label will be removed from tasks

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `label_id` | integer (Label Id) | Yes | Examples: `2147509004`<br><br>String ID of the label |

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

# Get Label

`GET` `/api/v1/labels/{label_id}`

Base URL: `https://api.todoist.com`

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `label_id` | integer (Label Id) | Yes | Examples: `2147509004`<br><br>String ID of the label |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `name` | string (Name) | Yes |  |
| `color` | string (Color) | Yes |  |
| `order` | integer or null (Order) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |

##### `order`

Any of:

- Order (integer)
- Order (null)

#### Response Sample

```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Label

`POST` `/api/v1/labels/{label_id}`

Base URL: `https://api.todoist.com`

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `label_id` | integer (Label Id) | Yes | Examples: `2147509004`<br><br>String ID of the label |

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string or null (Name) | No | <= 128 characters<br><br>Updated label name. Passing null or omitting this field will leave it unchanged. |
| `order` | integer or null (Order) | No | [ -32768 .. 32767 ]<br><br>Position of the label in the label list. Passing null or omitting this field will leave it unchanged. |
| `color` | string or integer or null (Color) | No | Enum: `"berry_red"`, `"red"`, `"orange"`, `"yellow"`, `"olive_green"`, `"lime_green"`, `"green"`, `"mint_green"`, `"teal"`, `"sky_blue"`, `"light_blue"`, `"blue"`, `"grape"`, `"violet"`, `"lavender"`, `"magenta"`, `"salmon"`, `"charcoal"`, `"grey"`, `"taupe"`<br><br>Label color. Passing null or omitting this field will leave it unchanged. |
| `is_favorite` | boolean or null (Is Favorite) | No | Whether the label is marked as a favorite. Passing null or omitting this field will leave it unchanged. |

#### Request Sample

```json
{
  "name": "string",
  "order": 12,
  "color": "charcoal",
  "is_favorite": true
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `name` | string (Name) | Yes |  |
| `color` | string (Color) | Yes |  |
| `order` | integer or null (Order) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |

##### `order`

Any of:

- Order (integer)
- Order (null)

#### Response Sample

```json
{
  "id": "string",
  "name": "string",
  "color": "string",
  "order": 0,
  "is_favorite": true
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
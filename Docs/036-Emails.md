# Emails

# Emails

---

# Email Disable

`DELETE` `/api/v1/emails`

Base URL: `https://api.todoist.com`

Disable the current email to a Todoist object

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `obj_type` | string (Obj Type) | Yes | Enum: `"project"`, `"project_comments"`, `"task"` |
| `obj_id` | string or integer (Obj Id) | Yes |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `status` | string (Status) | Yes | Value: `"ok"` |

#### Response Sample

```json
{
  "status": "ok"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Email Get Or Create

`PUT` `/api/v1/emails`

Base URL: `https://api.todoist.com`

Get or create an email to a Todoist object,
currently only projects and tasks are supported.

## Request

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `obj_type` | string (EmailObjectType) | Yes | Enum: `"project"` `"project_comments"` `"task"` |
| `obj_id` | string or integer (Obj Id) | Yes |  |

#### Request Sample

```json
{
  "obj_type": "project",
  "obj_id": "string"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `email` | string (Email) | Yes |  |

#### Response Sample

```json
{
  "email": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
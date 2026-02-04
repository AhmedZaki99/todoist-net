# Backups

# Backups

*Availability of backups functionality is dependent on the current user plan. This value is indicated by the automatic_backups property of the user plan limits object.*

---

# Download Backup

`GET` `/api/v1/backups/download`

Base URL: `https://api.todoist.com`

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `file` | string (File) | Yes | Examples: `file=https://api.todoist.com/api/v1/backups/download?file=12345678901234567890123456789012.zip`<br><br>Backup URL |

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

# Get Backups

`GET` `/api/v1/backups`

Base URL: `https://api.todoist.com`

Todoist creates a backup archive of users' data on a daily basis. Backup
archives can also be accessed from the web app (Todoist Settings ->
Backups).

When using the default token, with the `data:read_write` scope, and having MFA enabled, the MFA
token is required and must be provided with the request. To be able to use this endpoint without an
MFA token, your token must have the `backups:read` scope.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `mfa_token` | string (Mfa Token) or null | No |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | array of objects | No |  |

#### `(root)[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `version` | string (Version) | Yes | Date and time of the backup version |
| `url` | string (Url) | Yes | Backup URL |

#### Response Sample

```json
[
  {
    "version": "2025-02-13 02:03",
    "url": "https://api.todoist.com/api/v1/backups/download?file=12345678901234567890123456789012.zip"
  }
]
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
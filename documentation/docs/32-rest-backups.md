# Backups

## Download Backup

`GET /api/v1/backups/download`

Downloads a backup file.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `backup_id` | string or integer | Yes | ID of the backup to download |

### Responses

**200** Successful Response

Content type: `application/zip`

Returns the backup file as a ZIP archive.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Backups

`GET /api/v1/backups`

Returns list of available backups for the user.

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of backup objects

| Field | Type | Description |
|-------|------|-------------|
| `id` | string (required) | Backup ID |
| `version` | string (required) | Backup version |
| `created_at` | string (required) | ISO 8601 timestamp when backup was created |
| `size` | integer (required) | Backup file size in bytes |

Response sample:
```json
[
  {
    "id": "12345",
    "version": "1.0",
    "created_at": "2024-01-15T02:00:00Z",
    "size": 1048576
  },
  {
    "id": "12346",
    "version": "1.0",
    "created_at": "2024-01-14T02:00:00Z",
    "size": 1024000
  }
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

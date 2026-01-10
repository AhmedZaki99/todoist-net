# Uploads

## Delete Upload

`DELETE /api/v1/uploads`

Deletes an uploaded file.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `file_url` | string | Yes | URL of the uploaded file to delete |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Upload File

`POST /api/v1/uploads`

Uploads a file attachment.

### Request Body

Content type: `multipart/form-data`

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `file` | string (binary) | Yes | File to upload |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `file_name` | string (required) | Original filename |
| `file_size` | integer (required) | File size in bytes |
| `file_type` | string (required) | MIME type |
| `file_url` | string (required) | URL of uploaded file |
| `upload_state` | string (required) | Upload state ("completed") |

Response sample:
```json
{
  "file_name": "document.pdf",
  "file_size": 102400,
  "file_type": "application/pdf",
  "file_url": "https://cdn.todoist.com/...",
  "upload_state": "completed"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

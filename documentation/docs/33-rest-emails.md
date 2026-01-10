# Emails

## Email Disable

`DELETE /api/v1/emails`

Disables email forwarding for a project.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | ID of the project to disable email for |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Email Get Or Create

`PUT /api/v1/emails`

Gets existing email address for a project, or creates one if it doesn't exist.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | ID of the project |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `email` | string (required) | Email address for the project |

Response sample:
```json
{
  "email": "project.abc123@todoist.net"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

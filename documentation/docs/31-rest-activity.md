# Activity

## Get Activity Logs

`GET /api/v1/activities`

Returns activity logs showing changes made to projects, tasks, and other resources.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `object_type` | string or null | No | Filter by object type (e.g., "item", "project", "section") |
| `object_id` | string, integer, or null | No | Filter by specific object ID |
| `object_event_types` | array of strings or null | No | Filter by event types (e.g., "added", "updated", "completed", "deleted") |
| `parent_project_id` | string, integer, or null | No | Filter by parent project ID |
| `parent_item_id` | string, integer, or null | No | Filter by parent task ID |
| `initiator_id` | string, integer, or null | No | Filter by user who initiated the change |
| `since` | string or null | No | ISO 8601 timestamp - show activities after this time |
| `until` | string or null | No | ISO 8601 timestamp - show activities before this time |
| `limit` | integer | No | Maximum number of results. Default: 30, Range: (0..100] |
| `offset` | integer | No | Number of items to skip. Default: 0 |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer (required) | Total count of activity items |
| `events` | Array of objects (required) | Array of activity event objects |

Each event object contains:
- `id`: Unique event ID (string)
- `object_type`: Type of object ("item", "project", "note", etc.)
- `object_id`: ID of the object
- `event_type`: Type of event ("added", "updated", "completed", "uncompleted", "deleted", "archived", "unarchived", "shared")
- `event_date`: ISO 8601 timestamp
- `parent_project_id`: ID of parent project (if applicable)
- `parent_item_id`: ID of parent task (if applicable)
- `initiator_id`: ID of user who initiated the change
- `extra_data`: Object containing event-specific details

Response sample:
```json
{
  "count": 45,
  "events": [
    {
      "id": "12345678",
      "object_type": "item",
      "object_id": "98765432",
      "event_type": "completed",
      "event_date": "2024-01-15T14:30:00Z",
      "parent_project_id": "11111111",
      "parent_item_id": null,
      "initiator_id": "22222222",
      "extra_data": {
        "content": "Buy groceries",
        "last_content": "Buy groceries",
        "completed_at": "2024-01-15T14:30:00Z"
      }
    },
    {
      "id": "12345679",
      "object_type": "project",
      "object_id": "11111111",
      "event_type": "updated",
      "event_date": "2024-01-15T10:00:00Z",
      "parent_project_id": null,
      "parent_item_id": null,
      "initiator_id": "22222222",
      "extra_data": {
        "name": "Shopping List",
        "last_name": "Shopping"
      }
    }
  ]
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

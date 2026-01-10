# IDs

Endpoints related to ID mappings between v1 and v2

## Id Mappings

`GET /api/v1/id_mappings/{obj_name}/{obj_ids}`

Translates IDs from v1 to v2 or vice versa.

IDs are not unique across object types, hence the need to specify the object type.

When V1 ids are provided, the function will return the corresponding V2 ids, if they exist, and vice versa.

When no objects are found, an empty list is returned.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `obj_name` | string | Yes | Enum: `"sections"` `"tasks"` `"comments"` `"reminders"` `"location_reminders"` `"projects"` |
| `obj_ids` | string | Yes | A comma-separated list of IDs. Examples: `6VfWjjjFg2xqX6Pa` `918273645` `6VfWjjjFg2xqX6Pa,6WMVPf8Hm8JP6mC8` |

### Responses

**200** Successful Response

Content type: `application/json`

Example:
```json
[
  {
    "old_id": "918273645",
    "new_id": "6VfWjjjFg2xqX6Pa"
  }
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

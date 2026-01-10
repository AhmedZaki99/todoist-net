# User

## User Info

`GET /api/v1/user`

Returns information about the current user.

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `auto_reminder` | integer (required) | Minutes before task due time for automatic reminders |
| `avatar_big` | string (required) | URL of large avatar image |
| `avatar_medium` | string (required) | URL of medium avatar image |
| `avatar_s640` | string (required) | URL of 640px avatar image |
| `avatar_small` | string (required) | URL of small avatar image |
| `business_account_id` | string or null (required) | Business account ID if applicable |
| `daily_goal` | integer (required) | Daily task completion goal |
| `date_format` | integer (required) | Date format preference (0 or 1) |
| `dateist_inline_disabled` | boolean (required) | Whether inline date parsing is disabled |
| `dateist_lang` | string or null (required) | Language for date parsing |
| `days_off` | Array of integers (required) | Days of week that are days off (0=Sunday) |
| `default_reminder` | string or null (required) | Default reminder setting |
| `email` | string (required) | User email address |
| `features` | object (required) | Feature flags object |
| `full_name` | string (required) | User's full name |
| `id` | string (required) | User ID |
| `image_id` | string or null (required) | Custom avatar image ID |
| `inbox_project_id` | string (required) | ID of inbox project |
| `is_biz_admin` | boolean (required) | Whether user is business admin |
| `is_premium` | boolean (required) | Whether user has premium subscription |
| `joined_at` | string (required) | ISO 8601 timestamp of when user joined |
| `karma` | number (required) | User's karma score |
| `karma_trend` | string (required) | Karma trend ("up" or "down") |
| `lang` | string (required) | User's language preference |
| `legacy_inbox_project_id` | integer or null (required) | Legacy inbox project ID |
| `mobile_host` | string or null (required) | Mobile host URL |
| `mobile_number` | string or null (required) | User's mobile number |
| `next_week` | integer (required) | Day of week when next week starts (1=Monday) |
| `premium_until` | string or null (required) | ISO 8601 timestamp when premium expires |
| `sort_order` | integer (required) | Task sort order preference |
| `start_day` | integer (required) | Day when week starts (1=Monday, 7=Sunday) |
| `start_page` | string (required) | Default start page |
| `theme` | integer (required) | Theme ID |
| `time_format` | integer (required) | Time format preference (0=24h, 1=12h) |
| `timezone` | string (required) | User's timezone |
| `token` | string (required) | API token |
| `tz_info` | object (required) | Timezone information object |
| `unique_prefix` | integer (required) | Unique prefix for user |
| `weekly_goal` | integer (required) | Weekly task completion goal |

Response sample:
```json
{
  "auto_reminder": 30,
  "avatar_big": "https://dcff1xvirvpfp.cloudfront.net/big.jpg",
  "avatar_medium": "https://dcff1xvirvpfp.cloudfront.net/medium.jpg",
  "avatar_s640": "https://dcff1xvirvpfp.cloudfront.net/s640.jpg",
  "avatar_small": "https://dcff1xvirvpfp.cloudfront.net/small.jpg",
  "business_account_id": null,
  "daily_goal": 5,
  "date_format": 0,
  "dateist_inline_disabled": false,
  "dateist_lang": null,
  "days_off": [0, 6],
  "default_reminder": "mobile",
  "email": "user@example.com",
  "features": {},
  "full_name": "John Doe",
  "id": "123456",
  "image_id": null,
  "inbox_project_id": "789012",
  "is_biz_admin": false,
  "is_premium": true,
  "joined_at": "2020-01-15T10:30:00Z",
  "karma": 1234.5,
  "karma_trend": "up",
  "lang": "en",
  "legacy_inbox_project_id": null,
  "mobile_host": null,
  "mobile_number": null,
  "next_week": 1,
  "premium_until": "2025-01-15T10:30:00Z",
  "sort_order": 0,
  "start_day": 1,
  "start_page": "inbox",
  "theme": 0,
  "time_format": 0,
  "timezone": "America/New_York",
  "token": "abc123def456",
  "tz_info": {
    "timezone": "America/New_York",
    "gmt_string": "-05:00",
    "is_dst": 0,
    "hours": -5,
    "minutes": 0
  },
  "unique_prefix": 12345,
  "weekly_goal": 25
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Productivity Stats

`GET /api/v1/tasks/completed/stats`

Returns productivity statistics for completed tasks.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string, integer, or null | No | Filter by project ID |
| `label_id` | string, integer, or null | No | Filter by label ID |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `days_items` | Array of objects (required) | Daily completion data |
| `week_items` | Array of objects (required) | Weekly completion data |
| `total` | integer (required) | Total tasks completed |
| `goals` | object (required) | Goal information |

Each item in `days_items` contains:
- `date`: Date string (YYYY-MM-DD)
- `items`: Array of completed task IDs
- `total_completed`: Integer count

Response sample:
```json
{
  "days_items": [
    {
      "date": "2024-01-15",
      "items": ["task1", "task2"],
      "total_completed": 2
    }
  ],
  "week_items": [
    {
      "date": "2024-01-08",
      "items": ["task1", "task2", "task3"],
      "total_completed": 3
    }
  ],
  "total": 150,
  "goals": {
    "daily_goal": 5,
    "weekly_goal": 25
  }
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Notification Setting

`PUT /api/v1/notification_setting`

Updates user notification settings.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `notification_type` | string | Yes | Type of notification to configure |
| `service` | string | Yes | Service type ("email" or "push") |
| `enabled` | boolean | Yes | Whether notification is enabled |

### Request sample

Content type: `application/json`

```json
{
  "notification_type": "task_completed",
  "service": "email",
  "enabled": true
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

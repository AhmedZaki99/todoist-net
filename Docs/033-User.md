# User

# User

---

# User Info

`GET` `/api/v1/user`

Base URL: `https://api.todoist.com`

Get information about the currently authenticated user.

## Request

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes | User ID |
| `email` | string (Email) | Yes | User's email address |
| `full_name` | string (Full Name) | Yes | The user's real name formatted as Firstname Lastname |
| `has_password` | boolean (Has Password) | Yes | Whether the user has a password set on the account. It will be false if they have only authenticated without a password (e.g. using Google, Facebook, etc.) |
| `verification_status` | string (Verification Status) | Yes | User's email verification status. unverified (just signed up), verified (verified email or social login), blocked (failed to verify in 7 days), legacy (signed up before August 2022) |
| `mfa_enabled` | boolean (Mfa Enabled) | Yes | Whether multi-factor authentication is enabled |
| `token` | string or null (Token) | No | The user's token that should be used to call the other API methods |
| `is_premium` | boolean (Is Premium) | Yes | Whether the user has a Todoist Pro subscription (a true or false value) |
| `premium_status` | string or null (Premium Status) | No | Outlines why a user is premium, possible values are: not_premium, current_personal_plan, legacy_personal_plan or teams_business_member |
| `premium_until` | string or null (Premium Until) | No | The date when the user's Todoist Pro subscription ends (null if not a Todoist Pro user). This should be used for informational purposes only as this does not include the grace period upon expiration |
| `free_trial_expires` | string or null (Free Trial Expires) | No | Date when free trial expires (ISO 8601 format) |
| `has_started_a_trial` | boolean (Has Started A Trial) | Yes | Whether the user has ever started a free trial |
| `joined_at` | string or null (Joined At) | No | Date when user joined Todoist (ISO 8601 format) |
| `is_deleted` | boolean (Is Deleted) | No | Whether the user is deleted |
| `deleted_at` | string or null (Deleted At) | No | Date when user was deleted (ISO 8601 format) |
| `business_account_id` | integer or null (Business Account Id) | No | The ID of the user's business account |
| `date_format` | integer (Date Format) | Yes | Whether to use the DD-MM-YYYY date format (if set to 0), or the MM-DD-YYYY format (if set to 1) |
| `time_format` | integer or null (Time Format) | No | Whether to use a 24h format such as 13:00 (if set to 0) when displaying time, or a 12h format such as 1:00pm (if set to 1) |
| `sort_order` | integer (Sort Order) | Yes | Whether to show projects in an oldest dates first order (if set to 0), or a oldest dates last order (if set to 1) |
| `theme_id` | string (Theme Id) | Yes | The currently selected Todoist theme (a number between 0 and 13) |
| `start_day` | integer (Start Day) | Yes | The first day of the week (between 1 and 7, where 1 is Monday and 7 is Sunday) |
| `weekend_start_day` | integer (Weekend Start Day) | Yes | The day used when a user chooses to schedule a task for the 'Weekend' (between 1 and 7, where 1 is Monday and 7 is Sunday) |
| `next_week` | integer (Next Week) | Yes | The day of the next week, that tasks will be postponed to (between 1 and 7, where 1 is Monday and 7 is Sunday) |
| `auto_reminder` | integer (Auto Reminder) | Yes | The default time in minutes for the automatic reminders set, whenever a due date has been specified for a task |
| `start_page` | string (Start Page) | Yes | The user's default view on Todoist. The start page can be one of the following: inbox, teaminbox, today, next7days, project?id=1234 to open a project, label?name=abc to open a label, or filter?id=1234 to open a filter |
| `inbox_project_id` | string or null (Inbox Project Id) | No | The ID of the user's Inbox project |
| `lang` | string (Lang) | Yes | The user's language |
| `tz_info` | object (Tz Info) | Yes | The user's timezone (a dictionary structure), which includes the following elements: the timezone as a string value, the hours and minutes difference from GMT, whether daylight saving time applies denoted by is_dst, and a string value of the time difference from GMT that is gmt_string |
| `karma` | number (Karma) | Yes | The user's karma score |
| `karma_trend` | string or null (Karma Trend) | No | The user's karma trend. Can be 'up', 'down', or '-' (no change) |
| `daily_goal` | integer (Daily Goal) | Yes | The daily goal number of completed tasks for karma |
| `weekly_goal` | integer (Weekly Goal) | Yes | The target number of tasks to complete per week |
| `days_off` | array of integers (Days Off) | Yes | Array of integers representing user's days off (between 1 and 7, where 1 is Monday and 7 is Sunday) |
| `is_celebrations_enabled` | boolean (Is Celebrations Enabled) | Yes | Whether celebration animations are enabled |
| `completed_count` | integer (Completed Count) | Yes | Total number of tasks completed by user |
| `completed_today` | integer (Completed Today) | Yes | Number of tasks completed today by the user |
| `share_limit` | integer (Share Limit) | Yes | Maximum number of collaborators allowed in shared projects |
| `features` | object (Features) | Yes | Feature flags and settings for the user |
| `feature_identifier` | string (Feature Identifier) | Yes | Feature identifier for feature flag evaluations |
| `joinable_workspace` | object or null (Joinable Workspace) | Yes | Information about workspaces the user can join |
| `onboarding_completed` | boolean (Onboarding Completed) | Yes | Whether the user has completed onboarding |
| `onboarding_initiated` | boolean (Onboarding Initiated) | Yes | Whether the user has initiated onboarding |
| `onboarding_started` | boolean (Onboarding Started) | Yes | Whether the user has started onboarding |
| `onboarding_level` | string or null (Onboarding Level) | No | User's self-reported skill level during onboarding |
| `onboarding_persona` | string or null (Onboarding Persona) | No | User's onboarding persona selection |
| `onboarding_role` | string or null (Onboarding Role) | No | User's role selection during onboarding |
| `onboarding_team_mode` | string or null (Onboarding Team Mode) | No | Whether user selected team mode during onboarding |
| `onboarding_use_cases` | array of strings or null (Onboarding Use Cases) | No | Use cases the user selected during onboarding |
| `getting_started_guide_projects` | array of strings or null (Getting Started Guide Projects) | No | List of project IDs for getting started guide |
| `activated_user` | boolean (Activated User) | Yes | Whether the user is considered activated (completed key onboarding actions) |
| `has_magic_number` | boolean (Has Magic Number) | Yes | Whether the user has reached a magic number milestone |
| `image_id` | string or null (Image Id) | No | The ID of the user's avatar |
| `avatar_big` | string or null (Avatar Big) | No | The link to a 195x195 pixels image of the user's avatar |
| `avatar_medium` | string or null (Avatar Medium) | No | The link to a 60x60 pixels image of the user's avatar |
| `avatar_s640` | string or null (Avatar S640) | No | The link to a 640x640 pixels image of the user's avatar |
| `avatar_small` | string or null (Avatar Small) | No | The link to a 35x35 pixels image of the user's avatar |
| `websocket_url` | string (Websocket Url) | Yes | WebSocket URL for real-time updates |

#### Response Sample

```json
{
  "id": "string",
  "email": "string",
  "full_name": "string",
  "has_password": true,
  "verification_status": "unverified",
  "mfa_enabled": true,
  "token": "string",
  "is_premium": true,
  "premium_status": "not_premium",
  "premium_until": "string",
  "free_trial_expires": "string",
  "has_started_a_trial": true,
  "joined_at": "string",
  "is_deleted": false,
  "deleted_at": "string",
  "business_account_id": 0,
  "date_format": 0,
  "time_format": 0,
  "sort_order": 0,
  "theme_id": "string",
  "start_day": 1,
  "weekend_start_day": 1,
  "next_week": 1,
  "auto_reminder": 0,
  "start_page": "string",
  "inbox_project_id": "string",
  "lang": "cs",
  "tz_info": { },
  "karma": 0,
  "karma_trend": "up",
  "daily_goal": 0,
  "weekly_goal": 0,
  "days_off": [
    0
  ],
  "is_celebrations_enabled": true,
  "completed_count": 0,
  "completed_today": 0,
  "share_limit": 0,
  "features": { },
  "feature_identifier": "string",
  "joinable_workspace": { },
  "onboarding_completed": true,
  "onboarding_initiated": true,
  "onboarding_started": true,
  "onboarding_level": "beginner",
  "onboarding_persona": "analog",
  "onboarding_role": "leader",
  "onboarding_team_mode": "string",
  "onboarding_use_cases": [
    "personal"
  ],
  "getting_started_guide_projects": [
    "string"
  ],
  "activated_user": true,
  "has_magic_number": true,
  "image_id": "string",
  "avatar_big": "string",
  "avatar_medium": "string",
  "avatar_s640": "string",
  "avatar_small": "string",
  "websocket_url": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Productivity Stats

`GET` `/api/v1/tasks/completed/stats`

Base URL: `https://api.todoist.com`

Get comprehensive productivity statistics for the authenticated user.

Returns detailed completion statistics including:

- Daily completion counts with per-project breakdowns for the last 7 days
- Weekly completion counts with per-project breakdowns for the last 4 weeks
- Total completed task count
- Karma score, trend, graph data, and update history
- Goal settings (daily/weekly goals, ignore days, vacation mode)
- Streak information (current, last, and maximum daily and weekly streaks)
- Project color mappings for visualization

## Request

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `days_items` | array of objects (Days Items) | Yes | Daily completion statistics for the last 7 days, ordered chronologically |
| `week_items` | array of objects (Week Items) | Yes | Weekly completion statistics for the last 4 weeks, ordered chronologically. Only available for Pro and Business plan users |
| `project_colors` | object (Project Colors) | Yes | Mapping of project IDs to their color names (e.g., {'2aB3cD4eF5gH6iJ7': 'blue', '8kL9mN0oP1qR2sT3': 'red'}) |
| `completed_count` | integer (Completed Count) | Yes | Total number of tasks completed by the user (all time) |
| `karma` | number (Karma) | Yes | Current karma score |
| `karma_trend` | string (Karma Trend) | Yes | Karma trend indicator ('up' or 'down') |
| `karma_graph_data` | array of objects (Karma Graph Data) | Yes | Historical karma data points for graphing, each containing date and karma_avg |
| `karma_last_update` | number (Karma Last Update) | Yes | Net karma change from the most recent update (positive_karma + negative_karma) |
| `karma_update_reasons` | array of objects (Karma Update Reasons) | Yes | Recent karma update events with reasons and amounts |
| `goals` | object (GoalsSettings) | Yes | User goals, streaks, and productivity settings |

##### `days_items` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `date` | string &lt;date&gt; (Date) | Yes | Date for this daily completion data (ISO 8601 format: YYYY-MM-DD) |
| `items` | array of objects (Items) | Yes | Per-project completion breakdown for this day |
| `total_completed` | integer (Total Completed) | Yes | Total number of tasks completed across all projects on this day |

##### `days_items.items` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes | Project ID (v1 or v2 format depending on API version) |
| `completed` | integer (Completed) | Yes | Number of tasks completed in this project during the time period |

##### `week_items` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `from` | string &lt;date&gt; (From) | Yes | Start date of the week (Monday, ISO 8601 format: YYYY-MM-DD) |
| `to` | string &lt;date&gt; (To) | Yes | End date of the week (Sunday, ISO 8601 format: YYYY-MM-DD) |
| `items` | array of objects (Items) | Yes | Per-project completion breakdown for this week |
| `total_completed` | integer (Total Completed) | Yes | Total number of tasks completed across all projects during this week |

##### `week_items.items` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes | Project ID (v1 or v2 format depending on API version) |
| `completed` | integer (Completed) | Yes | Number of tasks completed in this project during the time period |

##### `project_colors` schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | string or null | No | additional property |

##### `karma_graph_data` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `date` | string (Date) | Yes |  |
| `karma_avg` | integer (Karma Avg) | Yes |  |

##### `karma_update_reasons` item schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `time` | string or null (Time) | No | Timestamp when this karma update occurred (ISO 8601 datetime) |
| `new_karma` | number (New Karma) | Yes | New total karma score after this update |
| `positive_karma` | number (Positive Karma) | Yes | Amount of karma gained in this update |
| `negative_karma` | number (Negative Karma) | Yes | Amount of karma lost in this update (negative value) |
| `positive_karma_reasons` | array of integers (Positive Karma Reasons) | Yes | List of reason codes for karma gains. Values: 1=Tasks added, 2=Tasks completed, 3=Advanced features, 4=Signup, 5=Beta upgrade, 6=Support activity, 7=Premium upgrade, 8=Getting started, 9=Daily goal reached, 10=Weekly goal reached |
| `negative_karma_reasons` | array of integers (Negative Karma Reasons) | Yes | List of reason codes for karma losses. Values: 50=Tasks overdue, 51=Tasks postponed, 52=Is inactive |

##### `goals` schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user` | string (User) | Yes | User ID (legacy field, same as user_id) |
| `user_id` | string (User Id) | Yes | User ID |
| `daily_goal` | integer (Daily Goal) | Yes | Daily task completion goal set by the user |
| `weekly_goal` | integer (Weekly Goal) | Yes | Weekly task completion goal set by the user |
| `ignore_days` | array of integers (Ignore Days) | Yes | List of weekday numbers (0=Sunday, 1=Monday, ..., 6=Saturday) excluded from daily goals |
| `vacation_mode` | integer (Vacation Mode) | Yes | Vacation mode status (0=disabled, 1=enabled) |
| `karma_disabled` | integer (Karma Disabled) | Yes | Karma tracking status (0=enabled, 1=disabled) |
| `current_daily_streak` | object (Current Daily Streak) | Yes | Current consecutive daily completion streak |
| `current_weekly_streak` | object (Current Weekly Streak) | Yes | Current consecutive weekly completion streak |
| `last_daily_streak` | object (Last Daily Streak) | Yes | Previous daily streak (before the current one) |
| `last_weekly_streak` | object (Last Weekly Streak) | Yes | Previous weekly streak (before the current one) |
| `max_daily_streak` | object (Max Daily Streak) | Yes | Longest daily streak achieved by the user |
| `max_weekly_streak` | object (Max Weekly Streak) | Yes | Longest weekly streak achieved by the user |

##### streak schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `count` | integer (Count) | Yes | Number of consecutive days/weeks in the streak |
| `start` | string or null (Start) | No | Start date of the streak (ISO 8601 format: YYYY-MM-DD), or null if no streak |
| `end` | string or null (End) | No | End date of the streak (ISO 8601 format: YYYY-MM-DD), or null if no streak |

#### Response Sample

```json
{
  "days_items": [
    {
      "date": "2019-08-24",
      "items": [
        {
          "id": "string",
          "completed": 0
        }
      ],
      "total_completed": 0
    }
  ],
  "week_items": [
    {
      "from": "2019-08-24",
      "to": "2019-08-24",
      "items": [
        {
          "id": "string",
          "completed": 0
        }
      ],
      "total_completed": 0
    }
  ],
  "project_colors": {
    "property1": "string",
    "property2": "string"
  },
  "completed_count": 0,
  "karma": 0,
  "karma_trend": "string",
  "karma_graph_data": [
    {
      "date": "string",
      "karma_avg": 0
    }
  ],
  "karma_last_update": 0,
  "karma_update_reasons": [
    {
      "time": "string",
      "new_karma": 0,
      "positive_karma": 0,
      "negative_karma": 0,
      "positive_karma_reasons": [
        1
      ],
      "negative_karma_reasons": [
        50
      ]
    }
  ],
  "goals": {
    "user": "string",
    "user_id": "string",
    "daily_goal": 0,
    "weekly_goal": 0,
    "ignore_days": [
      0
    ],
    "vacation_mode": 0,
    "karma_disabled": 0,
    "current_daily_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    },
    "current_weekly_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    },
    "last_daily_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    },
    "last_weekly_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    },
    "max_daily_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    },
    "max_weekly_streak": {
      "count": 0,
      "start": "2019-08-24",
      "end": "2019-08-24"
    }
  }
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Notification Setting

`PUT` `/api/v1/notification_setting`

Base URL: `https://api.todoist.com`

## Request

Request Body Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `notification_type` | string (NotificationType) | Yes | Enum: `"note_added"` `"item_assigned"` `"item_completed"` `"item_uncompleted"` `"karma_level"` `"share_invitation_sent"` `"share_invitation_accepted"` `"share_invitation_rejected"` `"share_invitation_blocked_by_project_limit"` `"user_left_project"` `"user_removed_from_project"` `"teams_workspace_upgraded"` `"teams_workspace_canceled"` `"teams_workspace_payment_failed"` `"pro_trial_started"` `"pro_trial_ended"` `"workspace_invitation_created"` `"workspace_invitation_accepted"` `"workspace_invitation_rejected"` `"project_archived"` `"project_moved"` `"removed_from_workspace"` `"workspace_deleted"` `"message"` `"workspace_user_joined_by_domain"` `"price_increase_new_pro_users"` `"price_increase_new_team"` `"price_increase_new_team_trial"` `"price_increase_android"` `"workspace_team_cohort_tagged"`<br><br>The type of notification being sent |
| `service` | string (NotificationChannel) | Yes | Enum: `"email"` `"push"`<br><br>Which communication mechanism is being used to send this notification |
| `token` | string or null (Token) | No |  |
| `dont_notify` | boolean or null (Dont Notify) | No |  |

#### Request Sample

```json
{
  "notification_type": "note_added",
  "service": "email",
  "token": "string",
  "dont_notify": true
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | object | No | additional property |

##### `property name*` schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | boolean | No | additional property |

#### Response Sample

```json
{
  "user_left_project": {
    "notify_push": true,
    "notify_email": true
  }
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
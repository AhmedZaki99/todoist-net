# User

> An example user:

```json
{
    "activated_user": false,
    "auto_reminder": 0,
    "avatar_big": "https://*.cloudfront.net/*_big.jpg",
    "avatar_medium": "https://*.cloudfront.net/*_medium.jpg",
    "avatar_s640": "https://*.cloudfront.net/*_s640.jpg",
    "avatar_small": "https://*.cloudfront.net/*_small.jpg",
    "business_account_id": "1",
    "daily_goal": 15,
    "date_format": 0,
    "days_off": [6, 7],
    "email": "me@example.com",
    "feature_identifier": "2671355_0123456789abcdef70123456789abcdefe0123456789abcdefd0123456789abc",
    "features": {
        "beta": 1,
        "dateist_inline_disabled": false,
        "dateist_lang": null,
        "global.teams": true,
        "has_push_reminders": true,
        "karma_disabled": false,
        "karma_vacation": false,
        "kisa_consent_timestamp": null,
        "restriction": 3
    },
    "full_name": "Example User",
    "has_password": true,
    "id": "2671355",
    "image_id": "d160009dfd52b991030d55227003450f",
    "inbox_project_id": "6X7fqH39MwjmwV4q",
    "is_celebrations_enabled": true,
    "is_premium": true,
    "joinable_workspace": null,
    "joined_at": "2015-07-31T18:32:06.000000Z",
    "karma": 37504,
    "karma_trend": "up",
    "lang": "en",
    "mfa_enabled": false,
    "next_week": 1,
    "premium_status": "current_personal_plan",
    "premium_until": null,
    "share_limit": 51,
    "sort_order": 0,
    "start_day": 1,
    "start_page": "project?id=2203306141",
    "theme_id": "11",
    "time_format": 0,
    "token": "0123456789abcdef0123456789abcdef01234567",
    "tz_info": {
        "gmt_string": "-03:00",
        "hours": -3,
        "is_dst": 0,
        "minutes": 0,
        "timezone": "America/Sao_Paulo"
    },
    "verification_status": "legacy",
    "weekend_start_day": 6,
    "weekly_goal": 30
}
```

A Todoist user is represented by a JSON object. The dates will be in the UTC timezone. Typically, a user object will have the following properties:

## Properties

| Property | Description |
|----------|-------------|
| auto_reminder *Integer* | The default time in minutes for the automatic reminders set, whenever a due date has been specified for a task. |
| avatar_big *String* | The link to a 195x195 pixels image of the user's avatar. |
| avatar_medium *String* | The link to a 60x60 pixels image of the user's avatar. |
| avatar_s640 *String* | The link to a 640x640 pixels image of the user's avatar. |
| avatar_small *String* | The link to a 35x35 pixels image of the user's avatar. |
| business_account_id *String* | The ID of the user's business account. |
| daily_goal *Integer* | The daily goal number of completed tasks for karma. |
| date_format *Integer* | Whether to use the `DD-MM-YYYY` date format (if set to `0`), or the `MM-DD-YYYY` format (if set to `1`). |
| dateist_lang *String* | The language expected for date recognition instead of the user's `lang` (`null` if the user's `lang` determines this), one of the following values: `da`, `de`, `en`, `es`, `fi`, `fr`, `it`, `ja`, `ko`, `nl`, `pl`, `pt_BR`, `ru`, `sv`, `tr`, `zh_CN`, `zh_TW`. |
| days_off *Array* | Array of integers representing user's days off (between `1` and `7`, where `1` is `Monday` and `7` is `Sunday`). |
| email *String* | The user's email. |
| feature_identifier *String* | An opaque id used internally to handle features for the user. |
| features *Object* | Used internally for any special features that apply to the user. Current special features include whether the user has enabled `beta`, whether `dateist_inline_disabled` that is inline date parsing support is disabled, whether the `dateist_lang` is set which overrides the date parsing language, whether the `gold_theme` has been awarded to the user, whether the user `has_push_reminders` enabled, whether the user has `karma_disabled`, whether the user has `karma_vacation` mode enabled, and whether any special `restriction` applies to the user. |
| full_name *String* | The user's real name formatted as `Firstname Lastname`. |
| has_password *Boolean* | Whether the user has a password set on the account. It will be `false` if they have only authenticated without a password (e.g. using Google, Facebook, etc.) |
| id *String* | The user's ID. |
| image_id *String* | The ID of the user's avatar. |
| inbox_project_id *String* | The ID of the user's `Inbox` project. |
| is_premium *Boolean* | Whether the user has a Todoist Pro subscription (a `true` or `false` value). |
| joined_at *String* | The date when the user joined Todoist. |
| karma *Integer* | The user's karma score. |
| karma_trend *String* | The user's karma trend (for example `up`). |
| lang *String* | The user's language, which can take one of the following values: `da`, `de`, `en`, `es`, `fi`, `fr`, `it`, `ja`, `ko`, `nl`, `pl`, `pt_BR`, `ru`, `sv`, `tr`, `zh_CN`, `zh_TW`. |
| next_week *Integer* | The day of the next week, that tasks will be postponed to (between `1` and `7`, where `1` is `Monday` and `7` is `Sunday`). |
| premium_status *String* | Outlines why a user is premium, possible values are: `not_premium`, `current_personal_plan`, `legacy_personal_plan` or `teams_business_member`. |
| premium_until *String* | The date when the user's Todoist Pro subscription ends (`null` if not a Todoist Pro user). This should be used for informational purposes only as this does not include the grace period upon expiration. As a result, avoid using this to determine whether someone has a Todoist Pro subscription and use `is_premium` instead. |
| sort_order *Integer* | Whether to show projects in an `oldest dates first` order (if set to `0`, or a `oldest dates last` order (if set to `1`). |
| start_day *Integer* | The first day of the week (between `1` and `7`, where `1` is `Monday` and `7` is `Sunday`). |
| start_page *String* | The user's default view on Todoist. The start page can be one of the following: `inbox`, `teaminbox`, `today`, `next7days`, `project?id=1234` to open a project, `label?name=abc` to open a label, `filter?id=1234` to open a personal filter or `workspace_filter?id=1234` to open a workspace filter. |
| theme_id *String* | The currently selected Todoist theme (a number between `0` and `10`). |
| time_format *Integer* | Whether to use a `24h` format such as `13:00` (if set to `0`) when displaying time, or a `12h` format such as `1:00pm` (if set to `1`). |
| token *String* | The user's token that should be used to call the other API methods. |
| tz_info *Object* | The user's timezone (a dictionary structure), which includes the following elements: the `timezone` as a string value, the `hours` and `minutes` difference from GMT, whether daylight saving time applies denoted by `is_dst`, and a string value of the time difference from GMT that is `gmt_string`. |
| weekend_start_day *Integer* | The day used when a user chooses to schedule a task for the 'Weekend' (between 1 and 7, where 1 is Monday and 7 is Sunday). |
| verification_status *String* | Describes if the user has verified their e-mail address or not. Possible values are: |
| weekly_goal *Integer* | The target number of tasks to complete per week. |

**Verification Status Values:**

- `unverified`, for users that have just signed up. Those users cannot use any of Todoist's social features like sharing projects or accepting project invitations.
- `verified`, for users that have verified themselves somehow. Clicking on the verification link inside the account confirmation e-mail is one such way alongside signing up through a social account.
- `blocked`, for users that have failed to verify themselves in 7 days. Those users will have restricted usage of Todoist.
- `legacy`, for users that have signed up before August, 2022

## Commands

### Update User's Properties

> Example update user request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "user_update",
        "uuid": "52f83009-7e27-4b9f-9943-1c5e3d1e6889",
        "args": {
            "current_password": "fke4iorij",
            "email": "mynewemail@example.com"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"52f83009-7e27-4b9f-9943-1c5e3d1e6889": "ok"},
  ...
}
```

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| current_password *String* | Yes (if modifying `email` or `password`) | The user's current password. This must be provided if the request is modifying the user's password or email address and the user already has a password set (indicated by `has_password` in the [user](#user) object). For amending other properties this is not required. |
| email *String* | No | The user's email. |
| full_name *String* | No | The user's name. |
| password *String* | No | The user's updated password. Must contain at least 8 characters and not be a common or easily guessed password. |
| timezone *String* | No | The user's timezone (a string value such as `UTC`, `Europe/Lisbon`, `US/Eastern`, `Asia/Taipei`). |
| start_page *String* | No | The user's default view on Todoist. The start page can be one of the following: `inbox`, `teaminbox`, `today`, `next7days`, `project?id=1234` to open a project, `label?name=abc` to open a label, `filter?id=1234` to open a personal filter or `workspace_filter?id=1234` to open a workspace filter. |
| start_day *Integer* | No | The first day of the week (between `1` and `7`, where `1` is `Monday` and `7` is `Sunday`). |
| next_week *Integer* | No | The day of the next week, that tasks will be postponed to (between `1` and `7`, where `1` is `Monday` and `7` is `Sunday`). |
| time_format *Integer* | No | Whether to use a `24h` format such as `13:00` (if set to `0`) when displaying time, or a `12h` format such as `1:00pm` (if set to `1`). |
| date_format *Integer* | No | Whether to use the `DD-MM-YYYY` date format (if set to `0`), or the `MM-DD-YYYY` format (if set to `1`). |
| sort_order *Integer* | No | Whether to show projects in an `oldest dates first` order (if set to `0`, or a `oldest dates last` order (if set to `1`). |
| auto_reminder *Integer* | No | The default time in minutes for the automatic reminders set, whenever a due date has been specified for a task. |
| theme *Integer* | No | The currently selected Todoist theme (between `0` and `10`). |
| weekend_start_day *Integer* | No | The day used when a user chooses to schedule a task for the 'Weekend' (between 1 and 7, where 1 is Monday and 7 is Sunday). |
| beta *Boolean* | No | Whether the user is included in the beta testing group. |
| onboarding_completed *Boolean* | No | For first-party clients usage only. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_initiated *Boolean* | No | For first-party clients usage only. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_level *String* | No | For first-party clients usage only. The onboarding level (`pro`, `intermediate`, `beginner`). This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_persona *String* | No | For first-party clients usage only. The onboarding persona (`analog`, `tasks`, `calendar`, `organic`). This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_role *String* | No | For first-party clients usage only. The onboarding role (`leader`, `founder`, `ic`). This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_skipped *Boolean* | No | For first-party clients usage only. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_started *Boolean* | No | For first-party clients usage only. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_team_mode *Boolean* | No | For first-party clients usage only. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| onboarding_use_cases *Array* | No | For first-party clients usage only. JSON array of onboarding use cases (`personal`, `work`, `education`, `teamwork`, `solo`, `teamcreator`, `simple`, `teamjoiner`). This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| completed_guide_project_id *String* | No | For first-party clients usage only. Mark a Getting Started Guide project as completed by providing its project ID. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| closed_guide_project_id *String* | No | For first-party clients usage only. Mark a Getting Started Guide project as closed (dismissed) by providing its project ID. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |
| getting_started_guide_projects *String* | No | For first-party clients usage only. JSON array of Getting Started guide projects with completion tracking. Each project contains `project_id`, `onboarding_use_case`, `completed`, and `closed` status. This attribute may be removed or changed without notice, so we strongly advise not to rely on it. |

#### Error Codes

| Error Tag | Description |
|-----------|-------------|
| `PASSWORD_REQUIRED` | The command attempted to modify `password` or `email`, but no value was provided for `current_password`. |
| `AUTHENTICATION_ERROR` | The value for `current_password` was incorrect. |
| `PASSWORD_TOO_SHORT` | The value for `password` was shorter than the minimum 8 characters. |
| `COMMON_PASSWORD` | The value for `password` was matched against a common password list and rejected. |
| `PASSWORD_CONTAINS_EMAIL` | The value for password was matched against the user's email address or a part of the address. |
| `INVALID_EMAIL` | The value for `email` was not a valid email address. |

### Update Karma Goals

> Example update karma goals request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "update_goals",
        "uuid": "b9bbeaf8-9db6-452a-a843-a192f1542892",
        "args": {"vacation_mode": 1}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"b9bbeaf8-9db6-452a-a843-a192f1542892": "ok"},
  ...
}
```

Update the karma goals of the user.

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| daily_goal *Integer* | No | The target number of tasks to complete per day. |
| weekly_goal *Integer* | No | The target number of tasks to complete per week. |
| ignore_days *Integer* | No | A list with the days of the week to ignore (`1` for `Monday` and `7` for `Sunday`). |
| vacation_mode *Integer* | No | Marks the user as being on vacation (where `1` is true and `0` is false). |
| karma_disabled *Integer* | No | Whether to disable the karma and goals measuring altogether (where `1` is true and `0` is false). |

## User Plan Limits

> An example user plan limits sync response

```json
{
    "user_plan_limits": {
        "current": {
            "plan_name": "free",
            ...details of the current user plan
        },
        "next": {
            "plan_name": "pro",
            ...details of a potential upgrade
        }
    }
}
```

The `user_plan_limits` sync resource type describes the available features and limits applicable to the current user plan. The user plan info object (detailed in the next section) returned within the `current` property shows the values that are currently applied to the user.

If there is an upgrade available, the `next` property will show the values that will apply if the user chooses to upgrade. If there is no available upgrade, the `next` value will be null.

### Properties

| Property | Description |
|----------|-------------|
| current *Object* | A user plan info object representing the available functionality and limits for the user's current plan. |
| next *Object* | A user plan info object representing the plan available for upgrade. If there is no available upgrade, this value will be null. |

### User Plan Info

> An example user plan info object

```json
{
    "activity_log": true,
    "activity_log_limit": 7,
    "advanced_permissions": true,
    "automatic_backups": false,
    "calendar_feeds": true,
    "calendar_layout": true,
    "comments": true,
    "completed_tasks": true,
    "custom_app_icon": false,
    "customization_color": false,
    "deadlines": true,
    "durations": true,
    "email_forwarding": true,
    "filters": true,
    "labels": true,
    "max_calendar_accounts": 1,
    "max_collaborators": 5,
    "max_filters": 3,
    "max_folders_per_workspace": 25,
    "max_workspace_filters": 3,
    "workspace_filters": true,
    "max_free_workspaces_created": 1,
    "max_guests_per_workspace": 25,
    "max_labels": 500,
    "max_projects": 5,
    "max_projects_joined": 500,
    "max_reminders_location": 300,
    "max_reminders_time": 700,
    "max_sections": 20,
    "max_tasks": 300,
    "max_user_templates": 100,
    "plan_name": "free",
    "reminders": false,
    "reminders_at_due": true,
    "templates": true,
    "upload_limit_mb": 5,
    "uploads": true,
    "weekly_trends": true
}
```

The user plan info object describes the availability of features and any limitations applied for a given user plan.

#### Properties

| Property | Description |
|----------|-------------|
| plan_name *String* | The name of the plan. |
| activity_log *Boolean* | Whether the user can view the [activity log](#activity). |
| activity_log_limit *Integer* | The number of days of history that will be displayed within the activity log. If there is no limit, the value will be `-1`. |
| automatic_backups *Boolean* | Whether [backups](#backups) will be automatically created for the user's account and available for download. |
| calendar_feeds *Boolean* | Whether calendar feeds can be enabled for the user's projects. |
| comments *Boolean* | Whether the user can add [comments](#comments). |
| completed_tasks *Boolean* | Whether the user can search in the completed tasks archive or access the completed tasks overview. |
| custom_app_icon *Boolean* | Whether the user can set a custom app icon on the iOS app. |
| customization_color *Boolean* | Whether the user can use special themes or other visual customization. |
| email_forwarding *Boolean* | Whether the user can add tasks or comments via [email](#emails). |
| filters *Boolean* | Whether the user can add and update [filters](#filters). |
| max_filters *Integer* | The maximum number of filters a user can have. |
| workspace_filters *Boolean* | Whether the user can add and update [workspace filters](#workspace-filters) (Business/Enterprise plans only). |
| max_workspace_filters *Integer* | The maximum number of workspace filters a user can have per workspace. |
| labels *Boolean* | Whether the user can add [labels](#labels). |
| max_labels *Integer* | The maximum number of labels a user can have. |
| reminders *Boolean* | Whether the user can add [reminders](#reminders). |
| max_reminders_location *Integer* | The maximum number of location reminders a user can have. |
| max_reminders_time *Integer* | The maximum number of time-based reminders a user can have. |
| templates *Boolean* | Whether the user can import and export [project templates](#templates). |
| uploads *Boolean* | Whether the user can [upload attachments](#uploads). |
| upload_limit_mb *Integer* | The maximum size of an individual file the user can upload. |
| weekly_trends *Boolean* | Whether the user can view [productivity stats](#productivity-stats). |
| max_projects *Integer* | The maximum number of active [projects](#projects) a user can have. |
| max_sections *Integer* | The maximum number of active [sections](#sections) a user can have. |
| max_tasks *Integer* | The maximum number of active [tasks](#tasks) a user can have. |
| max_collaborators *Integer* | The maximum number of [collaborators](#collaborators) a user can add to a project. |

## User Settings

> Example user settings object:

```json
{
    "reminder_push": true,
    "reminder_desktop": true,
    "reminder_email": true,
    "completed_sound_desktop": true,
    "completed_sound_mobile": true
}
```

*Availability of reminders functionality is dependent on the current user plan. This value is indicated by the `reminders` property of the [user plan limits](#user-plan-limits) object. These settings will have no effect if the user is not eligible for reminders.*

### Properties

| Property | Description |
|----------|-------------|
| reminder_push *Boolean* | Set to true to send reminders as push notifications. |
| reminder_desktop *Boolean* | Set to true to show reminders in desktop applications. |
| reminder_email *Boolean* | Set to true to send reminders by email. |
| completed_sound_desktop *Boolean* | Set to true to enable sound when a task is completed in Todoist desktop clients. |
| completed_sound_mobile *Boolean* | Set to true to enable sound when a task is completed in Todoist mobile clients. |

### Update User Settings

> Example update user settings request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "user_settings_update",
        "temp_id": "e24ad822-a0df-4b7d-840f-83a5424a484a",
        "uuid": "41e59a76-3430-4e44-92b9-09d114be0d49",
        "args": {"reminder_desktop": false}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"41e59a76-3430-4e44-92b9-09d114be0d49": "ok"},
  ...
}
```

Update one or more user settings.

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| reminder_push *Boolean* | No | Set to true to send reminders as push notifications. |
| reminder_desktop *Boolean* | No | Set to true to show reminders in desktop applications. |
| reminder_email *Boolean* | No | Set to true to send reminders by email. |
| completed_sound_desktop *Boolean* | No | Set to true to enable sound when a task is completed in Todoist desktop clients. |
| completed_sound_mobile *Boolean* | No | Set to true to enable sound when a task is completed in Todoist mobile clients. |

## User Productivity Stats

> Example stats object:

```json
{
  "completed_count": 123,
  "days_items": [
    {
      "date": "2025-10-17",
      "total_completed": 5
    }
  ],
  "week_items": [
    {
      "from": "2025-10-13",
      "to": "2025-10-19",
      "total_completed": 12
    }
  ]
}
```

### Properties

| Property | Description |
|----------|-------------|
| completed_count *Integer* | The total number of tasks the user has completed across all time. |
| days_items *Array* | An array containing completion statistics for today. Each item contains `date` and `total_completed`. |
| week_items *Array* | An array containing completion statistics for the current week. Each item contains `from`, `to`, and `total_completed`. |

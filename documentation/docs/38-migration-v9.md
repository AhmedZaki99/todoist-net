# Migrating from v9

The Todoist API v1 is a new API that unifies the Sync API v9 and the REST API v2. This section shows what was changed in the new version in one single place to ease the migration for current apps and integrations.

The documentation for the [Sync API v9](https://developer.todoist.com/sync/v9) and [REST API v2](https://developer.todoist.com/rest/v2) are still available for reference.

## General changes

### IDs

Since 2023, our objects returned `v2_*_id` attributes. That "v2 id" has now become the main `id`.

IDs have been opaque strings almost everywhere since the release of Sync API v9, but were still mostly numbers in that version. This version officially makes them non-number opaque strings, changing the old IDs.

The `v2_*_id` attribute is still available on Sync API v9, but was removed on the new version. We suggest relying on them for migrating stored or cached data before bumping the major version.

You can also rely on the following endpoint to translate between both ID versions: [`/api/v1/ids_mapping/<object>/<id>[,<id>]`](#tag/Ids/operation/id_mappings_api_v1_id_mappings__obj_name___obj_ids__get). It supports up to 100 IDs (of the same object) at a time.

Old IDs will NOT be accepted in this new API version for the following objects:

- notes / comments
- items / tasks
- projects
- sections
- notifications / reminders
- notifications_locations / location_reminder

Trying to use old IDs will result in an error.

### Task URLs

The previous task object included a `url` property:

```
"url": "https://todoist.com/showTask?id=<v1_id>>"
```

This has been removed. See below for information regarding the format for task URLs going forward.

Valid Task URLs are formatted as follows:

```
https://app.todoist.com/app/task/<v2_id>
```

### Lowercase endpoints

Up until now, Todoist's endpoints were case-insensitive. The Todoist API v1 will make endpoints default to lowercase (mostly snake_case) and reject mixed casing.

As an example:

https://api.todoist.com/API/v9/Sync

would before be accepted in the same way as:

https://api.todoist.com/api/v9/sync

but now, the former will return 404.

Please confirm you're only issuing requests to lowercase endpoints.

### Pagination

This version adds pagination to many endpoints.

The following endpoints are now paginated:

- `/api/v1/tasks`
- `/api/v1/tasks/filter`
- `/api/v1/labels`
- `/api/v1/labels/shared`
- `/api/v1/comments`
- `/api/v1/sections`
- `/api/v1/projects`
- `/api/v1/projects/archived`
- `/api/v1/projects/<project_id>/collaborators`
- `/api/v1/activities`

They all use cursor-based pagination. See the [Pagination guide](#tag/Pagination) for complete details.

## Previous REST API endpoints error responses

All endpoints related to `/tasks`, `/comments`, `/sections`, `/projects`, and `/labels` were returning `plain/text` error responses before the Todoist API v1. With the unification of the APIs, we have now unified the error response to return `application/json` on these endpoints.

Instead of:

```
Content-type: plain/text
Task not found
```

It will return:

```json
Content-type: application/json
{
  'error': 'Task not found',
  'error_code': 478,
  'error_extra': {'event_id': '<hash>', 'retry_after': 3},
  'error_tag': 'NOT_FOUND',
  'http_code': 404
}
```

This is the same format used in the previous Sync API, which is now the default for the new Todoist API.

## Object renames

The API kept the old names of objects for a long time to avoid breaking compatibility, but the unification of APIs was the perfect time to unformize.

The Todoist API v1 renames objects to match what users currently see in the app:

| Sync v9 / REST v2 | Todoist API v1 |
|-------------------|----------------|
| items | tasks |
| notes | comments |
| notifications | reminders |
| notifications_locations | location_reminders |

The nomenclature listed on the left in the table above, should be renamed to the associated term to the right, unless a documented exception exists.

The only exceptions for renaming are the `/sync` and `/activities` endpoints. These are currently scheduled for bigger architectural refactoring in the near future, so we will retain the the old naming conventions for now.

## URL renames

With the unification of both APIs, we took the chance to unify concepts and improve some URLs to new standards. These are the endpoint signature changes:

| Sync v9 / REST v2 | Todoist API v1 |
|-------------------|----------------|
| `/api/v9/update_notification_setting` | PUT `/api/v1/notification_setting` |
| `/api/v9/uploads/add` | POST `/api/v1/uploads` |
| `/api/v9/uploads/get` | GET `/api/v1/uploads` |
| `/api/v9/uploads/delete` | DELETE `/api/v1/uploads` |
| `/api/v9/backups/get` | GET `/api/v1/backups` |
| `/api/v9/access_tokens/revoke` | DELETE `/api/v1/access_tokens` |
| `/api/access_tokens/revoke` | DELETE `/api/v1/access_tokens` |
| `/api/access_tokens/migrate_personal_token` | POST `/api/v1/access_tokens/migrate_personal_token` |
| `/api/v9/access_tokens/migrate_personal_token` | POST `/api/v1/access_tokens/migrate_personal_token` |
| `/api/v9/archive/sections` | GET `/api/v1/sections/archived` |
| `/api/v9/quick/add` | POST `/api/v1/tasks/quick` |
| `/api/v9/emails/get_or_create` | PUT `/api/v1/emails` |
| `/api/v9/emails/disable` | DELETE `/api/v1/emails` |
| `/api/v9/get_productivity_stats` | GET `/api/v1/tasks/completed/stats` |
| `/api/v9/completed/get_stats` | GET `/api/v1/tasks/completed/stats` |
| `/api/v9/completed/get_all` | GET `/api/v1/tasks/completed` |
| `/api/v9/projects/get_archived` | GET `/api/v1/projects/archived` |
| `/api/v9/projects/join` | POST `/api/v1/projects/<project_id>/join` |
| `/api/v9/workspaces/projects/active` | GET `/api/v1/workspaces/<workspace_id>/projects/active` |
| `/api/v9/workspaces/projects/archived` | GET `/api/v1/workspaces/<workspace_id>/projects/archived` |
| `/api/v9/workspaces/update_logo` | POST `/api/v1/workspaces/logo` |
| `/api/v9/workspaces/invitations/accept` | PUT `/api/v1/workspaces/invitations/<invitation_code>/accept` |
| `/api/v9/workspaces/invitations/reject` | PUT `/api/v1/workspaces/invitations/<invitation_code>/reject` |
| `/api/v9/workspaces/joinable_workspaces` | GET `/api/v1/workspaces/joinable` |
| `/api/v9/projects/get_data` | GET `/api/v1/projects/<project_id>/full` |
| `/api/v9/templates/import_into_project` | POST `/api/v1/templates/import_into_project_from_file` |
| `/api/v9/templates/export_as_file` | GET `/api/v1/templates/file` |
| `/api/v9/templates/export_as_url` | GET `/api/v1/templates/url` |
| `/api/v9/activity/get` | GET `/api/v1/activities` |
| `/api/v9/tasks/archived/by_due_date` | GET `/api/v1/tasks/completed/by_due_date` |
| `/api/v9/tasks/completed/by_completion_date` | GET `/api/v1/tasks/completed/by_completion_date` |

## Deprecated endpoints

There are some endpoints that were previously available in the Sync or REST APIs, but were removed from the Todoist API v1. Below is a list of them and possible candidates for replacement:

| Sync v9 / REST v2 | New endpoint taking its place |
|-------------------|-------------------------------|
| `/sync/v9/archive/items_many` | `/api/v1/tasks/completed/by_completion_date` |
| `/sync/v9/archive/items` | `/api/v1/tasks/completed/by_completion_date` |
| `/sync/v9/completed/get_all` | `/api/v1/tasks/completed/by_completion_date` |
| `/sync/v9/projects/get` | `/api/v1/projects`, `/api/v1/comment` |
| `/sync/v9/items/get` | `/api/v1/tasks`, `/api/v1/comments`, `/api/v1/projects`, `/api/v1/sections` |
| `/sync/v9/projects/get_data` | `/api/v1/tasks`, `/api/v1/comments`, `/api/v1/projects`, `/api/v1/sections` |

## /sync endpoint changes

- This endpoint is one of the exceptions for [object renames](#tag/Migrating-from-v9/Object-renames), with legacy naming still in use
- `day_orders_timestamp` attribute was removed from the response on the `/sync` endpoint
- A new `full_sync_date_utc` attribute is included during initial sync, with the time when that sync data was generated. For big accounts, the data may be returned with some delay; doing an [incremental sync](#tag/Sync/Overview/Incremental-sync) afterwards is required to get up-to-date data.

### Sections

- `collapsed` attribute was renamed to `is_collapsed`

### User

- `is_biz_admin` attribute was removed

## Other endpoints

### Workspace projects

- `uncompleted_tasks_count` and `total_tasks_count` were removed from [Workspace Projects](#tag/Workspace/operation/active_projects_api_v1_workspaces__workspace_id__projects_active_get)

### /tasks

- The `comment_count` attribute was removed from the response: this applies to all `/tasks*` endpoints.
- The `filter` and `lang` parameters were removed: A new dedicated endpoint has been created specifically for filtering tasks: `/api/v1/tasks/filter`. This new endpoint allows for the same filtering capabilities but with a more specialized API surface.

### /projects

- The `comment_count` attribute was removed from the response. This applies to all `/projects*` endpoints.

### /sections

Sections used a slightly different response format in the Sync and REST APIs. The Todoist API v1 uses the format previously used by the Sync API everywhere.

### /comments

Comments a used slightly different response format in the Sync and REST APIs. The Todoist API v1 uses the format previously used by the Sync API everywhere.

## Webhooks

There are no changes specific to webhooks, but they will inherit all the other formatting and renaming changes outlined above. Developers are expected [to change the version of the webhook for their integration](https://developer.todoist.com/appconsole.html) and start accepting the new formatting once the integration is ready to handle it.

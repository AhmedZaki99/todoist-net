# Activity

# Activity

*Availability of the activity log and the duration of event storage are
dependent on the current user plan. These values are indicated by the
`activity_log` and `activity_log_limit` properties of the [user plan
limits](008-Sync-User) object.*

The activity log makes it easy to see everything that is happening across projects, items and notes.

**Note:** The activity log uses a unique page-based pagination system that differs from the standard cursor-based pagination used by most other endpoints. For information about cursor-based pagination, see the [Pagination guide](038-Pagination).

## Logged events

Currently the official Todoist clients present only the most important events
that most users may be interested in.
There are further types of events related to projects, items and notes that are
stored in our database, and can be accessed through the API.

The following events are logged for items:

- Items added
- Items updated (only changes to `content`, `description`, `due_date` and `responsible_uid`)
- Items deleted
- Items completed
- Items uncompleted

The following events are logged for notes:

- Notes added
- Notes updated (only changes to `content` or `file_name` if the former is empty)
- Notes deleted

The following events are logged for projects:

- Projects added
- Projects updated (only changes to `name`)
- Projects deleted
- Projects archived
- Projects unarchived
- Projects shared
- Projects left

## Pagination details

There are 3 parameters that control which events are returned from the activity
log. These parameters should be used in combination to get all the events one
is interested in.

### The `page` parameter

The events in the activity log are organized by week. Each week starts at
Sunday `12:00:00` (PM or noon), and ends the next Sunday at `11:59:59`, This
means that one can target a specific week, and get events from that week. The
`page` parameter specifies from which week to fetch events, and it does so in a
way that is relative to the current time.

This will be more easy to understand with the following example. Assuming it's
now `Wednesday, February 23`, then:

- `page=0`: Denotes events from the current week, that is from `Sunday, February 20`, to just now
- `page=1`: Denotes events from last week, from `February 13`, to `February 20`
- `page=2`: Denotes events from 2 weeks ago, from `February 6`, to `February 13`
- `page=3`: Denotes events from 3 weeks ago, from `January 30`, to `February 6`

And so on.

If the `page` parameter is not specified, then events from the current and last
week are returned. This is equivalent to getting events for `page=0` and
`page=1` together. So omitting the `page` parameter, and depending on which day
of the week the call is made, this should return events from `7` to `14` days
ago. This is useful in order to always fetch at least a week's events, even on
Mondays.

In the above example, this would return events from `Sunday, February 13` to
`Wednesday, February 23`, so around `10` days.

### The `limit` and `offset` parameters

Each week can have a lot of events. This is where the `limit` and `offset`
parameters come into play. Because it's not resource friendly to get hundreds
of events in one call, the events returned are limited by the default value of
the `limit` parameter, as defined above in the [Properties](034-Activity)
section. This limit can be increased, but up to a maximum value, again defined
in the [Properties](034-Activity) section.

Since not all of the events of a specific week, can be returned in a single
call, a subsequent call should use the `offset` parameter, in order to skip the
events already received.

As an example, assuming that the current week (ie. `page=0`) has `78` events,
and that a `limit=50` is used in order to get up to `50` events in each call,
one would need to do 2 calls:

1. A request with parameters `page=0`, `limit=50`, and `offset=0`, will return `50` events and also the `count=78` value
2. Since the return value `count=78` is larger than `limit=50`, an additional call is needed with the parameters `page=0`, `limit=50`, and `offset=50`, which will return the rest of the `28` events

If last week had `234` events, and assuming a `limit=100` was used:

1. A request with `page=1`, `limit=100` and `offset=0`, will return `100` events, and `count=234`
2. A second request with `page=1`, `limit=100` and `offset=100`, will return additional `100` events
3. A third request with `page=1`, `limit=100` and `offset=200`, will return the remaining `34` events

---

# Get Activity Logs

`GET` `/api/v1/activities`

Base URL: `https://api.todoist.com`

Get activity logs.

Returns a paginated list of activity events for the user. Events can be filtered by object
type (project, item, note), event type, and other criteria. Uses cursor-based pagination
for efficient navigation through results.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `object_type` | string (Object Type) or null | No | Enum: `"project"`, `"item"`, `"note"`<br><br>The type of object to filter activities by. Must be one of "project", "item" (task), or "note" (comment). When specified with `object_id`, returns activities for that specific object. |
| `object_id` | integer or string (Object Id) or null | No | The ID of the specific object to get activities for. Must be used together with `object_type`. For example, to get activities for a specific task, set `object_type=item` and `object_id=<task_id>`. |
| `parent_project_id` | integer or string (Parent Project Id) or null | No | Filter activities to only those belonging to the specified project. Returns activities for the project itself and all its tasks and comments. |
| `parent_item_id` | integer or string (Parent Item Id) or null | No | Filter activities to only those belonging to the specified task. Returns activities for the task itself and all its comments. |
| `include_parent_object` | boolean (Include Parent Object) | No | Default: `false`<br><br>When `true` and `object_id` is specified, also include activities for the parent object. For example, when filtering by a specific task, also include activities for its parent project. |
| `include_child_objects` | boolean (Include Child Objects) | No | Default: `false`<br><br>When `true` and `object_id` is specified, also include activities for all child objects. For example, when filtering by a project, also include activities for all its tasks and comments. |
| `initiator_id` | integer or array of integers (Initiator Id) or null | No | Filter activities to only those initiated by the specified user ID(s). Accepts either a single user ID or a list of user IDs. Useful for shared projects to see who made which changes. |
| `initiator_id_null` | boolean (Initiator Id Null) or null | No | Filter by whether the activity has an initiator. When `true`, returns only activities with no initiator (your own activities). When `false`, returns only activities initiated by collaborators. |
| `event_type` | string (Event Type) or null | No | Examples: `event_type=added`, `event_type=deleted`, `event_type=completed`, `event_type=updated`<br><br>Filter by a simple event type (e.g., "added", "deleted", "completed"). Returns events of this type across ALL object types that support it. For more precise filtering by both object type and event type, use `object_event_types` instead. |
| `ensure_last_state` | boolean (Ensure Last State) | No | Default: `false`<br><br>**Deprecated** - This parameter has no implementation and will be removed in a future version. |
| `object_event_types` | array of strings (Object Event Types) or null | No | Examples: `object_event_types=item:deleted`, `object_event_types=item:&object_event_types=note:added`, `object_event_types=:deleted`<br><br>Advanced filtering for specific object type and event type combinations. Format: `["object_type:event_type"]`. Examples: `["item:deleted"]` for deleted tasks, `["item:"]` for all task events, `[":deleted"]` for all delete events across all types, `["item:deleted", "note:added"]` for multiple filters. Valid event types: "added", "deleted", "updated", "completed", "uncompleted", "archived", "unarchived", "shared", "left", "reordered", "moved". This is the recommended way to filter events. |
| `annotate_notes` | boolean (Annotate Notes) | No | Default: `false`<br><br>When `true`, includes additional information about comments in the `extra_data` field, such as the content of the comment. |
| `annotate_parents` | boolean (Annotate Parents) | No | Default: `false`<br><br>When `true`, includes additional information about parent objects in the `extra_data` field, such as the name of the parent project or task. |
| `cursor` | string (Cursor) or null | No | Pagination cursor for fetching the next page of results. Use the value returned in the `next_cursor` field from a previous response.<br><br>non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 100 ]<br><br>Maximum number of activity logs to return per page. |

#### Request Sample

```bash
# Get all deleted tasks
$ curl --get https://api.todoist.com/api/v1/activities \
       -H "Authorization: Bearer $TOKEN" \
       -d object_event_types='["item:deleted"]'
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of objects (Results) | Yes |  |
| `next_cursor` | string (Next Cursor) or null | Yes |  |

#### `results[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `object_type` | string (Object Type) | Yes | The type of object this activity relates to. Valid values are `"project"`, `"item"`, or `"note"`. |
| `object_id` | string (Object Id) | Yes | The unique identifier of the object this activity relates to (project, item, or note ID). |
| `v2_object_id` | string (V2 Object Id) | Yes |  |
| `event_type` | string (Event Type) | Yes | The type of event that occurred. Valid values are: `"added"` (object was created), `"deleted"` (object was removed), `"updated"` (object was modified), `"archived"` (object was archived), `"unarchived"` (object was restored from archive), `"completed"` (task was completed), `"uncompleted"` (task was marked as incomplete), `"shared"` (project was shared with a user), `"left"` (user left a shared project). |
| `event_date` | string &lt;date-time&gt; (Event Date) | Yes | The timestamp when this activity occurred. |
| `id` | integer (Id) or null | No | The internal unique identifier for this activity log entry. |
| `parent_project_id` | string (Parent Project Id) or null | No | The ID of the project that contains the object this activity relates to. For project activities, this is the project itself. For item and note activities, this is the project containing the item or note. |
| `v2_parent_project_id` | string (V2 Parent Project Id) or null | No |  |
| `parent_item_id` | string (Parent Item Id) or null | No | For note (comment) activities, the ID of the item (task) that the note is attached to. `null` for project and item activities. |
| `v2_parent_item_id` | string (V2 Parent Item Id) or null | No |  |
| `initiator_id` | string (Initiator Id) or null | No | The ID of the user who is responsible for the event, which only makes sense in shared projects, items and notes, and is `null` for non-shared objects |
| `extra_data_id` | integer (Extra Data Id) or null | No |  |
| `extra_data` | object (Extra Data) or null | No | This object contains at least the `name` of the project, or the `content` of an item or comment, and optionally the `last_name` if a project was renamed, the `last_content` if an item or note was renamed, the `due_date` and `last_due_date` if an item's due date changed, the `responsible_uid` and `last_responsible_uid` if an item's responsible uid changed, the `description` and `last_description` if an item's description changed, and the `client` that caused the logging of the event |
| `source` | string (Source) or null | No | Enum: `"mysql"`, `"clickhouse"`<br><br>The data source where this activity log entry was retrieved from |

##### `extra_data` schema

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "results": [
    {
      "object_type": "string",
      "object_id": "string",
      "v2_object_id": "string",
      "event_type": "string",
      "event_date": "2019-08-24T14:15:22Z",
      "id": 0,
      "parent_project_id": "string",
      "v2_parent_project_id": "string",
      "parent_item_id": "string",
      "v2_parent_item_id": "string",
      "initiator_id": "string",
      "extra_data_id": 0,
      "extra_data": {},
      "source": "mysql"
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
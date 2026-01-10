# Workspace Filters

_The maximum number of saved filters are dependent on the workspace's current plan. These values are indicated by the `max_filters` property inside `limits` on the workspace object._

Workspace filters are filters shared across all members of a workspace. They provide workspace-level custom views of tasks.

## Workspace Filter Object

```json
{
    "id": "123456",
    "workspace_id": "789012",
    "name": "Team Priorities",
    "query": "priority 1 & assigned to: team",
    "color": "red",
    "item_order": 1,
    "is_deleted": false,
    "is_favorite": true,
    "is_frozen": false,
    "creator_uid": "111222",
    "updater_uid": "111222",
    "created_at": "2024-01-15T10:00:00Z",
    "updated_at": "2024-01-15T11:00:00Z"
}
```

## Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the workspace filter. |
| workspace_id *String* | The ID of the workspace this filter belongs to. |
| name *String* | The name of the workspace filter. |
| query *String* | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | Filter's order in the filter list (where the smallest value should place the filter at the top). |
| is_deleted *Boolean* | Whether the filter is marked as deleted (a `true` or `false` value). |
| is_favorite *Boolean* | Whether the filter is a favorite for the user (note: not at workspace level) (a `true` or `false` value). |
| is_frozen *Boolean* | Filters created outside plan limits (through cancellation, downgrade, etc) cannot be changed. This is a read-only attribute (a `true` or `false` value). |
| creator_uid *String* | The ID of the user who created the workspace filter. |
| updater_uid *String* | The ID of the user who last updated the workspace filter. |
| created_at *String* | The date when the workspace filter was created (RFC3339 format in UTC). |
| updated_at *String* | The date when the workspace filter was last updated (RFC3339 format in UTC). |

## Workspace Filter Commands

### Add a workspace filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_filter_add",
        "temp_id": "9204ca9f-e91c-436b-b408-ea02b3972686",
        "uuid": "0b8690b8-59e6-4d5b-9c08-6b4f1e8e0eb8",
        "args": {
            "workspace_id": "789012",
            "name": "Team Priorities",
            "query": "priority 1 & assigned to: team"
        }
    }]'
```

#### Response

```json
{
  ...
  "sync_status": {"0b8690b8-59e6-4d5b-9c08-6b4f1e8e0eb8": "ok"},
  "temp_id_mapping": {"9204ca9f-e91c-436b-b408-ea02b3972686": "123456"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| workspace_id *String or Integer* | Yes | The ID of the workspace this filter belongs to. |
| name *String* | Yes | The name of the workspace filter. |
| query *String* | Yes | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | No | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | No | Filter's order in the filter list (the smallest value should place the filter at the top). |

### Update a workspace filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_filter_update",
        "uuid": "a68b588a-44f7-434c-b3c5-a699949f755c",
        "args": {
            "id": "123456",
            "name": "High Priority Team Tasks",
            "query": "priority 1 & assigned to: team",
            "is_favorite": true
        }
    }]'
```

#### Response

```json
{
  ...
  "sync_status": {"a68b588a-44f7-434c-b3c5-a699949f755c": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the workspace filter. |
| name *String* | No | The name of the workspace filter. |
| query *String* | No | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | No | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | No | Filter's order in the filter list (where the smallest value should place the filter at the top). |
| is_favorite *Boolean* | No | Whether the filter is a favorite for the user (a `true` or `false` value). |

### Delete a workspace filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[{"type": "workspace_filter_delete", "uuid": "b8186025-66d5-4eae-b0dd-befa541abbed", "args": {"id": "123456"}}]'
```

#### Response

```json
{
  ...
  "sync_status": {"b8186025-66d5-4eae-b0dd-befa541abbed": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the workspace filter. |

### Update multiple workspace filter orders

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands=[
    {
        "type": "workspace_filter_update_orders",
        "uuid": "517560cc-f165-4ff6-947b-3adda8aef744",
        "args": {
            "id_order_mapping": {"123456":  1, "123457": 2}
        }
    }]'
```

#### Response

```json
{
  ...
  "sync_status": {"517560cc-f165-4ff6-947b-3adda8aef744": "ok"},
  ...
}
```

Update the orders of multiple workspace filters at once.

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id_order_mapping *Object* | Yes | A dictionary, where a workspace filter ID is the key, and the order its value: `filter_id: order`. |

**Key differences from personal filters:**

- Workspace filters require membership in the associated workspace
- Changes propagate to all workspace members via sync events
- Permissions are checked through workspace membership rather than user ownership

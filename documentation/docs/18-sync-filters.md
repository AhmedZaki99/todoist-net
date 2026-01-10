# Filters

_Availability of filters functionality and the maximum number of saved filters are dependent on the current user plan. These values are indicated by the `filters` and `max_filters` properties of the [user plan limits](07-sync-user.md#user-plan-limits) object._

## Personal Filters

Personal filters allow users to create custom views of their tasks based on query criteria.

### Filter Object

```json
{
    "id": "4638878",
    "name": "Important",
    "query": "priority 1",
    "color": "lime_green",
    "item_order": 3,
    "is_deleted": false,
    "is_favorite": false,
    "is_frozen": false
}
```

#### Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the filter. |
| name *String* | The name of the filter. |
| query *String* | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | Filter's order in the filter list (where the smallest value should place the filter at the top). |
| is_deleted *Boolean* | Whether the filter is marked as deleted (a `true` or `false` value). |
| is_favorite *Boolean* | Whether the filter is a favorite (a `true` or `false` value). |
| is_frozen *Boolean* | Filters from a cancelled subscription cannot be changed. This is a read-only attribute (a `true` or `false` value). |

## Personal Filter Commands

### Add a filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "filter_add",
        "temp_id": "9204ca9f-e91c-436b-b408-ea02b3972686",
        "uuid": "0b8690b8-59e6-4d5b-9c08-6b4f1e8e0eb8",
        "args": {
            "name": "Important",
            "query": "priority 1"
        }
    }]'
```

#### Response

```json
{
  ...
  "sync_status": {"0b8690b8-59e6-4d5b-9c08-6b4f1e8e0eb8": "ok"},
  "temp_id_mapping": {"9204ca9f-e91c-436b-b408-ea02b3972686": "4638878"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| name *String* | Yes | The name of the filter. |
| query *String* | Yes | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | No | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | No | Filter's order in the filter list (the smallest value should place the filter at the top). |
| is_favorite *Boolean* | No | Whether the filter is a favorite (a `true` or `false` value). |

### Update a filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "filter_update",
        "uuid": "a68b588a-44f7-434c-b3c5-a699949f755c",
        "args": {
            "id": "4638879",
            "name": "Not Important",
            "query": "priority 4"
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
| id *String* | Yes | The ID of the filter. |
| name *String* | No | The name of the filter. |
| query *String* | No | The query to search for. [Examples of searches](https://www.todoist.com/help/articles/introduction-to-filters-V98wIH) can be found in the Todoist help page. |
| color *String* | No | The color of the filter icon. Refer to the `name` column in the [Colors](38-colors.md) guide for more info. |
| item_order *Integer* | No | Filter's order in the filter list (where the smallest value should place the filter at the top). |
| is_favorite *Boolean* | No | Whether the filter is a favorite (a `true` or `false` value). |

### Delete a filter

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[{"type": "filter_delete", "uuid": "b8186025-66d5-4eae-b0dd-befa541abbed", "args": {"id": "9"}}]'
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
| id *String* | Yes | The ID of the filter. |

### Update multiple filter orders

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands=[
    {
        "type": "filter_update_orders",
        "uuid": "517560cc-f165-4ff6-947b-3adda8aef744",
        "args": {
            "id_order_mapping": {"4638878":  1, "4638879": 2}
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

Update the orders of multiple filters at once.

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id_order_mapping *Object* | Yes | A dictionary, where a filter ID is the key, and the order its value: `filter_id: order`. |

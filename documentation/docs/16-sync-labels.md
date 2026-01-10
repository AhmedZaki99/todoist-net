# Labels

> An example personal label:

```json
{
    "id": "2156154810",
    "name": "Food",
    "color": "berry_red",
    "item_order": 1,
    "is_deleted": false,
    "is_favorite": false
}
```

## Personal Labels

Personal labels are user-specific labels that can be used to organize tasks.

### Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the label. |
| name *String* | The name of the label. |
| color *String* | The color of the label icon. Refer to the `name` column in the [Colors](#tag/Colors) guide for more info. |
| item_order *Integer* | Label's order in the label list (a number, where the smallest value should place the label at the top). |
| is_deleted *Boolean* | Whether the label is marked as deleted (a `true` or `false` value). |
| is_favorite *Boolean* | Whether the label is a favorite (a `true` or `false` value). |

## Add a Personal Label

> Example add label request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "label_add",
        "temp_id": "f2f182ed-89fa-4bbb-8a42-ec6f7aa47fd0",
        "uuid": "ba204343-03a4-41ff-b964-95a102d12b35",
        "args": {"name": "Food"}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"ba204343-03a4-41ff-b964-95a102d12b35": "ok"},
  "temp_id_mapping": {"f2f182ed-89fa-4bbb-8a42-ec6f7aa47fd0": "2156154810"},
  ...
}
```

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| name *String* | Yes | The name of the label |
| color *String* | No | The color of the label icon. Refer to the `name` column in the [Colors](#tag/Colors) guide for more info. |
| item_order *Integer* | No | Label's order in the label list (a number, where the smallest value should place the label at the top). |
| is_favorite *Boolean* | No | Whether the label is a favorite (a `true` or `false` value). |

## Update a Personal Label

> Example update label request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "label_update",
        "uuid": "9c9a6e34-2382-4f43-a217-9ab017a83523",
        "args": {"id": "2156154810", "color": "berry_red"}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"9c9a6e34-2382-4f43-a217-9ab017a83523": "ok"},
  ...
}
```

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the label. |
| name *String* | No | The name of the label. |
| color *String* | No | The color of the label icon. Refer to the `name` column in the [Colors](#tag/Colors) guide for more info. |
| item_order *Integer* | No | Label's order in the label list. |
| is_favorite *Boolean* | No | Whether the label is a favorite (a `true` or `false` value). |

## Delete a Personal Label

> Example delete label request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "label_delete",
        "uuid": "aabaa5e0-b91b-439c-aa83-d1b35a5e9fb3",
        "args": {
            "id": "2156154810",
            "cascade": "all"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"aabaa5e0-b91b-439c-aa83-d1b35a5e9fb3": "ok"},
  ...
}
```

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the label. |
| cascade *String* | No | A string value, either `all` (default) or `none`. If no value or `all` is passed, the personal label will be removed and any instances of the label will also be removed from tasks (including tasks in shared projects). If `none` is passed, the personal label will be removed from the user's account but it will continue to appear on tasks as a shared label. |

## Shared Labels

Shared labels are labels that appear on tasks in shared projects. They are different from personal labels in that they don't have a dedicated label object - they only exist as label names on tasks.

## Rename a Shared Label

> Example rename shared label request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "label_rename",
        "uuid": "b863b0e5-2541-4a5a-a462-ce265ae2ff2d",
        "args": {
            "name_old": "Food",
            "name_new": "Drink"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"b863b0e5-2541-4a5a-a462-ce265ae2ff2d": "ok"},
  ...
}
```

This command enables renaming of shared labels. Any tasks containing a label matching the value of `name_old` will be updated with the new label name.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| name_old *String* | Yes | The current name of the label to modify. |
| name_new *String* | Yes | The new name for the label. |

## Delete Shared Label Occurrences

> Example delete shared label request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "label_delete_occurrences",
        "uuid": "6174264a-2842-410c-a8ff-603ec4d4736b",
        "args": {
            "name": "Shopping"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"6174264a-2842-410c-a8ff-603ec4d4736b": "ok"},
  ...
}
```

Deletes all occurrences of a shared label from any active tasks.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| name *String* | Yes | The name of the label to remove. |

## Update Multiple Label Orders

> Example update label orders request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands=[
    {
        "type": "label_update_orders",
        "uuid": "1402a911-5b7a-4beb-bb1f-fb9e1ed798fb",
        "args": {
            "id_order_mapping": {"2156154810":  1, "2156154820": 2}
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {
    "517560cc-f165-4ff6-947b-3adda8aef744": "ok"},
    ...
  },
  ...
}
```

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id_order_mapping *Object* | Yes | A dictionary, where a label `id` is the key, and the `item_order` value. |

# Comments

## Task comments

> An example task comment:

```json
{
    "id": "6X7gfQHG59V8CJJV",
    "posted_uid": "2671355",
    "item_id": "6X7gfV9G7rWm5hW8",
    "content": "Note",
    "file_attachment": {
        "file_type": "text/plain",
        "file_name": "File1.txt",
        "file_size": 1234,
        "file_url": "https://example.com/File1.txt",
        "upload_state": "completed"
    },
    "uids_to_notify": [
      "84129"
    ]
    "is_deleted": false,
    "posted_at": "2014-10-01T14:54:55.000000Z",
    "reactions": { "❤️": ["2671362"], "👍": ["2671362", "2671366"] }
}
```

*Availability of comments functionality is dependent on the current user plan. This value is indicated by the `comments` property of the [user plan limits](#tag/Sync/User/User-plan-limits) object.*

### Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the note. |
| posted_uid *String* | The ID of the user that posted the note. |
| item_id *String* | The item which the note is part of. |
| content *String* | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details). |
| uids_to_notify *Array of String* | A list of user IDs to notify. |
| is_deleted *Boolean* | Whether the note is marked as deleted (a `true` or `false` value). |
| posted_at *String* | The date when the note was posted. |
| reactions *Object* | List of emoji reactions and corresponding user IDs. |

### Add a task comment

> Example add comment request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_add",
        "temp_id": "59fe4461-287b-4b00-bacc-ee771137a732",
        "uuid": "e1005f08-acd6-4172-bab1-4338f8616e49",
        "args": {
            "item_id": "6X7gfV9G7rWm5hW8",
            "content": "Note1"
        }
    }]'

# or adding a comment with a file attached:

$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_add",
        "temp_id": "6149e689-1a54-48d6-a8c2-0ee5425554a9",
        "uuid": "554a65e9-56d9-478e-b35b-520c419e37d9",
        "args": {
            "item_id": "6X7gfV9G7rWm5hW8",
            "content": "Note1",
            "file_attachment": {
                "file_type": "image\/gif",
                "file_name": "image.gif",
                "image": "https:\/\/domain\/image.gif",
                "file_url": "https:\/\/domain\/image.gif",
                "image_width":90,
                "image_height":76,
                "file_size":7962
            }
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"e1005f08-acd6-4172-bab1-4338f8616e49": "ok"},
  "temp_id_mapping": {"59fe4461-287b-4b00-bacc-ee771137a732": "6X7gfQHG59V8CJJV"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| item_id *String* | Yes | The item which the note is part of (a unique number or temp id). |
| content *String* | Yes | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | No | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details, and learn how to upload a file in the [Uploads section](#tag/Uploads)). |
| uids_to_notify *Array of String* | No | A list of user IDs to notify. |

### Update a task comment

> Example update comment request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_update",
        "uuid": "8a38f9c5-2cd0-4da5-87c1-26d617b354e0",
        "args": {
            "id": "6X7gfQHG59V8CJJV",
            "content": "Updated Note1"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"8a38f9c5-2cd0-4da5-87c1-26d617b354e0": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the note. |
| content *String* | Yes | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | No | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details, and learn how to upload a file in the [Uploads section](#tag/Uploads)). |

### Delete a task comment

> Example delete comment request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_delete",
        "uuid": "8d666fda-73c3-4677-8b04-5d223632c24f",
        "args": {"id": "6X7hH7Gpwr3w7jX9"}
    }]'
```

> Example response:

```shell
{ ...
  "sync_status": {"8d666fda-73c3-4677-8b04-5d223632c24f": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the note. |

## Project Comments

> An example project comment:

```json
{
    "content": "Hello 2",
    "id": "6X7hH9GWrqWhF69Q",
    "is_deleted": false,
    "posted_at": "2018-08-14T10:56:50.000000Z",
    "posted_uid": "2671355",
    "project_id": "6Jf8VQXxpwv56VQ7",
    "reactions": null,
    "uids_to_notify": ["2671362"],
    "reactions": { "❤️": ["2671362"], "👍": ["2671362", "2671366"] },
    "file_attachment": {
        "file_type": "text/plain",
        "file_name": "File1.txt",
        "file_size": 1234,
        "file_url": "https://example.com/File1.txt",
        "upload_state": "completed"
    }
}
```

*Availability of comments functionality is dependent on the current user plan. This value is indicated by the `comments` property of the [user plan limits](#tag/Sync/User/User-plan-limits) object.*

### Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the note. |
| posted_uid *Integer* | The ID of the user that posted the note. |
| project_id *String* | The project which the note is part of. |
| content *String* | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details). |
| uids_to_notify *Array of String* | A list of user IDs to notify. |
| is_deleted *Boolean* | Whether the note is marked as deleted (a `true` or `false` value). |
| posted_at *String* | The date when the note was posted. |
| reactions *Object* | List of emoji reactions and corresponding user IDs. |

### Add a project comment

> Example add comment request:

```shell
curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_add",
        "temp_id": "59fe4461-287b-4b00-bacc-ee771137a732",
        "uuid": "e1005f08-acd6-4172-bab1-4338f8616e49",
        "args": {
            "project_id": "6Jf8VQXxpwv56VQ7",
            "content": "Note1"
        }
    }]'

# or adding a note with a file attached:

$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_add",
        "temp_id": "6149e689-1a54-48d6-a8c2-0ee5425554a9",
        "uuid": "554a65e9-56d9-478e-b35b-520c419e37d9",
        "args": {
            "project_id": "6Jf8VQXxpwv56VQ7",
            "content": "Note1",
            "file_attachment": {
                "file_type": "image\/gif",
                "file_name": "image.gif",
                "image": "https:\/\/domain\/image.gif",
                "file_url": "https:\/\/domain\/image.gif",
                "image_width":90,
                "image_height":76,
                "file_size":7962
            }
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"e1005f08-acd6-4172-bab1-4338f8616e49": "ok"},
  "temp_id_mapping": {"59fe4461-287b-4b00-bacc-ee771137a732": "6X7hH9GWrqWhF69Q"},
  ...
}
```

| Argument | Required | Description |
|----------|----------|-------------|
| project_id *String* | Yes | The project which the note is part of. |
| content *String* | Yes | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | No | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details, and learn how to upload a file in the [Uploads section](#tag/Uploads)). |

### Update a project comment

> Example update comment request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_update",
        "uuid": "8a38f9c5-2cd0-4da5-87c1-26d617b354e0",
        "args": {"id": "6X7hH9GWrqWhF69Q", "content": "Updated Note 1"}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"8a38f9c5-2cd0-4da5-87c1-26d617b354e0": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the note. |
| content *String* | Yes | The content of the note. This value may contain markdown-formatted text and hyperlinks. Details on markdown support can be found in the [Text Formatting article](https://www.todoist.com/help/articles/format-text-in-a-todoist-task-e5dHw9) in the Help Center. |
| file_attachment *Object* | No | A file attached to the note (see the [File Attachments](#tag/Sync/Comments/File-Attachments) section for details, and learn how to upload a file in the [Uploads section](#tag/Uploads)). |

### Delete a project comment

> Example delete comment request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "note_delete",
        "uuid": "8a38f9c5-2cd0-4da5-87c1-26d617b354e0",
        "args": {"id": "6X7hH9GWrqWhF69Q"}
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"8d666fda-73c3-4677-8b04-5d223632c24f": "ok"},
  ...
}
```

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the note. |

## File Attachments

A file attachment is represented as a JSON object. The file attachment may point to a document previously uploaded by the `uploads/add` API call, or by any external resource.

### Base file properties

| Attribute | Description |
|-----------|-------------|
| file_name *String* | The name of the file. |
| file_size *Integer* | The size of the file in bytes. |
| file_type *String* | MIME type (for example `text/plain` or `image/png`). The `file_type` is important for Todoist to render the proper preview for the given attachment. |
| file_url *String* | The URL where the file is located. Note that we don't cache the remote content on our servers and stream or expose files directly from third party resources. In particular this means that you should avoid providing links to non-encrypted (plain HTTP) resources, as exposing this files in Todoist may issue a browser warning. |
| upload_state *String* | Upload completion state (for example `pending` or `completed`). |

### Image file properties

If you upload an image, you may provide thumbnail paths to ensure Todoist handles them appropriately. Valid thumbnail information is a JSON array with URL, width in pixels, height in pixels. Ex.: ["https://example.com/img.jpg",400,300]. "Canonical" thumbnails (ones we create by `uploads/add` API call) have the following sizes: `96x96`, `288x288`, `528x528`.

| Attribute | Description |
|-----------|-------------|
| tn_l *List* | Large thumbnail (a list that contains the URL, the width and the height of the thumbnail). |
| tn_m *List* | Medium thumbnail (a list that contains the URL, the width and the height of the thumbnail). |
| tn_s *List* | Small thumbnail (a list that contains the URL, the width and the height of the thumbnail). |

### Audio file properties

If you upload an audio file, you may provide an extra attribute `file_duration` (duration of the audio file in seconds, which takes an integer value). In the web interface the file is rendered with a `<audio>` tag, so you should make sure it's supported in current web browsers. See [supported media formats](https://developer.mozilla.org/en-US/docs/Web/Media/Formats) for the reference.

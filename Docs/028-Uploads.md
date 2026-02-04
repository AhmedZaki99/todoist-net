# Uploads

# Uploads

Availability of uploads functionality and the maximum size for a file
attachment are dependent on the current user plan. These values are indicated
by the `uploads` and `upload_limit_mb` properties of the user plan limits object.

Files can be uploaded to our servers and used as [File
Attachments](013-Sync-Comments) in [comments](023-Comments).

---

# Delete Upload

`DELETE` `/api/v1/uploads`

Base URL: `https://api.todoist.com`

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `file_url` | string (File Url) | Yes |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | string (Response Delete Upload Api V1 Uploads Delete) | No |  |

#### Response Sample

```json
"ok"
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Upload File

`POST` `/api/v1/uploads`

Base URL: `https://api.todoist.com`

Upload a file to Todoist.

This endpoint accepts file uploads via two methods:

1. **Multipart form-data** (recommended):

   - Send the file as a form field with the actual file content
   - Optionally include `project_id` as another form field
   - The filename will be extracted from the Content-Disposition header

2. **Raw binary stream**:

   - Send the file content directly in the request body
   - Set `Content-Type` header to the file's MIME type
   - Set `X-File-Name` header with the desired filename
   - Optionally include `project_id` as a query parameter

The optional `project_id` parameter can be used to apply workspace-specific
upload limits when uploading to a workspace project.

## Request

### Request Body

Request Body Schema: `multipart/form-data`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `file_name` | string or null (File Name) | No |  |
| `project_id` | string or null (Project Id) | No |  |
| `file` | string &lt;binary&gt; (File) | Yes |  |

##### `file_name`

Any of:

- File Name (string)
- File Name (null)

##### `project_id`

Any of:

- Project Id (string)
- Project Id (null)

### Request Sample

```shell
$ curl https://api.todoist.com/api/v1/uploads \
       -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
       -F file=@/path/to/file.pdf
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `file_url` | string (File Url) | Yes |  |
| `file_name` | string (File Name) | Yes |  |
| `file_size` | integer (File Size) | Yes |  |
| `file_type` | string (File Type) | Yes |  |
| `resource_type` | string (Resource Type) | Yes |  |
| `image` | string or null (Image) | Yes |  |
| `image_width` | integer or null (Image Width) | Yes |  |
| `image_height` | integer or null (Image Height) | Yes |  |
| `upload_state` | string (Upload State) | No | Default: `"pending"`<br><br>Enum: `"pending"`, `"completed"` |

##### `image`

Any of:

- Image (string)
- Image (null)

##### `image_width`

Any of:

- Image Width (integer)
- Image Width (null)

##### `image_height`

Any of:

- Image Height (integer)
- Image Height (null)

#### Response Sample

```json
{
  "file_url": "string",
  "file_name": "string",
  "file_size": 0,
  "file_type": "string",
  "resource_type": "string",
  "image": "string",
  "image_width": 0,
  "image_height": 0,
  "upload_state": "pending"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
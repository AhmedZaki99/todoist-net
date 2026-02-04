# Templates

# Templates

Templates allow exporting of a project's tasks to a file or URL, and then
importing of the task list to a new or existing project.

Availability of project templates functionality is dependent on the current
user plan. This values is indicated by the `templates` property of the [user
plan limits](008-Sync-User) object.

---

# Import Into Project From File

`POST` `/api/v1/templates/import_into_project_from_file`

Base URL: `https://api.todoist.com`

A template can be imported in an existing project, or in a newly created one.

Upload a file suitable to be passed as a template to be imported into a project.

## Request

### Body

Content-Type: `multipart/form-data`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes |  |
| `file` | string <binary> (File) | Yes |  |

#### Request Sample

```bash
$ curl https://api.todoist.com/api/v1/templates/import_into_project_from_file \
       -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
       -F project_id=6XGgm6PHrGgMpCFX \
       -F file=@example.csv
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `status` | string (Status) | Yes | Value: `"ok"` |
| `template_type` | string (Template Type) | Yes |  |
| `projects` | array of objects (Projects) | Yes |  |
| `sections` | array of objects (Sections) | Yes |  |
| `tasks` | array of objects (Tasks) | Yes |  |
| `comments` | array of objects (Comments) | Yes |  |
| `project_notes` | array of objects (Project Notes) | Yes |  |

#### `projects[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `sections[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `tasks[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `comments[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `project_notes[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "status": "ok",
  "template_type": "string",
  "projects": [
    {
      "child_order": 1,
      "collapsed": false,
      "color": "lime_green",
      "id": "2203306141",
      "is_archived": true,
      "is_deleted": false,
      "name": "Shopping List",
      "view_style": "list"
    }
  ],
  "sections": [
    {}
  ],
  "tasks": [
    {}
  ],
  "comments": [
    {}
  ],
  "project_notes": [
    {}
  ]
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Create Project From File

`POST` `/api/v1/templates/create_project_from_file`

Base URL: `https://api.todoist.com`

A template can be imported in an existing project, or in a newly created one.

Upload a file suitable to be passed as a template to be imported into a project.

## Request

### Body

Content-Type: `multipart/form-data`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes |  |
| `workspace_id` | string or null (Workspace Id) | No |  |
| `file` | string <binary> (File) | Yes |  |

#### Request Sample

```bash
$ curl https://api.todoist.com/api/v1/templates/create_project_from_file \
       -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
       -F name=My Project \
       -F file=@example.csv
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `status` | string (Status) | Yes | Value: `"ok"` |
| `project_id` | string (Project Id) | Yes |  |
| `template_type` | string (Template Type) | Yes |  |
| `projects` | array of objects (Projects) | Yes |  |
| `sections` | array of objects (Sections) | Yes |  |
| `tasks` | array of objects (Tasks) | Yes |  |
| `comments` | array of objects (Comments) | Yes |  |
| `project_notes` | array of objects (Project Notes) | Yes |  |

#### `projects[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `sections[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `tasks[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `comments[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `project_notes[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "status": "ok",
  "project_id": "string",
  "template_type": "string",
  "projects": [
    {
      "child_order": 1,
      "collapsed": false,
      "color": "lime_green",
      "id": "2203306141",
      "is_archived": true,
      "is_deleted": false,
      "name": "Shopping List",
      "view_style": "list"
    }
  ],
  "sections": [
    {}
  ],
  "tasks": [
    {}
  ],
  "comments": [
    {}
  ],
  "project_notes": [
    {}
  ]
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Export As File

`GET` `/api/v1/templates/file`

Base URL: `https://api.todoist.com`

Get a template for a project as a CSV file

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes |  |
| `use_relative_dates` | boolean (Use Relative Dates) | No | Default: `true` |

## Responses

### 200 Successful Response

Response Schema: `text/plain`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | string | No |  |

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Export As Url

`GET` `/api/v1/templates/url`

Base URL: `https://api.todoist.com`

Get a template for a project as a shareable URL.

The URL can then be passed to `https://todoist.com/api/v1/import/project_from_url?t_url=<url>`
to make a shareable template.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes |  |
| `use_relative_dates` | boolean (Use Relative Dates) | No | Default: `true` |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `file_name` | string (File Name) | Yes |  |
| `file_url` | string (File Url) | Yes |  |

#### Response Sample

```json
{
  "file_name": "string",
  "file_url": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
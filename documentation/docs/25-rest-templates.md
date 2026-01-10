# Templates

Templates allow the exporting of a project's tasks to a file or URL, and then the importing of a task list to a new or existing project. The availability of the project templates functionality is dependent on the current user plan. This value is indicated by the templates property of the user plan limits object.

## Import Into Project From Template Id

`POST /api/v1/templates/import_into_project_from_template_id`

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |
| `template_id` | string | Yes | |
| `locale` | string | No | Default: "en" |

### Request sample

Content type: `application/json`

```json
{
  "project_id": "string",
  "template_id": "string",
  "locale": "en"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

Response sample:
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
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Import Into Project From File

`POST /api/v1/templates/import_into_project_from_file`

A template can be imported in an existing project, or in a newly created one.

Upload a file suitable to be passed as a template to be imported into a project.

### Request Body schema: multipart/form-data

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |
| `file` | string (binary) | Yes | |

### Request example

```bash
curl https://api.todoist.com/api/v1/templates/import_into_project_from_file \
     -H "Authorization: Bearer YOUR_TOKEN" \
     -F project_id=YOUR_PROJECT_ID \
     -F file=@example.csv
```

### Responses

**200** Successful Response

Content type: `application/json`

Response sample:
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Create Project From File

`POST /api/v1/templates/create_project_from_file`

A template can be imported in an existing project, or in a newly created one.

Upload a file suitable to be passed as a template to be imported into a project.

### Request Body schema: multipart/form-data

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | |
| `workspace_id` | string or null | No | |
| `file` | string (binary) | Yes | |

### Request example

```bash
curl https://api.todoist.com/api/v1/templates/create_project_from_file \
     -H "Authorization: Bearer YOUR_TOKEN" \
     -F name="My Project" \
     -F file=@example.csv
```

### Responses

**200** Successful Response

Content type: `application/json`

Response sample:
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Export As File

`GET /api/v1/templates/file`

Get a template for a project as a CSV file.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |
| `use_relative_dates` | boolean | No | Default: true |

### Responses

**200** Successful Response  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Export As Url

`GET /api/v1/templates/url`

Get a template for a project as a shareable URL.

The URL can then be passed to `https://todoist.com/api/v1/import/project_from_url?t_url=<url>` to make a shareable template.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |
| `use_relative_dates` | boolean | No | Default: true |

### Responses

**200** Successful Response  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

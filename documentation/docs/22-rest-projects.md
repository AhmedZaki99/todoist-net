# Projects

## Get Archived

`GET /api/v1/projects/archived`

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 50, Range: (0..200] |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects | Array of archived project objects |
| `next_cursor` | string | Cursor for pagination |

Response sample:
```json
{
  "results": [
    {
      "id": "string",
      "can_assign_tasks": true,
      "child_order": 0,
      "color": "string",
      "creator_uid": "string",
      "created_at": "string",
      "is_archived": true,
      "is_deleted": true,
      "is_favorite": true,
      "is_frozen": true,
      "name": "string",
      "updated_at": "string",
      "view_style": "string",
      "default_order": 0,
      "description": "string",
      "public_key": "string",
      "access": {
        "visibility": "restricted",
        "configuration": {}
      },
      "role": "string",
      "parent_id": "string",
      "inbox_project": true,
      "is_collapsed": true,
      "is_shared": true
    }
  ],
  "next_cursor": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Create Project

`POST /api/v1/projects`

Creates a new project and returns it.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string | Yes | |
| `description` | string or null | No | |
| `parent_id` | string, integer, or null | No | |
| `color` | string or null | No | Default: charcoal |
| `is_favorite` | boolean or null | No | Default: false |
| `view_style` | string or null | No | |
| `workspace_id` | integer or null | No | |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "description": "string",
  "parent_id": "6XGgm6PHrGgMpCFX",
  "color": "charcoal",
  "is_favorite": false,
  "view_style": "list",
  "workspace_id": 0
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `can_assign_tasks` | boolean | |
| `child_order` | integer | |
| `color` | string | |
| `creator_uid` | string | |
| `created_at` | string | |
| `is_archived` | boolean | |
| `is_deleted` | boolean | |
| `is_favorite` | boolean | |
| `is_frozen` | boolean | |
| `name` | string | |
| `updated_at` | string | |
| `view_style` | string | |
| `default_order` | integer | |
| `description` | string | |
| `public_key` | string | |
| `access` | object | Contains visibility and configuration |
| `role` | string | |
| `parent_id` | string | |
| `inbox_project` | boolean | |
| `is_collapsed` | boolean | |
| `is_shared` | boolean | |

Response sample:
```json
{
  "id": "string",
  "can_assign_tasks": true,
  "child_order": 0,
  "color": "string",
  "creator_uid": "string",
  "created_at": "string",
  "is_archived": true,
  "is_deleted": true,
  "is_favorite": true,
  "is_frozen": true,
  "name": "string",
  "updated_at": "string",
  "view_style": "string",
  "default_order": 0,
  "description": "string",
  "public_key": "string",
  "access": {
    "visibility": "restricted",
    "configuration": {}
  },
  "role": "string",
  "parent_id": "string",
  "inbox_project": true,
  "is_collapsed": true,
  "is_shared": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Projects

`GET /api/v1/projects`

Get all active user projects. This is a paginated endpoint.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 50, Range: (0..200] |

### Responses

**200** Successful Response

Content type: `application/json`
**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects | Array of archived project objects |
| `next_cursor` | string | Cursor for pagination |

Response sample:
```json
{
  "results": [
    {
      "id": "string",
      "can_assign_tasks": true,
      "child_order": 0,
      "color": "string",
      "creator_uid": "string",
      "created_at": "string",
      "is_archived": true,
      "is_deleted": true,
      "is_favorite": true,
      "is_frozen": true,
      "name": "string",
      "updated_at": "string",
      "view_style": "string",
      "default_order": 0,
      "description": "string",
      "public_key": "string",
      "access": {
        "visibility": "restricted",
        "configuration": {}
      },
      "role": "string",
      "parent_id": "string",
      "inbox_project": true,
      "is_collapsed": true,
      "is_shared": true
    }
  ],
  "next_cursor": "string"
}
```
**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects | Array of project objects |
| `next_cursor` | string | Cursor for pagination |

Response sample:
```json
{
  "results": [
    {
      "id": "string",
      "can_assign_tasks": true,
      "child_order": 0,
      "color": "string",
      "creator_uid": "string",
      "created_at": "string",
      "is_archived": true,
      "is_deleted": true,
      "is_favorite": true,
      "is_frozen": true,
      "name": "string",
      "updated_at": "string",
      "view_style": "string",
      "default_order": 0,
      "description": "string",
      "public_key": "string",
      "access": {
        "visibility": "restricted",
        "configuration": {}
      },
      "role": "string",
      "parent_id": "string",
      "inbox_project": true,
      "is_collapsed": true,
      "is_shared": true
    }
  ],
  "next_cursor": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Project Collaborators

`GET /api/v1/projects/{project_id}/collaborators`

Get all collaborators for a given project. This is a paginated endpoint.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 50, Range: (0..200] |
| `public_key` | string or null | No | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `results` | Array of objects | Array of collaborator objects |
| `next_cursor` | string | Cursor for pagination |

Response sample:
```json
{
  "results": [
    {
      "id": "string",
      "name": "string",
      "email": "string"
    }
  ],
  "next_cursor": "string"
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Unarchive Project

`POST /api/v1/projects/{project_id}/unarchive`

Marks a previously archived project as active again. For personal projects, makes the project visible for the initiating user. For workspace projects, makes the project visible for all applicable workspace users.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `can_assign_tasks` | boolean | |
| `child_order` | integer | |
| `color` | string | |
| `creator_uid` | string | |
| `created_at` | string | |
| `is_archived` | boolean | |
| `is_deleted` | boolean | |
| `is_favorite` | boolean | |
| `is_frozen` | boolean | |
| `name` | string | |
| `updated_at` | string | |
| `view_style` | string | |
| `default_order` | integer | |
| `description` | string | |
| `public_key` | string | |
| `access` | object | Contains visibility and configuration |
| `role` | string | |
| `parent_id` | string | |
| `inbox_project` | boolean | |
| `is_collapsed` | boolean | |
| `is_shared` | boolean | |

Response sample:
```json
{
  "id": "string",
  "can_assign_tasks": true,
  "child_order": 0,
  "color": "string",
  "creator_uid": "string",
  "created_at": "string",
  "is_archived": true,
  "is_deleted": true,
  "is_favorite": true,
  "is_frozen": true,
  "name": "string",
  "updated_at": "string",
  "view_style": "string",
  "default_order": 0,
  "description": "string",
  "public_key": "string",
  "access": {
    "visibility": "restricted",
    "configuration": {}
  },
  "role": "string",
  "parent_id": "string",
  "inbox_project": true,
  "is_collapsed": true,
  "is_shared": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Archive Project

`POST /api/v1/projects/{project_id}/archive`

Marks a project as archived. For personal projects, archives just for the initiating user (leaving it visible to collaborators). For workspace projects, archives for all workspace users.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `can_assign_tasks` | boolean | |
| `child_order` | integer | |
| `color` | string | |
| `creator_uid` | string | |
| `created_at` | string | |
| `is_archived` | boolean | |
| `is_deleted` | boolean | |
| `is_favorite` | boolean | |
| `is_frozen` | boolean | |
| `name` | string | |
| `updated_at` | string | |
| `view_style` | string | |
| `default_order` | integer | |
| `description` | string | |
| `public_key` | string | |
| `access` | object | Contains visibility and configuration |
| `role` | string | |
| `parent_id` | string | |
| `inbox_project` | boolean | |
| `is_collapsed` | boolean | |
| `is_shared` | boolean | |

Response sample:
```json
{
  "id": "string",
  "can_assign_tasks": true,
  "child_order": 0,
  "color": "string",
  "creator_uid": "string",
  "created_at": "string",
  "is_archived": true,
  "is_deleted": true,
  "is_favorite": true,
  "is_frozen": true,
  "name": "string",
  "updated_at": "string",
  "view_style": "string",
  "default_order": 0,
  "description": "string",
  "public_key": "string",
  "access": {
    "visibility": "restricted",
    "configuration": {}
  },
  "role": "string",
  "parent_id": "string",
  "inbox_project": true,
  "is_collapsed": true,
  "is_shared": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Permissions

`GET /api/v1/projects/permissions`

Returns a list of all available roles and the associated actions they can perform in a project.

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `project_collaborator_actions` | Array of objects | Array of project collaborator actions |
| `workspace_collaborator_actions` | Array of objects | Array of workspace collaborator actions |

Response sample:
```json
{
  "project_collaborator_actions": [
    {
      "name": "CREATOR",
      "actions": [
        {
          "name": "string"
        }
      ]
    }
  ],
  "workspace_collaborator_actions": [
    {
      "name": "CREATOR",
      "actions": [
        {
          "name": "string"
        }
      ]
    }
  ]
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Join

`POST /api/v1/projects/{project_id}/join`

Only used for workspaces. This endpoint is used to join a workspace project by a workspace_user. Only usable by a workspace user.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `project` | object | Project object |
| `items` | Array of objects | Array of items |
| `sections` | Array of objects | Array of sections |
| `project_notes` | Array of objects | Array of project notes |
| `collaborators` | Array of objects | Array of collaborators |
| `collaborator_states` | Array of objects | Array of collaborator states |
| `folder` | object or null | Folder view |
| `subprojects` | Array of objects | Array of subprojects |

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Project

`GET /api/v1/projects/{project_id}`

Returns a project object related to the given ID.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | String ID of the project |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `can_assign_tasks` | boolean | |
| `child_order` | integer | |
| `color` | string | |
| `creator_uid` | string | |
| `created_at` | string | |
| `is_archived` | boolean | |
| `is_deleted` | boolean | |
| `is_favorite` | boolean | |
| `is_frozen` | boolean | |
| `name` | string | |
| `updated_at` | string | |
| `view_style` | string | |
| `default_order` | integer | |
| `description` | string | |
| `public_key` | string | |
| `access` | object | Contains visibility and configuration |
| `role` | string | |
| `parent_id` | string | |
| `inbox_project` | boolean | |
| `is_collapsed` | boolean | |
| `is_shared` | boolean | |

Response sample:
```json
{
  "id": "string",
  "can_assign_tasks": true,
  "child_order": 0,
  "color": "string",
  "creator_uid": "string",
  "created_at": "string",
  "is_archived": true,
  "is_deleted": true,
  "is_favorite": true,
  "is_frozen": true,
  "name": "string",
  "updated_at": "string",
  "view_style": "string",
  "default_order": 0,
  "description": "string",
  "public_key": "string",
  "access": {
    "visibility": "restricted",
    "configuration": {}
  },
  "role": "string",
  "parent_id": "string",
  "inbox_project": true,
  "is_collapsed": true,
  "is_shared": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Project

`POST /api/v1/projects/{project_id}`

Updates an existing project and returns it.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | String ID of the project |

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `name` | string or null | No | |
| `color` | string or null | No | |
| `is_favorite` | boolean or null | No | |
| `parent_id` | string, integer, or null | No | |
| `child_order` | integer or null | No | |
| `is_collapsed` | boolean or null | No | |
| `view_style` | string or null | No | |
| `description` | string or null | No | |

### Request sample

Content type: `application/json`

```json
{
  "name": "string",
  "color": "string",
  "is_favorite": true,
  "parent_id": "6XGgm6PHrGgMpCFX",
  "child_order": 0,
  "is_collapsed": true,
  "view_style": "string",
  "description": "string"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | string | |
| `can_assign_tasks` | boolean | |
| `child_order` | integer | |
| `color` | string | |
| `creator_uid` | string | |
| `created_at` | string | |
| `is_archived` | boolean | |
| `is_deleted` | boolean | |
| `is_favorite` | boolean | |
| `is_frozen` | boolean | |
| `name` | string | |
| `updated_at` | string | |
| `view_style` | string | |
| `default_order` | integer | |
| `description` | string | |
| `public_key` | string | |
| `access` | object | Contains visibility and configuration |
| `role` | string | |
| `parent_id` | string | |
| `inbox_project` | boolean | |
| `is_collapsed` | boolean | |
| `is_shared` | boolean | |

Response sample:
```json
{
  "id": "string",
  "can_assign_tasks": true,
  "child_order": 0,
  "color": "string",
  "creator_uid": "string",
  "created_at": "string",
  "is_archived": true,
  "is_deleted": true,
  "is_favorite": true,
  "is_frozen": true,
  "name": "string",
  "updated_at": "string",
  "view_style": "string",
  "default_order": 0,
  "description": "string",
  "public_key": "string",
  "access": {
    "visibility": "restricted",
    "configuration": {}
  },
  "role": "string",
  "parent_id": "string",
  "inbox_project": true,
  "is_collapsed": true,
  "is_shared": true
}
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Delete Project

`DELETE /api/v1/projects/{project_id}`

Deletes a project.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `project_id` | string or integer | Yes | String ID of the project |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

Returns an empty response on success.

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

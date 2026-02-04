# Projects

# Projects

---

# Get Archived

`GET` `/api/v1/projects/archived`

Base URL: `https://api.todoist.com`

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of objects (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "results": [
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
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Search Projects

`GET` `/api/v1/projects/search`

Base URL: `https://api.todoist.com`

Search active user projects by name.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `query` | string (Query) | Yes | [ 1 .. 1024 ] characters<br><br>Examples: `query=Inbox`, `query=Client *`, `query=Q* 2026`, `query=Draft\*`<br><br>Search query to match project names. Matching is case-insensitive. Queries are matched literally unless `*` (wildcard) is included. Use `\*` for literal asterisk and `\\` for literal backslash. |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of PersonalProjectSyncView (object) or WorkspaceProjectSyncView (object) (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | object (RestrictedProjectConfiguration) | No |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Create Project

`POST` `/api/v1/projects`

Base URL: `https://api.todoist.com`

Creates a new project and returns it

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string or null (Name) | Yes | Name of the project |
| `description` | string or null (Description) | No | Description of the project |
| `parent_id` | string or integer or null (Parent Id) | No | Parent project ID. If provided, creates this project as a sub-project |
| `color` | string or integer (Color) | No | Default: `"charcoal"`<br><br>Enum: `"berry_red"`, `"red"`, `"orange"`, `"yellow"`, `"olive_green"`, `"lime_green"`, `"green"`, `"mint_green"`, `"teal"`, `"sky_blue"`, `"light_blue"`, `"blue"`, `"grape"`, `"violet"`, `"lavender"`, `"magenta"`, `"salmon"`, `"charcoal"`, `"grey"`, `"taupe"`<br><br>Color of the project icon |
| `is_favorite` | boolean (Is Favorite) | No | Default: `false`<br><br>Whether the project is a favorite for the user |
| `view_style` | string or null (View Style) | No | Enum: `"list"`, `"board"`, `"calendar"`<br><br>View style of the project |
| `workspace_id` | integer or null (Workspace Id) | No | Workspace ID. If provided, creates a workspace project instead of a personal project |

#### Request Sample

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

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | object (RestrictedProjectConfiguration) | No |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Projects

`GET` `/api/v1/projects`

Base URL: `https://api.todoist.com`

Get all active user projects.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of PersonalProjectSyncView (object) or WorkspaceProjectSyncView (object) (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | object (RestrictedProjectConfiguration) | No |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Project Collaborators

`GET` `/api/v1/projects/{project_id}/collaborators`

Base URL: `https://api.todoist.com`

Get all collaborators for a given project.

This is a paginated endpoint. See the [Pagination guide](038-Pagination) for details on using cursor-based pagination.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>String ID of the project |

### Query Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `cursor` | string or null (Cursor) | No | non-empty<br><br>`^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| `limit` | integer (Limit) | No | Default: `50`<br><br>( 0 .. 200 ]<br><br>The number of objects to return in a page |
| `public_key` | string or null (Public Key) | No |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `results` | array of objects (Results) | Yes |  |
| `next_cursor` | string or null (Next Cursor) | Yes |  |

#### `results[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes | The user's ID |
| `name` | string (Name) | Yes | The user's full name |
| `email` | string (Email) | Yes | The user's email address |

#### Response Sample

```json
{
  "results": [
    {
      "id": "0192837465",
      "name": "John Smith",
      "email": "email@example.org"
    }
  ],
  "next_cursor": "string"
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Unarchive Project

`POST` `/api/v1/projects/{project_id}/unarchive`

Base URL: `https://api.todoist.com`

Marks a previously archived project as active again. For personal projects, this
will make the project visible again for the initiating user. For workspace projects,
this will make the project visible again for all applicable workspace users.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | object (RestrictedProjectConfiguration) | No |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Archive Project

`POST` `/api/v1/projects/{project_id}/archive`

Base URL: `https://api.todoist.com`

Marks a project as archived. For personal projects, this will archive it just for
the initiating user (leaving it visible to any other collaborators). For workspace
projects, this will archive it for all workspace users, removing it from view.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>String ID of the project |

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | object (RestrictedProjectConfiguration) | No |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Permissions

`GET` `/api/v1/projects/permissions`

Base URL: `https://api.todoist.com`

Returns a list of all the available roles and the associated actions they can
perform in a project.

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_collaborator_actions` | array of objects (Project Collaborator Actions) | Yes |  |
| `workspace_collaborator_actions` | array of objects (Workspace Collaborator Actions) | Yes |  |

#### `project_collaborator_actions[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | CollaboratorRole (string) or WorkspaceRole (string) (Name) | Yes |  |
| `actions` | array of objects (Actions) | Yes |  |

##### `name`

Any of:

- CollaboratorRole
- WorkspaceRole

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | string (CollaboratorRole) | No | Enum: `"CREATOR"`, `"ADMIN"`, `"READ_WRITE"`, `"EDIT_ONLY"`, `"COMPLETE_ONLY"`<br><br>User role in the project. For personal project the role should be always `"CREATOR"`<br>User role for projects v1 maybe specified as `"CREATOR"` or `"ADMIN"`, because in the<br>past there was no permissions. |

##### `actions[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes |  |

#### `workspace_collaborator_actions[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | CollaboratorRole (string) or WorkspaceRole (string) (Name) | Yes |  |
| `actions` | array of objects (Actions) | Yes |  |

##### `name`

Any of:

- CollaboratorRole
- WorkspaceRole

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | string (CollaboratorRole) | No | Enum: `"CREATOR"`, `"ADMIN"`, `"READ_WRITE"`, `"EDIT_ONLY"`, `"COMPLETE_ONLY"`<br><br>User role in the project. For personal project the role should be always `"CREATOR"`<br>User role for projects v1 maybe specified as `"CREATOR"` or `"ADMIN"`, because in the<br>past there was no permissions. |

##### `actions[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string (Name) | Yes |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Join

`POST` `/api/v1/projects/{project_id}/join`

Base URL: `https://api.todoist.com`

*Only used for workspaces*

This endpoint is used to join a workspace project by a workspace_user and
is only usable by the workspace user.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string (Project Id) | Yes |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project` | object or null (Project) | Yes |  |
| `items` | array of objects (Items) | Yes |  |
| `sections` | array of objects (Sections) | Yes |  |
| `project_notes` | array of objects (Project Notes) | Yes |  |
| `collaborators` | array of objects (Collaborators) | Yes |  |
| `collaborator_states` | array of objects (Collaborator States) | Yes |  |
| `folder` | object or null (Folder) | Yes |  |
| `subprojects` | array of objects (Subprojects) | Yes |  |

#### `project`

Any of:

- Project (object)
- Project (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `items[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `sections[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `project_notes[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `collaborators[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `full_name` | string (Full Name) | Yes |  |
| `email` | string (Email) | Yes |  |
| `timezone` | string (Timezone) | Yes |  |
| `image_id` | string or null (Image Id) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | No |  |

#### `collaborator_states[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `folder`

Any of:

- Folder (object)
- Folder (null)

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### `subprojects[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
{
  "project": {
    "archived_timestamp": 0,
    "child_order": 4,
    "collapsed": false,
    "color": "lime_green",
    "id": "6XGgff2vcGGQCQvj",
    "is_archived": false,
    "is_deleted": false,
    "name": "Shopping List",
    "user_id": "2671355",
    "view_style": "list"
  },
  "items": [
    [
      {
        "added_at": "2016-07-19T12:50:49.000000Z",
        "assigned_by_uid": "2671355",
        "checked": false,
        "child_order": 1,
        "collapsed": false,
        "content": "Buy Milk",
        "description": "",
        "id": "6XGgfhhFwCC7G4Pc",
        "is_deleted": false,
        "labels": [],
        "notes_count": 1,
        "priority": 1,
        "project_id": "6XGgff2vcGGQCQvj",
        "section_id": "7025",
        "user_id": "2671355"
      }
    ]
  ],
  "sections": [
    [
      {
        "added_at": "2019-11-06T09:37:08.000000Z",
        "collapsed": false,
        "id": "7025",
        "is_archived": false,
        "is_deleted": false,
        "name": "Groceries",
        "project_id": "6XGgff2vcGGQCQvj",
        "section_order": 1,
        "user_id": "2671355"
      }
    ]
  ],
  "project_notes": [
    [
      {
        "content": "Things I need to buy",
        "id": "6XGgg2HHQPRr9rm7",
        "is_deleted": false,
        "posted_at": "2019-11-06T09:37:28.000000Z",
        "posted_uid": "2671355",
        "project_id": "6XGgff2vcGGQCQvj",
        "uids_to_notify": []
      }
    ]
  ],
  "collaborators": [
    {
      "id": "string",
      "full_name": "string",
      "email": "string",
      "timezone": "string",
      "image_id": "string",
      "is_deleted": true
    }
  ],
  "collaborator_states": [
    [
      {
        "is_deleted": false,
        "project_id": "6XGgff2vcGGQCQvj",
        "state": "active",
        "user_id": "2671355"
      }
    ]
  ],
  "folder": {
    "child_order": 1,
    "default_order": 0,
    "id": "12345",
    "is_deleted": false,
    "name": "Work Projects",
    "workspace_id": "67890"
  },
  "subprojects": [
    {
      "archived_timestamp": 0,
      "child_order": 4,
      "collapsed": false,
      "color": "lime_green",
      "id": "6XGgfvm7fMGJhwgm",
      "is_archived": false,
      "is_deleted": false,
      "name": "Shop name",
      "parent_id": "6XGgff2vcGGQCQvj",
      "user_id": "2671355",
      "view_style": "list"
    }
  ]
}
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Get Project

`GET` `/api/v1/projects/{project_id}`

Base URL: `https://api.todoist.com`

Returns a project object related to the given ID

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>String ID of the project |

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `hide_collaborator_details` | boolean (Hide Collaborator Details) | Yes |  |
| `disable_duplication` | boolean (Disable Duplication) | Yes |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Update Project

`POST` `/api/v1/projects/{project_id}`

Base URL: `https://api.todoist.com`

Updated a project and return it

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>( 0 .. 99999999999 )<br><br>String ID of the project |

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `name` | string or null (Name) | No | Updated project name. Passing null or omitting this field will leave it unchanged. |
| `description` | string or null (Description) | No | Updated project description. Passing null or omitting this field will leave it unchanged. |
| `color` | string or integer or null (Color) | No | Enum: `"berry_red"`, `"red"`, `"orange"`, `"yellow"`, `"olive_green"`, `"lime_green"`, `"green"`, `"mint_green"`, `"teal"`, `"sky_blue"`, `"light_blue"`, `"blue"`, `"grape"`, `"violet"`, `"lavender"`, `"magenta"`, `"salmon"`, `"charcoal"`, `"grey"`, `"taupe"`<br><br>Updated project color. Passing null or omitting this field will leave it unchanged. |
| `is_favorite` | boolean or null (Is Favorite) | No | Whether the project is marked as a favorite. Passing null or omitting this field will leave it unchanged. |
| `view_style` | string or null (View Style) | No | Enum: `"list"`, `"board"`, `"calendar"`<br><br>Updated project view style. Passing null or omitting this field will leave it unchanged. |

#### Request Sample

```json
{
  "name": "string",
  "description": "string",
  "color": "charcoal",
  "is_favorite": true,
  "view_style": "list"
}
```

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- PersonalProjectSyncView
- WorkspaceProjectSyncView

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `id` | string (Id) | Yes |  |
| `can_assign_tasks` | boolean (Can Assign Tasks) | Yes |  |
| `child_order` | integer (Child Order) | Yes |  |
| `color` | string (Color) | Yes |  |
| `creator_uid` | string or null (Creator Uid) | Yes |  |
| `created_at` | string or null (Created At) | Yes |  |
| `is_archived` | boolean (Is Archived) | Yes |  |
| `is_deleted` | boolean (Is Deleted) | Yes |  |
| `is_favorite` | boolean (Is Favorite) | Yes |  |
| `is_frozen` | boolean (Is Frozen) | Yes |  |
| `name` | string (Name) | Yes |  |
| `updated_at` | string or null (Updated At) | Yes |  |
| `view_style` | string (View Style) | Yes |  |
| `default_order` | integer (Default Order) | Yes |  |
| `description` | string (Description) | Yes |  |
| `public_key` | string (Public Key) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `role` | string or null (Role) | Yes |  |
| `parent_id` | string or null (Parent Id) | Yes |  |
| `inbox_project` | boolean (Inbox Project) | Yes |  |
| `is_collapsed` | boolean (Is Collapsed) | Yes |  |
| `is_shared` | boolean (Is Shared) | Yes |  |

##### `access`

Any of:

- ProjectAccessView
- null

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"`, `"team"`, `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

###### `configuration`

Any of:

- RestrictedProjectConfiguration
- TeamProjectConfiguration
- PublicProjectConfiguration

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `hide_collaborator_details` | boolean (Hide Collaborator Details) | Yes |  |
| `disable_duplication` | boolean (Disable Duplication) | Yes |  |

#### Response Sample

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

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Delete Project

`DELETE` `/api/v1/projects/{project_id}`

Base URL: `https://api.todoist.com`

Deletes a project and all of its sections and tasks.

## Request

### Path Parameters

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `project_id` | string or integer (Project Id) | Yes | Examples: `6XGgm6PHrGgMpCFX`<br><br>String ID of the project |

## Responses

### 200 Successful Response

Response Schema: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| *(root)* | any | No |  |

#### Response Sample

```json
null
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
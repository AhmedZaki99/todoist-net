# Workspace

# Workspace

---

# Delete Invitation

`POST` `/api/v1/workspaces/invitations/delete`

Base URL: `https://api.todoist.com`

Deletes a workspace invitation. Only admins can delete invitations.

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `workspace_id` | integer (Workspace Id) | Yes |  |
| `user_email` | string (User Email) | Yes |  |

### Example

```json
{
  "workspace_id": 0,
  "user_email": "string"
}
```

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `inviter_id` | string (Inviter Id) | Yes | ID of the user user who sent the invitation |
| `user_email` | string (User Email) | Yes | The invited person's email. |
| `workspace_id` | string (Workspace Id) | Yes | ID of the workspace |
| `role` | string (WorkspaceRole) | Yes | Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `id` | string (Id) | No | Default: `"0"`<br><br>The ID of the invitation |
| `is_existing_user` | boolean (Is Existing User) | Yes | Returns true if the user is already created in the system, and false otherwise |

### Example

```json
{
  "inviter_id": "1029384756",
  "user_email": "foo@example.com",
  "workspace_id": "12345",
  "role": "ADMIN",
  "id": "234",
  "is_existing_user": true
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# All Invitations

`GET` `/api/v1/workspaces/invitations/all`

Base URL: `https://api.todoist.com`

Return a list containing details of all pending invitation to a workspace.

This list is not paginated. All workspace members can access this list.

## Request

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer (Workspace Id) | Yes |  |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `inviter_id` | string (Inviter Id) | Yes | ID of the user user who sent the invitation |
| `user_email` | string (User Email) | Yes | The invited person's email. |
| `workspace_id` | string (Workspace Id) | Yes | ID of the workspace |
| `role` | string (WorkspaceRole) | Yes | Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `id` | string (Id) | No | Default: `"0"`<br><br>The ID of the invitation |
| `is_existing_user` | boolean (Is Existing User) | Yes | Returns true if the user is already created in the system, and false otherwise |

#### Response sample (200)

```json
[
  {
    "inviter_id": "1029384756",
    "user_email": "foo@example.com",
    "workspace_id": "12345",
    "role": "ADMIN",
    "id": "234",
    "is_existing_user": true
  }
]
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found

---

# Accept Invitation

`PUT` `/api/v1/workspaces/invitations/{invite_code}/accept`

Base URL: `https://api.todoist.com`

Accept a workspace invitation. Usable by authenticated users only.

## Request

### Path Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| invite_code | string (Invite Code) | Yes | An opaque string representing an invite code. This invitation code is sent to a user via email and is exclusive for the user. |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `inviter_id` | string (Inviter Id) | Yes | ID of the user user who sent the invitation |
| `user_email` | string (User Email) | Yes | The invited person's email. |
| `workspace_id` | string (Workspace Id) | Yes | ID of the workspace |
| `role` | string (WorkspaceRole) | Yes | Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `id` | string (Id) | No | Default: `"0"`<br><br>The ID of the invitation |
| `is_existing_user` | boolean (Is Existing User) | Yes | Returns true if the user is already created in the system, and false otherwise |

### Example

```json
{
  "inviter_id": "1029384756",
  "user_email": "foo@example.com",
  "workspace_id": "12345",
  "role": "ADMIN",
  "id": "234",
  "is_existing_user": true
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Reject Invitation

`PUT` `/api/v1/workspaces/invitations/{invite_code}/reject`

Base URL: `https://api.todoist.com`

Reject a workspace invitation. Usable by authenticated users only.

## Request

### Path Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| invite_code | string (Invite Code) | Yes | An opaque string representing an invite code. This invitation code is sent to a user via email and is exclusive for the user. |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `inviter_id` | string (Inviter Id) | Yes | ID of the user user who sent the invitation |
| `user_email` | string (User Email) | Yes | The invited person's email. |
| `workspace_id` | string (Workspace Id) | Yes | ID of the workspace |
| `role` | string (WorkspaceRole) | Yes | Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `id` | string (Id) | No | Default: `"0"`<br><br>The ID of the invitation |
| `is_existing_user` | boolean (Is Existing User) | Yes | Returns true if the user is already created in the system, and false otherwise |

### Example

```json
{
  "inviter_id": "1029384756",
  "user_email": "foo@example.com",
  "workspace_id": "12345",
  "role": "ADMIN",
  "id": "234",
  "is_existing_user": true
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Archived Projects

`GET` `/api/v1/workspaces/{workspace_id}/projects/archived`

Base URL: `https://api.todoist.com`

## Request

### Path Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer (Workspace Id) | Yes |  |

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| cursor | string or null (Cursor) | No | non-empty<br><br>Pattern: `^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| limit | integer (Limit) | No | Default: `100` |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `has_more` | boolean (Has More) | Yes |  |
| `next_cursor` | string (Next Cursor) | No |  |
| `workspace_projects` | array of objects (Workspace Projects) | Yes |  |

#### `workspace_projects[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `initiated_by_uid` | integer (Initiated By Uid) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `workspace_id` | integer (Workspace Id) | Yes |  |
| `public_access` | boolean (Public Access) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `folder_id` | integer or null (Folder Id) | No |  |
| `is_invite_only` | boolean or null (Is Invite Only) | No | Default: `false` |
| `is_archived` | boolean (Is Archived) | No | Default: `false` |
| `archived_timestamp` | integer (Archived Timestamp) | No | Default: `0` |
| `archived_date` | string &lt;date-time&gt; or null (Archived Date) | No |  |
| `is_frozen` | boolean (Is Frozen) | No | Default: `false` |
| `name` | string (Name) | No | Default: `""` |
| `color` | integer or null (Color) | No | Default: `47` |
| `view_style` | string (ProjectViewStyle) | No | Default: `"list"`<br><br>Enum: `"LIST"` `"BOARD"` `"CALENDAR"` |
| `description` | string (Description) | No | Default: `""` |
| `status` | string (ProjectStatus) | No | Default: `"IN_PROGRESS"`<br><br>Enum: `"PLANNED"` `"IN_PROGRESS"` `"PAUSED"` `"COMPLETED"` `"CANCELED"`<br><br>Project status.<br><br>At the moment, this is for workspace projects only. |
| `default_order` | integer (Default Order) | No | Default: `0` |
| `is_project_insights_enabled` | boolean (Is Project Insights Enabled) | No | Default: `false` |
| `_v1_id` | integer or null (V1 Id) | No |  |
| `_role` | CollaboratorRole (string) or Role (null) (Role) | No | Default: `0`<br><br>Enum: `"CREATOR"` `"ADMIN"` `"READ_WRITE"` `"EDIT_ONLY"` `"COMPLETE_ONLY"`<br><br>User role in the project. For personal project the role should be always `"CREATOR"`<br>User role for projects v1 maybe specified as `"CREATOR"` or `"ADMIN"`, because in the<br>past there was no permissions. |

#### `access`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"` `"team"` `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

### Example

```json
{
  "has_more": true,
  "next_cursor": "string",
  "workspace_projects": [
    {
      "initiated_by_uid": 0,
      "project_id": "string",
      "workspace_id": 0,
      "public_access": true,
      "access": {
        "visibility": "restricted",
        "configuration": {}
      },
      "folder_id": 0,
      "is_invite_only": false,
      "is_archived": false,
      "archived_timestamp": 0,
      "archived_date": "2019-08-24T14:15:22Z",
      "is_frozen": false,
      "name": "",
      "color": 47,
      "view_style": "LIST",
      "description": "",
      "status": "PLANNED",
      "default_order": 0,
      "is_project_insights_enabled": false,
      "_v1_id": 0,
      "_role": 0
    }
  ]
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Active Projects

`GET` `/api/v1/workspaces/{workspace_id}/projects/active`

Base URL: `https://api.todoist.com`

Returns all active workspace projects, including those visible but not joined by the user.

*For guests, returns all joined workspace projects only.*

## Request

### Path Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer (Workspace Id) | Yes |  |

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| cursor | string or null (Cursor) | No | non-empty<br><br>Pattern: `^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| limit | integer (Limit) | No | Default: `100` |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `has_more` | boolean (Has More) | Yes |  |
| `next_cursor` | string (Next Cursor) | No |  |
| `workspace_projects` | array of objects (Workspace Projects) | Yes |  |

#### `workspace_projects[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `initiated_by_uid` | integer (Initiated By Uid) | Yes |  |
| `project_id` | string (Project Id) | Yes |  |
| `workspace_id` | integer (Workspace Id) | Yes |  |
| `public_access` | boolean (Public Access) | Yes |  |
| `access` | ProjectAccessView (object) or null | Yes |  |
| `folder_id` | integer or null (Folder Id) | No |  |
| `is_invite_only` | boolean or null (Is Invite Only) | No | Default: `false` |
| `is_archived` | boolean (Is Archived) | No | Default: `false` |
| `archived_timestamp` | integer (Archived Timestamp) | No | Default: `0` |
| `archived_date` | string &lt;date-time&gt; or null (Archived Date) | No |  |
| `is_frozen` | boolean (Is Frozen) | No | Default: `false` |
| `name` | string (Name) | No | Default: `""` |
| `color` | integer or null (Color) | No | Default: `47` |
| `view_style` | string (ProjectViewStyle) | No | Default: `"list"`<br><br>Enum: `"LIST"` `"BOARD"` `"CALENDAR"` |
| `description` | string (Description) | No | Default: `""` |
| `status` | string (ProjectStatus) | No | Default: `"IN_PROGRESS"`<br><br>Enum: `"PLANNED"` `"IN_PROGRESS"` `"PAUSED"` `"COMPLETED"` `"CANCELED"`<br><br>Project status.<br><br>At the moment, this is for workspace projects only. |
| `default_order` | integer (Default Order) | No | Default: `0` |
| `is_project_insights_enabled` | boolean (Is Project Insights Enabled) | No | Default: `false` |
| `_v1_id` | integer or null (V1 Id) | No |  |
| `_role` | CollaboratorRole (string) or Role (null) (Role) | No | Default: `0`<br><br>Enum: `"CREATOR"` `"ADMIN"` `"READ_WRITE"` `"EDIT_ONLY"` `"COMPLETE_ONLY"`<br><br>User role in the project. For personal project the role should be always `"CREATOR"`<br>User role for projects v1 maybe specified as `"CREATOR"` or `"ADMIN"`, because in the<br>past there was no permissions. |

#### `access`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `visibility` | string (ProjectVisibility) | Yes | Enum: `"restricted"` `"team"` `"public"`<br><br>Indicates who can see a project |
| `configuration` | RestrictedProjectConfiguration (object) or TeamProjectConfiguration (object) or PublicProjectConfiguration (object) (Configuration) | Yes |  |

### Example

```json
{
  "has_more": true,
  "next_cursor": "string",
  "workspace_projects": [
    {
      "initiated_by_uid": 0,
      "project_id": "string",
      "workspace_id": 0,
      "public_access": true,
      "access": {
        "visibility": "restricted",
        "configuration": {}
      },
      "folder_id": 0,
      "is_invite_only": false,
      "is_archived": false,
      "archived_timestamp": 0,
      "archived_date": "2019-08-24T14:15:22Z",
      "is_frozen": false,
      "name": "",
      "color": 47,
      "view_style": "LIST",
      "description": "",
      "status": "PLANNED",
      "default_order": 0,
      "is_project_insights_enabled": false,
      "_v1_id": 0,
      "_role": 0
    }
  ]
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Plan Details

`GET` `/api/v1/workspaces/plan_details`

Base URL: `https://api.todoist.com`

Lists details of the workspace's current plan and usage

## Request

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer (Workspace Id) | Yes |  |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `current_member_count` | integer (Current Member Count) | Yes |  |
| `current_plan` | string (Current Plan) | Yes | Enum: `"Business"` `"Starter"` |
| `current_plan_status` | string (Current Plan Status) | Yes | Enum: `"Active"` `"Downgraded"` `"Cancelled"` `"NeverSubscribed"` |
| `downgrade_at` | string or null (Downgrade At) | Yes |  |
| `current_active_projects` | integer (Current Active Projects) | Yes |  |
| `maximum_active_projects` | integer (Maximum Active Projects) | Yes |  |
| `price_list` | array of objects (Price List) | Yes |  |
| `workspace_id` | integer (Workspace Id) | Yes |  |
| `is_trialing` | boolean (Is Trialing) | Yes |  |
| `trial_ends_at` | string or null (Trial Ends At) | Yes |  |
| `cancel_at_period_end` | boolean (Cancel At Period End) | Yes |  |
| `has_trialed` | boolean (Has Trialed) | Yes |  |
| `plan_price` | PlanPrice (object) or null | Yes |  |
| `has_billing_portal` | boolean (Has Billing Portal) | Yes |  |
| `has_billing_portal_switch_to_annual` | boolean (Has Billing Portal Switch To Annual) | Yes |  |

#### `price_list[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `billing_cycle` | string (Billing Cycle) | Yes | Enum: `"monthly"` `"yearly"` |
| `prices` | array of objects (Prices) | Yes |  |

#### `prices[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `currency` | string (Currency) | Yes |  |
| `unit_amount` | integer (Unit Amount) | Yes |  |
| `tax_behavior` | string (Tax Behavior) | Yes |  |

#### `plan_price`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `amount` | string (Amount) | Yes |  |
| `raw_amount` | number (Raw Amount) | Yes |  |
| `currency` | string (Currency) | Yes |  |
| `billing_cycle` | string or null (Billing Cycle) | Yes | Enum: `"monthly"` `"yearly"` |
| `tax_behavior` | string (Tax Behavior) | Yes | Enum: `"exclusive"` `"inclusive"` `"unspecified"` |

### Example

```json
{
  "current_member_count": 0,
  "current_plan": "Business",
  "current_plan_status": "Active",
  "downgrade_at": "string",
  "current_active_projects": 0,
  "maximum_active_projects": 0,
  "price_list": [
    {
      "billing_cycle": "monthly",
      "prices": [
        {
          "currency": "string",
          "unit_amount": 0,
          "tax_behavior": "string"
        }
      ]
    }
  ],
  "workspace_id": 0,
  "is_trialing": true,
  "trial_ends_at": "string",
  "cancel_at_period_end": true,
  "has_trialed": true,
  "plan_price": {
    "amount": "string",
    "raw_amount": 0,
    "currency": "string",
    "billing_cycle": "monthly",
    "tax_behavior": "exclusive"
  },
  "has_billing_portal": true,
  "has_billing_portal_switch_to_annual": true
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Invitations

`GET` `/api/v1/workspaces/invitations`

Base URL: `https://api.todoist.com`

Return a list of user emails who have a pending invitation to a workspace.

The list is not paginated. All workspace members can access this list.

## Request

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer (Workspace Id) | Yes |  |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

Array of `string`

Email of the invitation.

### Example

```json
[
  "example@email.org"
]
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Get Workspaces Users

`GET` `/api/v1/workspaces/users`

Base URL: `https://api.todoist.com`

Returns all workspace_users for a given workspace if workspace_id is
provided. Otherwise, returns all workspace_users for all workspaces that
the requesting user is part of.

*Not accessible by guests.*

## Request

### Query Parameters

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| workspace_id | integer or null (Workspace Id) | No |  |
| cursor | string or null (Cursor) | No | non-empty<br><br>Pattern: `^[0-9a-zA-Z_-]+\.[0-9a-zA-Z_-]+$`<br><br>An opaque string used as the cursor for pagination. Must be used with the same parameters from the previous request |
| limit | integer (Limit) | No | Default: `100` |

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `has_more` | boolean (Has More) | Yes |  |
| `next_cursor` | string (Next Cursor) | No |  |
| `workspace_users` | array of objects (Workspace Users) | Yes |  |

#### `workspace_users[]`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `workspace_id` | string (Workspace Id) | Yes |  |
| `user_email` | string (User Email) | Yes |  |
| `full_name` | string (Full Name) | Yes |  |
| `timezone` | string (Timezone) | Yes |  |
| `role` | string (WorkspaceRole) | Yes | Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `image_id` | string or null (Image Id) | No |  |
| `is_deleted` | boolean (Is Deleted) | No | Default: `false` |

### Example

```json
{
  "has_more": true,
  "next_cursor": "string",
  "workspace_users": [
    {
      "user_id": "0192837465",
      "workspace_id": "42",
      "user_email": "example@email.org",
      "full_name": "Dain Ironfoot",
      "timezone": "GMT -3:00",
      "role": "ADMIN",
      "image_id": "string",
      "is_deleted": false
    }
  ]
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Join

`POST` `/api/v1/workspaces/join`

Base URL: `https://api.todoist.com`

Join a workspace via link or via workspace ID, if the user can auto-join
the workspace by domain.

## Joining by Domain

This is possible if:

- The user is verified
- The user has a user e-mail belonging to a domain that is set
as a domain name for a workspace
- That workspace has the auto-join by domain feature enabled

## Request

### Body

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `invite_code` | string or null (Invite Code) | No |  |
| `workspace_id` | integer or null (Workspace Id) | No |  |

### Example

```json
{
  "invite_code": "string",
  "workspace_id": 0
}
```

## Responses

### `200 Successful Response`

Content-Type: `application/json`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `user_id` | string (User Id) | Yes |  |
| `workspace_id` | string (Workspace Id) | Yes |  |
| `role` | string (WorkspaceRole) | No | Default: `"MEMBER"`<br><br>Enum: `"ADMIN"` `"MEMBER"` `"GUEST"`<br><br>Role of the user inside the workspace |
| `custom_sorting_applied` | boolean (Custom Sorting Applied) | No | Default: `false` |
| `project_sort_preference` | string (ProjectSortPreference) | No | Default: `"MANUAL"`<br><br>Enum: `"MANUAL"` `"A_TO_Z"` `"Z_TO_A"`<br><br>User preference for workspace project sorting |

### Example

```json
{
  "user_id": "string",
  "workspace_id": "string",
  "role": "ADMIN",
  "custom_sorting_applied": false,
  "project_sort_preference": "MANUAL"
}
```

### `400 Bad Request`

### `401 Unauthorized`

### `403 Forbidden`

### `404 Not Found`

---

# Update Logo

`POST` `/api/v1/workspaces/logo`

Base URL: `https://api.todoist.com`

Upload an image to be used as the workspace logo. Similar to a user’s
avatar. If `delete` is set to true, it removes the logo completely and does
not return any `logo_*` attribute.

## Request

### Body

Content-Type: `multipart/form-data`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `workspace_id` | integer (Workspace Id) | Yes |  |
| `delete` | boolean (Delete) | No | Default: `false` |
| `file` | string &lt;binary&gt; (File) | Yes |  |

## Responses

### 200 Successful Response

Response Schema: `application/json`

Any of:

- Response Update Logo Api V1 Workspaces Logo Post
- Response Update Logo Api V1 Workspaces Logo Post

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `property name*` | any | No | additional property |

#### Response Sample

```json
null
```

### 400 Bad Request

### 401 Unauthorized

### 403 Forbidden

### 404 Not Found
# Workspaces

## Delete Invitation

`POST /api/v1/workspaces/invitations/delete`

Deletes a workspace invitation. Only admins can delete invitations.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |
| `user_email` | string | Yes | |

### Request Sample

Content type: `application/json`

```json
{
  "workspace_id": 0,
  "user_email": "string"
}
```

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `inviter_id` | string (required) | ID of the user who sent the invitation |
| `user_email` | string (required) | The invited person's email |
| `workspace_id` | string (required) | ID of the workspace |
| `role` | string (required) | Role of the user inside the workspace. Enum: "ADMIN", "MEMBER", "GUEST" |
| `id` | string | The ID of the invitation. Default: "0" |
| `is_existing_user` | boolean (required) | Returns true if the user is already created in the system, and false otherwise |

**Response sample:**
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## All Invitations

`GET /api/v1/workspaces/invitations/all`

Return a list containing the details of all pending invitations to a workspace. The list is not paginated. All workspace members can access this endpoint.

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array

| Field | Type | Description |
|-------|------|-------------|
| `inviter_id` | string (required) | ID of the user who sent the invitation |
| `user_email` | string (required) | The invited person's email |
| `workspace_id` | string (required) | ID of the workspace |
| `role` | string (required) | Role of the user inside the workspace. Enum: "ADMIN", "MEMBER", "GUEST" |
| `id` | string | The ID of the invitation. Default: "0" |
| `is_existing_user` | boolean (required) | Returns true if the user is already created in the system, and false otherwise |

**Response sample:**
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Accept Invitation

`PUT /api/v1/workspaces/invitations/{invite_code}/accept`

Accept a workspace invitation. Usable by authenticated users only.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `invite_code` | string | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `inviter_id` | string (required) | ID of the user who sent the invitation |
| `user_email` | string (required) | The invited person's email |
| `workspace_id` | string (required) | ID of the workspace |
| `role` | string (required) | Role of the user inside the workspace. Enum: "ADMIN", "MEMBER", "GUEST" |
| `id` | string | The ID of the invitation. Default: "0" |
| `is_existing_user` | boolean (required) | Returns true if the user is already created in the system, and false otherwise |

Response sample:
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Reject Invitation

`PUT /api/v1/workspaces/invitations/{invite_code}/reject`

Reject a workspace invitation. Usable by authenticated users only.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `invite_code` | string | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `inviter_id` | string (required) | ID of the user who sent the invitation |
| `user_email` | string (required) | The invited person's email |
| `workspace_id` | string (required) | ID of the workspace |
| `role` | string (required) | Role of the user inside the workspace. Enum: "ADMIN", "MEMBER", "GUEST" |
| `id` | string | The ID of the invitation. Default: "0" |
| `is_existing_user` | boolean (required) | Returns true if the user is already created in the system, and false otherwise |

Response sample:
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Archived Projects

`GET /api/v1/workspaces/{workspace_id}/projects/archived`

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 100 |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `has_more` | boolean (required) | |
| `next_cursor` | string | |
| `workspace_projects` | Array of objects (required) | |

**Response sample:**
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Active Projects

`GET /api/v1/workspaces/{workspace_id}/projects/active`

Returns all active workspace projects, including those visible but not joined by the user. For guests, returns all joined workspace projects only.

### path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 100 |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `has_more` | boolean (required) | |
| `next_cursor` | string | |
| `workspace_projects` | Array of objects (required) | |

**Response sample:**
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Plan Details

`GET /api/v1/workspaces/plan_details`

Lists the details of a workspace's current plan and usage.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `current_member_count` | integer (required) | |
| `current_plan` | string (required) | Enum: "Business", "Starter" |
| `current_plan_status` | string (required) | Enum: "Active", "Downgraded", "Cancelled", "NeverSubscribed" |
| `downgrade_at` | string or null (required) | |
| `current_active_projects` | integer (required) | |
| `maximum_active_projects` | integer (required) | |
| `price_list` | Array of objects (required) | |
| `workspace_id` | integer (required) | |
| `is_trialing` | boolean (required) | |
| `trial_ends_at` | string or null (required) | |
| `cancel_at_period_end` | boolean (required) | |
| `has_trialed` | boolean (required) | |
| `plan_price` | object or null (required) | |
| `has_billing_portal` | boolean (required) | |
| `has_billing_portal_switch_to_annual` | boolean (required) | |

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Invitations

`GET /api/v1/workspaces/invitations`

Return a list of user emails with a pending invitation to a workspace. The list is not paginated. All workspace members can access this endpoint.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:** Array of strings

Each string represents the email of an invitation.

**Response sample:**
```json
[
  "example@email.org"
]
```

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Get Workspaces Users

`GET /api/v1/workspaces/users`

Returns all workspace_users for a given workspace if workspace_id is provided. Otherwise returns all workspace_users for all workspaces the requesting user is part of. Not accessible by guests.

### query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer or null | No | |
| `cursor` | string or null | No | |
| `limit` | integer | No | Default: 100 |

### Responses

**200** Successful Response

Content type: `application/json`

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `has_more` | boolean (required) | |
| `next_cursor` | string | |
| `workspace_users` | Array of objects (required) | |

**Response sample:**
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

**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Join

`POST /api/v1/workspaces/join`

Join a workspace via a link or via workspace ID if user can auto-join by domain. Possible if: user is verified, user has an email belonging to a domain set as the domain name for the workspace, and workspace has auto-join by domain enabled.

### Request Body Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `invite_code` | string or null | No | |
| `workspace_id` | integer or null | No | |

### Responses

**200** Successful Response  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

## Update Logo

`POST /api/v1/workspaces/logo`

Upload an image for a workspace logo. If delete=true, removes the logo completely.

### Request Body schema: multipart/form-data

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `workspace_id` | integer | Yes | |
| `delete` | boolean | No | Default: false |
| `file` | string (binary) | Yes | |

### Responses

**200** Successful Response  
**400** Bad Request  
**401** Unauthorized  
**403** Forbidden  
**404** Not Found

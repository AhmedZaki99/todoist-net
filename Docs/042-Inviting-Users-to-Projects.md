# Inviting Users to Projects

# How to Invite a User to a Todoist Project Using curl

This guide explains how to invite a user to collaborate on a Todoist project using the unified Todoist API v1 via curl commands.

---

# Overview

The Todoist API v1 uses the `/sync` endpoint for project sharing operations. To invite a user to a project, you need to send a `share_project` command through the Sync API. This will send an invitation to the specified email address, allowing the user to join your project as a collaborator.

---

# Prerequisites

Before you can invite users to your projects, you need:

1. **API Token**: Your personal Todoist API token for authentication
   - You can find this in your Todoist settings at: [https://app.todoist.com/app/settings/integrations/developer](https://app.todoist.com/app/settings/integrations/developer)

2. **Project ID**: The ID of the project you want to share
   - You can get project IDs by listing your projects via the API: `GET /api/v1/projects`

3. **Collaborator Email**: The email address of the user you want to invite
   - The user will receive an invitation email if they don't have a Todoist account yet

---

# Basic Invitation Example

## Request

```shell
$ curl -X POST https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer YOUR_API_TOKEN" \
    -d sync_token="*" \
    -d commands='[
    {
        "type": "share_project",
        "uuid": "fe6637e3-03ce-4236-a202-8b28de2c8372",
        "args": {
            "project_id": "6H2c63wj7x9hFJfX",
            "email": "collaborator@example.com"
        }
    }]'
```

## Response

```json
{
  "sync_status": {
    "fe6637e3-03ce-4236-a202-8b28de2c8372": "ok"
  },
  "sync_token": "new_sync_token_string",
  "full_sync": false
}
```

A successful response will include `"ok"` status for your command UUID. The invited user will receive an email notification about the project invitation.

---

# Command Parameters

## Required Parameters

| Parameter | Type | Description |
| --- | --- | --- |
| `type` | String | Must be `"share_project"` |
| `uuid` | String | A unique identifier for this command (UUID v4 format recommended) |
| `args.project_id` | String | The ID of the project to share |
| `args.email` | String | The email address of the user to invite |

## Optional Parameters

| Parameter | Type | Description |
| --- | --- | --- |
| `args.role` | String | The role for the new collaborator. Valid values: `"ADMIN"`, `"READ_WRITE"`, `"EDIT_ONLY"`, `"COMPLETE_ONLY"`. Only used for workspace/team projects. If omitted, the workspace's default collaborator role will be used. |
| `temp_id` | String | Temporary ID for tracking the command (optional, UUID format) |

---

# Advanced Examples

## Inviting with a Specific Role (Workspace Projects)

For workspace/team projects, you can specify the role of the new collaborator:

```shell
$ curl -X POST https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer YOUR_API_TOKEN" \
    -d sync_token="*" \
    -d commands='[
    {
        "type": "share_project",
        "uuid": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
        "args": {
            "project_id": "6H2c63wj7x9hFJfX",
            "email": "manager@example.com",
            "role": "ADMIN"
        }
    }]'
```

## Inviting Multiple Users at Once

You can batch multiple invitation commands in a single API request:

```shell
$ curl -X POST https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer YOUR_API_TOKEN" \
    -d sync_token="*" \
    -d commands='[
    {
        "type": "share_project",
        "uuid": "11111111-1111-1111-1111-111111111111",
        "args": {
            "project_id": "6H2c63wj7x9hFJfX",
            "email": "user1@example.com"
        }
    },
    {
        "type": "share_project",
        "uuid": "22222222-2222-2222-2222-222222222222",
        "args": {
            "project_id": "6H2c63wj7x9hFJfX",
            "email": "user2@example.com"
        }
    }]'
```

---

# Collaborator Roles

When inviting users to workspace/team projects, you can assign different roles:

| Role | Description |
| --- | --- |
| `CREATOR` | Project creator with full permissions (automatically assigned to project owner) |
| `ADMIN` | Can manage project settings, invite/remove collaborators, and perform all task operations |
| `READ_WRITE` | Can create, edit, complete, and delete tasks |
| `EDIT_ONLY` | Can edit existing tasks but not create, complete, or delete them |
| `COMPLETE_ONLY` | Can only complete tasks, not create, edit, or delete them |

**Note**: For personal projects, the role parameter is not applicable. The project owner always has the `CREATOR` role.

---

# Permissions and Restrictions

## Workspace Projects

For workspace projects with `is_invite_only` set to `true`:

- Only workspace admins or project members with `ADMIN` or `CREATOR` role can invite new collaborators
- The role assigned to a new collaborator cannot be greater than the role of the person sending the invitation
- Users without proper permissions will receive a "forbidden" error

## Personal Projects

- Personal projects can be shared freely by the project owner
- No role restrictions apply to personal project sharing

---

# Generating UUIDs

Each command requires a unique UUID. You can generate UUIDs using various methods:

**Linux/Mac:**
```bash
$ uuidgen
fe6637e3-03ce-4236-a202-8b28de2c8372
```

**Python:**
```python
import uuid
print(uuid.uuid4())
```

**Online:**
Visit [https://www.uuidgenerator.net/](https://www.uuidgenerator.net/)

---

# Error Handling

## Common Errors

| Error | Cause | Solution |
| --- | --- | --- |
| `401 Unauthorized` | Invalid or missing API token | Check your API token in Authorization header |
| `403 Forbidden` | Insufficient permissions to share the project | Ensure you have ADMIN or CREATOR role for workspace projects |
| `404 Not Found` | Project ID doesn't exist or you don't have access | Verify the project ID is correct |
| `400 Bad Request` | Invalid email or malformed request | Check email format and JSON structure |

## Example Error Response

```json
{
  "sync_status": {
    "fe6637e3-03ce-4236-a202-8b28de2c8372": {
      "error": "Project not found"
    }
  }
}
```

---

# Complete Working Example

Here's a complete, copy-paste ready example:

```bash
#!/bin/bash

# Configuration
API_TOKEN="YOUR_API_TOKEN_HERE"
PROJECT_ID="YOUR_PROJECT_ID_HERE"
COLLABORATOR_EMAIL="collaborator@example.com"

# Generate a UUID for this command
UUID=$(uuidgen)

# Send the invitation
curl -X POST https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer $API_TOKEN" \
    -d sync_token="*" \
    -d commands="[{
        \"type\": \"share_project\",
        \"uuid\": \"$UUID\",
        \"args\": {
            \"project_id\": \"$PROJECT_ID\",
            \"email\": \"$COLLABORATOR_EMAIL\"
        }
    }]"
```

Save this as `invite-user.sh`, make it executable with `chmod +x invite-user.sh`, and run it with `./invite-user.sh` after updating the configuration values.

---

# Getting Your Project ID

To find your project IDs, you can use this curl command:

```bash
curl -X GET https://api.todoist.com/api/v1/projects \
    -H "Authorization: Bearer YOUR_API_TOKEN"
```

This will return a list of all your projects with their IDs.

---

# Verifying the Invitation

After sending the invitation, you can verify it was successful by:

1. **Checking the response** - Look for `"ok"` status in the `sync_status` field
2. **Listing collaborators** - Use the GET collaborators endpoint:

```bash
curl -X GET https://api.todoist.com/api/v1/projects/{project_id}/collaborators \
    -H "Authorization: Bearer YOUR_API_TOKEN"
```

3. **Checking the Todoist app** - The invited user should appear in the project's sharing settings

---

# Related Documentation

- [Projects](021-Projects.md) - Full Projects API reference
- [Sync/Sharing](009-Sync-Sharing.md) - Detailed sharing operations documentation
- [Authorization](002-Authorization.md) - How to get and use your API token
- [Sync/Overview](004-Sync-Overview.md) - Understanding the Sync API

---

# Additional Resources

- [Official Todoist API Documentation](https://developer.todoist.com/api/v1/)
- [Todoist Developer Portal](https://developer.todoist.com/)
- [API Support](mailto:submissions@doist.com)

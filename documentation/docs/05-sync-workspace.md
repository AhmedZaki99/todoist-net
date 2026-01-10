# Workspace

## Workspace Object

> An example workspace object:

```json
{
  "created_at": "2024-10-19T10:00:00.123456Z",
  "creator_id": "123",
  "current_active_projects": 5,
  "current_member_count": 2,
  "current_template_count": 0,
  "description": "Workspace description",
  "desktop_workspace_modal": null,
  "domain_discovery": false,
  "domain_name": null,
  "id": "1234",
  "invite_code": "ptoh4SICUu4",
  "is_collapsed": false,
  "is_deleted": false,
  "is_guest_allowed": true,
  "is_link_sharing_enabled": true,
  "is_trial_pending": false,
  "limits": {
    "current": {
      "admin_tools": false,
      "advanced_permissions": false,
      "automatic_backups": false,
      "calendar_layout": false,
      "durations": false,
      "max_collaborators": 250,
      "max_folders_per_workspace": 1000,
      "max_guests_per_workspace": 1000,
      "max_projects": 5,
      "max_workspace_templates": 100,
      "max_workspace_users": 1000,
      "max_workspaces": 50,
      "plan_name": "teams_workspaces_starter",
      "reminders": false,
      "reminders_at_due": true,
      "security_controls": false,
      "team_activity": true,
      "team_activity_plus": false,
      "upload_limit_mb": 5
    },
    "next": {
      "admin_tools": true,
      "advanced_permissions": true,
      "automatic_backups": true,
      "max_collaborators": 250,
      "max_guests_per_workspace": 1000,
      "max_projects": 1000,
      "max_workspace_users": 1000,
      "plan_name": "teams_workspaces_business",
      "reminders": true,
      "security_controls": true,
      "upload_limit_mb": 100
    }
  },
  "logo_big": "https://...",
  "logo_medium": "https://...",
  "logo_s640": "https://...",
  "logo_small": "https://...",
  "member_count_by_type": {
    "admin_count": 2,
    "guest_count": 0,
    "member_count": 0
  },
  "name": "Workspace name",
  "pending_invitations": [
    "pending@doist.com"
  ],
  "pending_invites_by_type": {
    "admin_count": 1,
    "guest_count": 0,
    "member_count": 0
  },
  "plan": "STARTER",
  "properties": {},
  "restrict_email_domains": false,
  "role": "MEMBER"
}
```

### Properties

| Property | Description |
|----------|-------------|
| id *String* | The ID of the workspace. |
| name *String* | The name of the workspace (up to 255 characters). |
| description *String* | The description of the workspace. |
| plan *String* | The subscription plan this workspace is currently on, either `STARTER` or `BUSINESS`. |
| is_link_sharing_enabled *Boolean* | True if users are allowed to join the workspace using an invitation link. Default value is True. *For guests, this field will be set to `null` as guests are not allowed to have access to this field.* |
| is_guest_allowed *Boolean* | True if users from outside the workspace are allowed to join or be invited to workspace projects. Default value is True. |
| invite_code *String* | The invitation code used to generate an invitation link. If `is_link_sharing_enabled` is True, anyone can join the workspace using this code. *For guests, this field will be set to `null` as guests are not allowed to have access to this field.* |
| role *String* | The role of the requesting user in this workspace. Possible values are: `ADMIN`, `MEMBER` or `GUEST`. A guest is someone who is a collaborator of a workspace project, without being an actual member of the workspace. This field can be `null` if the requesting user is not part of the workspace. For example, when receiving the workspace deletion related sync update when a user leaves or is removed from a workspace. |
| logo_big *String* | The URL for the big workspace logo image. |
| logo_medium *String* | The URL for the medium workspace logo image. |
| logo_small *String* | The URL for the small workspace logo image. |
| logo_s640 *String* | The URL for the square 640px workspace logo image. |
| limits *Object* | A list of restrictions for the workspace based on it's current plan, denoting what features are enabled and limits are imposed. |
| creator_id *String* | The ID of the user who created the workspace. |
| created_at *String* | The date when the workspace was created. |
| is_deleted *Boolean* | Whether the workspace is marked as deleted. |
| is_collapsed *Boolean* | The collapsed state of the workspace for the current user. |
| is_link_sharing_enabled *Boolean* | Indicates if users are allowed to join the workspace using an invitation link. Default value is True. |
| is_guest_allowed *Boolean* | Indicates if users from outside the workspace are allowed to join or be invited to workspace projects. Default value is True. |
| domain_name *String* | The domain name of the workspace. |
| domain_discovery *Boolean* | True if users with e-mail addresses in the workspace domain can join the workspace without an invitation. |
| restrict_email_domains *Boolean* | True if only users with e-mail addresses in the workspace domain can join the workspace. |
| properties *Object* | Configuration properties for the workspace. See [Workspace Properties](#workspace-properties) for detailed structure. |
| default_collaborators *Object* | Default collaborators for new projects. Object with `user_ids` (array of integers) and `predefined_group_ids` (array of strings). If not provided or set to `null` then by default all workspace members are added as the default collaborators. |

## Workspace Properties

The `properties` object contains configuration options for the workspace such as industry and department information.

| Property | Description |
|----------|-------------|
| industry *String* | The industry of the workspace organization. |
| department *String* | The department within the organization. |

## Commands

### Add a Workspace

> Example add workspace request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_add",
        "temp_id": "4ff1e388-5ca6-453a-b0e8-662ebf373b6b",
        "uuid": "32774db9-a1da-4550-8d9d-910372124fa4",
        "args": {
            "name": "ACME Corp"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"32774db9-a1da-4550-8d9d-910372124fa4": "ok"},
  "temp_id_mapping": {"4ff1e388-5ca6-453a-b0e8-662ebf373b6b": "6X6WMMqgq2PWxjCX"},
  ...
}
```

Create a new workspace.

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| name *String* | Yes | The name of the workspace. |
| description *String* | No | The description of the workspace (up to 1024 characters). |
| is_link_sharing_enabled *Boolean* | No | Indicates if users are allowed to join the workspace using an invitation link. Default value is True. |
| is_guest_allowed *Boolean* | No | Indicates if users from outside the workspace are allowed to join or be invited to workspace projects. Default value is True. |
| domain_name *String* | No | The domain name of the workspace. |
| domain_discovery *Boolean* | No | True if users with e-mail addresses in the workspace domain can join the workspace without an invitation. |
| restrict_email_domains *Boolean* | No | True if only users with e-mail addresses in the workspace domain can join the workspace. |
| properties *Object* | No | Configuration properties for the workspace. See [Workspace Properties](#workspace-properties) for detailed structure. |
| default_collaborators *Object* | No | Default collaborators for new projects. Object with `user_ids` (array of integers) and `predefined_group_ids` (array of strings). If not provided or set to `null` then by default all workspace members are added as the default collaborators. |

### Update a Workspace

> Example update workspace request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_update",
        "temp_id": "4ff1e388-5ca6-453a-b0e8-662ebf373b6b",
        "uuid": "32774db9-a1da-4550-8d9d-910372124fa4",
        "args": {
            "id": "12345",
            "description": "Where magic happens"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"32774db9-a1da-4550-8d9d-910372124fa4": "ok"},
  "temp_id_mapping": {"4ff1e388-5ca6-453a-b0e8-662ebf373b6b": "6X6WMMqgq2PWxjCX"},
  ...
}
```

Update an existing workspace.

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | Real or temp ID of the workspace |
| name *String* | No | The name of the workspace. |
| description *String* | No | The description of the workspace (up to 1024 characters). |
| is_collapsed *Boolean* | No | The collapsed state of the workspace for the current user |
| is_link_sharing_enabled *Boolean* | No | Indicates if users are allowed to join the workspace using an invitation link. |
| is_guest_allowed *Boolean* | No | Indicates if users from outside the workspace are allowed to join or be invited to workspace projects. Default value is True. |
| invite_code *String* | No | Regenerate the invite_code for the workspace. Any non-empty string value will regenerate a new code, the provided value with this argument is not significant, only an indication to regenerate the code. |
| domain_name *String* | No | The domain name of the workspace. |
| domain_discovery *Boolean* | No | True if users with e-mail addresses in the workspace domain can join the workspace without an invitation. |
| restrict_email_domains *Boolean* | No | True if only users with e-mail addresses in the workspace domain can join the workspace. |
| properties *Object* | No | Configuration properties for the workspace. See [Workspace Properties](#workspace-properties) for detailed structure. |
| default_collaborators *Object* | No | Default collaborators for new projects. Object with `user_ids` (array of integers) and `predefined_group_ids` (array of strings). If not provided or set to `null` then by default all workspace members are added as the default collaborators. |

### Leave a Workspace

> Example leave workspace request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_leave",
        "temp_id": "4ff1e388-5ca6-453a-b0e8-662ebf373b6b",
        "uuid": "32774db9-a1da-4550-8d9d-910372124fa4",
        "args": {
            "id": "6X6WMMqgq2PWxjCX",
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"32774db9-a1da-4550-8d9d-910372124fa4": "ok"},
  ...
}
```

Remove self from a workspace. The user is also removed from any workspace project previously joined.

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | Real or temp ID of the workspace |

*All workspace_users can leave a workspace by themselves.*

### Delete a Workspace

> Example delete workspace request:

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "workspace_delete",
        "temp_id": "4ff1e388-5ca6-453a-b0e8-662ebf373b6b",
        "uuid": "32774db9-a1da-4550-8d9d-910372124fa4",
        "args": {
            "id": "6X6WMRPC43g2gHVx"
        }
    }]'
```

> Example response:

```shell
{
  ...
  "sync_status": {"32774db9-a1da-4550-8d9d-910372124fa4": "ok"},
  ...
}
```

Delete an existing workspace.

*This command is only usable by workspace admins. Other users will get a "forbidden" error.*

#### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the workspace |

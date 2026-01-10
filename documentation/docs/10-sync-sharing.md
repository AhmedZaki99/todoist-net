# Sync Sharing

Projects can be shared with other users, who are then referred to as collaborators. This section describes the different commands that are related to sharing.

## Collaborators

There are two types of objects to get information about a user's collaborators, and their participation in shared projects: `collaborators` and `collaborator_states`.

Every user who shares at least one project with another user, has a collaborators record in the API response. The record contains a restricted subset of user-specific properties.

### Collaborator Object

Example collaborator object:

```json
{
    "id": "2671362",
    "email": "you@example.com",
    "full_name": "Example User",
    "timezone": "GMT +3:00",
    "image_id": null
}
```

### Properties

| Property | Description |
|----------|-------------|
| id *String* | The user ID of the collaborator |
| email *String* | The email of the collaborator |
| full_name *String* | The full name of the collaborator |
| timezone *String* | The timezone of the collaborator |
| image_id *String* | The image ID for the collaborator's avatar, which can be used to get an avatar from a specific URL. Specifically the `https://dcff1xvirvpfp.cloudfront.net/<image_id>_big.jpg` can be used for a big (195x195 pixels) avatar, `https://dcff1xvirvpfp.cloudfront.net/<image_id>_medium.jpg` for a medium (60x60 pixels) avatar, and `https://dcff1xvirvpfp.cloudfront.net/<image_id>_small.jpg` for a small (35x35 pixels) avatar |

Partial sync returns updated collaborator objects for users that have changed their attributes, such as their name or email.

## Collaborator States

The list of collaborators don't contain any information on how users are connected to shared projects. To provide information about these connections, the `collaborator_states` field should be used. Every collaborator state record is a mere "user to shared project" mapping.

### Collaborator State Object

Example collaborator state:

```json
{
    "project_id": "6H2c63wj7x9hFJfX",
    "user_id": "2671362",
    "state": "active",
    "is_deleted": false,
    "role": "READ_WRITE"
}
```

### Properties

| Property | Description |
|----------|-------------|
| project_id *String* | The shared project ID of the user |
| user_id *String* | The user ID of the collaborator |
| state *String* | The status of the collaborator state, either `active` or `invited` |
| is_deleted *Boolean* | Set to `true` when the collaborator leaves the shared project |
| role | The role of the collaborator in the project. *Only available for teams* |

## Share a Project

Share a project with another user.

*When sharing a teams project*

Workspace projects with `is_invite_only` set to true can only be shared by workspace admins, or by project members with `ADMIN` or `CREATOR` role. Other users will get a "forbidden" error. The role for the new collaborator cannot be greater than the role of the person sharing the project.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| project_id *String* | Yes | The project to be shared |
| email *String* | Yes | The email of the user |
| role *String* | No | The role of the collaborator in the project (ADMIN, CONTRIBUTOR, READ_ONLY). *Only available for teams* |

### Request Example

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "share_project",
        "temp_id": "854be9cd-965f-4ddd-a07e-6a1d4a6e6f7a",
        "uuid": "fe6637e3-03ce-4236-a202-8b28de2c8372",
        "args": {
            "project_id": "6H2c63wj7x9hFJfX",
            "email": "you@example.com"
        }
    }]'
```

### Response Example

```json
{
  ...
  "sync_status": {"fe6637e3-03ce-4236-a202-8b28de2c8372": "ok"},
  ...
}
```

## Delete a Collaborator

Delete a collaborator from a shared project.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| project_id *String* | Yes | The project the collaborator should be removed from |
| email *String* | Yes | The email of the collaborator |

## Accept an Invitation

Accept an invitation to join a shared project.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| invitation_id *Integer* | Yes | The invitation ID |
| invitation_secret *String* | Yes | The secret fetched from the live notification object |

## Reject an Invitation

Reject an invitation to join a shared project.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| invitation_id *Integer* | Yes | The invitation ID |
| invitation_secret *String* | Yes | The secret fetched from the live notification object |

## Delete an Invitation

Delete an invitation to join a shared project. This should be used to delete invitations that you've sent to other users.

### Command Arguments

| Argument | Required | Description |
|----------|----------|-------------|
| invitation_id *Integer* | Yes | The invitation ID |

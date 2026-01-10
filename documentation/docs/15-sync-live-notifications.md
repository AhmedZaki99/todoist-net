# Live Notifications

> Examples of live notifications:

```json
{
    "created_at": "2021-05-10T09:59:36.000000Z",
    "is_unread": false,
    "from_uid": "2671362",
    "id": "1",
    "invitation_id": "456",
    "invitation_secret": "abcdefghijklmno",
    "notification_key": "notification_123",
    "notification_type": "share_invitation_sent",
    "seq_no": 12345567890,
    "state": "accepted"
}

{
    "created_at": "2021-05-10T09:59:36.000000Z",
    "is_unread": false,
    "from_uid": "2671362",
    "id": "2",
    "invitation_id": "456",
    "notification_key": "notification_123",
    "notification_type": "share_invitation_accepted",
    "project_id": "6Jf8VQXxpwv56VQ7",
    "seq_no": 1234567890
}

{
    "assigned_by_uid": "2671362",
    "created_at": "2021-05-10T09:59:36.000000Z",
    "is_unread": false,
    "from_uid": "2671362",
    "id": "6",
    "item_content": "NewTask",
    "item_id": "6X7gfV9G7rWm5hW8",
    "notification_key": "notification_123",
    "notification_type": "item_assigned",
    "project_id": "6Jf8VQXxpwv56VQ7",
    "responsible_uid": "2671355",
    "seq_no": 1234567890
}
```

## Types

This is the list of notifications which can be issued by the system:

| Type | Description |
|------|-------------|
| share_invitation_sent | Sent to the sharing invitation receiver. |
| share_invitation_accepted | Sent to the sharing invitation sender, when the receiver accepts the invitation. |
| share_invitation_rejected | Sent to the sharing invitation sender, when the receiver rejects the invitation. |
| user_left_project | Sent to everyone when somebody leaves the project. |
| user_removed_from_project | Sent to everyone, when a person removes somebody from the project. |
| item_assigned | Sent to user who is responsible for the task. Optionally it's also sent to the user who created the task initially, if the assigner and the task creator is not the same person. |
| item_completed | Sent to the user who assigned the task when the task is completed. Optionally it's also sent to the user who is responsible for this task, if the responsible user and the user who completed the task is not the same person. |
| item_uncompleted | Sent to the user who assigned the task when the task is uncompleted. Optionally it's also sent to the user who is responsible for this task, if the responsible user and the user who completed the task is not the same person. |
| note_added | Sent to all members of the shared project, whenever someone adds a note to the task. |
| workspace_invitation_created | Sent to the invitee (if existing user) when invited to a workspace. |
| workspace_invitation_accepted | Sent to the inviter, and admins of paid workspaces, when the workspace invitation is accepted. |
| workspace_invitation_rejected | Sent to the inviter when the workspace invitation is declined. |
| project_archived | Sent to project collaborators when the project is archived. *Only for workspace projects at the moment.* |
| removed_from_workspace | Sent to removed user when removed from a workspace. |
| workspace_deleted | Sent to every workspace admin, member and guest. |
| teams_workspace_upgraded | Sent to workspace admins and members when workspace is upgraded to paid plan (access to paid features). |
| teams_workspace_canceled | Sent to workspace admins and members when workspace is back on Starter plan (no access to paid features). |
| teams_workspace_payment_failed | Sent to the workspace billing admin on the web when a payment failed since it requires their action. |
| karma_level | Sent when a new karma level is reached |
| share_invitation_blocked_by_project_limit | Sent when the invitation is blocked because the user reached the project limits |
| workspace_user_joined_by_domain | Sent when a user join a new workspace by domain |

## Common Properties

Some properties are common for all types of notifications, whereas some others depend on the notification type.

Every live notification has the following properties:

| Property | Description |
|----------|-------------|
| id *String* | The ID of the live notification. |
| created_at *String* | Live notification creation date. |
| from_uid *String* | The ID of the user who initiated this live notification. |
| notification_key *String* | Unique notification key. |
| notification_type *String* | Type of notification. Different notification type define different extra fields which are described below. |
| seq_no *Integer* | Notification sequence number. |
| is_unread *Boolean* | Whether the notification is unread. |

## Mark as Read

### live_notifications_mark_read

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "live_notifications_mark_read",
        "uuid": "fe244328-1262-4d90-9ba8-20ca238f33f0",
        "args": {"id": "1234567890"}
    }]'
```

Response:

```shell
{
  ...
  "sync_status": {"fe244328-1262-4d90-9ba8-20ca238f33f0": "ok"},
  ...
}
```

Mark a live notification as read.

#### Command arguments

| Argument | Required | Description |
|----------|----------|-------------|
| id *String* | Yes | The ID of the notification to mark as read. |

### live_notifications_mark_read_all

```shell
$ curl https://api.todoist.com/api/v1/sync \
    -H "Authorization: Bearer 0123456789abcdef0123456789abcdef01234567" \
    -d commands='[
    {
        "type": "live_notifications_mark_read_all",
        "uuid": "15c4338f-05e0-4e5b-8a90-3d4bfb98885b",
        "args": {}
    }]'
```

Response:

```shell
{
  ...
  "sync_status": {"15c4338f-05e0-4e5b-8a90-3d4bfb98885b": "ok"},
  ...
}
```

Mark all live notifications as read.

#### Command arguments

This command takes no arguments.

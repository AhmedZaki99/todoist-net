# Webhooks

The Todoist Webhooks API allows applications to receive real-time notification (in the form of HTTP POST payload) on the subscribed user events. Notice that once you have a webhook setup, you will start receiving webhook events from **all your app users** immediately.

## Important Considerations

- For security reasons, Todoist only allows webhook urls that have HTTPS enabled and no ports specified in the url.
  - Allowed webhook url:
    - `https://nice.integration.com`
  - Disallowed webhook url:
    - `http://evil.integration.com`
    - `https://bad.integration.com:5980`

- Due to the nature of network requests, your application should assume webhook requests could arrive delayed, out of order, or could even fail to arrive at all; webhooks should be used only as notifications and not as primary Todoist data sources (make sure your application could still work when webhook is not available).

## Webhook Activation & Personal Use

The webhook for a specific user is activated when that user completes the [OAuth flow](#tag/Authorization/OAuth) of the app that declares the webhook.

**Todoist webhooks don't fire by default for the user that has created the Todoist app, which is frequently the desired state for the personal use of webhooks.**

To activate webhooks for personal use, you need to complete the OAuth process with your account. You can do this without code by manually executing the OAuth flow in two steps.

1. Performing the [authorization request](#tag/Authorization/OAuth) in the browser and capturing the `code` via the browser's developer tools.
2. Performing the [token exchange request](#tag/Authorization/OAuth) through a tool like [Postman](https://www.postman.com/) and reading the `access_token` from the response. *Note that you can't make this request via the browser as it needs to be a POST request.*

## Configuration

Before you can start receiving webhook event notifications, you must first have your webhook configured at the App Management Console.

### Events

Here is a list of events that you could subscribe to, and they are configured at the [App Management Console](https://app.todoist.com/app/settings/integrations/app-management).

| Event Name | Description | Event Data |
|------------|-------------|------------|
| item:added | A task was added | The new [Task](#tag/Sync/Tasks). |
| item:updated | A task was updated | The updated [Task](#tag/Sync/Tasks). |
| item:deleted | A task was deleted | The deleted [Task](#tag/Sync/Tasks). |
| item:completed | A task was completed | The completed [Task](#tag/Sync/Tasks). |
| item:uncompleted | A task was uncompleted | The uncompleted [Task](#tag/Sync/Tasks). |
| note:added | A comment was added | The new [Comment](#tag/Sync/Comments). |
| note:updated | A comment was updated | The updated [Comment](#tag/Sync/Comments). |
| note:deleted | A comment was deleted | The deleted [Comment](#tag/Sync/Comments). |
| project:added | A project was added | The new [Project](#tag/Sync/Projects). |
| project:updated | A project was updated | The updated [Project](#tag/Sync/Projects). |
| project:deleted | A project was deleted | The deleted [Project](#tag/Sync/Projects). |
| project:archived | A project was archived | The archived [Project](#tag/Sync/Projects). |
| project:unarchived | A project was unarchived | The unarchived [Project](#tag/Sync/Projects). |
| section:added | A section was added | The new [Section](#tag/Sync/Sections). |
| section:updated | A section was updated | The updated [Section](#tag/Sync/Sections). |
| section:deleted | A section was deleted | The deleted [Section](#tag/Sync/Sections). |
| section:archived | A section was archived | The archived [Section](#tag/Sync/Sections). |
| section:unarchived | A section was unarchived | The unarchived [Section](#tag/Sync/Sections). |
| label:added | A label was added | The new [Label](#tag/Sync/Labels). |
| label:deleted | A label was deleted | The deleted [Label](#tag/Sync/Labels). |
| label:updated | A label was updated | The updated [Label](#tag/Sync/Labels). |
| filter:added | A filter was added | The new [Filter](#tag/Sync/Filters). |
| filter:deleted | A filter was deleted | The deleted [Filter](#tag/Sync/Filters). |
| filter:updated | A filter was updated | The updated [Filter](#tag/Sync/Filters). |
| reminder:fired | A reminder has fired | The [Reminder](#/tag/Sync/Reminders) that fired. |

### Events Extra

Some events can include extra meta information in the `event_data_extra` field. These can be useful, for example, if you need to distinguish between item updates that are postponed and initiated by the user and item updates that are task completions (initiated by completing a recurring task)

| Event Name | Description | Event Data |
|------------|-------------|------------|
| item:updated | For events issued by the user directly these include `old_item` and `update_intent` | `old_item` will be an [Task](#tag/Sync/Tasks), and `update_intent` can be `item_updated`, `item_completed`, `item_uncompleted`. |

## Request Format

### Event JSON Object

> Example Webhook Request

```text
POST /payload HTTP/1.1

Host: your_callback_url_host
Content-Type: application/json
X-Todoist-Hmac-SHA256: UEEq9si3Vf9yRSrLthbpazbb69kP9+CZQ7fXmVyjhPs=
```

```json
{
    "event_name": "item:added",
    "user_id": "2671355",
    "event_data": {
        "added_by_uid": "2671355",
        "assigned_by_uid": null,
        "checked": false,
        "child_order": 3,
        "collapsed": false,
        "content": "Buy Milk",
        "description": "",
        "added_at": "2025-02-10T10:33:38.000000Z",
        "completed_at": null,
        "due": null,
        "deadline": null,
        "id": "6XR4GqQQCW6Gv9h4",
        "is_deleted": false,
        "labels": [],
        "parent_id": null,
        "priority": 1,
        "project_id": "6XR4H993xv8H5qCR",
        "responsible_uid": null,
        "section_id": null,
        "url": "https://app.todoist.com/app/task/6XR4GqQQCW6Gv9h4",
        "user_id": "2671355"
    },
    "initiator": {
        "email": "alice@example.com",
        "full_name": "Alice",
        "id": "2671355",
        "image_id": "ad38375bdb094286af59f1eab36d8f20",
        "is_premium": true
    },
    "triggered_at": "2025-02-10T10:39:38.000000Z",
    "version": "10"
}
```

Each webhook event notification request contains a JSON object. The event JSON contains the following properties:

| Property | Description |
|----------|-------------|
| event_name *String* | The event name for the webhook, see the table in the [Configuration](#tag/Webhooks/Configuration) section for the list of supported events. |
| user_id *String* | The ID of the user that is the destination for the event. |
| event_data *Object* | An object representing the modified entity that triggered the event, see the table in the [Configuration](#tag/Webhooks/Configuration) section for details of the `event_data` for each event. |
| version *String* | The version number of the webhook configured in the [App Management Console](https://app.todoist.com/app/settings/integrations/app-management). |
| initiator *Object* | A [Collaborator](#collaborators) object representing the user that triggered the event. This may be the same user indicated in `user_id` or a collaborator from a shared project. |
| triggered_at *String* | The date and time when the event was triggered. |
| event_data_extra *Object* | Optional object that can include meta information, see the table in the [Configuration](#tag/Webhooks/Configuration) section for details of the `event_data_extra` for each event. |

### Request Header

| Header Name | Description |
|-------------|-------------|
| User-Agent | Will be set to "Todoist-Webhooks" |
| X-Todoist-Hmac-SHA256 | To verify each webhook request was indeed sent by Todoist, an `X-Todoist-Hmac-SHA256` header is included; it is a SHA256 Hmac generated using your `client_secret` as the encryption key and the whole request payload as the message to be encrypted. The resulting Hmac would be encoded in a base64 string. |
| X-Todoist-Delivery-ID | Each webhook event notification has a unique `X-Todoist-Delivery-ID`. When a notification request failed to be delivered to your endpoint, the request would be re-delivered with the same `X-Todoist-Delivery-ID`. |

### Failed Delivery

When an event notification fails to be delivered to your webhook callback URL (i.e. due to server / network error, incorrect response, etc), it will be reattempted after 15 minutes. Each notification will be reattempted for at most three times.

**Your callback endpoint must respond with an HTTP 200 when receiving an event notification request.**

A response other than HTTP 200 will be considered as a failed delivery, and the notification will be attempted again.

# Todoist API v1 Documentation

Complete documentation for the Todoist API v1, covering REST endpoints, Sync API, OAuth authorization, and more.

## Table of Contents

### Getting Started

1. [Introduction](docs/01-introduction.md)
   - Developing with Todoist
   - Our API
   - Our SDKs
   - Integrations

2. [Authorization](docs/02-authorization.md)
   - OAuth Flow
   - Cross Origin Resource Sharing (CORS)
   - Token Management
   - Migrate Personal Token
   - Revoke Access Token

3. [Todoist MCP](docs/03-todoist-mcp.md)
   - Model Context Protocol Integration
   - Setup Guides (Claude, Cursor, VS Code, CLI)

### Sync API

4. [Sync API Overview](docs/04-sync-overview.md)
   - Read Resources
   - Write Resources
   - Command UUID
   - Temporary Resource ID
   - Response / Error Handling
   - Response Status Codes
   - Batching Commands
   - Incremental Sync

5. [Workspace](docs/05-sync-workspace.md)
   - Workspace Properties
   - Add/Update/Delete Workspace
   - Workspace Configuration

6. [Workspace Users](docs/06-sync-workspace-users.md)
   - User Management
   - Invitations
   - Roles and Permissions

7. [View Options](docs/07-sync-view-options.md)
   - View Option Properties
   - Set/Delete View Options
   - View Configuration

8. [Project View Options Defaults](docs/08-sync-project-view-options-defaults.md)
   - PVOD Properties
   - Default View Preferences
   - Baseline Configuration

9. [User](docs/09-sync-user.md)
   - User Data Structures
   - User Settings
   - User Plan Limits
   - Productivity Stats

10. [Sharing/Collaborators](docs/10-sync-sharing.md)
    - Collaborators
    - Collaborator States
    - Share a Project
    - Delete a Collaborator
    - Accept/Reject/Delete Invitations

11. [Sections](docs/11-sync-sections.md)
    - Section Properties
    - Add/Update/Delete/Reorder Sections

12. [Reminders](docs/12-sync-reminders.md)
    - Reminder Types
    - Add/Update/Delete Reminders
    - Location-based Reminders

13. [Projects](docs/13-sync-projects.md)
    - Project Properties
    - Add/Update/Delete/Archive Projects
    - Project Reordering
    - Workspace Projects
    - Folders

14. [Comments](docs/14-sync-comments.md)
    - Task Comments
    - Project Comments
    - Add/Update/Delete Comments
    - Attachments

15. [Live Notifications](docs/15-sync-live-notifications.md)
    - Notification Types
    - Mark as Read
    - Notification Settings

16. [Labels](docs/16-sync-labels.md)
    - Personal Labels
    - Shared Labels
    - Add/Update/Delete/Rename Labels
    - Label Colors

17. [Tasks/Items](docs/17-sync-tasks.md)
    - Task Properties
    - Add/Update/Move/Delete Tasks
    - Complete/Uncomplete Tasks
    - Recurring Tasks
    - Subtasks
    - Task Reordering

18. [Filters](docs/18-sync-filters.md)
    - Personal Filters
    - Add/Update/Delete Filters
    - Filter Queries
    - Update Filter Orders

19. [Workspace Filters](docs/19-sync-workspace-filters.md)
    - Workspace Filter Properties
    - Add/Update/Delete Workspace Filters
    - Workspace Filter Management
    - Update Multiple Orders

### REST API Endpoints

20. [IDs](docs/20-rest-ids.md)
    - ID Mappings
    - Legacy ID Handling

21. [Workspace APIs](docs/21-rest-workspace-apis.md)
    - Delete/All Invitations
    - Accept/Reject Invitations
    - Archived/Active Projects
    - Plan Details
    - Get Workspaces Users
    - Join Workspace
    - Update Logo

22. [Projects](docs/22-rest-projects.md)
    - Get Archived Projects
    - Create Project
    - Get Projects
    - Get Collaborators
    - Unarchive/Archive Project
    - Permissions
    - Join Project
    - Get/Update/Delete Project

23. [Colors](docs/23-rest-colors.md)
    - Available Colors Reference
    - Color Names and Codes

24. [Comments](docs/24-rest-comments.md)
    - Create Comment
    - Get Comments
    - Get/Update/Delete Comment

25. [Templates](docs/25-rest-templates.md)
    - Import From Template
    - Import From File
    - Create From File

26. [Sections](docs/26-rest-sections.md)
    - Create Section
    - Get Sections
    - Get/Update/Delete Section

27. [Tasks](docs/27-rest-tasks.md)
    - Create Task
    - Get Tasks
    - Tasks Completed (By Completion/Due Date)
    - Get Tasks by Filter
    - Quick Add
    - Reopen/Close/Move Task
    - Get/Update/Delete Task

28. [Labels](docs/28-rest-labels.md)
    - Get/Create Labels
    - Shared Labels
    - Get/Update/Delete Label
    - Remove/Rename Shared Labels

29. [Uploads](docs/29-rest-uploads.md)
    - Upload File
    - Delete Upload

30. [User](docs/30-rest-user.md)
    - User Info
    - Productivity Stats
    - Update Notification Settings

31. [Activity](docs/31-rest-activity.md)
    - Get Activity Logs

32. [Backups](docs/32-rest-backups.md)
    - Get Backups
    - Download Backup

33. [Emails](docs/33-rest-emails.md)
    - Email Get or Create
    - Email Disable

### Supplementary Information

34. [Webhooks](docs/34-webhooks.md)
    - Configuration
    - Event Types
    - Request Format
    - HMAC Verification
    - Webhook Activation

35. [Pagination](docs/35-pagination.md)
    - How Pagination Works
    - Making Paginated Requests
    - Pagination Parameters
    - Best Practices
    - Error Handling
    - Activity Log Pagination

36. [Request Limits](docs/36-request-limits.md)
    - Payload Size Limits
    - Header Size Limits
    - Processing Timeouts
    - Rate Limiting

37. [URL Schemes](docs/37-url-schemes.md)
    - Mobile App URL Schemes
    - Desktop App URL Schemes
    - Views
    - Tasks
    - Projects

38. [Migration from v9](docs/38-migration-v9.md)
    - General Changes
    - IDs
    - Task URLs
    - Lowercase Endpoints
    - Pagination
    - REST API Changes
    - Object Renames
    - URL Renames
    - Deprecated Endpoints
    - /sync Endpoint Changes
    - Webhooks Changes

## Quick Links

- [Official Todoist API Python SDK](https://doist.github.io/todoist-api-python/)
- [Official Todoist TypeScript SDK](https://doist.github.io/todoist-api-typescript/)
- [Todoist Integrations](https://www.todoist.com/integrations)
- [Submit Your Integration](https://doist.typeform.com/to/Vvq7kNcl?typeform-source=todoist.com/api/v1/docs)
- [API Mailing List](https://groups.google.com/a/doist.com/g/todoist-api)

## Support

For questions or issues, contact us at [submissions@doist.com](mailto:submissions@doist.com).

## API Base URL

```
https://api.todoist.com
```

All API requests should include an authorization header with your API token:

```
Authorization: Bearer YOUR_API_TOKEN
```

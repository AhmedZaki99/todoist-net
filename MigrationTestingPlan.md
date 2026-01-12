# Migration Testing Plan

## Goals
- Validate full Todoist API v1 coverage (REST + Sync) with breaking-change renames.
- Exercise pagination, transactions, incremental sync, and premium-only features.
- Ensure error handling matches new JSON format; verify null/unset serialization.

## Test Matrix
- **Unit tests**
  - Serialization: unset pattern with new models (due/deadline/duration), internal setters, cursor responses.
  - Converters: duration units, ComplexId opaque strings, StringEnum updates, error payload parsing.
  - Command mapping: command type values for renamed operations; resource type enums; lower-case endpoints generation.
  - Pagination helpers: `IAsyncEnumerable` pagination over mocked cursor responses.
  - Webhook payload models + optional HMAC helper.
- **Integration tests (free)**
  - Tasks: CRUD, move/close/reopen, quick add, filter endpoint, pagination (`limit`, `cursor`), completed tasks by completion/due date, stats endpoints if available.
  - Projects: CRUD, archived/active lists, full project data, collaborators list (paginated), join project.
  - Sections: CRUD, archived sections endpoint, move/reorder.
  - Comments: CRUD on tasks/projects with attachments.
  - Labels: CRUD, shared labels endpoints, pagination.
  - Uploads: upload + delete.
  - Activity: list with pagination mechanism noted in docs.
  - IDs mapping: `/id_mappings` coverage for tasks/sections/projects/comments/reminders/location_reminders.
  - Workspaces: joinable, logo update, invitations accept/reject, archived/active projects under workspace.
  - Notification setting: PUT endpoint.
- **Integration tests (premium)**
  - Reminders + location reminders.
  - Filters + workspace filters.
  - Templates import/export endpoints.
  - Emails: get/create (PUT) and disable (DELETE).
  - Duration fields on tasks; auto_reminder/auto_parse_labels behaviors.
  - Completed tasks stats (if premium-only).
- **Transactions/Sync**
  - Command batching with temp ID resolution (add + move + close).
  - Incremental sync: `full_sync_date_utc`, removal of `day_orders_timestamp`, section `is_collapsed` rename.
  - Mixed operations: add project + tasks + comments in one transaction; ensure mapping to persistent IDs.

## Test Infrastructure Notes
- Keep existing traits: `unit`, `integration-free`, `integration-premium`, `mfa-required`.
- Continue using `RateLimitAwareRestClient`; adjust for cursor pagination endpoints.
- Token: request user-provided `todoist:token` when running integration suites; ask for premium token for premium suite.
- Execution commands:
  - `./build.cmd unit-test`
  - `./build.cmd test` (all except mfa-required).

## Coverage Checklist per Service
- Tasks: create/read/update/delete; move/close/reopen; filter; quick add; completed (by completion/due date); pagination; duration/due variants.
- Projects: create/read/update/delete; archive/unarchive; archived lists; full project; collaborators (paginated); join.
- Sections: create/read/update/delete; archived; reorder/move.
- Comments: task + project CRUD; attachments.
- Labels: personal/shared CRUD; pagination.
- Reminders/location reminders: CRUD; premium trait.
- Filters/workspace filters: CRUD; premium trait.
- Uploads: upload/delete.
- Templates: import (file/url) + export (file/url); premium trait.
- Activity: listing pagination.
- Backups: list + download.
- Emails: get/create, disable; premium trait.
- Notification setting: update.
- IDs mapping: tasks/sections/projects/comments/reminders/location_reminders.
- Workspaces: joinable list, logo update, invitations accept/reject, active/archived project lists.

## Regression/Breaking Checks
- Ensure numeric ID assumptions removed; ComplexId handles opaque strings.
- Confirm `comment_count` removals where documented; reconcile with actual responses in assertions.
- Verify task URL formatting (`https://app.todoist.com/app/task/<id>`).
- Validate lower-case endpoint paths.
- Ensure unset/null handling sends nulls only when explicitly unset.

## Reporting
- Track failing endpoints with request/response samples (sanitized).
- Note any doc vs. actual discrepancies for follow-up.
- Document test coverage status by trait category.

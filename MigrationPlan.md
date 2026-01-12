# Migration Plan: Todoist API v1 (Unified)

## Goals and Constraints
- Migrate from Sync API v9 to Todoist API v1 with full endpoint/model coverage (REST + Sync), releasing as v11.0.0 (breaking).
- Keep existing targets (`netstandard2.0`, `net462`); keep non-nullable stance but ensure null-safety.
- Preserve transaction/command pattern and incremental sync; add REST-first flows where applicable.
- Rename entities to API v1 nomenclature (tasks, comments, reminders, location_reminders) while avoiding `Task` name conflicts by adopting `DetailedTask` for the read model and consistent variants for add/update/info DTOs.
- Enforce lowercase endpoints, new error JSON shape, cursor pagination with `IAsyncEnumerable` helpers, and updated task URL format.
- Remove v9-only/obsolete endpoints and models; align names and behavior with documented API v1.

## Terminology and Naming Updates
- Items → Tasks; Notes → Comments; Notifications → Reminders; Notifications_locations → LocationReminders.
- Classes: `Item` → `DetailedTask`, `AddItem` → `AddTask`, `UpdateItem` → `UpdateTask`, `ItemInfo` → `TaskInfo`, command enums to new names; similar renames for services/interfaces.
- Properties: apply documented snake_case mappings; drop `comment_count` on tasks/projects; adopt v1 `url` format; ensure `id` uses opaque strings.

## Documentation Crosswalk (what to cover in code)
- Getting started & auth: [documentation/docs/01-introduction.md](documentation/docs/01-introduction.md), [documentation/docs/02-authorization.md](documentation/docs/02-authorization.md) → OAuth/token flows, no special MFA/device support.
- Sync fundamentals: [documentation/docs/04-sync-overview.md](documentation/docs/04-sync-overview.md) → incremental sync, batching, temp IDs, command UUIDs, error handling.
- Workspace + users + roles: [documentation/docs/05-sync-workspace.md](documentation/docs/05-sync-workspace.md), [documentation/docs/06-sync-workspace-users.md](documentation/docs/06-sync-workspace-users.md).
- View options defaults: [documentation/docs/07-sync-view-options.md](documentation/docs/07-sync-view-options.md), [documentation/docs/08-sync-project-view-options-defaults.md](documentation/docs/08-sync-project-view-options-defaults.md).
- User profile and stats: [documentation/docs/09-sync-user.md](documentation/docs/09-sync-user.md).
- Sharing/collaborators: [documentation/docs/10-sync-sharing.md](documentation/docs/10-sync-sharing.md).
- Sections/reminders/projects/comments/live notifications/labels/tasks/filters/workspace filters: [documentation/docs/11-sync-sections.md](documentation/docs/11-sync-sections.md) through [documentation/docs/19-sync-workspace-filters.md](documentation/docs/19-sync-workspace-filters.md).
- REST endpoints: IDs, workspace APIs, projects, colors, comments, templates, sections, tasks, labels, uploads, user, activity, backups, emails: [documentation/docs/20-rest-ids.md](documentation/docs/20-rest-ids.md) through [documentation/docs/33-rest-emails.md](documentation/docs/33-rest-emails.md).
- Webhooks: [documentation/docs/34-webhooks.md](documentation/docs/34-webhooks.md) → payloads, HMAC verification, event matrix.
- Pagination: cursor model & limits: [documentation/docs/35-pagination.md](documentation/docs/35-pagination.md).
- Request limits: [documentation/docs/36-request-limits.md](documentation/docs/36-request-limits.md).
- URL schemes: [documentation/docs/37-url-schemes.md](documentation/docs/37-url-schemes.md).
- Migration specifics: [documentation/docs/38-migration-v9.md](documentation/docs/38-migration-v9.md).

## High-Level Architecture Changes
- REST client: update base paths to `/api/v1/...`, enforce lowercase endpoints, JSON error parsing, and application/json defaults.
- Sync client: retain `/sync/v9` semantics but adopt v1 naming exceptions; include new `full_sync_date_utc`, remove `day_orders_timestamp`, rename section `collapsed` → `is_collapsed`.
- Services: rename to v1 names; add missing services/endpoints (ids_mapping, pagination-enabled listings, projects full, archived projects/sections, notification_setting, workspaces join/accept/reject/logo, emails PUT/DELETE, uploads, backups, activity).
- Transactions: keep command queue pattern for sync commands; for REST-only endpoints, execute immediately; ensure consistent public surface (command services for batchable ops, full services for read).
- Pagination: introduce reusable cursor paged response + `IAsyncEnumerable<T>` helpers; expose raw cursor + limit where needed.
- Error handling: unify to JSON error payload (error, error_code, error_tag, error_extra, http_code); map to `TodoistException`; remove plain/text parsing.

## Model Work (Examples; apply across all resources)
- IDs: use opaque string `id`; drop numeric assumptions; incorporate `id_mappings` endpoint support via helper service and model.
- Task model (`DetailedTask`): update fields per REST (`creator_id`, `assigner_id`, `assignee_id`, `is_completed`, `url`, `duration {amount,unit}`, due/ deadline objects). Ensure comment_count is removed per migration doc (REST sample still shows comment_count; follow doc preference and align with actual endpoint during implementation/testing).
- Task create/update DTOs: support `due_string/due_date/due_datetime/due_lang`, `duration` + `duration_unit`, `order`, `labels`, `assignee_id`, `meta` for quick add, auto_parse_labels/auto_reminder flags.
- Completed tasks responses: paged results with `results`, `next_cursor`, and stats endpoints.
- Projects/sections/comments/labels/reminders/location_reminders: rename properties, add/remove fields per docs; ensure sections/comments use sync format consistently.
- Workspace/project collaborators & roles: align to new REST endpoints (archived/active lists, join, accept/reject, logo update).
- Templates/uploads/backups/emails/activity: add/adjust request/response DTOs and file upload forms.
- Webhooks: request payload models (event_data + event_data_extra); HMAC validation helper optional.

## Service Surface Updates (rename/expand)
- ItemsService → TasksService; NotesService → CommentsService; NotificationsService → RemindersService; NotificationsLocationsService → LocationRemindersService.
- Add new service(s) for IdMappings, Pagination helpers, Activity (v1 endpoint), Backups (v1), Uploads (v1), Emails (PUT/DELETE), NotificationSetting (PUT), Workspaces (joinable, logo, invitations accept/reject, join), Projects full/archived, Labels shared/paginated.
- Ensure REST task endpoints (`tasks`, `tasks/filter`, `tasks/{id}`, `tasks/quick`, `tasks/completed/*`, move/close/reopen).
- Ensure Sync command coverage remains for batchable operations and incremental sync.

## Serialization and JSON Options
- Preserve `JsonResolverModifiers` (internal setters, unset pattern, include nulls when explicitly unset).
- Update converters if needed for new enums/strings (e.g., duration units, pagination cursor handling, error payload).
- Confirm `DefaultIgnoreCondition` handling for unset; ensure deadline/due/date parsing accepts string/iso.

## Pagination Strategy
- Shared cursor response type (`results`, `next_cursor`).
- Helper `IAsyncEnumerable<T>` wrappers for tasks, labels, projects, sections, comments, activities.
- Expose raw `cursor` and `limit` parameters on service methods; default limit 50, max 200; handle `next_cursor == null`.

## Error Handling
- Parse JSON error shape; populate `TodoistException` with `error`, `error_code`, `error_tag`, `error_extra`, `http_code`.
- Update tests to assert JSON error parsing; drop plain/text branches.

## Transactions and Incremental Sync
- Keep current transaction/command queue for sync endpoints; ensure command types use new names where the API expects (item_add→task_add etc. per documentation).
- Maintain `CreateTransaction()` exposing command services only; commit sends batched commands to sync endpoint.
- Incremental sync: support `full_sync_date_utc`, remove `day_orders_timestamp`; ensure resource type enums align with v1 naming exceptions (items remain in sync payload per docs).

## Testing Scope (see MigrationTestingPlan.md for detail)
- Unit: serialization (unset, internal setters), error parsing, command type mapping, pagination cursor handling.
- Integration (free + premium): CRUD for every service, pagination flows, completed tasks endpoints, quick add, move/close/reopen, uploads, templates, activity, backups, emails, webhooks HMAC helper (if added), id_mappings.
- Transactions: verify temp ID mapping still works; mixed command types; rollback expectations.

## Documentation and Samples
- Update README to v1 positioning, rename entities, update code samples with `DetailedTask`, new endpoints, pagination examples with `IAsyncEnumerable`, breaking changes list.
- Mention removal of Sync v9 compatibility and ID format changes.

## Breaking Changes to Communicate
- Namespace/type renames (Items→Tasks, Notes→Comments, etc.) and DTO renames.
- API surface changes (REST endpoints, pagination parameters, error types).
- Removal of Sync v9-only endpoints and models.
- Opaque string IDs replacing numeric IDs; task URL property format change.

## Execution Outline (implementation order)
1) Foundation: adjust REST client base path, error handling, pagination helper types, command type enum mapping, resource type enum updates.
2) Core renames: models and services for tasks/comments/reminders/location reminders; add new DTOs for due/deadline/duration; rename public APIs.
3) REST endpoints: implement tasks (all), labels (incl. shared/paged), projects (incl. archived/full/collaborators), sections, comments, uploads, backups, emails, activity, ids_mapping, notification_setting, workspaces.
4) Sync adjustments: incremental sync changes, command renames, section/task property updates, resource responses.
5) Transactions: ensure batchable commands align; keep create/update/delete/move/close/complete flows working.
6) Pagination: add `IAsyncEnumerable` wrappers and tests for paged endpoints.
7) Webhooks: payload models + optional HMAC helper; docs.
8) Tests: follow MigrationTestingPlan; add coverage for free + premium features.
9) Documentation: README, XML comments refresh, samples; note breaking changes and version bump to v11.0.0.

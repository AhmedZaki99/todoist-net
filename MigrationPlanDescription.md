# Migration Plan (Summary)

- Scope: Full Todoist API v1 coverage (REST + Sync) in v11.0.0; keep transactions and incremental sync; keep targets (`netstandard2.0`, `net462`); no backwards compatibility.
- Naming: Items→Tasks, Notes→Comments, Notifications→Reminders, Notifications_locations→LocationReminders; use `DetailedTask` for read model; rename DTOs/services/enums accordingly.
- Core changes: lowercase endpoints; JSON error format; opaque string IDs; task URL format; remove deprecated v9 endpoints/models; enforce pagination with cursor + `IAsyncEnumerable` helpers.
- Services: update/rename task/comment/reminder/location-reminder services; add/expand ids_mapping, tasks (filter/quick/completed/move/close/reopen), labels (shared/paged), projects (archived/full/collaborators), sections, uploads, backups, emails, notification_setting, workspaces (joinable/logo/invitations), activity, templates.
- Models: refresh due/deadline/duration, drop comment_count where documented, align fields to v1; add pagination response types; update command/resource enums.
- Sync: keep command batching and incremental sync; apply naming exceptions; add `full_sync_date_utc`, remove `day_orders_timestamp`, rename `collapsed`→`is_collapsed`.
- Testing: unit (serialization/error/pagination/command mapping), integration free+premium across all services, pagination flows, transactions/temp IDs, uploads/templates/activity/backups/emails/id_mappings/webhooks helper; see MigrationTestingPlan.md.
- Docs: update README samples to v1, add breaking-changes list, refresh XML comments.

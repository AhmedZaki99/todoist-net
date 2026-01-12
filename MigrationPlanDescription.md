## Migration Plan (Summary)

- **Goal**: Ship a breaking release with full Todoist API v1.0 coverage (all documented endpoints/models), replacing Sync v9/REST v2.
- **Naming**: Use v1 terminology; tasks are `DetailedTask`/`AddTask`/`UpdateTask` to avoid `Task` conflicts. Notes → Comments, Notifications → Reminders, notifications_locations → location_reminders.
- **Foundations**: Enforce lowercase endpoints; IDs are opaque strings; add `/api/v1/ids_mapping`; adopt JSON error envelopes; add cursor-based pagination primitives.
- **Services/Endpoints**: Implement or rename for tasks (incl. quick/filter/completed), comments, reminders/location_reminders, projects (incl. archived/full/join), sections (incl. archived), labels (incl. shared), filters + workspace filters, workspaces + users, view options + project defaults, sharing/collaborators, templates, uploads, colors, user (stats/notification settings), activity logs, backups, emails, webhooks, IDs mapping, pagination helpers; keep `/sync` legacy naming where documented.
- **Models/Serialization**: Align schemas (remove deprecated fields), update due/duration/url formats, add pagination wrappers, refresh XML docs.
- **Client Surface**: Update `ITodoistClient`/`TodoistClient` to new/renamed services; adjust transactions for command-capable resources.
- **Testing**: See `MigrationTestingPlan.md`—unit serialization/error/pagination/ID mapping; integration CRUD + pagination across resources; quick add, completed tasks, uploads; webhook signature unit tests; rate-limit behavior.
- **Docs**: Refresh README, architecture, contributing, testing docs with v1 examples, pagination/error handling, breaking changes, OAuth/token migration, webhooks.

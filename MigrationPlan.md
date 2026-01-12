## Migration Plan: Todoist API v1.0 (Unified REST + Sync) for Todoist.Net

### Goals & Scope
- Deliver full coverage of Todoist API v1.0 (all documented endpoints and models) with a single major, breaking release.
- Follow v1 naming as documented (e.g., *tasks* instead of items, *comments* instead of notes). For task models, use `DetailedTask` (read), `AddTask`, `UpdateTask`, etc., to avoid `Task` type collisions while matching API terminology.
- Drop backward compatibility with Sync v9/REST v2, replace the client surface, models, docs, and tests to align with v1.

### Baseline & Constraints
- Architecture stays service-per-resource + command pattern + transactions where applicable.
- Keep target frameworks (`netstandard2.0`, `net462`) and treat warnings as errors; keep XML docs required.
- Endpoints must be lowercase; IDs are opaque strings (no numeric fallback). Old IDs must be migrated/translated via `/api/v1/ids_mapping`.
- Pagination is cursor-based on the newly paginated endpoints; add reusable pagination primitives.
- Error payloads are unified JSON (Sync-style) for REST endpoints; update exception handling/serialization.
- Webhooks inherit new formatting; assume version bump in app console when ready.

### High-Level Phases
1) **Foundations & Renames**
   - Rename item-centric types/services to task terminology (`ItemsService` → `TasksService`, `Item` → `DetailedTask`, `AddItem` → `AddTask`, `UpdateItem` → `UpdateTask`, commands/enums/resources, DTOs, tests, docs).
   - Rename notes → comments, notifications → reminders, notifications_locations → location_reminders; adjust services, command types, resource types, models, tests.
   - Ensure URL paths and route builders are lowercase; update constants/endpoints.
   - Introduce pagination primitives (cursor, limit defaults, next_cursor handling) and response wrappers as needed.
   - Update ComplexId usage and serializers to ensure string IDs only; remove legacy numeric assumptions and `v2_*_id` fields.

2) **Cross-Cutting Infrastructure**
   - Serialization: align property names to v1 schemas, remove deprecated fields (`comment_count` on tasks/projects, `day_orders_timestamp`, `is_biz_admin`, etc.), adjust due/duration/date shapes per docs; ensure unset/null handling remains correct.
   - Error handling: enforce JSON error parsing for REST endpoints; adjust `TodoistException` construction to new fields (`error`, `error_code`, `error_tag`, `error_extra`).
   - Request building: add pagination query support; add IDs mapping endpoint helper; ensure cursor-based pagination supported across services.
   - Transactions: preserve command batching for `/sync`; revisit command types to match v1 naming/renames where `/sync` keeps legacy names (per migration doc exceptions).

3) **Endpoint Coverage (per documentation)**
   - **Authorization (02)**: ensure OAuth flow samples/docs updated; add token revoke/migrate calls (`/api/v1/access_tokens`, `.../migrate_personal_token`).
   - **Sync Overview (04)**: adjust incremental/full sync, remove `day_orders_timestamp`, add `full_sync_date_utc`; keep legacy naming on `/sync`.
   - **Workspace (05)**: add workspace models/services (add/update/delete/config), include pagination for workspace project endpoints; align roles/limits.
   - **Workspace Users (06)**: manage invitations, roles/permissions.
   - **View Options (07) & Project View Option Defaults (08)**: add models/services for view option CRUD and defaults.
   - **User (09)**: update user settings, plan limits; remove `is_biz_admin`.
   - **Sharing/Collaborators (10)**: share project, collaborator state management, invitation accept/reject/delete.
   - **Sections (11)**: adopt v1 format (Sync-style), rename fields (`is_collapsed`), CRUD + archived.
   - **Reminders (12)**: rename notifications → reminders; include location_reminders coverage.
   - **Projects (13)**: CRUD, archive/unarchive, reordering, folders/workspace project endpoints; remove `comment_count`.
   - **Comments (14)**: rename notes → comments; support task/project comments, attachments.
   - **Live Notifications (15)**: retain as reminders naming alignment; mark gaps if any.
   - **Labels (16)**: personal/shared labels, rename/remove endpoints per v1; pagination.
   - **Tasks/Items (17)**: rename to tasks, adjust schema (URL format, removed filter/lang params, `comment_count` removed), new `/tasks/filter`, completed endpoints by completion/due date, quick add at `/tasks/quick`, move/close/reopen.
   - **Filters (18)** & **Workspace Filters (19)**: add workspace-specific filters endpoints/order updates.
   - **IDs (20)**: implement `/api/v1/ids_mapping/<object>/<ids>` helper.
   - **REST Workspace APIs (21)**: invitations, archived/active projects, plan details, join workspace, logo update.
   - **REST Projects (22)**: archived list, collaborators, join project, full project retrieval, permissions.
   - **Colors (23)**: expose available colors reference endpoint.
   - **REST Comments (24)**: CRUD with pagination.
   - **Templates (25)**: import/export endpoints (`file`, `url`, `import_into_project_from_file`).
   - **REST Sections (26)**: CRUD + archived list; pagination.
   - **REST Tasks (27)**: create/get/filter/quick/close/reopen/move/delete; completed stats/by date; pagination.
   - **REST Labels (28)**: CRUD, shared labels rename/remove, pagination.
   - **Uploads (29)**: upload/delete endpoints renamed; ensure MIME provider fits.
   - **User (30)**: info, productivity stats (`/tasks/completed/stats`), notification settings update (PUT `/notification_setting`).
   - **Activity (31)**: `/api/v1/activities` with pagination.
   - **Backups (32)**: list and download.
   - **Emails (33)**: get/create, disable endpoints renamed to `/api/v1/emails`.
   - **Webhooks (34)**: document HMAC verification, event types; plan signing utilities and samples.
   - **Pagination (35)**: implement consistent cursor handling; add reusable client helpers and tests.
   - **Request Limits (36)**: update rate-limit handling, retry guidance; ensure headers/limits documented.
   - **URL Schemes (37)**: update URL generation helpers and docs (tasks/projects/sections).
   - **Migration (38)**: enforce renames, deprecated endpoint removal, ID changes, lowercase URLs, error schema adoption.

4) **Client Surface & DI**
   - Update `ITodoistClient`/`TodoistClient` properties to renamed services; add new services for missing resources (workspace, workspace users, view options, project view options defaults, workspace filters, IDs, colors, activities, backups, emails, pagination helpers).
   - Update transactions to expose command services for resources that support batching; document which endpoints remain sync-only.
   - Update factory/DI registration to include new services.

5) **Models & Serialization**
   - Create new models per v1 schemas; adjust existing ones (remove deprecated fields, adjust due/duration shapes, new URL formats).
   - Introduce response wrapper models for paginated endpoints (`results`, `next_cursor`).
   - Update command/argument models to new endpoints and paths.
   - Ensure `JsonPropertyName` snake_case alignment across new fields; refresh XML documentation for clarity per v1 docs.

6) **Testing & Quality**
   - See `MigrationTestingPlan.md` for detailed matrix.
   - Add unit tests for serialization (null handling, renamed fields, pagination envelopes, error payloads) and for ID mapping.
   - Add integration tests per resource (free/premium traits) covering CRUD, pagination, filters, completed tasks, quick add, uploads, webhooks signature verification (offline unit), and rate-limit handling.

7) **Documentation & Samples**
   - Update root `README.md` to v1 positioning, examples using `TasksService`/`AddTask`/`DetailedTask`, pagination examples, error examples, webhooks, OAuth/token migration, and breaking changes list.
   - Update `CONTRIBUTING.md`, `ProjectArchitecture.md`, `TestingDesign.md` to reflect renamed services/models, new endpoints, testing strategy, and DI surface.
   - Provide migration notes for consumers (breaking changes, renamed namespaces/types, removed APIs).

### Work Breakdown by Layer
- **Endpoints/Services**: Implement/rename services for Tasks, Comments, Reminders, LocationReminders, Projects, Sections, Labels, Filters, WorkspaceFilters, Workspaces, WorkspaceUsers, ViewOptions, ProjectViewOptionDefaults, Templates, Uploads, Colors, Emails, Activity, Backups, IDs Mapping, User, Sharing, Live Notifications (as applicable), QuickAdd, Completed Tasks, Pagination helpers.
- **Models**: Align properties/types per docs; remove legacy fields; add pagination wrappers; rename Item/Note/etc. to Task/Comment/Reminder; ensure due/duration formats; add URL scheme helper changes.
- **Serialization/Error**: Unified JSON errors; remove plain-text handling; lower-case endpoints; adjust `TodoistRestClient` for pagination params and new base URLs.
- **Transactions**: Keep `/sync` semantics with legacy command names; document exceptions; ensure temporary ID handling remains via ComplexId.
- **Naming**: Use `DetailedTask`, `AddTask`, `UpdateTask`, `TaskInfo` (if needed), `TaskMoveArgument`, etc., to match API terminology while avoiding `Task` type conflicts.

### Risks & Mitigations
- **Extensive rename surface** → Do structured rename per layer (models, services, commands, tests, docs) to avoid drift.
- **Pagination regressions** → Centralize cursor handling with shared helpers and tests.
- **Error handling differences** → Add unit tests for JSON error envelopes; ensure legacy plain-text branches removed.
- **Breaking consumer API** → Provide thorough breaking changes section in README; consider minor shims only where trivial (e.g., type forwarders) if feasible.

### Deliverables
- Updated codebase with v1 endpoints, models, and naming.
- Updated documentation (README, architecture, contributing).
- Updated tests per `MigrationTestingPlan.md`.
- Clear release notes/breaking changes summary.

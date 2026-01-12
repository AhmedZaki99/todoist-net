## Migration Testing Plan

### Objectives
- Validate full Todoist API v1.0 coverage with renamed services/models.
- Ensure pagination, error handling, and ID handling match v1 contracts.
- Preserve transaction semantics for `/sync` while adopting new REST endpoints.

### Environments & Inputs
- Target frameworks: `netstandard2.0`, `net462`; tests run on `net8.0` test project.
- Required secrets: `todoist:token` for integration tests (ask maintainer for token); optional premium-capable token for premium traits.
- Base URL: `https://api.todoist.com`.

### Test Taxonomy (xUnit Traits)
- `unit`: Serialization, converters, pagination helpers, error handling, ID mapping, webhook signature verification, URL scheme helpers.
- `integration-free`: CRUD and pagination for free features across v1 endpoints.
- `integration-premium`: Premium-only flows (filters, reminders where premium behavior differs, productivity stats if gated).
- `mfa-required`: None expected; avoid password-based flows.

### Coverage Matrix (high level)
- **Auth & Tokens**: Access token revoke, migrate_personal_token (integration-free).
- **IDs Mapping**: `/api/v1/ids_mapping` happy/invalid path (unit + integration).
- **Tasks**: Create/get/filter/quick add/close/reopen/move/delete; completed (stats, by_completion_date, by_due_date); pagination cursors; due/duration/due_lang; URL format. Rename coverage (`DetailedTask`, `AddTask`, `UpdateTask`).
- **Comments**: Task/project comments CRUD; attachments; pagination.
- **Reminders/Location Reminders**: CRUD + any location-specific fields.
- **Projects**: CRUD, archive/unarchive, full project, join, collaborators, archived list, workspace project endpoints; pagination.
- **Sections**: CRUD + archived list; field rename `is_collapsed`.
- **Labels**: CRUD, shared labels, rename/remove shared; pagination.
- **Filters & Workspace Filters**: CRUD, order updates; workspace-specific endpoints.
- **Workspaces & Users**: Workspace CRUD/config, invitations (accept/reject/delete), roles/permissions, joinable list, plan details, logo update, workspace users listing.
- **View Options & Project Defaults**: CRUD and defaults application.
- **Sharing/Collaborators**: Share project, collaborator states.
- **Templates**: Import/export (file/url) and import_into_project_from_file.
- **Uploads**: Upload/delete; MIME handling.
- **User**: Info, productivity stats (`/tasks/completed/stats`), notification_setting update.
- **Activity**: `/api/v1/activities` with pagination.
- **Backups**: List/download.
- **Emails**: Get/create, disable.
- **Webhooks**: HMAC verification unit tests; payload shape alignment (no live callback required).
- **Request Limits/Pagination**: Rate-limit retry logging (unit), cursor advancement (unit + integration on at least one paginated endpoint).
- **/sync**: Incremental/full sync adjustments (`full_sync_date_utc`, removed `day_orders_timestamp`), legacy naming confirmation; command batching sanity.

### Test Design Notes
- Use `TodoistClientFactory` with rate-limit wrapper; maintain sequential execution via collection fixture.
- Ensure cleanup for every created resource in `finally`.
- Include negative tests: invalid IDs (opaque), cursor misuse, validation errors, unauthorized.
- Serialization tests: removed fields (e.g., `comment_count`), new snake_case properties, pagination envelopes, error JSON envelopes.
- Webhook signature: deterministic fixture with shared secret and sample payload.

### Execution Workflow
- Unit suite: `dotnet test src/Todoist.Net.Tests/Todoist.Net.Tests.csproj -t unit`.
- Integration (free): `build.cmd test` with trait filter `integration-free` (requires `todoist:token`).
- Integration (premium where applicable): trait `integration-premium` gated by premium token.
- Run targeted suites per resource after implementing changes; avoid full sweep until near completion.

### Tooling & Quality Gates
- Keep TreatWarningsAsErrors; ensure XML docs compile.
- Add CodeQL/security scan after functional changes (existing pipeline).
- Capture pagination/ID mapping helpers in unit tests to reduce live API calls.

### Deliverables
- Updated tests per resource covering v1 behaviors.
- Documented test instructions in `README`/`TestingDesign.md` reflecting new endpoints and traits.

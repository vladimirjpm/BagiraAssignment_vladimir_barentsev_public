# Scenario Builder — Full-Stack (Backend Oriented)

Create scenarios and manage the entities inside each scenario. .NET 10 Web API
(Clean Architecture) + React/Vite frontend, runnable with a single
`docker compose up`.

- **Frontend chosen:** React (`frontend/fronntend-react`).
- **Backend:** .NET 10 Web API, Clean Architecture (Domain / Application / Infrastructure / Api).
- **Storage:** configurable via `Storage:Provider` in `appsettings.json` — `"inmemory"` (default) or `"postgres"` (EF Core + Npgsql, auto-migrates on startup). Docker uses Postgres by default.

## Run with Docker (recommended)

```bash
docker compose up --build
```

Then open **http://localhost:8080**.

- `frontend` (nginx) is published on host port **8080** and serves the SPA.
- `backend` listens on **8080 inside the compose network** (not published).
- nginx proxies `/api/*` to the backend, so the browser stays **same-origin**
  (no CORS needed in the container). Backend CORS origins are still configurable
  via `Cors__AllowedOrigins__N` as a safety net for cross-origin setups.

Stop with `docker compose down`.

## Run locally (without Docker)

Backend:
```bash
cd backend
dotnet run --project Backend.Api   # http://localhost:5000  (Swagger at /swagger)
```

Frontend (separate terminal):
```bash
cd frontend/fronntend-react
npm install
npm run dev                        # http://localhost:5173
```

The Vite dev server proxies `/api` to `http://localhost:5000` (see
`vite.config.ts`), so the SPA is same-origin in dev too.

## API

| Method | Route | Purpose |
|---|---|---|
| GET  | `/api/scenarios?name=&description=&sortBy=&sortOrder=` | list + server-side filter/sort |
| GET  | `/api/scenarios/{id}` | one scenario (404 if missing) |
| POST | `/api/scenarios` | create |
| PUT  | `/api/scenarios/{id}` | update (404 if missing, same validation as create) |
| DELETE | `/api/scenarios/{id}` | delete — **cascades** to the scenario's entities |
| GET  | `/api/entities/scenarios/{scenarioId}/entities?type=&taskForce=&sortBy=&sortOrder=` | entities of a scenario + filter/sort |
| GET  | `/api/entities/{id}` | one entity (404 if missing) |
| POST | `/api/entities/scenarios/{scenarioId}/entities` | create entity in a scenario |
| PUT  | `/api/entities/{id}` | update (404 if missing, same validation as create) |
| DELETE | `/api/entities/{id}` | delete |
| GET  | `/api/search?q=` | search scenarios (name/description) and entities (name), case-insensitive contains |

Validation/Not-found errors return a uniform body: `{ message, status, errors? }`.
Enums are serialized as strings (`"Soldier"`, `"Friendly"`).

## Completed

- Clean Architecture (.NET 10): Domain / Application / Infrastructure / Api — domain invariants enforced in factories, never duplicated in controllers.
- Full Scenario + Entity CRUD (Create, Read, Update, Delete) end-to-end.
- Server-side filtering (scenario name/description, entity type/taskForce) and sorting (whitelisted fields, never reflected).
- Global search endpoint (`/api/search?q=`) — case-insensitive contains across scenario name/description and entity name.
- Unified `ErrorResponse { message, status, errors? }` for both validation 400s and domain/not-found errors.
- React frontend: list, detail, create/edit forms, Leaflet map view, delete with confirmation, search with debounce.
- Docker: multi-stage frontend (Vite → nginx), backend Dockerfile, `docker-compose.yml` with Postgres + healthcheck; single `docker compose up --build` starts everything.
- Configurable storage: `Storage:Provider = "inmemory"` (default, zero deps) or `"postgres"` (EF Core + Npgsql, auto-migrates on startup).
- Unit tests (xUnit + Moq): domain invariants (Scenario, Entity) and service layer (ScenarioService — 404, cascade delete, create).

## Trade-offs

- **InMemory by default** — zero setup for local dev; swapping to Postgres requires only two config changes (`Storage:Provider` + `ConnectionStrings:Default` in `appsettings.json`) — no code changes, repository interfaces keep all layers above Infrastructure unchanged.
- **Cascade delete on scenario** — entities have no meaning without their parent (no-orphan invariant), so blocking delete and forcing the user to remove entities one-by-one adds friction with no safety benefit; the action is still gated behind a confirm dialog.
- **Same-origin over CORS** — Vite proxy (dev) and nginx (prod) keep the SPA same-origin, so CORS is never needed in practice; the config remains as a fallback for cross-origin setups.
- **Entity route matches skeleton** — `/api/entities/scenarios/{id}/entities` is unusual but matches the provided frontend services exactly; changing it would break the skeleton contract.
- **Double fetch in dev** — React StrictMode intentionally mounts components twice; disappears in the production build.

## Extensibility

- **New storage backend** — implement the two repository interfaces, swap registrations in `DependencyInjection.cs`; nothing above Infrastructure changes.
- **Auth** — add middleware in `Program.cs` + `[Authorize]` on controllers; business logic untouched.
- **New entity fields** — add to domain model + one EF Core migration; controllers and services don't change.
- **New filters / sort fields** — extend the whitelist in `ScenarioService` / `EntityService`; call sites unaffected.
- **More tests** — all services depend on interfaces; any new service can be unit-tested with Moq without a database.

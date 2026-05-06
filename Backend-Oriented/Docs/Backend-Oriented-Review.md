# Backend-Oriented Assignment — Tester Review Guide

## Overview

This assignment focuses on backend architecture designed and built from scratch, plus connecting it to the provided frontend skeleton. The candidate must:

- Design and implement domain models, repository layer, and API controllers
- Implement server-side filtering and data validation
- Wire the backend up to the chosen frontend skeleton (React or Angular)
- Containerize the solution with Docker (or document the approach)

**Provided skeleton (do not flag as missing):**
- `backend/Backend.Api` — a single .NET project with `Program.cs` pre-configured (Swagger + controllers). Architecture, models, and structure are the candidate's design.
- `frontend/` — React and Angular skeleton with TODOs for the candidate to complete

---

## Setup Verification

### Prerequisites
- .NET 9.0 SDK
- Node.js and npm
- Database tools (if candidate chose a database)

### Running the project

**Backend:**
```bash
cd Backend-Oriented/backend
dotnet restore
dotnet run --project Backend.Api
```
Backend runs on `http://localhost:5000`. Swagger at `http://localhost:5000/swagger`.

**Frontend (React):**
```bash
cd Backend-Oriented/frontend/fronntend-react
npm install
npm run dev
```

**Frontend (Angular):**
```bash
cd Backend-Oriented/frontend/frontend-angular
npm install
npx ng serve
```

**Docker (if implemented):**
```bash
docker-compose up
```

---

## Code Review Checklist

### Backend

#### Project Structure & Architecture
- [ ] Project compiles and runs without errors
- [ ] Clean architecture evident — at minimum an API layer and a data access layer
- [ ] Domain models exist: Scenario, Entity, EntityType enum, TaskForce enum
- [ ] Data rules encoded correctly (EntityType values, TaskForce values, coordinate ranges, entity belongs to scenario)
- [ ] Application/Service layer optional — note presence or absence

#### Data Access Layer — Repositories
- [ ] `ScenarioRepository` implemented (no `NotImplementedException`)
  - `GetByIdAsync` — returns scenario or null
  - `GetAllAsync` — returns all scenarios
  - `AddAsync` — assigns ID + timestamps, returns created entity
  - `GetEntityCountAsync` (or equivalent) — entity count per scenario
- [ ] `EntityRepository` implemented (no `NotImplementedException`)
  - `GetByIdAsync`, `GetAllAsync`, `AddAsync`
  - `GetByScenarioIdAsync` — filters by scenario ID
  - `ScenarioExistsAsync` (or equivalent check in controller)
- [ ] Thread-safe if in-memory; DbContext + migrations if using a database
- [ ] Storage choice consistent across both repositories

#### API Controllers

##### ScenariosController
- [ ] `GET /api/scenarios` — returns scenario list with entity count
  - [ ] Filtering by name (server-side)
  - [ ] Filtering by description (server-side)
- [ ] `GET /api/scenarios/{scenarioId}` — returns details; 404 if not found
- [ ] `POST /api/scenarios` — creates scenario; returns 201; returns 400 with `ErrorResponse` on invalid input
- [ ] Proper HTTP status codes (200, 201, 400, 404)
- [ ] Error handling with try-catch; errors logged

##### EntitiesController
- [ ] `GET /api/entities/scenarios/{scenarioId}/entities` — returns entities for scenario
  - [ ] Filtering by EntityType (server-side)
  - [ ] Filtering by TaskForce (server-side)
- [ ] `GET /api/entities/{entityId}` — returns entity; 404 if not found
- [ ] `POST /api/entities/scenarios/{scenarioId}/entities` — creates entity; returns 201; 404 if scenario not found; 400 with `ErrorResponse` on invalid input
- [ ] Coordinate validation: latitude -90 to 90, longitude -180 to 180
- [ ] EntityType and TaskForce validated against allowed values

##### SearchController (bonus — do not penalize if missing)
- [ ] `GET /api/search?q=...` — searches scenario names/descriptions and entity names
- [ ] Returns grouped results (scenarios + entities)

#### DTOs & Validation
- [ ] Request DTOs have validation attributes (Required, Range, MaxLength)
- [ ] `ErrorResponse` returned on all 400 responses with field-level errors
- [ ] Response DTOs cover all required fields

#### Program.cs
- [ ] CORS configured — frontend origin allowed
- [ ] Repositories (and any services) registered in DI
- [ ] Swagger available in development

---

### Frontend (Skeleton TODOs)

The candidate chose one of the provided skeletons. Verify the chosen one.

#### Services
- [ ] `scenarioService` — `listScenarios` and `createScenario` make real HTTP calls (no stubs)
- [ ] `entityService` — `listEntities` and `createEntity` make real HTTP calls (no stubs)
- [ ] API base URL points to the candidate's backend

#### Scenario List Page
- [ ] Fetches and displays scenarios on load
- [ ] Loading, empty, and error states shown
- [ ] Filter by name/description works

#### Create Scenario Page
- [ ] Calls `createScenario` on submit
- [ ] Validation rules implemented (name required, length constraints)
- [ ] Server-side validation errors displayed
- [ ] Button disabled during submission

#### Scenario Details & Create Entity Pages
- [ ] Entities fetched and displayed
- [ ] Create entity form wired up with all required fields
- [ ] Coordinate validation shown

---

## Functional Testing Checklist

### Backend API

1. **Scenarios**
   - [ ] `GET /api/scenarios` — returns list
   - [ ] `GET /api/scenarios?name=test` — filters by name
   - [ ] `GET /api/scenarios?description=test` — filters by description
   - [ ] `GET /api/scenarios/{id}` — returns details
   - [ ] `GET /api/scenarios/{invalid}` — returns 404
   - [ ] `POST /api/scenarios` with valid data — returns 201
   - [ ] `POST /api/scenarios` with invalid data — returns 400 + `ErrorResponse`

2. **Entities**
   - [ ] `GET /api/entities/scenarios/{id}/entities` — returns entities
   - [ ] Filter by `entityType` — results correct
   - [ ] Filter by `taskForce` — results correct
   - [ ] `GET /api/entities/{id}` — returns entity
   - [ ] `GET /api/entities/{invalid}` — returns 404
   - [ ] `POST` with valid data — returns 201
   - [ ] `POST` to invalid scenario — returns 404
   - [ ] `POST` with invalid coordinates — returns 400
   - [ ] `POST` with invalid EntityType — returns 400

### Frontend

3. **Scenario Management**
   - [ ] Create scenario — appears in list
   - [ ] Open scenario details — only its entities shown
   - [ ] Filter scenarios by name

4. **Entity Management**
   - [ ] Create entity — appears in list with TaskForce shown
   - [ ] Filter by EntityType and TaskForce
   - [ ] Coordinate validation works

5. **UX States**
   - [ ] Loading indicator during data fetch
   - [ ] Empty state when no items
   - [ ] Error state when backend unreachable

### Docker (if implemented)

6. **Docker**
   - [ ] `docker-compose.yml` exists with backend and frontend services
   - [ ] Multi-stage `Dockerfile` for frontend
   - [ ] `docker-compose up --build` starts all services
   - [ ] CORS configured correctly in containers
   - [ ] Full flow works end-to-end in Docker

---

## Acceptance Criteria Verification

- [ ] User can create a scenario and see it in the scenario list
- [ ] User can open scenario details and view only entities belonging to that scenario
- [ ] User can select TaskForce (Friendly/Enemy) when creating entities; TaskForce displayed in entity lists
- [ ] User can filter scenarios by name/description (server-side)
- [ ] User can filter entities by Type and TaskForce (server-side)
- [ ] Backend validates all input fields and returns appropriate error responses
- [ ] Backend returns proper HTTP status codes for all operations
- [ ] Data persistence works (in-memory or database)
- [ ] Solution runs locally
- [ ] Clean architecture; code quality and maintainability
- [ ] Validation and error handling properly implemented

---

## Notes for Tester

- The `backend/Backend.Api/Program.cs` is pre-provided — evaluate what the candidate built on top of it, not the file itself
- Architecture, domain models, DTOs, and project structure are all the candidate's design — evaluate quality and cleanliness
- If Docker was not implemented, check the README for a written explanation — this is acceptable
- SearchController is a bonus; do not penalize its absence
- Server-side sorting is a bonus; do not penalize its absence
- The candidate chose either React OR Angular — verify the chosen skeleton's TODOs are resolved

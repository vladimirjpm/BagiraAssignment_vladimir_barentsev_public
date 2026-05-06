# Frontend-Oriented Assignment — Tester Review Guide

## Overview

This assignment focuses on frontend architecture built from scratch, plus completing the backend API layer. The candidate must:

- Implement the API controllers (`ScenariosController`, `EntitiesController`) on top of pre-built repositories
- Add DTO validation attributes and configure CORS
- Build a complete frontend application from scratch (React or Angular)
- Connect the frontend to the backend APIs

**Pre-implemented (do not flag as missing):**
- In-memory repository implementations (`ScenarioRepository`, `EntityRepository`)
- Domain models, interfaces, DTO structure

---

## Setup Verification

### Prerequisites
- .NET 8.0 SDK
- Node.js and npm

### Running the project

**Backend:**
```bash
cd Frontend-Oriented/backend
dotnet restore
dotnet run --project Backend.Api
```
Backend runs on `http://localhost:5000` or `https://localhost:5001`. Swagger at `/swagger`.

**Frontend** (look for a `frontend/` folder the candidate created inside `Frontend-Oriented/`):
```bash
# React
cd Frontend-Oriented/frontend
npm install && npm run dev

# Angular
cd Frontend-Oriented/frontend
npm install && ng serve
```

**Docker (if implemented):**
```bash
docker-compose up
```

---

## Code Review Checklist

### Backend

#### Controllers

##### `ScenariosController`
- [ ] `GET /api/scenarios` — returns list of scenarios with entity count
- [ ] `GET /api/scenarios/{scenarioId}` — returns scenario details; 404 if not found
- [ ] `POST /api/scenarios` — creates scenario; returns 201; returns 400 with `ErrorResponse` on invalid input
- [ ] Proper HTTP status codes throughout (200, 201, 400, 404)
- [ ] Error handling with try-catch; errors logged

##### `EntitiesController`
- [ ] `GET /api/entities/scenarios/{scenarioId}/entities` — returns entities for scenario; 404 if scenario not found
- [ ] `GET /api/entities/{entityId}` — returns entity by ID; 404 if not found
- [ ] `POST /api/entities/scenarios/{scenarioId}/entities` — creates entity; returns 201; returns 404 if scenario not found; returns 400 with `ErrorResponse` on invalid input
- [ ] Coordinate validation: latitude -90 to 90, longitude -180 to 180
- [ ] EntityType and TaskForce validated against allowed values

#### DTOs & Validation

**Pre-implemented — do not flag as missing:**
- `CreateScenarioRequest` — `[Required]` on Name, `[MaxLength(200)]` on Name, `[MaxLength(1000)]` on Description
- `CreateEntityRequest` — `[Required]` + `[EnumDataType]` on Type and TaskForce, `[Required]` + `[MaxLength(200)]` on Name, `[Range(-90,90)]` on Latitude, `[Range(-180,180)]` on Longitude
- Invalid requests automatically return `400` with `ErrorResponse` body (configured in `Program.cs` via `InvalidModelStateResponseFactory`)
- Enum values from the frontend (strings like `"Soldier"`, `"Friendly"`) are deserialized correctly via `JsonStringEnumConverter`

**Still candidate's responsibility:**
- [ ] `ErrorResponse` returned on `404` (scenario or entity not found) — must be implemented in controllers

#### Program.cs

- [ ] CORS configured — frontend origin allowed *(candidate must implement)*
- ✅ Repositories registered in DI (`IScenarioRepository`, `IEntityRepository`) — pre-configured
- ✅ `InvalidModelStateResponseFactory` returns `ErrorResponse` on `400` — pre-configured
- ✅ `JsonStringEnumConverter` registered — enum strings deserialize correctly — pre-configured

---

### Frontend

The candidate built the frontend from scratch. There is no prescribed layout — verify that the architecture satisfies the requirements.

#### Project Structure & Architecture

- [ ] Compiles and runs without errors
- [ ] React or Angular (candidate specified which)
- [ ] Clear separation between pages/views, components, and service/API layer
- [ ] TypeScript used
- [ ] All required screens present and navigable

#### Required Screens

- [ ] **Scenario List** — displays all scenarios; navigation to details; create action
- [ ] **Create Scenario** — form with name (required) and description (optional)
- [ ] **Scenario Details** — displays scenario info; list of its entities
- [ ] **Create Entity** — form with EntityType, TaskForce, Name/Callsign, Latitude, Longitude

#### API Integration

- [ ] Scenario service: `listScenarios`, `createScenario` — real HTTP calls, no stubs
- [ ] Entity service: `getEntitiesByScenario`, `createEntity` — real HTTP calls, no stubs
- [ ] Services abstract HTTP calls; components do not call fetch/HttpClient directly
- [ ] Backend error responses handled and surfaced to the user

#### Filtering

- [ ] Scenarios filterable by name/description
- [ ] Entities filterable by EntityType and TaskForce

#### UX States

- [ ] Loading state shown during data fetch
- [ ] Empty state shown when list has no items
- [ ] Error state shown on fetch failure

#### Validation

- [ ] Create Scenario: name required, length validated in real time
- [ ] Create Entity: all fields validated (coordinate ranges, enum values, required fields)
- [ ] Server-side validation errors displayed per-field

---

## Functional Testing Checklist

### Scenario Management

1. **Create Scenario**
   - [ ] Fill in name + description; submit
   - [ ] Scenario appears in scenario list
   - [ ] Redirect or navigation after success

2. **Scenario List**
   - [ ] Loading state appears initially
   - [ ] Scenarios shown with entity count
   - [ ] Navigation to details works
   - [ ] Filter by name works
   - [ ] Filter by description works

3. **Scenario Details**
   - [ ] Scenario name and description displayed
   - [ ] Entity list shown; empty state when no entities

### Entity Management

4. **Create Entity**
   - [ ] All fields filled; submit
   - [ ] Entity appears in entity list
   - [ ] TaskForce visible in entity list

5. **Entity Validation**
   - [ ] Latitude > 90 — shows error
   - [ ] Longitude > 180 — shows error
   - [ ] Missing required fields — shows validation errors

### Filtering

6. **Scenario List Filtering**
   - [ ] Filter by name — results update
   - [ ] Filter by description — results update

7. **Entity Filtering**
   - [ ] Filter by EntityType — results update
   - [ ] Filter by TaskForce — results update

### UX States

8. **Loading / Empty / Error**
   - [ ] Loading indicator during data fetch
   - [ ] Empty state when no scenarios exist
   - [ ] Empty state when no entities exist
   - [ ] Error state when backend is unreachable

9. **Form UX**
   - [ ] Submit button disabled during submission
   - [ ] Real-time validation feedback

### Error Handling

10. **Backend Errors**
    - [ ] 400 responses — field errors displayed
    - [ ] 404 responses — handled gracefully
    - [ ] Network errors — user-friendly message shown

### Docker (if implemented)

11. **Docker**
    - [ ] `docker-compose.yml` exists
    - [ ] Backend Dockerfile exists
    - [ ] Frontend Dockerfile exists (multi-stage)
    - [ ] `docker-compose up` starts all services
    - [ ] CORS configured correctly in containers
    - [ ] Full flow works end-to-end in Docker

---

## Acceptance Criteria Verification

- [ ] User can create a scenario and see it in the scenario list
- [ ] User can open scenario details and view only entities belonging to that scenario
- [ ] User can select TaskForce (Friendly/Enemy) when creating entities; TaskForce displayed in entity lists
- [ ] User can filter entities by Type and TaskForce
- [ ] User can filter scenarios by name/description
- [ ] Loading, empty, and error states displayed appropriately
- [ ] Frontend validates all input fields (coordinates, types, required fields)
- [ ] Backend validates all input fields and returns appropriate error responses
- [ ] Backend returns proper HTTP status codes for all operations
- [ ] Data persistence works (in-memory)
- [ ] Solution runs locally
- [ ] Clean architecture; code quality and maintainability
- [ ] Validation and error handling properly implemented

---

## Notes for Tester

- The candidate built the frontend from scratch — no prescribed folder layout; look inside `Frontend-Oriented/`
- The candidate chose either React OR Angular, not both
- **Repositories are pre-implemented** (`ScenarioRepository`, `EntityRepository`) — do not flag their absence as a gap; do not evaluate their implementation
- **DTO validation is pre-implemented** — all `[Required]`, `[Range]`, `[EnumDataType]`, and `[MaxLength]` attributes are already on the request DTOs; do not flag their absence
- **`400` validation responses are pre-configured** in `Program.cs` — the candidate is only responsible for returning `404` from controllers when a resource is not found
- No `return null;` or empty method bodies should remain in `ScenariosController` or `EntitiesController`
- If Docker was not implemented, check whether the candidate documented how they would containerize it in the README — this is acceptable

# Frontend-Oriented Assignment - Tester Review Guide

## Overview
This assignment focuses on frontend implementation built from scratch, plus completing the full backend stack. The candidate must:
- Implement the data access layer (ScenarioRepository, EntityRepository) with chosen storage (In-Memory or Database)
- Implement the API controllers (endpoints, error handling, validation)
- Add DTO validation attributes and configure CORS
- Build a complete frontend application from scratch (React or Angular, candidate's choice)
- Connect the frontend to the backend APIs

The provided backend foundation includes: domain models, repository interfaces, DTO structures, and controller stubs. Everything else is for the candidate to implement.

## Setup Verification

### Prerequisites
- .NET 8.0 SDK installed
- Node.js and npm installed
- Docker (optional, for containerized testing)

### Running the Project

1. **Backend Setup:**
   ```bash
   cd Frontend-Oriented/backend
   dotnet restore
   dotnet run --project Backend.Api
   ```
   - Backend should run on `https://localhost:5001` or `http://localhost:5000`
   - Swagger UI should be available at `/swagger`

2. **Frontend Setup** (candidate's own implementation — look for a `frontend/` folder inside `Frontend-Oriented/`):
   - **React:**
     ```bash
     cd Frontend-Oriented/frontend
     npm install
     npm run dev
     ```
   - **Angular:**
     ```bash
     cd Frontend-Oriented/frontend
     npm install
     ng serve
     ```

3. **Docker (if implemented):**
   ```bash
   docker-compose up
   ```

## Code Review Checklist

### Backend Implementation

#### 1. Infrastructure Layer - Repositories

##### `Frontend-Oriented/backend/Backend.Infrastructure/Repositories/ScenarioRepository.cs`
**TODOs to verify:**
- [ ] **TODO: Implement this repository** — not throwing `NotImplementedException`
  - Verify chosen storage approach (In-Memory, Entity Framework, Dapper, etc.)
  - If using database, verify DbContext or connection is properly configured

- [ ] **TODO: Implement scenario retrieval by id** — `GetByIdAsync(Guid id)` returns `Scenario?` or null if not found

- [ ] **TODO: Implement scenario retrieval** — `GetAllAsync()` returns all scenarios

- [ ] **TODO: Implement scenario creation** — `AddAsync(Scenario entity)` assigns ID/timestamps, returns created entity

- [ ] **TODO: Implement entity count retrieval for scenario** — `GetEntityCountAsync(Guid scenarioId)` returns correct count

**Expected Features:**
- Thread-safe implementation (if in-memory)
- Async/await pattern used correctly

##### `Frontend-Oriented/backend/Backend.Infrastructure/Repositories/EntityRepository.cs`
**TODOs to verify:**
- [ ] **TODO: Implement this repository** — not throwing `NotImplementedException`
  - Storage approach matches ScenarioRepository

- [ ] **TODO: Implement entity retrieval by id** — `GetByIdAsync(Guid id)` returns `Entity?` or null

- [ ] **TODO: Implement entity retrieval** — `GetAllAsync()` returns all entities

- [ ] **TODO: Implement entity creation** — `AddAsync(Entity entity)` assigns ID/timestamps, returns created entity

- [ ] **TODO: Implement entity retrieval by scenario id** — `GetByScenarioIdAsync(Guid scenarioId)` returns entities filtered by scenario

- [ ] **TODO: Implement scenario existence check** — `ScenarioExistsAsync(Guid scenarioId)` returns true/false

#### 2. Controllers

##### `Frontend-Oriented/backend/Backend.Api/Controllers/ScenariosController.cs`
- [ ] **TODO: Implement proper error handling, validation and return codes** — resolved
  - All endpoints return appropriate HTTP status codes (200, 201, 400, 404, 500)
  - Error handling with try-catch blocks
  - Validation errors return 400 BadRequest with ErrorResponse
  - 404 NotFound returned when scenario doesn't exist

- [ ] **TODO: Implement all endpoints** — resolved
  - [ ] `GET /api/scenarios` — returns list of scenarios with entity count
  - [ ] `GET /api/scenarios/{scenarioId}` — returns scenario details by ID
  - [ ] `POST /api/scenarios` — creates a new scenario (returns 201)

**Expected Features:**
- Entity count calculation (repository already supports this)
- Proper DTO mapping (ScenarioListItemDto, ScenarioDetailsDto)
- Input validation using DTO validation attributes

##### `Frontend-Oriented/backend/Backend.Api/Controllers/EntitiesController.cs`
- [ ] **TODO: Implement proper error handling, validation and return codes** — resolved
  - All endpoints return appropriate HTTP status codes
  - Error handling with try-catch blocks
  - Validation errors return 400 BadRequest
  - 404 NotFound for non-existent entities/scenarios

- [ ] **TODO: Implement all endpoints** — resolved
  - [ ] `GET /api/entities/scenarios/{scenarioId}/entities` — returns entities for a scenario
  - [ ] `GET /api/entities/{entityId}` — returns entity by ID
  - [ ] `POST /api/entities/scenarios/{scenarioId}/entities` — creates a new entity (returns 201)

**Expected Features:**
- Validation that scenario exists before creating entity
- Coordinate validation (latitude: -90 to 90, longitude: -180 to 180)
- EntityType and TaskForce validation against allowed values
- Proper DTO mapping (EntityDto)

#### 2. DTOs (Data Transfer Objects)

##### `Frontend-Oriented/backend/Backend.Api/DTOs/CreateScenarioRequest.cs`
- [ ] **TODO: Add validation attributes as needed** — resolved
  - Name: Required, min/max length
  - Description: Optional, max length

##### `Frontend-Oriented/backend/Backend.Api/DTOs/CreateEntityRequest.cs`
- [ ] **TODO: Add validation attributes as needed** — resolved
  - EntityType: Required, must be valid enum value
  - TaskForce: Required, must be valid enum value
  - Name/Callsign: Required
  - Latitude: Required, Range(-90, 90)
  - Longitude: Required, Range(-180, 180)

##### `Frontend-Oriented/backend/Backend.Api/DTOs/ScenarioListItemDto.cs`
- [ ] **TODO: Add validation attributes as needed** — verify all properties properly mapped

##### `Frontend-Oriented/backend/Backend.Api/DTOs/ScenarioDetailsDto.cs`
- [ ] **TODO: Add validation attributes as needed** — verify all properties properly mapped

##### `Frontend-Oriented/backend/Backend.Api/DTOs/EntityDto.cs`
- [ ] **TODO: Add validation attributes as needed** — verify all properties properly mapped

##### `Frontend-Oriented/backend/Backend.Api/DTOs/ErrorResponse.cs`
- [ ] **TODO: Consider adding more fields and validation attributes as needed** — verify error response format

#### 3. Program Configuration

##### `Frontend-Oriented/backend/Backend.Api/Program.cs`
- [ ] **TODO: Configure CORS for frontend communication** — resolved
  - CORS policy registered with `AllowFrontend` name
  - Frontend origin is allowed
  - CORS middleware added before UseAuthorization

### Frontend Implementation

The candidate built the frontend from scratch. There is no prescribed file layout — verify the architecture they chose and that it satisfies all requirements.

#### Project Structure & Architecture

- [ ] Project compiles and runs without errors
- [ ] Frontend is written in React or Angular (candidate specified which)
- [ ] Clear separation between pages, components, and service/API layer
- [ ] TypeScript used (recommended)
- [ ] All required screens are present and navigable

#### Required Screens

- [ ] **Scenario List** — displays all scenarios with navigation to details and a create action
- [ ] **Create Scenario** — form with name (required) and description (optional)
- [ ] **Scenario Details** — displays scenario info and a list/view of its entities
- [ ] **Create Entity** — form with EntityType, TaskForce, Name/Callsign, Latitude, Longitude

#### API Integration

- [ ] Scenario service implemented: listScenarios, createScenario
- [ ] Entity service implemented: getEntitiesByScenario, createEntity
- [ ] Search service implemented: search across scenarios and entities
- [ ] All services make real HTTP calls to the backend (no stubs remaining)
- [ ] Error responses from backend are handled and displayed to the user

#### Filtering & Sorting

- [ ] Scenarios can be filtered by name/description
- [ ] Entities can be filtered by EntityType and TaskForce
- [ ] Entities can be sorted by relevant fields (name, type, coordinates, date)

#### Search Functionality

- [ ] Global search bar available in navigation
- [ ] Searches across scenarios and entities
- [ ] Results displayed in a meaningful way (e.g. dropdown or results page)
- [ ] Clicking a result navigates to the correct page

#### Map View

- [ ] Map view available on scenario details page
- [ ] Entities are plotted on the map using their latitude/longitude
- [ ] Toggle between table and map view
- [ ] Filtering applies to map view

#### UX Polish

- [ ] Toast notifications for successful operations and errors
- [ ] Loading states shown during data fetching
- [ ] Empty states shown when lists have no data
- [ ] Form submit button disabled during submission
- [ ] Active route highlighted in navigation

#### Validation

- [ ] Client-side validation on Create Scenario form (name required, length limits)
- [ ] Client-side validation on Create Entity form (all fields, coordinate ranges, enum values)
- [ ] Server-side validation errors displayed per-field

## Functional Testing Checklist

### Scenario Management

1. **Create Scenario**
   - [ ] Navigate to "Create Scenario" page
   - [ ] Fill in name (required field)
   - [ ] Fill in description (optional)
   - [ ] Submit form
   - [ ] Verify success toast notification appears
   - [ ] Verify redirect to scenario details or list
   - [ ] Verify scenario appears in scenario list

2. **View Scenario List**
   - [ ] Navigate to scenarios list
   - [ ] Verify loading state appears initially
   - [ ] Verify scenarios are displayed in table/list
   - [ ] Verify entity count is shown for each scenario
   - [ ] Verify navigation to scenario details works

3. **View Scenario Details**
   - [ ] Click on a scenario
   - [ ] Verify scenario name and description are displayed
   - [ ] Verify entities list/table is shown
   - [ ] Verify empty state if no entities exist

### Entity Management

4. **Create Entity**
   - [ ] Navigate to scenario details
   - [ ] Click create entity action
   - [ ] Fill in EntityType (Soldier, Tank, Drone, Aircraft, Vehicle, Civilian)
   - [ ] Fill in TaskForce (Friendly/Enemy)
   - [ ] Fill in Name/Callsign
   - [ ] Fill in Latitude (-90 to 90)
   - [ ] Fill in Longitude (-180 to 180)
   - [ ] Submit form
   - [ ] Verify success toast notification
   - [ ] Verify entity appears in entities list
   - [ ] Verify TaskForce is displayed in list

5. **Entity Validation**
   - [ ] Invalid latitude (>90 or <-90) — shows error
   - [ ] Invalid longitude (>180 or <-180) — shows error
   - [ ] Missing required fields — shows validation errors
   - [ ] Real-time validation feedback

### Filtering & Sorting

6. **Scenario List Filtering**
   - [ ] Filter scenarios by name
   - [ ] Filter scenarios by description
   - [ ] Results update correctly

7. **Entities Table Filtering**
   - [ ] Filter entities by EntityType
   - [ ] Filter entities by TaskForce
   - [ ] Filtered results are correct

8. **Entities Table Sorting**
   - [ ] Sort by Name (ascending/descending)
   - [ ] Sort by Type
   - [ ] Sort by Latitude
   - [ ] Sort by Longitude
   - [ ] Sort by Last Updated
   - [ ] Sorting works correctly

### Search Functionality

9. **Global Search**
   - [ ] Use search bar in navigation
   - [ ] Search for scenario by name — verify results
   - [ ] Search for scenario by description — verify results
   - [ ] Search for entity by name/callsign — verify results
   - [ ] Clicking result navigates to correct page

### Data Visualization

10. **Map View**
    - [ ] Toggle to map view from entities view
    - [ ] Entities displayed on map using coordinates
    - [ ] Filter by EntityType — map updates
    - [ ] Filter by TaskForce — map updates
    - [ ] Toggle back to table view

### UX Polish

11. **Toast Notifications**
    - [ ] Success toast on create operations
    - [ ] Error toast on failures
    - [ ] Toast auto-dismisses or has close button

12. **Loading States**
    - [ ] Spinners or skeleton loaders during data fetch
    - [ ] Button spinners during form submission

13. **Empty States**
    - [ ] Empty state when no scenarios exist
    - [ ] Empty state when no entities exist
    - [ ] Empty states have action buttons (e.g., "Create Scenario")

14. **Form Validation**
    - [ ] Real-time validation feedback
    - [ ] Submit button disabled when form is invalid

15. **Navigation**
    - [ ] Active route highlighted
    - [ ] Navigation links work correctly

### Error Handling

16. **Backend Errors**
    - [ ] Invalid data — 400 errors handled and displayed
    - [ ] Non-existent IDs — 404 errors handled
    - [ ] Network errors — user-friendly error messages

### Docker (If Implemented)

17. **Docker Setup**
    - [ ] `docker-compose.yml` exists
    - [ ] Dockerfile for backend exists
    - [ ] Dockerfile for frontend exists (multi-stage build)
    - [ ] `docker-compose up` starts all services successfully
    - [ ] CORS is configured correctly
    - [ ] Environment variables used for API base URL
    - [ ] Full application flow works in Docker

## Acceptance Criteria Verification

- [ ] User can create a scenario and see it in the scenario list
- [ ] User can open scenario details and view entities belonging to that scenario only
- [ ] User can select TaskForce (Friendly/Enemy) when creating entities, and TaskForce is displayed in entity lists
- [ ] User can filter and sort entities by Type, TaskForce, and other fields
- [ ] User can search for scenarios and entities
- [ ] User can view entities on a map and toggle between table and map views
- [ ] Toast notifications appear for successful operations and errors
- [ ] Enhanced loading and empty states are displayed appropriately
- [ ] Frontend validates all input fields (coordinates, types, required fields)
- [ ] Backend validates all input fields and returns appropriate error responses
- [ ] Backend returns proper HTTP status codes for all operations
- [ ] Data persistence works (in-memory or database)
- [ ] Solution runs locally
- [ ] Solution runs in Docker using docker-compose up (if implemented)
- [ ] Clean architecture used
- [ ] Code quality and maintainability (structure, naming, separation of concerns)
- [ ] Validation and error handling are properly implemented

## Notes for Tester

- The candidate built the frontend from scratch — there is no prescribed folder layout, but it should be inside `Frontend-Oriented/`
- The candidate should have chosen either React OR Angular — not both
- The candidate implemented the full backend stack on top of the provided foundation: repositories (data access), controllers (API layer), DTO validation, and CORS
- No `throw new NotImplementedException()` should remain in ScenarioRepository or EntityRepository
- No `return null;` or unimplemented method bodies should remain in ScenariosController or EntitiesController
- If using database, verify migrations are included or setup instructions are provided
- Verify that the solution works end-to-end without errors

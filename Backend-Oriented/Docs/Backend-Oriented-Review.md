# Backend-Oriented Assignment - Tester Review Guide

## Overview
This assignment focuses on backend implementation built from scratch. The candidate must:
- Design and implement the data access layer (repositories) with chosen storage (Database or In-Memory)
- Design and implement the application architecture (optionally with an Application/Service layer)
- Implement API controllers with proper error handling, validation, and return codes
- Implement search, filtering, and sorting functionality
- Connect the backend to one of the provided frontend skeletons (React or Angular)

## Setup Verification

### Prerequisites
- .NET 8.0 SDK installed
- Node.js and npm installed
- Database (if candidate chose database over in-memory) - SQL Server, PostgreSQL, SQLite, etc.
- Docker (optional, for containerized testing)

### Running the Project

1. **Backend Setup** (candidate's own implementation — look for a `backend/` folder inside `Backend-Oriented/`):
   ```bash
   cd Backend-Oriented/backend
   dotnet restore
   dotnet run --project Backend.Api
   ```
   - Backend should run on `https://localhost:5001` or `http://localhost:5000`
   - Swagger UI should be available at `/swagger`
   - If using database, verify connection string in `appsettings.json`

2. **Frontend Setup (the chosen skeleton):**
   - **React:**
     ```bash
     cd Backend-Oriented/frontend/fronntend-react
     npm install
     npm run dev
     ```
   - **Angular:**
     ```bash
     cd Backend-Oriented/frontend/frontend-angular
     npm install
     ng serve
     ```

3. **Docker (if implemented):**
   ```bash
   docker-compose up
   ```

## Code Review Checklist

### Backend Implementation

The candidate built the backend from scratch. There is no prescribed file layout — verify the architecture they chose and that it satisfies all requirements.

#### 1. Project Structure & Architecture

- [ ] Project compiles and runs without errors
- [ ] Clean architecture is evident: at minimum an API layer and an Infrastructure/Data layer
- [ ] Domain models exist for: Scenario, Entity, EntityType (enum), TaskForce (enum)
- [ ] Data rules are encoded in the domain or enforced at the API level:
  - EntityType: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian
  - TaskForce: Friendly, Enemy
  - Latitude: -90 to 90
  - Longitude: -180 to 180
  - Entities always belong to exactly one Scenario
- [ ] Optional but recommended: Application/Service layer separating business logic from controllers

#### 2. Data Access Layer

- [ ] ScenarioRepository is implemented (not throwing NotImplementedException)
- [ ] EntityRepository is implemented (not throwing NotImplementedException)
- [ ] Chosen storage approach is clear (In-Memory or Database)
- [ ] If using In-Memory: thread-safe implementation (e.g. `List<T>` with lock, or `ConcurrentDictionary`)
- [ ] If using Database: DbContext configured, migrations included, connection string in appsettings.json
- [ ] Repositories implement: GetByIdAsync, GetAllAsync, AddAsync, and entity-specific queries (e.g. GetByScenarioIdAsync)
- [ ] Entity count per scenario is available (used by scenario list endpoint)

#### 3. API Controllers

##### ScenariosController
- [ ] `GET /api/scenarios` — returns list of scenarios
  - [ ] Filtering by name and description (server-side)
  - [ ] Sorting by name, description, entityCount, updatedAt (server-side)
  - [ ] Entity count is included in each list item
- [ ] `GET /api/scenarios/{scenarioId}` — returns scenario details
  - [ ] Returns 404 if scenario not found
- [ ] `POST /api/scenarios` — creates a new scenario
  - [ ] Returns 201 Created with location header
  - [ ] Returns 400 BadRequest for validation errors
- [ ] Proper HTTP status codes throughout
- [ ] ErrorResponse DTO returned on errors
- [ ] Logging implemented for errors

##### EntitiesController
- [ ] `GET /api/entities/scenarios/{scenarioId}/entities` — returns entities for a scenario
  - [ ] Filtering by EntityType and TaskForce (server-side)
  - [ ] Sorting by name, type, taskForce, latitude, longitude, updatedAt (server-side)
- [ ] `GET /api/entities/{entityId}` — returns entity by ID
  - [ ] Returns 404 if entity not found
- [ ] `POST /api/entities/scenarios/{scenarioId}/entities` — creates a new entity
  - [ ] Returns 201 Created with location header
  - [ ] Returns 400 BadRequest for validation errors
  - [ ] Returns 404 NotFound if scenario does not exist
- [ ] Coordinate validation: latitude -90 to 90, longitude -180 to 180
- [ ] EntityType and TaskForce validation against allowed values
- [ ] Proper HTTP status codes throughout

##### SearchController
- [ ] `GET /api/search?query=...` — global search across scenarios and entities
  - [ ] Searches scenario name and description
  - [ ] Searches entity name/callsign
  - [ ] Returns combined SearchResultDto with Scenarios and Entities arrays
  - [ ] Case-insensitive search
  - [ ] Partial match search
  - [ ] Handles empty query (returns empty results or 400)

#### 4. DTOs & Validation

- [ ] Request DTOs have validation attributes (Required, Range, MaxLength, etc.)
- [ ] CreateScenarioRequest: Name required, Description optional
- [ ] CreateEntityRequest: EntityType required, TaskForce required, Name required, Latitude Range(-90,90), Longitude Range(-180,180)
- [ ] Response DTOs cover all required fields for each endpoint
- [ ] ErrorResponse DTO properly structured (Message, optional Errors dictionary)

#### 5. Program Configuration

- [ ] Repository implementations are registered in DI container
- [ ] If using database, DbContext is registered and migrations applied
- [ ] CORS is configured to allow frontend origin
- [ ] Swagger is configured for development

### Frontend Implementation (Skeleton TODOs)

The candidate chose one of the provided skeletons. The skeleton already has most functionality pre-implemented (services, listing pages, details pages, entity creation form). The candidate's required TODO work is limited to the Create Scenario page. Verify the chosen skeleton.

**Pre-implemented in the skeleton (verify they still work end-to-end with the candidate's backend):**
- `ScenarioListPage` / `scenario-list` — data fetching, loading/error/empty states, sorting, filtering all wired up
- `ScenarioDetailsPage` / `scenario-details` — entity listing with filtering, sorting, and map view
- `CreateEntityPage` / `create-entity` — full entity creation with client-side validation
- `entityService` / `entity.service` — all entity API calls implemented
- `searchService` / `search.service` — global search API call implemented
- Global search bar in navigation — calls backend search endpoint

**Candidate must complete (TODOs in code):**

#### React Skeleton (`Backend-Oriented/frontend/fronntend-react/`)

##### `src/services/scenarioService.ts`
- [ ] **TODO: Implement real API call** — `listScenarios()` makes a real fetch call to `GET /api/scenarios` with filter/sort query params
- [ ] **TODO: Implement real API call** — `createScenario()` makes a real fetch call to `POST /api/scenarios`
  - Handles error responses appropriately
  - Returns proper `Scenario` type on success

##### `src/pages/CreateScenarioPage.tsx`
- [ ] **TODO: Implement real validation rules** — length constraints added (min/max length for name)
- [ ] **TODO: Implement submit behavior** — calls `scenarioService.createScenario()`, button disabled during submit, navigates to scenario details on success
- [ ] **TODO: Display server validation messages** — server-side field errors parsed and displayed per field

#### Angular Skeleton (`Backend-Oriented/frontend/frontend-angular/`)

##### `src/services/scenario.service.ts`
- [ ] **TODO: Implement real API call** — `listScenarios()` makes a real HttpClient call to `GET /api/scenarios` with filter/sort query params via `HttpParams`
- [ ] **TODO: Implement real API call** — `createScenario()` makes a real HttpClient call to `POST /api/scenarios`
  - Handles errors with RxJS operators
  - Returns `Observable<Scenario>`

##### `src/pages/create-scenario/create-scenario.component.ts`
- [ ] **TODO: Implement real validation rules** — `Validators.minLength`, `Validators.maxLength` added to form group
- [ ] **TODO: Implement submit behavior** — calls `scenarioService.createScenario().subscribe(...)`, form disabled during submit, navigates to scenario details on success
- [ ] **TODO: Display server validation messages** — server-side field errors parsed and displayed per field

## Functional Testing Checklist

### Backend API Testing

1. **Scenario Endpoints**
   - [ ] `GET /api/scenarios` — returns list of scenarios
   - [ ] `GET /api/scenarios?name=test` — filters by name
   - [ ] `GET /api/scenarios?description=test` — filters by description
   - [ ] `GET /api/scenarios?sortBy=name&sortDirection=asc` — sorts scenarios
   - [ ] `GET /api/scenarios/{scenarioId}` — returns scenario details
   - [ ] `GET /api/scenarios/{invalidId}` — returns 404
   - [ ] `POST /api/scenarios` — creates scenario (returns 201)
   - [ ] `POST /api/scenarios` with invalid data — returns 400

2. **Entity Endpoints**
   - [ ] `GET /api/entities/scenarios/{scenarioId}/entities` — returns entities for scenario
   - [ ] `GET /api/entities/scenarios/{scenarioId}/entities?entityType=Soldier` — filters by type
   - [ ] `GET /api/entities/scenarios/{scenarioId}/entities?taskForce=Friendly` — filters by task force
   - [ ] `GET /api/entities/scenarios/{scenarioId}/entities?sortBy=name` — sorts entities
   - [ ] `GET /api/entities/{entityId}` — returns entity details
   - [ ] `GET /api/entities/{invalidId}` — returns 404
   - [ ] `POST /api/entities/scenarios/{scenarioId}/entities` — creates entity (returns 201)
   - [ ] `POST /api/entities/scenarios/{invalidId}/entities` — returns 404 (scenario not found)
   - [ ] `POST /api/entities/scenarios/{scenarioId}/entities` with invalid coordinates — returns 400
   - [ ] `POST /api/entities/scenarios/{scenarioId}/entities` with invalid EntityType — returns 400

3. **Search Endpoint**
   - [ ] `GET /api/search?query=test` — returns matching scenarios and entities
   - [ ] `GET /api/search?query=` — returns empty results or 400
   - [ ] Scenarios matched by name and description
   - [ ] Entities matched by name/callsign
   - [ ] Search is case-insensitive
   - [ ] Partial matches work

4. **Data Persistence**
   - [ ] If using database: create scenario, restart backend, verify scenario still exists
   - [ ] If using in-memory: data resets on restart (expected behavior)

### Frontend Testing

5. **Scenario Management**
   - [ ] Create scenario through UI
   - [ ] View scenario list with filtering
   - [ ] View scenario details
   - [ ] Entity count displayed correctly

6. **Entity Management**
   - [ ] Create entity through UI
   - [ ] View entities with filtering by Type and TaskForce
   - [ ] View entities with sorting
   - [ ] Coordinate validation works

7. **Search Functionality**
   - [ ] Global search bar in navigation works
   - [ ] Search finds scenarios and entities
   - [ ] Results are highlighted
   - [ ] Clicking a result navigates to correct page

8. **Filtering & Sorting**
   - [ ] Filter scenarios by name/description
   - [ ] Filter entities by Type/TaskForce
   - [ ] Sort entities by various fields
   - [ ] Filters and sorts work together

9. **Map View**
   - [ ] Toggle to map view on scenario details
   - [ ] Entities displayed on map at correct coordinates
   - [ ] Filtering updates the map
   - [ ] Click entity on map shows details

10. **Error Handling**
    - [ ] Invalid data — validation error messages shown
    - [ ] Network errors — user-friendly messages shown
    - [ ] 404 errors — properly handled

### Docker (If Implemented)

11. **Docker Setup**
    - [ ] `docker-compose.yml` exists
    - [ ] Dockerfile for backend exists
    - [ ] Dockerfile for frontend exists (multi-stage build)
    - [ ] If using database: docker-compose includes database service
    - [ ] `docker-compose up` starts all services successfully
    - [ ] Backend connects to database (if applicable)
    - [ ] CORS is configured correctly
    - [ ] Environment variables used for API base URL
    - [ ] Full application flow works in Docker

## Acceptance Criteria Verification

- [ ] User can create a scenario and see it in the scenario list
- [ ] User can open scenario details and view entities belonging to that scenario only
- [ ] User can select TaskForce (Friendly/Enemy) when creating entities, and TaskForce is displayed in entity lists
- [ ] User can filter scenarios by name/description (server-side filtering works)
- [ ] User can filter entities by Type and TaskForce (server-side filtering works)
- [ ] User can sort scenarios and entities by various fields (server-side sorting works)
- [ ] User can search for scenarios and entities using the global search endpoint
- [ ] User can view entities on a map and toggle between table and map views
- [ ] Toast notifications appear for successful operations and errors
- [ ] Loading and empty states are displayed appropriately
- [ ] Frontend validates all input fields (coordinates, types, required fields)
- [ ] Backend validates all input fields and returns appropriate error responses
- [ ] Backend returns proper HTTP status codes for all operations
- [ ] Data persistence works (database or in-memory as chosen)
- [ ] Solution runs locally
- [ ] Solution runs in Docker using docker-compose up (if implemented)
- [ ] Clean architecture used (Domain, Infrastructure, API layers)
- [ ] Code quality and maintainability (structure, naming, separation of concerns)
- [ ] Validation and error handling are properly implemented

## Notes for Tester

- The candidate built the backend from scratch — there is no prescribed folder layout, but it should be inside `Backend-Oriented/`
- The candidate should have chosen either React OR Angular — not both
- Search functionality (SearchController) is mandatory in Backend-Oriented
- Application/Service layer is optional but recommended for clean architecture
- If using database, verify migrations are included or setup instructions are provided
- Verify that the solution works end-to-end without errors

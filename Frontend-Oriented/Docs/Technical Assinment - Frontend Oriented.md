# Technical Test – Full-Stack Developer (Frontend Oriented)

Dear candidate,

As part of the recruitment process at Bagira Systems, you are required to complete this assignment. You will receive the assignment at the time of your choice (e.g., 9 AM) and must submit your solution within 24 hours (e.g., by 9 AM the next day). Please read the description carefully before starting.

## Objectives

Complete the provided backend API layer, then build a frontend application from scratch for a small web application used to create training scenarios and manage entities inside each scenario.

## Instructions

1. Clone this repository – Bagira-Assignment and create a fork with the following structure: `BagiraAssignment_<YourName>`.
2. Work inside the `Frontend-Oriented` folder.
3. The backend contains pre-implemented domain models, interfaces, and in-memory repositories. You must implement the API controllers and complete the backend wiring.
4. Choose either the React or Angular frontend skeleton in the `frontend/` folder and build your application from there. Specify which one you chose.
5. Follow the README in the chosen frontend skeleton folder for setup instructions.

## Requirements

### 1. Data Rules

- `EntityType` must be one of: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian.
- `TaskForce` must be one of: Friendly, Enemy.
- Latitude must be between -90 and 90.
- Longitude must be between -180 and 180.
- Entities always belong to exactly one Scenario (no orphan entities).

### 2. Screens

- **Scenario List** – view all scenarios, navigate to details, create.
- **Create Scenario** – form with name (required) and description (optional).
- **Scenario Details** – view scenario info and manage its entities.
- **Create Entity** – form with EntityType, TaskForce, Name/Callsign, Latitude, Longitude.

### 3. Backend

#### Already provided – do not modify:
- Domain models: `Scenario`, `Entity`, `EntityType`, `TaskForce`
- Repository interfaces: `IRepository`, `IScenarioRepository`, `IEntityRepository`
- Repository implementations: `ScenarioRepository`, `EntityRepository` (in-memory)
- DTO structure: `CreateScenarioRequest`, `CreateEntityRequest`, `ScenarioListItemDto`, `ScenarioDetailsDto`, `EntityDto`, `ErrorResponse`
- DTO validation attributes on all request DTOs (required fields, coordinate ranges, enum constraints)
- Server-side validation wired up — invalid requests automatically return `400` with an `ErrorResponse` body

#### You must implement:
- **API Controllers** – implement all endpoint stubs in `ScenariosController` and `EntitiesController`.
- **Error Handling** – proper HTTP status codes; return `ErrorResponse` on 404.
- **CORS** – configure CORS in `Program.cs` to allow requests from your frontend origin.

### 4. Frontend

Build your application inside the chosen skeleton (`frontend/frontend-react` or `frontend/frontend-angular`).

- Implement all four required screens.
- Build a service/API abstraction layer (architecture is your decision).
- Form submission with real-time validation and display of server-side error messages.
- Filtering: scenarios by name/description, entities by EntityType and TaskForce.
- Loading states, empty states, and error states on all data-fetching views.

### 5. Docker

Containerize the solution using Docker:

- Create a `Dockerfile` for the backend.
- Create a `Dockerfile` for the frontend (use a multi-stage build: build then serve).
- Create a `docker-compose.yml` that runs both services together.
- Use environment variables for the API base URL in the frontend and CORS origins in the backend.
- Update the main README with instructions to run via `docker-compose up`.

> **Note:** If Docker takes more time than expected, write a short explanation in the README describing how you would containerize the solution and move on — a written explanation is acceptable and demonstrates the same understanding.

### 6. Optional Bonus

- Connect to a database of your choice (replace in-memory repositories).
- Map view for entities — display entities on a map using their coordinates.
- Global search — search across scenarios and entities from a single search bar.
- Server-side sorting for scenarios and entities.
- Add tests for validation (backend) or key UI flows (frontend).
- Implement Update and Delete for Scenarios and Entities end-to-end.
- UX improvements.

## Acceptance Criteria

- User can create a scenario and see it in the scenario list.
- User can open scenario details and view only the entities belonging to that scenario.
- User can select TaskForce (Friendly/Enemy) when creating entities; TaskForce is displayed in entity lists.
- User can filter entities by Type and TaskForce.
- User can filter scenarios by name/description.
- Loading, empty, and error states are displayed appropriately.
- Frontend validates all input fields (coordinates, types, required fields).
- Backend returns proper HTTP status codes for all operations (validation errors return 400 automatically; 404 must be returned when a resource is not found).
- Solution runs locally.
- Clean architecture; code quality and maintainability.
- Validation and error handling are properly implemented.

## Please Provide

- A link to your branch.
- Short notes (bullet points) describing what you completed and any trade-offs.

**Good Luck!**

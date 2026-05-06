# Technical Test – Full-Stack Developer (Backend Oriented)

Dear candidate,

As part of the recruitment process at Bagira Systems, you are required to complete this assignment. You will receive the assignment at the time of your choice (e.g., 9 AM) and must submit your solution within 24 hours (e.g., by 9 AM the next day). Please read the description carefully before starting.

## Objectives

Design and implement a backend from scratch, then connect it to the provided frontend skeleton.

## Instructions

1. Clone this repository – Bagira-Assignment and create a fork with the following structure: `BagiraAssignment_<YourName>`.
2. Work inside the `Backend-Oriented` folder.
3. A minimal backend starter is provided in the `backend/` folder — a single `Backend.Api` project with a working `Program.cs`. Build your backend from there.
4. Choose either the React or Angular frontend skeleton in the `frontend/` folder and connect your backend to it. Specify which one you chose.
5. Follow the README in the `backend/` and chosen `frontend/` folders for setup instructions.

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

The `backend/Backend.Api` project is pre-configured with controllers and Swagger. Build your full backend from there — architecture, domain models, DTOs, and project structure are your design decision.

You must implement:

- **Domain models** — Scenario, Entity, and any enums required by the data rules.
- **Data Access Layer** — implement `ScenarioRepository` and `EntityRepository` with your chosen storage (in-memory or database).
- **API Controllers** — `ScenariosController` and `EntitiesController` with all required endpoints.
- **Server-side filtering** — scenarios by name/description, entities by EntityType and TaskForce.
- **DTO validation** — validate all input fields; return proper HTTP status codes and an `ErrorResponse` on 400/404.
- **CORS** — configure CORS in `Program.cs` to allow requests from your frontend origin.

### 4. Frontend

Choose one of the provided skeletons (`frontend/fronntend-react` or `frontend/frontend-angular`) and complete the TODOs inside it to connect your backend APIs. Follow the README in the chosen folder.

### 5. Docker

Containerize the solution using Docker:

- Create a `Dockerfile` for the backend.
- Create a `Dockerfile` for the frontend (use a multi-stage build: build then serve).
- Create a `docker-compose.yml` that runs both services together.
- Use environment variables for the API base URL in the frontend and CORS origins in the backend.
- Update the main README with instructions to run via `docker-compose up`.

> **Note:** If Docker takes more time than expected, write a short explanation in the README describing how you would containerize the solution and move on — a written explanation is acceptable and demonstrates the same understanding.

### 6. Optional Bonus

- Connect to a database of your choice (instead of in-memory storage).
- Global search endpoint (`SearchController`) — search across scenarios and entities by name.
- Server-side sorting for scenarios and entities.
- Add tests for validation (backend) or key UI flows (frontend).
- Implement Update and Delete for Scenarios and Entities end-to-end.
- UX improvements.

## Acceptance Criteria

- User can create a scenario and see it in the scenario list.
- User can open scenario details and view only the entities belonging to that scenario.
- User can select TaskForce (Friendly/Enemy) when creating entities; TaskForce is displayed in entity lists.
- User can filter scenarios by name/description (server-side).
- User can filter entities by Type and TaskForce (server-side).
- Backend validates all input fields and returns appropriate error responses.
- Backend returns proper HTTP status codes for all operations.
- Data persistence works correctly (in-memory or database).
- Solution runs locally.
- Clean architecture; code quality and maintainability.
- Validation and error handling are properly implemented.

## Please Provide

- A link to your branch.
- Short notes (bullet points) describing what you completed and any trade-offs.

**Good Luck!**

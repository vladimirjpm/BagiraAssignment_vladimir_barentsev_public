# Scenario Builder - Backend API

This is the backend for the Scenario Builder application built with ASP.NET Web API. It provides domain models, repositories, DTOs with validation, and controller stubs. Your task is to implement the controller logic and configure CORS.

## Setup

**Prerequisites:**
   - .NET 8.0 SDK or later
   - Your preferred IDE (Visual Studio, Rider, VS Code)

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `http://localhost:5000/swagger` or `https://localhost:5001/swagger`

## Project Structure

```
backend/
├── Backend.Api/              # Web API project
│   ├── Controllers/          # API controllers
│   │   ├── ScenariosController.cs
│   │   └── EntitiesController.cs
│   ├── DTOs/                # Data Transfer Objects (temporary location)
│   │   ├── Scenario DTOs
│   │   └── Entity DTOs
│   └── Program.cs           # Application entry point
├── Backend.Domain/           # Domain layer
│   └── Models/              # Domain models and enums
│       ├── Scenario.cs
│       ├── Entity.cs
│       ├── EntityType.cs
│       └── TaskForce.cs
└── Backend.Infrastructure/  # Infrastructure layer
    ├── Interfaces/          # Repository interfaces
    │   ├── IRepository.cs
    │   ├── IScenarioRepository.cs
    │   └── IEntityRepository.cs
    └── Repositories/        # Repository implementations (in-memory)
        ├── ScenarioRepository.cs
        └── EntityRepository.cs
```

## API Endpoints

### Scenarios
- `GET /api/scenarios` - Get all scenarios
- `GET /api/scenarios/{scenarioId}` - Get scenario by ID
- `POST /api/scenarios` - Create a new scenario

### Entities
- `GET /api/scenarios/{scenarioId}/entities` - Get all entities for a scenario
- `GET /api/entities/{entityId}` - Get entity by ID
- `POST /api/scenarios/{scenarioId}/entities` - Create a new entity

**Note:** Update and Delete operations have been removed from the skeleton. The application supports Create and Read operations only. Update and Delete operations are Bonus

## What's Implemented

- ✅ Complete API controller structure with Create and Read endpoint stubs
- ✅ DTOs for all requests and responses
- ✅ Domain models (Scenario, Entity) with enums (EntityType, TaskForce)
- ✅ Repository interfaces (IRepository, IScenarioRepository, IEntityRepository)
- ✅ In-memory repository implementations (ScenarioRepository and EntityRepository)
- ✅ **DTO validation attributes** — `[Required]`, `[Range]`, `[MaxLength]`, `[EnumDataType]` on all request DTOs
- ✅ **Automatic 400 responses** — invalid requests return `ErrorResponse` with field-level errors (no extra code needed)
- ✅ **Enum string deserialization** — frontend can send `"Soldier"`, `"Friendly"` etc. as strings
- ✅ Error response shape (ErrorResponse)
- ✅ Swagger/OpenAPI configuration
- ✅ Dependency injection setup for repositories

## What Needs to be Implemented

1. **API Controllers** — implement the endpoint stubs in `ScenariosController` and `EntitiesController` (look for `return null;`)
2. **Error Handling** — return `404 NotFound` (with an `ErrorResponse` body) when a scenario or entity is not found
3. **CORS** — configure CORS in `Program.cs` (look for the `TODO` comment) to allow requests from your frontend origin

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- Validate that a scenario exists before creating an entity under it (return `404` if it doesn't)
- The connection between HTTP status codes, DTO validation, and error responses is already wired — focus on the controller logic

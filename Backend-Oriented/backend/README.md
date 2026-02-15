# Scenario Builder - Backend API Skeleton

This is the backend skeleton for the Scenario Builder application built with ASP.NET Web API. It provides a complete API structure with controllers, DTOs, and repository interfaces, but leaves the implementation of business logic and the Application layer for the candidate.

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
│   │   ├── EntitiesController.cs
│   │   └── SearchController.cs
│   ├── DTOs/                # Data Transfer Objects (temporary location)
│   │   ├── Scenario DTOs
│   │   ├── Entity DTOs
│   │   ├── ScenarioFilterParams.cs
│   │   ├── EntityFilterParams.cs
│   │   └── SearchResultDto.cs
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
    └── Repositories/        # Repository implementations (stubs)
        ├── ScenarioRepository.cs
        └── EntityRepository.cs
```

## API Endpoints

### Scenarios
- `GET /api/scenarios` - Get all scenarios (supports filtering by name/description, sorting by name/description/entityCount/updatedAt)
- `GET /api/scenarios/{scenarioId}` - Get scenario by ID
- `POST /api/scenarios` - Create a new scenario

### Entities
- `GET /api/entities/scenarios/{scenarioId}/entities` - Get all entities for a scenario (supports filtering by type/taskForce, sorting by name/type/taskForce/latitude/longitude/updatedAt)
- `GET /api/entities/{entityId}` - Get entity by ID
- `POST /api/entities/scenarios/{scenarioId}/entities` - Create a new entity

### Search
- `GET /api/search?q={query}` - Global search across scenarios and entities

**Note:** Update and Delete operations have been removed from the skeleton. The application supports Create and Read operations only. Update and Delete operations are Bonus

## What's Implemented

- ✅ Complete API controller structure with Create and Read endpoints
- ✅ DTOs for all requests and responses (temporarily in API layer)
- ✅ Domain models (Scenario, Entity) with enums (EntityType, TaskForce) in Domain layer
- ✅ Repository interfaces (IRepository, IScenarioRepository, IEntityRepository)
- ✅ Repository stub implementations that throw NotImplementedException
- ✅ Basic validation attributes on DTOs
- ✅ Error response shape (ErrorResponse)
- ✅ Swagger/OpenAPI configuration
- ✅ Dependency injection setup for repositories
- ✅ Filter/Sort query parameter DTOs (ScenarioFilterParams, EntityFilterParams)
- ✅ SearchController with search endpoint stub
- ✅ SearchResultDto for combined search results
- ✅ Controller signatures accept filter/sort parameters
- ✅ Clear `TODO(candidate):` markers throughout

## What Needs to be Implemented

All functionality marked with `TODO(candidate):` comments needs to be implemented:

1. **Repository Implementations** - Implement ScenarioRepository and EntityRepository with chosen data storage (In-Memory, Database, etc.)
2. **Controller Logic** - Implement all endpoint bodies (currently return null)
3. **Filtering & Sorting** - Implement server-side filtering and sorting logic in controllers
4. **Search Logic** - Implement the search endpoint in SearchController
5. **Validation** - Add proper validation attributes to DTOs and validate in controllers
6. **Error Handling** - Implement proper error handling with appropriate HTTP status codes

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- Latitude must be between -90 and 90
- Longitude must be between -180 and 180
- Entity types are fixed: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian
- TaskForce must be Friendly or Enemy

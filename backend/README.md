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

- ✅ Complete API controller structure with Create and Read endpoints
- ✅ DTOs for all requests and responses (temporarily in API layer)
- ✅ Domain models (Scenario, Entity) with enums (EntityType, TaskForce) in Domain layer
- ✅ Repository interfaces (IRepository, IScenarioRepository, IEntityRepository)
- ✅ **In-memory repository implementations** (ScenarioRepository and EntityRepository)
- ✅ **Entity count calculation** in scenario list endpoint
- ✅ Basic validation attributes on DTOs
- ✅ Error response shape (ErrorResponse)
- ✅ Swagger/OpenAPI configuration
- ✅ Dependency injection setup for repositories
- ✅ Clear `TODO(candidate):` markers throughout

**Note:** The repository implementations use in-memory storage (`List<T>`) with thread-safe operations. The candidate can replace this with a database implementation if desired, but it's not required for the assignment.

## What Needs to be Implemented

1. All functionality marked with `TODO(candidate):` comments needs to be implemented:
2. **Basic Validation**
3. **Proper Error Handling**

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- **Validation Note**: Most validation is handled client-side. The frontend validates:
  - Latitude must be between -90 and 90
  - Longitude must be between -180 and 180
  - Entity types are fixed: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian
  - TaskForce must be Friendly or Enemy
- Backend performs basic required field validation (handled by ModelState/Data Annotations)
- Backend must validate scenario existence when creating entities (business rule)

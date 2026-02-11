# Scenario Builder - Frontend Skeleton (Angular)

This is the frontend skeleton for the Scenario Builder application built with Angular. It provides a complete UI structure with routing, components, and service interfaces, but leaves the implementation of business logic for the candidate.

## Setup

The application will be available at `http://localhost:4200` (or the port shown in the terminal).

## Project Structure

```
src/
├── app/                    # Main app component and routing
│   ├── app.component.ts
│   ├── app.routes.ts
│   └── app.config.ts
├── components/             # Reusable UI components
│   ├── navigation/        # Main navigation component
│   └── ui/                # UI state components (Loading, Empty, Error, Success)
├── pages/                 # Page components
│   ├── scenario-list/
│   ├── create-scenario/
│   ├── scenario-details/
│   └── create-entity/
├── services/              # Service layer (stubs only)
│   ├── scenario.service.ts
│   └── entity.service.ts
├── types/                 # TypeScript type definitions
│   └── index.ts
└── main.ts                # Entry point
```

**Note:** Edit and Delete operations have been removed. The application supports Create and Read operations only. Update and Delete operations are Bonus

## What's Implemented

- ✅ Complete routing structure for all screens
- ✅ Navigation component with active route highlighting
- ✅ All page layouts and forms (UI structure only)
- ✅ Service layer interfaces with method signatures using RxJS Observables
- ✅ UI state placeholder components (Loading, Empty, Error, Success)
- ✅ TypeScript type definitions for Scenario and Entity
- ✅ Angular Reactive Forms setup
- ✅ Basic styling and layout

## What Needs to be Implemented

All functionality marked with `TODO(candidate):` comments needs to be implemented:

1. **Service Layer** - Implement API calls
2. **Data Fetching** - Load data and manage state
3. **Form Validation** - Client-side validation (coordinate ranges, required fields, EntityType, TaskForce)
4. **Error Handling** - Display errors and retry logic
5. **Filtering & Sorting** - Filter entities by Type/TaskForce, sort by Name/Type/Coordinates/Updated
6. **Search Functionality** - Search scenarios and entities
7. **UX Polish** - Toast notifications, enhanced loading/empty states
8. **Data Visualization** - Map view for entities (toggle between table and map)
9. **Navigation Guards** - Prevent orphan entity creation

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- **Validation** - Frontend must validate:
  - Latitude must be between -90 and 90
  - Longitude must be between -180 and 180
  - Entity types are fixed: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian
  - TaskForce must be Friendly or Enemy
- **Filtering & Sorting** - Implement client-side filtering and sorting for entities table
- **Search** - Global search for scenarios and entities
- **Map View** - Display entities on a map using coordinates (toggle with table view)
- **UX** - Toast notifications, skeleton loaders, enhanced empty states
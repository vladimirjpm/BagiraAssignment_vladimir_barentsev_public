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
│   ├── navigation/        # Main navigation component with global search
│   ├── entity-map-view/   # Leaflet map view for entities
│   └── ui/                # UI state components (Loading, Empty, Error, Success)
├── pages/                 # Page components
│   ├── scenario-list/     # With filtering and sortable columns
│   ├── create-scenario/
│   ├── scenario-details/  # With entity filtering, sorting, and map/table toggle
│   └── create-entity/
├── services/              # Service layer
│   ├── scenario.service.ts  # Supports filter/sort query params
│   ├── entity.service.ts    # Supports filter/sort query params
│   └── search.service.ts    # Global search service
├── types/                 # TypeScript type definitions
│   └── index.ts
└── main.ts                # Entry point
```

**Note:** Edit and Delete operations have been removed. The application supports Create and Read operations only. Update and Delete operations are Bonus

## What's Implemented

- ✅ Complete routing structure for all screens
- ✅ Navigation component with active route highlighting
- ✅ Global search bar in navigation (calls backend search endpoint, debounced with RxJS, dropdown results)
- ✅ All page layouts and forms (UI structure only)
- ✅ Scenario list page with name/description filter inputs and sortable column headers
- ✅ Scenario details page with entity type/taskForce filter dropdowns and sortable column headers
- ✅ Table/Map view toggle on scenario details (Leaflet map with colored markers by TaskForce)
- ✅ Service layer with filter/sort query parameter support using HttpParams
- ✅ Search service for global search
- ✅ Entity creation form with full client-side validation (Angular Reactive Forms)
- ✅ UI state placeholder components (Loading, Empty, Error, Success)
- ✅ TypeScript type definitions for Scenario and Entity
- ✅ Angular Reactive Forms setup
- ✅ Navigation guards for route validation
- ✅ Basic styling and layout

## What Needs to be Implemented

All functionality marked with `TODO(candidate):` comments needs to be implemented:

1. **Scenario Service** - Implement `listScenarios()` and `createScenario()` in `src/services/scenario.service.ts` to make real API calls to your backend
2. **Create Scenario Form** - Uncomment service call, implement proper validation, display server validation messages
3. **Error Handling** - Improve error handling across all pages (toast notifications, retry logic)
4. **UX Polish** - Add skeleton loaders, toast notifications, loading states during form submission
5. **Leaflet Dependency** - Install `leaflet` and `@types/leaflet` npm packages for map view to work

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- Latitude must be between -90 and 90
- Longitude must be between -180 and 180
- Entity types are fixed: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian

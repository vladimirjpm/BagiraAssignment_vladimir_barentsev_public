# Scenario Builder - Frontend Skeleton (React)

This is the frontend skeleton for the Scenario Builder application. It provides a complete UI structure with routing, components, and service interfaces, but leaves the implementation of business logic for the candidate.

## Setup

The application will be available at `http://localhost:5173` (or the port shown in the terminal).

## Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── ui/             # UI state components (Loading, Empty, Error, Success)
│   ├── Navigation.tsx  # Main navigation component with global search
│   └── EntityMapView.tsx # Leaflet map view for entities
├── pages/              # Page components
│   ├── ScenarioListPage.tsx    # With filtering and sortable columns
│   ├── CreateScenarioPage.tsx
│   ├── ScenarioDetailsPage.tsx # With entity filtering, sorting, and map/table toggle
│   └── CreateEntityPage.tsx
├── services/           # Service layer
│   ├── scenarioService.ts  # Supports filter/sort query params
│   ├── entityService.ts    # Supports filter/sort query params
│   └── searchService.ts    # Global search service
├── types/              # TypeScript type definitions
│   └── index.ts
├── App.tsx             # Main app component with routing
└── main.tsx            # Entry point
```

**Note:** Edit and Delete operations have been removed. The application supports Create and Read operations only. Update and Delete operations are Bonus

## What's Implemented

- ✅ Complete routing structure for all screens
- ✅ Navigation component with active route highlighting
- ✅ Global search bar in navigation (calls backend search endpoint, debounced, dropdown results)
- ✅ All page layouts and forms (UI structure only)
- ✅ Scenario list page with name/description filter inputs and sortable column headers
- ✅ Scenario details page with entity type/taskForce filter dropdowns and sortable column headers
- ✅ Table/Map view toggle on scenario details (Leaflet map with colored markers by TaskForce)
- ✅ Service layer with filter/sort query parameter support
- ✅ Search service for global search
- ✅ Entity creation form with full client-side validation
- ✅ UI state placeholder components (Loading, Empty, Error, Success)
- ✅ TypeScript type definitions for Scenario and Entity
- ✅ Navigation guards for route validation
- ✅ Basic styling and layout

## What Needs to be Implemented

All functionality marked with `TODO(candidate):` comments needs to be implemented:

1. **Create Scenario Form** - Uncomment service call, implement proper validation, display server validation messages
2. **Error Handling** - Improve error handling across all pages (toast notifications, retry logic)
3. **UX Polish** - Add skeleton loaders, toast notifications, loading states during form submission
4. **Leaflet Dependency** - Install `leaflet` and `@types/leaflet` npm packages for map view to work

## Key Requirements

- Entities must always belong to a Scenario (no orphan entities)
- Latitude must be between -90 and 90
- Longitude must be between -180 and 180
- Entity types are fixed: Soldier, Tank, Drone, Aircraft, Vehicle, Civilian

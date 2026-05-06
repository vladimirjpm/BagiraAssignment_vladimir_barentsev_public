# Frontend — Angular

This is the Angular starter skeleton for the Frontend-Oriented assignment.

## Getting started

```bash
npm install
ng serve
```

The app runs on `http://localhost:4200` by default.

## Environment variables

The API base URL is configured in `src/environments/environment.ts`:

```ts
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000'
};
```

Update `apiUrl` if your backend runs on a different port.

## What to implement

Build the full frontend application from here. See the assignment document for requirements.

Routing is configured in `src/app/app.routes.ts` — add your routes and components from there.
`HttpClient` is pre-configured in `app.config.ts` via `provideHttpClient()`.

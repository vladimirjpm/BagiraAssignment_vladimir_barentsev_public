/**
 * Base URL for all API calls.
 *
 * Hybrid wiring: defaults to an empty string, i.e. relative/same-origin requests
 * (`/api/...`). In dev a Vite proxy forwards `/api` to the backend; in Docker
 * nginx serves the SPA and proxies `/api` to the backend container — both
 * same-origin, so no CORS is needed. Set VITE_API_BASE_URL to point the SPA at a
 * cross-origin API (then backend CORS applies).
 */
export const API_BASE = import.meta.env.VITE_API_BASE_URL ?? '';

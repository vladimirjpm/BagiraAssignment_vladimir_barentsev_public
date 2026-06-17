# ---- Build stage ----
FROM node:20-alpine AS build
WORKDIR /app

# Install deps first for layer caching.
COPY package*.json ./
RUN npm ci

# Build the production bundle. VITE_API_BASE_URL is left empty so the SPA uses
# relative /api paths, which nginx proxies to the backend (same-origin, no CORS).
COPY . .
RUN npm run build

# ---- Runtime stage ----
FROM nginx:alpine AS runtime
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

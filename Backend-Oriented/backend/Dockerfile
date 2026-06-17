# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first so dotnet restore is cached unless they change.
COPY Backend.sln ./
COPY Backend.Domain/Backend.Domain.csproj Backend.Domain/
COPY Backend.Application/Backend.Application.csproj Backend.Application/
COPY Backend.Infrastructure/Backend.Infrastructure.csproj Backend.Infrastructure/
COPY Backend.Api/Backend.Api.csproj Backend.Api/
RUN dotnet restore Backend.Api/Backend.Api.csproj

# Copy the rest and publish.
COPY . .
RUN dotnet publish Backend.Api/Backend.Api.csproj -c Release -o /app /p:UseAppHost=false

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Listen on 8080 (no TLS inside the container; nginx fronts it).
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["dotnet", "Backend.Api.dll"]

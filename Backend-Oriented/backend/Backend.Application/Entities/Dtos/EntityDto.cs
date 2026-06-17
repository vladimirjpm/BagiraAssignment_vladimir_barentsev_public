using Backend.Domain.Enums;

namespace Backend.Application.Entities.Dtos;

/// <summary>
/// Entity as returned to the client. Shape matches the frontend's
/// <c>Entity</c> TypeScript interface (enums serialized as strings).
/// </summary>
public sealed record EntityDto(
    Guid Id,
    Guid ScenarioId,
    EntityType Type,
    TaskForce TaskForce,
    string Name,
    double Latitude,
    double Longitude,
    DateTimeOffset UpdatedAt);

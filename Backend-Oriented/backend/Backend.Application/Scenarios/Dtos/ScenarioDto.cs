namespace Backend.Application.Scenarios.Dtos;

/// <summary>
/// Scenario as returned to the client. Shape matches the frontend's
/// <c>Scenario</c> TypeScript interface (camelCase on the wire).
/// </summary>
public sealed record ScenarioDto(
    Guid Id,
    string Name,
    string? Description,
    int EntityCount,
    DateTimeOffset UpdatedAt);

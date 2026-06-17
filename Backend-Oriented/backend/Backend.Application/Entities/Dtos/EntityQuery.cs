using Backend.Domain.Enums;

namespace Backend.Application.Entities.Dtos;

/// <summary>
/// Server-side filter + sort parameters for the entity list.
/// Bound from the query string; all fields optional. Invalid enum values are
/// rejected by model binding (→ 400) before reaching the service.
/// </summary>
public sealed record EntityQuery
{
    public EntityType? Type { get; init; }
    public TaskForce? TaskForce { get; init; }
    public string? SortBy { get; init; }
    public string? SortOrder { get; init; }
}

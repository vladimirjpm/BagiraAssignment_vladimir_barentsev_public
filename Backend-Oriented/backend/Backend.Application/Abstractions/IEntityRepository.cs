using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Application.Abstractions;

/// <summary>
/// Persistence boundary for entities. Filtering by type/taskForce happens here so
/// it can be pushed into a SQL WHERE clause later without touching callers.
/// </summary>
public interface IEntityRepository
{
    Task<IReadOnlyList<Entity>> ListByScenarioAsync(
        Guid scenarioId, EntityType? type, TaskForce? taskForce, CancellationToken ct);

    /// <summary>Search entities across all scenarios by name (case-insensitive contains).</summary>
    Task<IReadOnlyList<Entity>> SearchByNameAsync(string name, CancellationToken ct);

    Task<Entity?> GetAsync(Guid id, CancellationToken ct);

    Task<int> CountByScenarioAsync(Guid scenarioId, CancellationToken ct);

    Task AddAsync(Entity entity, CancellationToken ct);

    /// <summary>Persist changes made to a previously-fetched entity instance.</summary>
    Task UpdateAsync(Entity entity, CancellationToken ct);

    Task DeleteAsync(Guid id, CancellationToken ct);

    /// <summary>Delete every entity belonging to a scenario (cascade on scenario delete).</summary>
    Task DeleteByScenarioAsync(Guid scenarioId, CancellationToken ct);
}

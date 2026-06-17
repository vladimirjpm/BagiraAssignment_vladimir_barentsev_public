using Backend.Application.Abstractions;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Infrastructure.Persistence;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// In-memory <see cref="IEntityRepository"/>. Filtering by scenario/type/taskForce
/// happens here so it can later be pushed into a SQL WHERE clause without touching callers.
/// </summary>
public sealed class InMemoryEntityRepository : IEntityRepository
{
    private readonly InMemoryStore _store;

    public InMemoryEntityRepository(InMemoryStore store) => _store = store;

    public Task<IReadOnlyList<Entity>> ListByScenarioAsync(
        Guid scenarioId, EntityType? type, TaskForce? taskForce, CancellationToken ct)
    {
        IEnumerable<Entity> query = _store.Entities.Values.Where(e => e.ScenarioId == scenarioId);

        if (type is not null)
            query = query.Where(e => e.Type == type.Value);

        if (taskForce is not null)
            query = query.Where(e => e.TaskForce == taskForce.Value);

        IReadOnlyList<Entity> result = query.ToList();
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Entity>> SearchByNameAsync(string name, CancellationToken ct)
    {
        IReadOnlyList<Entity> result = _store.Entities.Values
            .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Task.FromResult(result);
    }

    public Task<Entity?> GetAsync(Guid id, CancellationToken ct) =>
        Task.FromResult(_store.Entities.GetValueOrDefault(id));

    public Task<int> CountByScenarioAsync(Guid scenarioId, CancellationToken ct) =>
        Task.FromResult(_store.Entities.Values.Count(e => e.ScenarioId == scenarioId));

    public Task AddAsync(Entity entity, CancellationToken ct)
    {
        _store.Entities[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Entity entity, CancellationToken ct)
    {
        _store.Entities[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct)
    {
        _store.Entities.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task DeleteByScenarioAsync(Guid scenarioId, CancellationToken ct)
    {
        var ids = _store.Entities.Values
            .Where(e => e.ScenarioId == scenarioId)
            .Select(e => e.Id)
            .ToList();

        foreach (var id in ids)
            _store.Entities.TryRemove(id, out _);

        return Task.CompletedTask;
    }
}

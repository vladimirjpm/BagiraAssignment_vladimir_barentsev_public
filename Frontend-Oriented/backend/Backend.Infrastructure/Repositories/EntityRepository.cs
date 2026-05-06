using System.Collections.Concurrent;
using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

public class EntityRepository : IEntityRepository
{
    internal static readonly ConcurrentDictionary<Guid, Entity> Store = new();

    public Task<Entity?> GetByIdAsync(Guid id)
    {
        Store.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Entity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Entity>>(Store.Values.ToList());
    }

    public Task<Entity> AddAsync(Entity entity)
    {
        if (entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();

        var now = DateTime.UtcNow;
        entity.CreatedAt = now;
        entity.UpdatedAt = now;

        Store[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Entity>> GetByScenarioIdAsync(Guid scenarioId)
    {
        var entities = Store.Values
            .Where(e => e.ScenarioId == scenarioId)
            .ToList();
        return Task.FromResult<IEnumerable<Entity>>(entities);
    }

    public Task<bool> ScenarioExistsAsync(Guid scenarioId)
    {
        return Task.FromResult(ScenarioRepository.Store.ContainsKey(scenarioId));
    }
}

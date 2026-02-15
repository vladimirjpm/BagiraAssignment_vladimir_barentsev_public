using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Entity repository implementation placeholder
/// TODO(candidate): Implement this repository with your chosen data storage (Database, In-Memory, etc.)
/// </summary>
public class EntityRepository : IEntityRepository
{
    public Task<Entity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException("TODO(candidate): Implement entity retrieval by id");
    }

    public Task<IEnumerable<Entity>> GetAllAsync()
    {
        throw new NotImplementedException("TODO(candidate): Implement entity retrieval");
    }

    public Task<Entity> AddAsync(Entity entity)
    {
        throw new NotImplementedException("TODO(candidate): Implement entity creation");
    }

    public Task<IEnumerable<Entity>> GetByScenarioIdAsync(Guid scenarioId)
    {
        throw new NotImplementedException("TODO(candidate): Implement entity retrieval by scenario id");
    }

    public Task<bool> ScenarioExistsAsync(Guid scenarioId)
    {
        throw new NotImplementedException("TODO(candidate): Implement scenario existence check");
    }
}

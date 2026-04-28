using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Entity repository implementation
/// TODO(candidate): Implement this repository with your chosen data storage (Database, In-Memory, etc.)
/// </summary>
public class EntityRepository : IEntityRepository
{
    /// <summary>
    /// TODO(candidate): Implement entity retrieval by id
    /// </summary>
    public Task<Entity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement entity retrieval
    /// </summary>
    public Task<IEnumerable<Entity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement entity creation
    /// </summary>
    public Task<Entity> AddAsync(Entity entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement entity retrieval by scenario id
    /// </summary>
    public Task<IEnumerable<Entity>> GetByScenarioIdAsync(Guid scenarioId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement scenario existence check
    /// </summary>
    public Task<bool> ScenarioExistsAsync(Guid scenarioId)
    {
        throw new NotImplementedException();
    }
}

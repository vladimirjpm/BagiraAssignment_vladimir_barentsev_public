using Backend.Domain.Models;

namespace Backend.Infrastructure.Interfaces;

/// <summary>
/// Repository interface for Entity data access
/// </summary>
public interface IEntityRepository : IRepository<Entity>
{
    Task<IEnumerable<Entity>> GetByScenarioIdAsync(Guid scenarioId);
    Task<bool> ScenarioExistsAsync(Guid scenarioId);
}

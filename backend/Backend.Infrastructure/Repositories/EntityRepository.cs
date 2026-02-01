using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// In-memory entity repository implementation
/// </summary>
public class EntityRepository : IEntityRepository
{
    private readonly List<Entity> _entities = new();
    private readonly object _lock = new();
    private readonly IScenarioRepository _scenarioRepository;

    public EntityRepository(IScenarioRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }

    public Task<Entity?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            return Task.FromResult<Entity?>(entity);
        }
    }

    public Task<IEnumerable<Entity>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<Entity>>(_entities.ToList());
        }
    }

    public Task<Entity> AddAsync(Entity entity)
    {
        lock (_lock)
        {
            _entities.Add(entity);
            return Task.FromResult(entity);
        }
    }

    public Task<IEnumerable<Entity>> GetByScenarioIdAsync(Guid scenarioId)
    {
        lock (_lock)
        {
            var entities = _entities.Where(e => e.ScenarioId == scenarioId).ToList();
            return Task.FromResult<IEnumerable<Entity>>(entities);
        }
    }

    public async Task<bool> ScenarioExistsAsync(Guid scenarioId)
    {
        var scenario = await _scenarioRepository.GetByIdAsync(scenarioId);
        return scenario != null;
    }
}

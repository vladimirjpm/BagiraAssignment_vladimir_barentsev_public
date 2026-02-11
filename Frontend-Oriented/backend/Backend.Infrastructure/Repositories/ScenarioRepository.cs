using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// In-memory scenario repository implementation
/// </summary>
public class ScenarioRepository : IScenarioRepository
{
    private readonly List<Scenario> _scenarios = new();
    private readonly object _lock = new();

    public Task<Scenario?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var scenario = _scenarios.FirstOrDefault(s => s.Id == id);
            return Task.FromResult<Scenario?>(scenario);
        }
    }

    public Task<IEnumerable<Scenario>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<Scenario>>(_scenarios.ToList());
        }
    }

    public Task<Scenario> AddAsync(Scenario entity)
    {
        lock (_lock)
        {
            _scenarios.Add(entity);
            return Task.FromResult(entity);
        }
    }

    public Task<int> GetEntityCountAsync(Guid scenarioId)
    {
        // Entity count is calculated in the controller using IEntityRepository
        // This method is kept for interface compliance but returns 0
        // The actual count is calculated in ScenariosController.GetScenarios()
        return Task.FromResult(0);
    }
}

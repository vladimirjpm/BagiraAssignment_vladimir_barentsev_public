using System.Collections.Concurrent;
using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

public class ScenarioRepository : IScenarioRepository
{
    internal static readonly ConcurrentDictionary<Guid, Scenario> Store = new();

    public Task<Scenario?> GetByIdAsync(Guid id)
    {
        Store.TryGetValue(id, out var scenario);
        return Task.FromResult(scenario);
    }

    public Task<IEnumerable<Scenario>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Scenario>>(Store.Values.ToList());
    }

    public Task<Scenario> AddAsync(Scenario scenario)
    {
        if (scenario.Id == Guid.Empty)
            scenario.Id = Guid.NewGuid();

        var now = DateTime.UtcNow;
        scenario.CreatedAt = now;
        scenario.UpdatedAt = now;

        Store[scenario.Id] = scenario;
        return Task.FromResult(scenario);
    }

    public Task<int> GetEntityCountAsync(Guid scenarioId)
    {
        var count = EntityRepository.Store.Values.Count(e => e.ScenarioId == scenarioId);
        return Task.FromResult(count);
    }
}

using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Scenario repository implementation placeholder
/// TODO(candidate): Implement this repository with your chosen data storage (Database, In-Memory, etc.)
/// </summary>
public class ScenarioRepository : IScenarioRepository
{
    public Task<Scenario?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException("TODO(candidate): Implement scenario retrieval by id");
    }

    public Task<IEnumerable<Scenario>> GetAllAsync()
    {
        throw new NotImplementedException("TODO(candidate): Implement scenario retrieval");
    }

    public Task<Scenario> AddAsync(Scenario entity)
    {
        throw new NotImplementedException("TODO(candidate): Implement scenario creation");
    }

    public Task<int> GetEntityCountAsync(Guid scenarioId)
    {
        throw new NotImplementedException("TODO(candidate): Implement entity count retrieval for scenario");
    }
}

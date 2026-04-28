using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Scenario repository implementation
/// TODO(candidate): Implement this repository with your chosen data storage (Database, In-Memory, etc.)
/// </summary>
public class ScenarioRepository : IScenarioRepository
{
    /// <summary>
    /// TODO(candidate): Implement scenario retrieval by id
    /// </summary>
    public Task<Scenario?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement scenario retrieval
    /// </summary>
    public Task<IEnumerable<Scenario>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement scenario creation
    /// </summary>
    public Task<Scenario> AddAsync(Scenario entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO(candidate): Implement entity count retrieval for scenario
    /// </summary>
    public Task<int> GetEntityCountAsync(Guid scenarioId)
    {
        throw new NotImplementedException();
    }
}

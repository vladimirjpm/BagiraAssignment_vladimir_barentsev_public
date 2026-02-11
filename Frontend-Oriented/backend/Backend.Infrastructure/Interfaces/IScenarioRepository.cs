using Backend.Domain.Models;

namespace Backend.Infrastructure.Interfaces;

/// <summary>
/// Repository interface for Scenario data access
/// TODO(candidate): Implement concrete repository in Infrastructure layer
/// </summary>
public interface IScenarioRepository : IRepository<Scenario>
{
    Task<int> GetEntityCountAsync(Guid scenarioId);
}

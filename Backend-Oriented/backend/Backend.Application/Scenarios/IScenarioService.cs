using Backend.Application.Scenarios.Dtos;

namespace Backend.Application.Scenarios;

public interface IScenarioService
{
    Task<IReadOnlyList<ScenarioDto>> ListAsync(ScenarioQuery query, CancellationToken ct);
    Task<ScenarioDto> GetAsync(Guid id, CancellationToken ct);
    Task<ScenarioDto> CreateAsync(CreateScenarioRequest request, CancellationToken ct);
    Task<ScenarioDto> UpdateAsync(Guid id, UpdateScenarioRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}

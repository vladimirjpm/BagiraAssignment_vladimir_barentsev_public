using Backend.Application.Entities.Dtos;

namespace Backend.Application.Entities;

public interface IEntityService
{
    Task<IReadOnlyList<EntityDto>> ListByScenarioAsync(Guid scenarioId, EntityQuery query, CancellationToken ct);
    Task<EntityDto> GetAsync(Guid id, CancellationToken ct);
    Task<EntityDto> CreateAsync(Guid scenarioId, CreateEntityRequest request, CancellationToken ct);
    Task<EntityDto> UpdateAsync(Guid id, UpdateEntityRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}

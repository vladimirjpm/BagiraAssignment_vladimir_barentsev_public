using Backend.Application.Abstractions;
using Backend.Application.Entities.Dtos;
using Backend.Application.Entities.Mapping;
using Backend.Domain.Entities;
using Backend.Domain.Exceptions;

namespace Backend.Application.Entities;

public sealed class EntityService : IEntityService
{
    private readonly IEntityRepository _entities;
    private readonly IScenarioRepository _scenarios;

    public EntityService(IEntityRepository entities, IScenarioRepository scenarios)
    {
        _entities = entities;
        _scenarios = scenarios;
    }

    public async Task<IReadOnlyList<EntityDto>> ListByScenarioAsync(
        Guid scenarioId, EntityQuery query, CancellationToken ct)
    {
        // Listing entities of a non-existent scenario is a 404, not an empty list.
        if (!await _scenarios.ExistsAsync(scenarioId, ct))
            throw NotFoundException.For("Scenario", scenarioId);

        var entities = await _entities.ListByScenarioAsync(scenarioId, query.Type, query.TaskForce, ct);
        return Sort(entities.Select(e => e.ToDto()), query.SortBy, query.SortOrder).ToList();
    }

    public async Task<EntityDto> GetAsync(Guid id, CancellationToken ct)
    {
        var entity = await _entities.GetAsync(id, ct)
                     ?? throw NotFoundException.For("Entity", id);
        return entity.ToDto();
    }

    public async Task<EntityDto> CreateAsync(Guid scenarioId, CreateEntityRequest request, CancellationToken ct)
    {
        // No-orphan invariant: the parent scenario must exist.
        if (!await _scenarios.ExistsAsync(scenarioId, ct))
            throw NotFoundException.For("Scenario", scenarioId);

        var entity = Entity.Create(
            scenarioId, request.Type, request.TaskForce,
            request.Name, request.Latitude, request.Longitude);

        await _entities.AddAsync(entity, ct);
        return entity.ToDto();
    }

    public async Task<EntityDto> UpdateAsync(Guid id, UpdateEntityRequest request, CancellationToken ct)
    {
        var entity = await _entities.GetAsync(id, ct)
                     ?? throw NotFoundException.For("Entity", id);

        entity.Update(request.Type, request.TaskForce, request.Name, request.Latitude, request.Longitude);
        await _entities.UpdateAsync(entity, ct);
        return entity.ToDto();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        if (await _entities.GetAsync(id, ct) is null)
            throw NotFoundException.For("Entity", id);

        await _entities.DeleteAsync(id, ct);
    }

    // Whitelisted server-side sorting — never reflect arbitrary property names.
    private static IEnumerable<EntityDto> Sort(IEnumerable<EntityDto> source, string? sortBy, string? sortOrder)
    {
        var descending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        Func<EntityDto, object> key = sortBy?.ToLowerInvariant() switch
        {
            "name"      => e => e.Name,
            "type"      => e => e.Type.ToString(),
            "taskforce" => e => e.TaskForce.ToString(),
            "latitude"  => e => e.Latitude,
            "longitude" => e => e.Longitude,
            "updatedat" => e => e.UpdatedAt,
            _           => e => e.UpdatedAt // stable default
        };

        return descending ? source.OrderByDescending(key) : source.OrderBy(key);
    }
}

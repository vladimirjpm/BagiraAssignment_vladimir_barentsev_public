using Backend.Application.Abstractions;
using Backend.Application.Scenarios.Dtos;
using Backend.Application.Scenarios.Mapping;
using Backend.Domain.Entities;
using Backend.Domain.Exceptions;

namespace Backend.Application.Scenarios;

public sealed class ScenarioService : IScenarioService
{
    private readonly IScenarioRepository _repository;
    private readonly IEntityRepository _entities;

    public ScenarioService(IScenarioRepository repository, IEntityRepository entities)
    {
        _repository = repository;
        _entities = entities;
    }

    public async Task<IReadOnlyList<ScenarioDto>> ListAsync(ScenarioQuery query, CancellationToken ct)
    {
        var scenarios = await _repository.ListAsync(query.Name, query.Description, ct);

        var dtos = new List<ScenarioDto>(scenarios.Count);
        foreach (var s in scenarios)
        {
            var count = await _entities.CountByScenarioAsync(s.Id, ct);
            dtos.Add(s.ToDto(count));
        }

        return Sort(dtos, query.SortBy, query.SortOrder).ToList();
    }

    public async Task<ScenarioDto> GetAsync(Guid id, CancellationToken ct)
    {
        var scenario = await _repository.GetAsync(id, ct)
                       ?? throw NotFoundException.For("Scenario", id);
        var count = await _entities.CountByScenarioAsync(id, ct);
        return scenario.ToDto(count);
    }

    public async Task<ScenarioDto> CreateAsync(CreateScenarioRequest request, CancellationToken ct)
    {
        // Domain factory is the backstop invariant check behind DTO validation.
        var scenario = Scenario.Create(request.Name, request.Description);
        await _repository.AddAsync(scenario, ct);
        return scenario.ToDto(entityCount: 0);
    }

    public async Task<ScenarioDto> UpdateAsync(Guid id, UpdateScenarioRequest request, CancellationToken ct)
    {
        var scenario = await _repository.GetAsync(id, ct)
                       ?? throw NotFoundException.For("Scenario", id);

        scenario.Update(request.Name, request.Description);
        await _repository.UpdateAsync(scenario, ct);

        var count = await _entities.CountByScenarioAsync(id, ct);
        return scenario.ToDto(count);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        if (!await _repository.ExistsAsync(id, ct))
            throw NotFoundException.For("Scenario", id);

        // Cascade: a scenario's entities have no meaning without their parent (no-orphan invariant).
        await _entities.DeleteByScenarioAsync(id, ct);
        await _repository.DeleteAsync(id, ct);
    }

    // Whitelisted server-side sorting — never reflect arbitrary property names.
    private static IEnumerable<ScenarioDto> Sort(IEnumerable<ScenarioDto> source, string? sortBy, string? sortOrder)
    {
        var descending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        Func<ScenarioDto, object> key = sortBy?.ToLowerInvariant() switch
        {
            "name"        => s => s.Name,
            "description" => s => s.Description ?? string.Empty,
            "entitycount" => s => s.EntityCount,
            "updatedat"   => s => s.UpdatedAt,
            _             => s => s.UpdatedAt // stable default
        };

        return descending ? source.OrderByDescending(key) : source.OrderBy(key);
    }
}

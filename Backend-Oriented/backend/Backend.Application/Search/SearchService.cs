using Backend.Application.Abstractions;
using Backend.Application.Entities.Mapping;
using Backend.Application.Scenarios.Mapping;

namespace Backend.Application.Search;

public sealed class SearchService : ISearchService
{
    private readonly IScenarioRepository _scenarios;
    private readonly IEntityRepository _entities;

    public SearchService(IScenarioRepository scenarios, IEntityRepository entities)
    {
        _scenarios = scenarios;
        _entities = entities;
    }

    public async Task<SearchResultDto> SearchAsync(string query, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new SearchResultDto([], []);

        var scenarios = await _scenarios.SearchAsync(query, ct);
        var entities = await _entities.SearchByNameAsync(query, ct);

        var scenarioDtos = new List<Scenarios.Dtos.ScenarioDto>(scenarios.Count);
        foreach (var s in scenarios)
        {
            var count = await _entities.CountByScenarioAsync(s.Id, ct);
            scenarioDtos.Add(s.ToDto(count));
        }

        return new SearchResultDto(scenarioDtos, entities.Select(e => e.ToDto()).ToList());
    }
}

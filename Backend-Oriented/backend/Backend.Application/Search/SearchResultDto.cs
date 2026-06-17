using Backend.Application.Entities.Dtos;
using Backend.Application.Scenarios.Dtos;

namespace Backend.Application.Search;

/// <summary>Shape matches the frontend's SearchResult interface.</summary>
public sealed record SearchResultDto(
    IReadOnlyList<ScenarioDto> Scenarios,
    IReadOnlyList<EntityDto> Entities);

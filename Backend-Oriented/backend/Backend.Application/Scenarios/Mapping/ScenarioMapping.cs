using Backend.Application.Scenarios.Dtos;
using Backend.Domain.Entities;

namespace Backend.Application.Scenarios.Mapping;

/// <summary>Manual domain → DTO mapping (no AutoMapper — explicit and trivial).</summary>
public static class ScenarioMapping
{
    public static ScenarioDto ToDto(this Scenario scenario, int entityCount) =>
        new(scenario.Id, scenario.Name, scenario.Description, entityCount, scenario.UpdatedAt);
}

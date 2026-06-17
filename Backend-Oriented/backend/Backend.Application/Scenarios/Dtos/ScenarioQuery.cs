namespace Backend.Application.Scenarios.Dtos;

/// <summary>
/// Server-side filter + sort parameters for the scenario list.
/// Bound from the query string; all fields optional.
/// </summary>
public sealed record ScenarioQuery
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? SortBy { get; init; }
    public string? SortOrder { get; init; }
}

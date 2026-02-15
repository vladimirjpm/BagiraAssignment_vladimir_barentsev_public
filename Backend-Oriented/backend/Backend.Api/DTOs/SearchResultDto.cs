namespace Backend.Api.DTOs;

/// <summary>
/// DTO for combined search results across scenarios and entities
/// </summary>
public class SearchResultDto
{
    public IEnumerable<ScenarioListItemDto> Scenarios { get; set; } = new List<ScenarioListItemDto>();
    public IEnumerable<EntityDto> Entities { get; set; } = new List<EntityDto>();
}

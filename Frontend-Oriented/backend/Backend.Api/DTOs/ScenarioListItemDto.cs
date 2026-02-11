namespace Backend.Api.DTOs;

/// <summary>
/// DTO for scenario list items
/// </summary>
/// TODO(candidate): Add validation attributes as needed
public class ScenarioListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? EntityCount { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

namespace Backend.Api.DTOs;

/// <summary>
/// Request DTO for creating a scenario
/// </summary>
/// TODO(candidate): Add validation attributes as needed
public class CreateScenarioRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}

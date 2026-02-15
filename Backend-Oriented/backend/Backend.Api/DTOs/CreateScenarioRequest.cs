using System.ComponentModel.DataAnnotations;

namespace Backend.Api.DTOs;

/// <summary>
/// Request DTO for creating a scenario
/// </summary>
public class CreateScenarioRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}

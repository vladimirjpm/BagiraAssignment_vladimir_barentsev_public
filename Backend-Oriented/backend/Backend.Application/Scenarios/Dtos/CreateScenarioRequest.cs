using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Scenarios.Dtos;

/// <summary>
/// Payload for creating a scenario. Matches the frontend's CreateScenarioPayload.
/// Validation attributes sit on the constructor parameters (record requirement).
/// </summary>
public sealed record CreateScenarioRequest(
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be 1-100 characters.")]
    string Name,

    [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
    string? Description);

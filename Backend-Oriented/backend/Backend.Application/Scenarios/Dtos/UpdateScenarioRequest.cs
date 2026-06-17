using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Scenarios.Dtos;

/// <summary>
/// Payload for updating a scenario. Same shape and validation as
/// <see cref="CreateScenarioRequest"/> — full replace, not a patch.
/// </summary>
public sealed record UpdateScenarioRequest(
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be 1-100 characters.")]
    string Name,

    [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
    string? Description);

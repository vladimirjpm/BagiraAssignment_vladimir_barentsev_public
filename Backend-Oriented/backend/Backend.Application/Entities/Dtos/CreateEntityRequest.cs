using System.ComponentModel.DataAnnotations;
using Backend.Domain.Enums;

namespace Backend.Application.Entities.Dtos;

/// <summary>
/// Payload for creating an entity. Matches the frontend's CreateEntityPayload.
/// ScenarioId is NOT here — it comes from the route (no orphan entities).
/// Validation attributes sit on the constructor parameters (record requirement).
/// </summary>
public sealed record CreateEntityRequest(
    [Required(ErrorMessage = "Type is required.")]
    [EnumDataType(typeof(EntityType), ErrorMessage = "Invalid entity type.")]
    EntityType Type,

    [Required(ErrorMessage = "TaskForce is required.")]
    [EnumDataType(typeof(TaskForce), ErrorMessage = "Invalid task force.")]
    TaskForce TaskForce,

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be 1-100 characters.")]
    string Name,

    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    double Latitude,

    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    double Longitude);

using Backend.Domain.Models;

namespace Backend.Api.DTOs;

/// <summary>
/// Request DTO for creating an entity
/// </summary>
/// TODO(candidate): Add validation attributes as needed
public class CreateEntityRequest
{
    public EntityType Type { get; set; }

    public TaskForce TaskForce { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}

namespace Backend.Domain.Models;

/// <summary>
/// Entity domain model
/// </summary>
public class Entity
{
    public Guid Id { get; set; }
    public Guid ScenarioId { get; set; }
    public EntityType Type { get; set; }
    public TaskForce TaskForce { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

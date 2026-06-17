using Backend.Domain.Enums;
using Backend.Domain.Exceptions;

namespace Backend.Domain.Entities;

/// <summary>
/// An entity placed inside a scenario. Always belongs to exactly one scenario
/// (no orphans). Constructed only through <see cref="Create"/> so the coordinate
/// and required-field invariants always hold.
/// </summary>
public sealed class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ScenarioId { get; private set; }
    public EntityType Type { get; private set; }
    public TaskForce TaskForce { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    // Required by EF Core materialization; not for application use.
    private Entity() { }

    public static Entity Create(
        Guid scenarioId, EntityType type, TaskForce taskForce,
        string name, double latitude, double longitude)
    {
        if (scenarioId == Guid.Empty)
            throw new DomainException("ScenarioId is required.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");
        if (latitude is < -90 or > 90)
            throw new DomainException("Latitude must be between -90 and 90.");
        if (longitude is < -180 or > 180)
            throw new DomainException("Longitude must be between -180 and 180.");

        return new Entity
        {
            ScenarioId = scenarioId,
            Type = type,
            TaskForce = taskForce,
            Name = name.Trim(),
            Latitude = latitude,
            Longitude = longitude
        };
    }

    /// <summary>Update fields in place, enforcing the same invariants as <see cref="Create"/>.</summary>
    public void Update(EntityType type, TaskForce taskForce, string name, double latitude, double longitude)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");
        if (latitude is < -90 or > 90)
            throw new DomainException("Latitude must be between -90 and 90.");
        if (longitude is < -180 or > 180)
            throw new DomainException("Longitude must be between -180 and 180.");

        Type = type;
        TaskForce = taskForce;
        Name = name.Trim();
        Latitude = latitude;
        Longitude = longitude;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}

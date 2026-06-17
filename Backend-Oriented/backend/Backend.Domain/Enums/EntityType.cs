namespace Backend.Domain.Enums;

/// <summary>
/// The kind of entity that can be placed inside a scenario.
/// Values are fixed by the assignment data rules.
/// </summary>
public enum EntityType
{
    Soldier,
    Tank,
    Drone,
    Aircraft,
    Vehicle,
    Civilian
}

using Backend.Domain.Exceptions;

namespace Backend.Domain.Entities;

/// <summary>
/// A scenario groups together a set of entities.
/// Constructed only through <see cref="Create"/> so invariants always hold.
/// Private setters + parameterless ctor keep it EF Core-friendly for the DB swap.
/// </summary>
public sealed class Scenario
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    // Required by EF Core materialization; not for application use.
    private Scenario() { }

    public static Scenario Create(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Scenario name is required.");

        return new Scenario
        {
            Name = name.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
        };
    }

    /// <summary>Refresh the last-modified timestamp.</summary>
    public void Touch() => UpdatedAt = DateTimeOffset.UtcNow;

    /// <summary>Update fields in place, enforcing the same invariants as <see cref="Create"/>.</summary>
    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Scenario name is required.");

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Touch();
    }
}

using System.Collections.Concurrent;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Persistence;

/// <summary>
/// Process-wide, thread-safe in-memory data store. Registered as a singleton so
/// state survives across requests. Swapping to EF Core replaces the repositories
/// that read from this, not the layers above.
/// </summary>
public sealed class InMemoryStore
{
    public ConcurrentDictionary<Guid, Scenario> Scenarios { get; } = new();
    public ConcurrentDictionary<Guid, Entity> Entities { get; } = new();
}

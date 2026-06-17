using Backend.Domain.Entities;

namespace Backend.Application.Abstractions;

/// <summary>
/// Persistence boundary for scenarios. The in-memory implementation lives in
/// Infrastructure; swapping to EF Core changes only the implementation, not this
/// contract or anything above it.
/// </summary>
public interface IScenarioRepository
{
    /// <summary>List scenarios, optionally filtered by name/description (case-insensitive contains).</summary>
    Task<IReadOnlyList<Scenario>> ListAsync(string? name, string? description, CancellationToken ct);

    /// <summary>Search scenarios by name OR description (case-insensitive contains).</summary>
    Task<IReadOnlyList<Scenario>> SearchAsync(string query, CancellationToken ct);

    Task<Scenario?> GetAsync(Guid id, CancellationToken ct);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct);

    Task AddAsync(Scenario scenario, CancellationToken ct);

    /// <summary>Persist changes made to a previously-fetched scenario instance.</summary>
    Task UpdateAsync(Scenario scenario, CancellationToken ct);

    Task DeleteAsync(Guid id, CancellationToken ct);
}

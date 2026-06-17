using Backend.Application.Abstractions;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// In-memory <see cref="IScenarioRepository"/>. Filtering happens here so it can
/// later be pushed into a SQL WHERE clause without touching callers.
/// Async signatures wrap synchronous work so the EF Core swap is a no-op at call sites.
/// </summary>
public sealed class InMemoryScenarioRepository : IScenarioRepository
{
    private readonly InMemoryStore _store;

    public InMemoryScenarioRepository(InMemoryStore store) => _store = store;

    public Task<IReadOnlyList<Scenario>> ListAsync(string? name, string? description, CancellationToken ct)
    {
        IEnumerable<Scenario> query = _store.Scenarios.Values;

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(s => s.Description is not null &&
                                     s.Description.Contains(description, StringComparison.OrdinalIgnoreCase));

        IReadOnlyList<Scenario> result = query.ToList();
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Scenario>> SearchAsync(string query, CancellationToken ct)
    {
        IReadOnlyList<Scenario> result = _store.Scenarios.Values
            .Where(s => s.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        (s.Description is not null && s.Description.Contains(query, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        return Task.FromResult(result);
    }

    public Task<Scenario?> GetAsync(Guid id, CancellationToken ct) =>
        Task.FromResult(_store.Scenarios.GetValueOrDefault(id));

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
        Task.FromResult(_store.Scenarios.ContainsKey(id));

    public Task AddAsync(Scenario scenario, CancellationToken ct)
    {
        _store.Scenarios[scenario.Id] = scenario;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Scenario scenario, CancellationToken ct)
    {
        _store.Scenarios[scenario.Id] = scenario;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct)
    {
        _store.Scenarios.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}

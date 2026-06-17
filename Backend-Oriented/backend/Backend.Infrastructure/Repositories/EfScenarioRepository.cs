using Backend.Application.Abstractions;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

public sealed class EfScenarioRepository : IScenarioRepository
{
    private readonly AppDbContext _db;

    public EfScenarioRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Scenario>> ListAsync(string? name, string? description, CancellationToken ct)
    {
        var query = _db.Scenarios.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(s => EF.Functions.ILike(s.Name, $"%{name}%"));

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(s => s.Description != null && EF.Functions.ILike(s.Description, $"%{description}%"));

        return await query.ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Scenario>> SearchAsync(string query, CancellationToken ct) =>
        await _db.Scenarios
            .Where(s => EF.Functions.ILike(s.Name, $"%{query}%") ||
                        (s.Description != null && EF.Functions.ILike(s.Description, $"%{query}%")))
            .ToListAsync(ct);

    public async Task<Scenario?> GetAsync(Guid id, CancellationToken ct) =>
        await _db.Scenarios.FindAsync([id], ct);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
        await _db.Scenarios.AnyAsync(s => s.Id == id, ct);

    public async Task AddAsync(Scenario scenario, CancellationToken ct)
    {
        _db.Scenarios.Add(scenario);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Scenario scenario, CancellationToken ct)
    {
        // Entity is already tracked from GetAsync; just save changes.
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        await _db.Scenarios.Where(s => s.Id == id).ExecuteDeleteAsync(ct);
    }
}

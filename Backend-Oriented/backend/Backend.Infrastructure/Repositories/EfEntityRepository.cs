using Backend.Application.Abstractions;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

public sealed class EfEntityRepository : IEntityRepository
{
    private readonly AppDbContext _db;

    public EfEntityRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Entity>> ListByScenarioAsync(
        Guid scenarioId, EntityType? type, TaskForce? taskForce, CancellationToken ct)
    {
        var query = _db.Entities.Where(e => e.ScenarioId == scenarioId);

        if (type is not null)
            query = query.Where(e => e.Type == type.Value);

        if (taskForce is not null)
            query = query.Where(e => e.TaskForce == taskForce.Value);

        return await query.ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Entity>> SearchByNameAsync(string name, CancellationToken ct) =>
        await _db.Entities
            .Where(e => EF.Functions.ILike(e.Name, $"%{name}%"))
            .ToListAsync(ct);

    public async Task<Entity?> GetAsync(Guid id, CancellationToken ct) =>
        await _db.Entities.FindAsync([id], ct);

    public async Task<int> CountByScenarioAsync(Guid scenarioId, CancellationToken ct) =>
        await _db.Entities.CountAsync(e => e.ScenarioId == scenarioId, ct);

    public async Task AddAsync(Entity entity, CancellationToken ct)
    {
        _db.Entities.Add(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Entity entity, CancellationToken ct)
    {
        // Entity is already tracked from GetAsync; just save changes.
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        await _db.Entities.Where(e => e.Id == id).ExecuteDeleteAsync(ct);
    }

    public async Task DeleteByScenarioAsync(Guid scenarioId, CancellationToken ct)
    {
        await _db.Entities.Where(e => e.ScenarioId == scenarioId).ExecuteDeleteAsync(ct);
    }
}

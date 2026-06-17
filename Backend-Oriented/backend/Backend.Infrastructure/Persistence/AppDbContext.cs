using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Scenario> Scenarios => Set<Scenario>();
    public DbSet<Entity> Entities => Set<Entity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scenario>(b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.Name).IsRequired().HasMaxLength(100);
            b.Property(s => s.Description).HasMaxLength(500);
            b.Property(s => s.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<Entity>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.ScenarioId).IsRequired();
            b.Property(e => e.Name).IsRequired().HasMaxLength(100);
            b.Property(e => e.Type).HasConversion<string>().IsRequired();
            b.Property(e => e.TaskForce).HasConversion<string>().IsRequired();
            b.Property(e => e.Latitude).IsRequired();
            b.Property(e => e.Longitude).IsRequired();
            b.Property(e => e.UpdatedAt).IsRequired();

            // DB-level cascade keeps Entities in sync when a Scenario is hard-deleted.
            b.HasOne<Scenario>().WithMany()
                .HasForeignKey(e => e.ScenarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

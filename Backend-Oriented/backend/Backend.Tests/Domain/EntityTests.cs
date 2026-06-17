using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;

namespace Backend.Tests.Domain;

public class EntityTests
{
    private static readonly Guid ScenarioId = Guid.NewGuid();

    [Fact]
    public void Create_ValidInputs_ReturnsEntity()
    {
        var entity = Entity.Create(ScenarioId, EntityType.Tank, TaskForce.Friendly, "Alpha-1", 32.0, 34.5);

        Assert.Equal("Alpha-1", entity.Name);
        Assert.Equal(EntityType.Tank, entity.Type);
        Assert.Equal(TaskForce.Friendly, entity.TaskForce);
        Assert.Equal(ScenarioId, entity.ScenarioId);
    }

    [Theory]
    [InlineData(-91)]
    [InlineData(91)]
    public void Create_LatitudeOutOfRange_ThrowsDomainException(double lat)
    {
        Assert.Throws<DomainException>(() =>
            Entity.Create(ScenarioId, EntityType.Soldier, TaskForce.Enemy, "X", lat, 0));
    }

    [Theory]
    [InlineData(-181)]
    [InlineData(181)]
    public void Create_LongitudeOutOfRange_ThrowsDomainException(double lon)
    {
        Assert.Throws<DomainException>(() =>
            Entity.Create(ScenarioId, EntityType.Drone, TaskForce.Friendly, "X", 0, lon));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_EmptyName_ThrowsDomainException(string name)
    {
        Assert.Throws<DomainException>(() =>
            Entity.Create(ScenarioId, EntityType.Civilian, TaskForce.Friendly, name, 0, 0));
    }

    [Fact]
    public void Update_LatitudeOutOfRange_ThrowsDomainException()
    {
        var entity = Entity.Create(ScenarioId, EntityType.Tank, TaskForce.Friendly, "Alpha-1", 0, 0);

        Assert.Throws<DomainException>(() =>
            entity.Update(EntityType.Tank, TaskForce.Friendly, "Alpha-1", 95, 0));
    }
}

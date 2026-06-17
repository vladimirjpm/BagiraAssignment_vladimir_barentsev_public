using Backend.Domain.Entities;
using Backend.Domain.Exceptions;

namespace Backend.Tests.Domain;

public class ScenarioTests
{
    [Fact]
    public void Create_ValidInputs_ReturnsScenario()
    {
        var scenario = Scenario.Create("Op Thunder", "A test operation");

        Assert.Equal("Op Thunder", scenario.Name);
        Assert.Equal("A test operation", scenario.Description);
        Assert.NotEqual(Guid.Empty, scenario.Id);
    }

    [Fact]
    public void Create_TrimsWhitespace()
    {
        var scenario = Scenario.Create("  Op Thunder  ", "  desc  ");

        Assert.Equal("Op Thunder", scenario.Name);
        Assert.Equal("desc", scenario.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyName_ThrowsDomainException(string? name)
    {
        Assert.Throws<DomainException>(() => Scenario.Create(name!, null));
    }

    [Fact]
    public void Update_EmptyName_ThrowsDomainException()
    {
        var scenario = Scenario.Create("Op Thunder", null);

        Assert.Throws<DomainException>(() => scenario.Update("", null));
    }

    [Fact]
    public void Update_ValidInputs_ChangesNameAndDescription()
    {
        var scenario = Scenario.Create("Old Name", "Old desc");

        scenario.Update("New Name", "New desc");

        Assert.Equal("New Name", scenario.Name);
        Assert.Equal("New desc", scenario.Description);
    }
}

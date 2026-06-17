using Backend.Application.Abstractions;
using Backend.Application.Scenarios;
using Backend.Application.Scenarios.Dtos;
using Backend.Domain.Entities;
using Backend.Domain.Exceptions;
using Moq;

namespace Backend.Tests.Application;

public class ScenarioServiceTests
{
    private readonly Mock<IScenarioRepository> _scenarioRepo = new();
    private readonly Mock<IEntityRepository> _entityRepo = new();
    private readonly ScenarioService _sut;

    public ScenarioServiceTests()
    {
        _sut = new ScenarioService(_scenarioRepo.Object, _entityRepo.Object);
    }

    [Fact]
    public async Task GetAsync_ScenarioNotFound_ThrowsNotFoundException()
    {
        var id = Guid.NewGuid();
        _scenarioRepo.Setup(r => r.GetAsync(id, default)).ReturnsAsync((Scenario?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetAsync(id, default));
    }

    [Fact]
    public async Task GetAsync_ExistingScenario_ReturnsDto()
    {
        var scenario = Scenario.Create("Op Thunder", "desc");
        _scenarioRepo.Setup(r => r.GetAsync(scenario.Id, default)).ReturnsAsync(scenario);
        _entityRepo.Setup(r => r.CountByScenarioAsync(scenario.Id, default)).ReturnsAsync(3);

        var dto = await _sut.GetAsync(scenario.Id, default);

        Assert.Equal("Op Thunder", dto.Name);
        Assert.Equal(3, dto.EntityCount);
    }

    [Fact]
    public async Task DeleteAsync_ScenarioNotFound_ThrowsNotFoundException()
    {
        var id = Guid.NewGuid();
        _scenarioRepo.Setup(r => r.ExistsAsync(id, default)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteAsync(id, default));
    }

    [Fact]
    public async Task DeleteAsync_ExistingScenario_CascadesEntitiesToo()
    {
        var id = Guid.NewGuid();
        _scenarioRepo.Setup(r => r.ExistsAsync(id, default)).ReturnsAsync(true);

        await _sut.DeleteAsync(id, default);

        // Both cascade entity delete and scenario delete must be called.
        _entityRepo.Verify(r => r.DeleteByScenarioAsync(id, default), Times.Once);
        _scenarioRepo.Verify(r => r.DeleteAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_CallsRepositoryAndReturnsDto()
    {
        var request = new CreateScenarioRequest("Op Thunder", "desc");

        var dto = await _sut.CreateAsync(request, default);

        _scenarioRepo.Verify(r => r.AddAsync(It.IsAny<Scenario>(), default), Times.Once);
        Assert.Equal("Op Thunder", dto.Name);
        Assert.Equal(0, dto.EntityCount);
    }
}

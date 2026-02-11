using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for managing scenarios
/// </summary>
/// /// TODO(candidate): Implement proper error handling, validation and return codes.
/// TODO(candidate): Implement all endpoints
[ApiController]
[Route("api/[controller]")]
public class ScenariosController : ControllerBase
{
    private readonly IScenarioRepository _scenarioRepository;
    private readonly IEntityRepository _entityRepository;
    private readonly ILogger<ScenariosController> _logger;

    public ScenariosController(
        IScenarioRepository scenarioRepository,
        IEntityRepository entityRepository,
        ILogger<ScenariosController> logger)
    {
        _scenarioRepository = scenarioRepository;
        _entityRepository = entityRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get all scenarios
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScenarioListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScenarioListItemDto>>> GetScenarios()
    {
        return null;
    }

    /// <summary>
    /// Get a scenario by ID
    /// </summary>
    [HttpGet("{scenarioId}")]
    [ProducesResponseType(typeof(ScenarioDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScenarioDetailsDto>> GetScenarioById(Guid scenarioId)
    {
        return null;
    }

    /// <summary>
    /// Create a new scenario
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ScenarioDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScenarioDetailsDto>> CreateScenario([FromBody] CreateScenarioRequest request)
    {
        return null;
    }
}

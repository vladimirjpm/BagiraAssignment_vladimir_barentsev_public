using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for managing scenarios
/// </summary>
/// TODO(candidate): Implement proper error handling, validation and return codes.
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
    /// Get all scenarios with optional filtering and sorting
    /// </summary>
    /// <param name="filters">Optional filter and sort parameters</param>
    /// <remarks>
    /// Supports filtering by name and description (partial match),
    /// and sorting by name, description, entityCount, or updatedAt.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScenarioListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScenarioListItemDto>>> GetScenarios([FromQuery] ScenarioFilterParams filters)
    {
        // TODO(candidate): Implement scenario retrieval with filtering and sorting
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
        // TODO(candidate): Implement scenario retrieval by ID
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
        // TODO(candidate): Implement scenario creation
        return null;
    }
}

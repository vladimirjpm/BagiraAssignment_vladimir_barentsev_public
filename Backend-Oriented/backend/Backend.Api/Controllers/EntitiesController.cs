using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for managing entities
/// </summary>
/// TODO(candidate): Implement proper error handling, validation and return codes.
/// TODO(candidate): Implement all endpoints
[ApiController]
[Route("api/[controller]")]
public class EntitiesController : ControllerBase
{
    private readonly IEntityRepository _entityRepository;
    private readonly IScenarioRepository _scenarioRepository;
    private readonly ILogger<EntitiesController> _logger;

    public EntitiesController(
        IEntityRepository entityRepository,
        IScenarioRepository scenarioRepository,
        ILogger<EntitiesController> logger)
    {
        _entityRepository = entityRepository;
        _scenarioRepository = scenarioRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get all entities for a specific scenario with optional filtering and sorting
    /// </summary>
    /// <param name="scenarioId">The scenario ID</param>
    /// <param name="filters">Optional filter and sort parameters</param>
    /// <remarks>
    /// Supports filtering by EntityType and TaskForce,
    /// and sorting by name, type, taskForce, latitude, longitude, or updatedAt.
    /// </remarks>
    [HttpGet("scenarios/{scenarioId}/entities")]
    [ProducesResponseType(typeof(IEnumerable<EntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<EntityDto>>> GetEntitiesByScenario(Guid scenarioId, [FromQuery] EntityFilterParams filters)
    {
        // TODO(candidate): Implement entity retrieval with filtering and sorting
        return null;
    }

    /// <summary>
    /// Get an entity by ID
    /// </summary>
    [HttpGet("{entityId}")]
    [ProducesResponseType(typeof(EntityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EntityDto>> GetEntityById(Guid entityId)
    {
        // TODO(candidate): Implement entity retrieval by ID
        return null;
    }

    /// <summary>
    /// Create a new entity for a scenario
    /// </summary>
    [HttpPost("scenarios/{scenarioId}/entities")]
    [ProducesResponseType(typeof(EntityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EntityDto>> CreateEntity(Guid scenarioId, [FromBody] CreateEntityRequest request)
    {
        // TODO(candidate): Implement entity creation
        return null;
    }
}

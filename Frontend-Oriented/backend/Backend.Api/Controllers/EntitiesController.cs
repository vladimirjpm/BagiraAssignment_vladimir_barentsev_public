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
    /// Get all entities for a specific scenario
    /// </summary>
    [HttpGet("scenarios/{scenarioId}/entities")]
    [ProducesResponseType(typeof(IEnumerable<EntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<EntityDto>>> GetEntitiesByScenario(Guid scenarioId)
    {
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
        return null;
    }
}

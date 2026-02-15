using Microsoft.AspNetCore.Mvc;
using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for managing entities
/// </summary>
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
        try
        {
            // TODO(candidate): Create Application layer with business logic
            // TODO(candidate): Implement proper 404 handling if scenario not found
            // TODO(candidate): Map domain models to DTOs
            var entities = await _entityRepository.GetByScenarioIdAsync(scenarioId);
            var entityDtos = entities.Select(e => new EntityDto
            {
                Id = e.Id,
                ScenarioId = e.ScenarioId,
                Type = e.Type,
                TaskForce = e.TaskForce,
                Name = e.Name,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                UpdatedAt = e.UpdatedAt
            });
            return Ok(entityDtos);
        }
        catch (NotImplementedException)
        {
            // TODO(candidate): Remove this catch block once logic is implemented
            return StatusCode(500, new ErrorResponse
            {
                Message = "Not implemented yet",
                Errors = null
            });
        }
        catch (Exception ex)
        {
            // TODO(candidate): Implement proper error handling
            _logger.LogError(ex, "Error retrieving entities for scenario {ScenarioId}", scenarioId);
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while retrieving entities",
                Errors = null
            });
        }
    }

    /// <summary>
    /// Get an entity by ID
    /// </summary>
    [HttpGet("{entityId}")]
    [ProducesResponseType(typeof(EntityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EntityDto>> GetEntityById(Guid entityId)
    {
        try
        {
            // TODO(candidate): Create Application layer with business logic
            // TODO(candidate): Implement proper 404 handling
            var entity = await _entityRepository.GetByIdAsync(entityId);
            if (entity == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Entity with id {entityId} not found",
                    Errors = null
                });
            }
            var entityDto = new EntityDto
            {
                Id = entity.Id,
                ScenarioId = entity.ScenarioId,
                Type = entity.Type,
                TaskForce = entity.TaskForce,
                Name = entity.Name,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                UpdatedAt = entity.UpdatedAt
            };
            return Ok(entityDto);
        }
        catch (NotImplementedException)
        {
            // TODO(candidate): Remove this catch block once logic is implemented
            return StatusCode(500, new ErrorResponse
            {
                Message = "Not implemented yet",
                Errors = null
            });
        }
        catch (Exception ex)
        {
            // TODO(candidate): Implement proper error handling
            _logger.LogError(ex, "Error retrieving entity {EntityId}", entityId);
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while retrieving the entity",
                Errors = null
            });
        }
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
        try
        {
            if (!ModelState.IsValid)
            {
                // TODO(candidate): Format validation errors properly
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new ErrorResponse
                {
                    Message = "Validation failed",
                    Errors = errors.ToDictionary(k => k.Key, v => v.Value)
                });
            }

            // TODO(candidate): Create Application layer with business logic
            // TODO(candidate): Validate scenarioId exists (return 404 if not found)
            // TODO(candidate): Validate entity type is from allowed list
            // TODO(candidate): Validate taskForce is Friendly or Enemy
            // TODO(candidate): Validate latitude is between -90 and 90
            // TODO(candidate): Validate longitude is between -180 and 180
            // TODO(candidate): Set CreatedAt and UpdatedAt timestamps
            // TODO(candidate): Implement CreatedAtAction with proper location header
            var entity = new Entity
            {
                Id = Guid.NewGuid(),
                ScenarioId = scenarioId,
                Type = request.Type,
                TaskForce = request.TaskForce,
                Name = request.Name,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var createdEntity = await _entityRepository.AddAsync(entity);
            var entityDto = new EntityDto
            {
                Id = createdEntity.Id,
                ScenarioId = createdEntity.ScenarioId,
                Type = createdEntity.Type,
                TaskForce = createdEntity.TaskForce,
                Name = createdEntity.Name,
                Latitude = createdEntity.Latitude,
                Longitude = createdEntity.Longitude,
                UpdatedAt = createdEntity.UpdatedAt
            };
            return CreatedAtAction(nameof(GetEntityById), new { entityId = entityDto.Id }, entityDto);
        }
        catch (NotImplementedException)
        {
            // TODO(candidate): Remove this catch block once logic is implemented
            return StatusCode(500, new ErrorResponse
            {
                Message = "Not implemented yet",
                Errors = null
            });
        }
        catch (Exception ex)
        {
            // TODO(candidate): Implement proper error handling
            _logger.LogError(ex, "Error creating entity for scenario {ScenarioId}", scenarioId);
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while creating the entity",
                Errors = null
            });
        }
    }

}

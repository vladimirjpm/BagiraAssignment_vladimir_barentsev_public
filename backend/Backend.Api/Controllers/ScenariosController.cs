using Microsoft.AspNetCore.Mvc;
using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Backend.Domain.Models;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for managing scenarios
/// </summary>
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
        try
        {
            // TODO(candidate): Create Application layer with business logic
            // TODO(candidate): Implement error handling and proper status codes
            // TODO(candidate): Map domain models to DTOs
            var scenarios = await _scenarioRepository.GetAllAsync();
            var scenarioDtos = new List<ScenarioListItemDto>();
            
            foreach (var scenario in scenarios)
            {
                var entities = await _entityRepository.GetByScenarioIdAsync(scenario.Id);
                var entityCount = entities.Count();
                
                scenarioDtos.Add(new ScenarioListItemDto
                {
                    Id = scenario.Id,
                    Name = scenario.Name,
                    Description = scenario.Description,
                    EntityCount = entityCount,
                    UpdatedAt = scenario.UpdatedAt
                });
            }
            
            return Ok(scenarioDtos);
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
            _logger.LogError(ex, "Error retrieving scenarios");
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while retrieving scenarios",
                Errors = null
            });
        }
    }

    /// <summary>
    /// Get a scenario by ID
    /// </summary>
    [HttpGet("{scenarioId}")]
    [ProducesResponseType(typeof(ScenarioDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScenarioDetailsDto>> GetScenarioById(Guid scenarioId)
    {
        try
        {
            // TODO(candidate): Create Application layer with business logic
            // TODO(candidate): Implement proper 404 handling
            var scenario = await _scenarioRepository.GetByIdAsync(scenarioId);
            if (scenario == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Scenario with id {scenarioId} not found",
                    Errors = null
                });
            }
            var scenarioDto = new ScenarioDetailsDto
            {
                Id = scenario.Id,
                Name = scenario.Name,
                Description = scenario.Description,
                CreatedAt = scenario.CreatedAt,
                UpdatedAt = scenario.UpdatedAt
            };
            return Ok(scenarioDto);
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
            _logger.LogError(ex, "Error retrieving scenario {ScenarioId}", scenarioId);
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while retrieving the scenario",
                Errors = null
            });
        }
    }

    /// <summary>
    /// Create a new scenario
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ScenarioDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScenarioDetailsDto>> CreateScenario([FromBody] CreateScenarioRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // Basic validation error formatting (ModelState already handles required fields)
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
            // Note: Most validation (name required, etc.) is handled by the frontend and ModelState/Data Annotations.
            // Backend only needs basic required field validation (already handled by ModelState).
            // TODO(candidate): Set CreatedAt and UpdatedAt timestamps
            // TODO(candidate): Implement CreatedAtAction with proper location header
            var scenario = new Scenario
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var createdScenario = await _scenarioRepository.AddAsync(scenario);
            var scenarioDto = new ScenarioDetailsDto
            {
                Id = createdScenario.Id,
                Name = createdScenario.Name,
                Description = createdScenario.Description,
                CreatedAt = createdScenario.CreatedAt,
                UpdatedAt = createdScenario.UpdatedAt
            };
            return CreatedAtAction(nameof(GetScenarioById), new { scenarioId = scenarioDto.Id }, scenarioDto);
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
            _logger.LogError(ex, "Error creating scenario");
            return StatusCode(500, new ErrorResponse
            {
                Message = "An error occurred while creating the scenario",
                Errors = null
            });
        }
    }

}

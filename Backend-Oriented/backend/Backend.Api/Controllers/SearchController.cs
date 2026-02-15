using Backend.Api.DTOs;
using Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

/// <summary>
/// Controller for global search across scenarios and entities
/// </summary>
/// TODO(candidate): Implement search logic
[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IScenarioRepository _scenarioRepository;
    private readonly IEntityRepository _entityRepository;
    private readonly ILogger<SearchController> _logger;

    public SearchController(
        IScenarioRepository scenarioRepository,
        IEntityRepository entityRepository,
        ILogger<SearchController> logger)
    {
        _scenarioRepository = scenarioRepository;
        _entityRepository = entityRepository;
        _logger = logger;
    }

    /// <summary>
    /// Search across scenarios and entities
    /// </summary>
    /// <param name="query">Search term to match against scenario names/descriptions and entity names/callsigns</param>
    /// <returns>Combined search results with matching scenarios and entities</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SearchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SearchResultDto>> Search([FromQuery] string query)
    {
        // TODO(candidate): Implement search logic
        return null;
    }
}

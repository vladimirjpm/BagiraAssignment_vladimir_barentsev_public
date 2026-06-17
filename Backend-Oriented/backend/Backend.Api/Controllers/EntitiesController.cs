using Backend.Application.Entities;
using Backend.Application.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

// Route shape is fixed by the frontend services: entities live under
// /api/entities, with scenario-scoped list/create at
// /api/entities/scenarios/{scenarioId}/entities.
[ApiController]
[Route("api/entities")]
public sealed class EntitiesController : ControllerBase
{
    private readonly IEntityService _service;

    public EntitiesController(IEntityService service) => _service = service;

    /// <summary>GET /api/entities/{id}</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EntityDto>> Get(Guid id, CancellationToken ct)
    {
        var entity = await _service.GetAsync(id, ct);
        return Ok(entity);
    }

    /// <summary>GET /api/entities/scenarios/{scenarioId}/entities?type=&amp;taskForce=&amp;sortBy=&amp;sortOrder=</summary>
    [HttpGet("scenarios/{scenarioId:guid}/entities")]
    public async Task<ActionResult<IReadOnlyList<EntityDto>>> ListByScenario(
        Guid scenarioId, [FromQuery] EntityQuery query, CancellationToken ct)
    {
        var entities = await _service.ListByScenarioAsync(scenarioId, query, ct);
        return Ok(entities);
    }

    /// <summary>POST /api/entities/scenarios/{scenarioId}/entities</summary>
    [HttpPost("scenarios/{scenarioId:guid}/entities")]
    public async Task<ActionResult<EntityDto>> Create(
        Guid scenarioId, [FromBody] CreateEntityRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(scenarioId, request, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    /// <summary>PUT /api/entities/{id}</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EntityDto>> Update(
        Guid id, [FromBody] UpdateEntityRequest request, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        return Ok(updated);
    }

    /// <summary>DELETE /api/entities/{id}</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}

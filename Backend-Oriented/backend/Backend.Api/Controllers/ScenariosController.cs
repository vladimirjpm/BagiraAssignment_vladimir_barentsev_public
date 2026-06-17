using Backend.Application.Scenarios;
using Backend.Application.Scenarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/scenarios")]
public sealed class ScenariosController : ControllerBase
{
    private readonly IScenarioService _service;

    public ScenariosController(IScenarioService service) => _service = service;

    /// <summary>GET /api/scenarios?name=&amp;description=&amp;sortBy=&amp;sortOrder=</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ScenarioDto>>> List(
        [FromQuery] ScenarioQuery query, CancellationToken ct)
    {
        var scenarios = await _service.ListAsync(query, ct);
        return Ok(scenarios);
    }

    /// <summary>GET /api/scenarios/{id}</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScenarioDto>> Get(Guid id, CancellationToken ct)
    {
        var scenario = await _service.GetAsync(id, ct);
        return Ok(scenario);
    }

    /// <summary>POST /api/scenarios</summary>
    [HttpPost]
    public async Task<ActionResult<ScenarioDto>> Create(
        [FromBody] CreateScenarioRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    /// <summary>PUT /api/scenarios/{id}</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ScenarioDto>> Update(
        Guid id, [FromBody] UpdateScenarioRequest request, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        return Ok(updated);
    }

    /// <summary>DELETE /api/scenarios/{id} — cascades to the scenario's entities.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}

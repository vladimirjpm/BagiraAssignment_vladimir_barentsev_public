using Backend.Application.Search;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/search")]
public sealed class SearchController : ControllerBase
{
    private readonly ISearchService _service;

    public SearchController(ISearchService service) => _service = service;

    /// <summary>GET /api/search?q=...</summary>
    [HttpGet]
    public async Task<ActionResult<SearchResultDto>> Search([FromQuery] string? q, CancellationToken ct)
    {
        var result = await _service.SearchAsync(q ?? string.Empty, ct);
        return Ok(result);
    }
}

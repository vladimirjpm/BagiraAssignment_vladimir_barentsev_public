namespace Backend.Api.DTOs;

/// <summary>
/// Query parameters for filtering and sorting scenarios
/// </summary>
public class ScenarioFilterParams
{
    /// <summary>
    /// Filter scenarios by name (partial match)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Filter scenarios by description (partial match)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Field to sort by (e.g., "name", "description", "entityCount", "updatedAt")
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort order: "asc" or "desc" (default: "asc")
    /// </summary>
    public string? SortOrder { get; set; } = "asc";
}

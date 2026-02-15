using Backend.Domain.Models;

namespace Backend.Api.DTOs;

/// <summary>
/// Query parameters for filtering and sorting entities
/// </summary>
public class EntityFilterParams
{
    /// <summary>
    /// Filter entities by EntityType
    /// </summary>
    public EntityType? Type { get; set; }

    /// <summary>
    /// Filter entities by TaskForce
    /// </summary>
    public TaskForce? TaskForce { get; set; }

    /// <summary>
    /// Field to sort by (e.g., "name", "type", "taskForce", "latitude", "longitude", "updatedAt")
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort order: "asc" or "desc" (default: "asc")
    /// </summary>
    public string? SortOrder { get; set; } = "asc";
}

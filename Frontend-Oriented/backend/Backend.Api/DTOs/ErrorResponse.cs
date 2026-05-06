namespace Backend.Api.DTOs;

/// <summary>
/// Standard error response shape
/// </summary>
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? Errors { get; set; }
}

namespace Backend.Api.DTOs;

/// <summary>
/// Standard error response shape
/// </summary>
/// TODO(candidate): Consider adding more fields and validation attributes as needed
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? Errors { get; set; }
}

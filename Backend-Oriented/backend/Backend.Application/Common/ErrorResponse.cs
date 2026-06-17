namespace Backend.Application.Common;

/// <summary>
/// Uniform error body returned for every 400/404/500.
/// The <c>Message</c> field is contract-critical: the frontend reads
/// <c>error.message</c> when a request fails.
/// </summary>
public sealed record ErrorResponse(
    string Message,
    int Status,
    IReadOnlyDictionary<string, string[]>? Errors = null);

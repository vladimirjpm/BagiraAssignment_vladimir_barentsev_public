namespace Backend.Domain.Exceptions;

/// <summary>
/// Thrown when a requested resource does not exist.
/// Surfaces as HTTP 404 at the API boundary.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public static NotFoundException For(string resource, object id) =>
        new($"{resource} '{id}' was not found.");
}

namespace Backend.Domain.Exceptions;

/// <summary>
/// Thrown when a domain invariant is violated (e.g. invalid coordinates,
/// missing required field). Surfaces as HTTP 400 at the API boundary.
/// </summary>
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

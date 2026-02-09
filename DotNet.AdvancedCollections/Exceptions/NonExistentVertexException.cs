namespace DotNet.AdvancedCollections.Exceptions;

/// <summary>
/// Exception thrown when a vertex that does not exist is referenced.
/// </summary>
public class NonExistentVertexException : Exception
{
    /// <summary>
    /// Constructs a new instance of the exception with a default message.
    /// </summary>
    public NonExistentVertexException() : base("The vertex does not exist!") { }

    /// <summary>
    /// Constructs a new instance of the exception with a custom message.
    /// </summary>
    /// <param name="message">The custom message to be included with the exception.</param>
    public NonExistentVertexException(string message)
      : base(message) { }
}

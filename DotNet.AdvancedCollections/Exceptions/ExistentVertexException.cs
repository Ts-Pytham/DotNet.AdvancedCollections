namespace DotNet.AdvancedCollections.Exceptions;

/// <summary>
/// Exception thrown when a vertex that does exist is referenced.
/// </summary>
public class ExistentVertexException : Exception
{
    /// <summary>
    /// Constructs a new instance of the exception with a default message.
    /// </summary>
    public ExistentVertexException() : base("The vertex does exist!") { }

    /// <summary>
    /// Constructs a new instance of the exception with a custom message.
    /// </summary>
    /// <param name="message">The custom message to be included with the exception.</param>
    public ExistentVertexException(string message)
      : base(message) { }
}

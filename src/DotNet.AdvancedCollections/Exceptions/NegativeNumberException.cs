namespace DotNet.AdvancedCollections.Exceptions;

/// <summary>
/// The <see cref="NegativeNumberException"/> class represents an error that occurs when a negative number is provided where a 
/// non-negative number is expected.
/// </summary>
public class NegativeNumberException : Exception
{
    /// <summary>
    /// The <see cref="NegativeNumberException"/> class represents an error that occurs when a negative number is provided where a 
    /// non-negative number is expected.
    /// </summary>
    public NegativeNumberException()
        : base("The value cannot be negative!") 
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NegativeNumberException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NegativeNumberException(string message) 
        : base(message) { }

}

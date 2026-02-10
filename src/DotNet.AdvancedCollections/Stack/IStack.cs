namespace DotNet.AdvancedCollections.Stack;

/// <summary>
/// Represents an interface for a stack data structure.
/// </summary>
/// <typeparam name="T">The type of elements in the stack.</typeparam>
public interface IStack<T> : ICollection<T>
    where T : notnull
{
    /// <summary>
    /// Gets the maximum number of elements that the stack can hold.
    /// A value of -1 indicates that the stack has no capacity limit.
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Returns the element at the top of the stack without removing it.
    /// </summary>
    /// <returns>The element at the top of the stack.</returns>
    T Peek();

    /// <summary>
    /// Returns a value that indicates whether there is an object at the top of the 
    /// <see cref="IStack{T}"/>, and if one is present, copies it to the result parameter. 
    /// The object is not removed from the <see cref="IStack{T}"/>.
    /// </summary>
    /// <param name="result">If present, the object at the top of the <see cref="IStack{T}"/>; otherwise, the default value of `T`.</param>
    /// <returns><see langword="true"/> if there is an object at the top of the <see cref="IStack{T}"/>; <see langword="false"/> if the <see cref="IStack{T}"/> is empty.</returns>
    bool TryPeek(out T? result);

    /// <summary>
    /// Removes and returns the element at the top of the stack.
    /// </summary>
    /// <returns>The element at the top of the stack.</returns>
    T Pop();

    /// <summary>
    /// Returns a value that indicates whether there is an object at the top of the 
    /// <see cref="IStack{T}"/>, and if one is present, copies it to the result parameter,
    /// and removes it from the <see cref="IStack{T}"/>.
    /// </summary>
    /// <param name="result">If present, the object at the top of the <see cref="IStack{T}"/>; otherwise, the default value of `T`.</param>
    /// <returns><see langword="true"/> if there is an object at the top of the <see cref="IStack{T}"/>; <see langword="false"/> if the <see cref="IStack{T}"/> is empty.</returns>
    bool TryPop(out T? result);

}
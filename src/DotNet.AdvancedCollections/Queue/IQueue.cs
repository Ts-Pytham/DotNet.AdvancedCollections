namespace DotNet.AdvancedCollections.Queue;

/// <summary>
/// This interface defines the operations that a queue data structure should support.
/// A queue is a collection of elements that are inserted and removed according to the first-in-first-out (FIFO) principle.
/// </summary>
/// <typeparam name="T">The type of elements in the queue.</typeparam>
public interface IQueue<T> : ICollection<T>
    where T : notnull
{
    /// <summary>
    /// Gets the maximum number of elements that the queue can hold.
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Removes and returns the first element in the queue.
    /// </summary>
    /// <returns>The first element in the queue.</returns>
    T Dequeue();

    /// <summary>
    /// Returns a value that indicates whether there is an object at the beginning of the 
    /// <see cref="IQueue{T}"/>, and if one is present, copies it to the result parameter,
    /// and removes it from the <see cref="IQueue{T}"/>.
    /// </summary>
    /// <param name="result">If present, the object at the beginning of the <see cref="IQueue{T}"/>; otherwise, the default value of `T`.</param>
    /// <returns><see langword="true"/> if the object is successfully removed; <see langword="false"/> if the <see cref="IQueue{T}"/> is empty.</returns>
    bool TryDequeue(out T? result);

    /// <summary>
    /// Returns the first element in the queue without removing it.
    /// </summary>
    /// <returns>The first element in the queue.</returns>
    T Peek();

    /// <summary>
    /// Returns a value that indicates whether there is an object
    /// <see cref="IQueue{T}"/>, and if one is present, copies it to the result parameter. 
    /// The object is not removed from the <see cref="IQueue{T}"/>.
    /// </summary>
    /// <param name="result">If present, the object at the beginning of the <see cref="IQueue{T}"/>; otherwise, the default value of `T`.</param>
    /// <returns><see langword="true"/> if there is an object at the beginning of the <see cref="IQueue{T}"/>; <see langword="false"/> if the <see cref="IQueue{T}"/> is empty.</returns>
    bool TryPeek(out T? result);
}
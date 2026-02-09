namespace DotNet.AdvancedCollections.Queue.Deque;

/// <summary>
/// This interface represents a double-ended queue (deque).
/// A deque is a queue that supports adding and removing elements from both ends of the queue.
/// </summary>
/// <typeparam name="T">The type of elements in the deque.</typeparam>
public interface IDeque<T> : IQueue<T>
    where T : notnull
{
    /// <summary>
    /// Adds the specified element to the front of the deque.
    /// </summary>
    /// <param name="item">The element to add to the front of the deque.</param>
    void PushFirst(T item);

    /// <summary>
    /// Adds the specified element to the back of the deque.
    /// </summary>
    /// <param name="item">The element to add to the back of the deque.</param>
    void PushLast(T item);

    /// <summary>
    /// Removes and returns the last element in the deque.
    /// </summary>
    /// <returns>The last element in the deque.</returns>
    T PopLast();

    /// <summary>
    /// Returns the last element in the deque without removing it.
    /// </summary>
    /// <returns>The last element in the deque.</returns>
    T PeekLast();
}

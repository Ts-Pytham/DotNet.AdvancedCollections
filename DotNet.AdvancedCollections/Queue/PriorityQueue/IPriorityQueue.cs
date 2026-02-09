namespace DotNet.AdvancedCollections.Queue.PriorityQueue;

/// <summary>
/// This interface defines the operations that a priority queue data structure should support.
/// </summary>
/// <typeparam name="T">The type of elements in the priority queue.</typeparam>
public interface IPriorityQueue<T> : IQueue<T>
    where T : notnull
{
    /// <summary>
    /// Adds an element to the priority queue with the specified priority.
    /// </summary>
    /// <param name="item">The element to add to the priority queue.</param>
    /// <param name="priority">The priority of the element.</param>
    void Enqueue(T item, int priority);
}
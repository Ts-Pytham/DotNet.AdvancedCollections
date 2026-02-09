namespace DotNet.AdvancedCollections.Stack.PriorityStack;

/// <summary>
/// Represents an interface for a priority stack data structure.
/// </summary>
/// <typeparam name="T">The type of elements in the stack.</typeparam>
public interface IPriorityStack<T> : IStack<T>
    where T : notnull
{
    /// <summary>
    /// Adds the specified element to the stack with the given priority.
    /// Elements with higher priority values will be popped from the stack before
    /// elements with lower priority values.
    /// </summary>
    /// <param name="item">The element to add to the stack.</param>
    /// <param name="priority">The priority of the element.</param>
    void Push(T item, int priority);

}

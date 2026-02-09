namespace DotNet.AdvancedCollections.List.DoublyLinkedList;

/// <summary>
/// Represents a doubly linked list node.
/// </summary>
/// <typeparam name="T">The type of the value stored in the node.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="Node{T}"/> class with the specified value.
/// </remarks>
/// <param name="value">The value to be stored in the node.</param>
internal class Node<T>(T value) where T : notnull
{
    /// <summary>
    /// Gets or sets the value stored in the node.
    /// </summary>
    public T Value { get; set; } = value;

    /// <summary>
    /// Gets or sets the previous node in the linked list.
    /// </summary>
    public Node<T>? Previous { get; set; } = null;

    /// <summary>
    /// Gets or sets the next node in the linked list.
    /// </summary>
    public Node<T>? Next { get; set; } = null;
}
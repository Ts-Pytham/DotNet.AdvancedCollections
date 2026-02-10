namespace DotNet.AdvancedCollections.List.DoublyLinkedList;

/// <summary>
/// Represents a doubly linked list data structure that stores a collection of items.
/// </summary>
/// <typeparam name="T">The type of elements in the linked list.</typeparam>
public interface IDoublyLinkedList<T> : ICollection<T>, IEnumerable<T>
{
    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    T this[int index] { get; set; }

    /// <summary>
    /// Adds an item to the end of the linked list.
    /// </summary>
    /// <param name="item">The item to add to the linked list.</param>
    void AddLast(T item);

    /// <summary>
    /// Finds the index of the specified item in the linked list.
    /// </summary>
    /// <param name="item">The item to search for in the linked list.</param>
    /// <returns>The index of the item if found, or -1 if the item is not found.</returns>
    int Find(T item);

    /// <summary>
    /// Gets the last item in the linked list.
    /// </summary>
    /// <returns>The last item in the linked list.</returns>
    T? GetLast();

    /// <summary>
    /// Gets the first item in the linked list.
    /// </summary>
    /// <returns>The first item in the linked list.</returns>
    T? GetFirst();
}
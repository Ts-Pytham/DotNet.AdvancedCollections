namespace DotNet.AdvancedCollections.List.SortedList;

/// <summary>
/// Represents SortedList Interface.
/// </summary>
public interface ISortedList<T> : ICollection<T>, IEnumerable<T>
    where T: notnull
{
    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index of the element to get or set.
    /// </param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// index is less than 0. -or- index is equal to or greater than Count.
    /// </exception>
    T this[int index] { get; set; }

    /// <summary>
    /// Determines the index of a specific item.
    /// </summary>
    /// <param name="item">The object to find.</param>
    /// <returns>The index of item if found in the list; otherwise, -</returns>
    int IndexOf(T item);

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// index is less than 0. -or- index is equal to or greater than Count.
    /// </exception>
    /// <exception cref="System.NotSupportedException">
    /// The SortedList is read-only.
    /// </exception>
    void RemoveAt(int index);
}
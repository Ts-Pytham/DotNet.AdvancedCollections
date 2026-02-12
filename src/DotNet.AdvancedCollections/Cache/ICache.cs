namespace DotNet.AdvancedCollections.Cache;

/// <summary>
/// Defines a generic cache that stores key-value pairs and provides methods for adding, retrieving, and removing items.
/// </summary>
/// <remarks>Implementations of this interface may have different eviction policies, thread safety guarantees, or
/// capacity management strategies. Refer to the specific implementation for details on behavior such as item
/// expiration, concurrency, or performance characteristics.</remarks>
/// <typeparam name="TKey">The type of keys in the cache. Must be non-nullable.</typeparam>
/// <typeparam name="TValue">The type of values in the cache. Must be non-nullable.</typeparam>
public interface ICache<TKey, TValue>
    where TKey   : notnull
    where TValue : notnull
{
    /// <summary>
    /// Gets the number of elements contained in the collection.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the total number of elements that the collection can hold without resizing.
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// Attempts to retrieve the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose associated value is to be retrieved.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise,
    /// the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>true if the object that implements the method contains an element with the specified key; otherwise, false.</returns>
    bool TryGet(TKey key, out TValue value);

    /// <summary>
    /// Adds or updates the value associated with the specified key.
    /// </summary>
    /// <remarks>If the key already exists, its value is updated; otherwise, a new key-value pair is
    /// added.</remarks>
    /// <param name="key">The key with which the value will be associated. Cannot be null.</param>
    /// <param name="value">The value to associate with the specified key.</param>
    void Put(TKey key, TValue value);

    /// <summary>
    /// Removes the element with the specified key from the collection.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    void Remove(TKey key);

    /// <summary>
    /// Removes all items from the collection.
    /// </summary>
    void Clear();

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the dictionary.</param>
    /// <returns><see langword="true"/> if the dictionary contains an element with the specified key; otherwise, <see
    /// langword="false"/>.</returns>
    bool ContainsKey(TKey key);

    /// <summary>
    /// Attempts to add the specified key and value to the collection.
    /// </summary>
    /// <remarks>This method does not throw an exception if the key already exists in the collection. The
    /// behavior regarding null keys or values depends on the specific implementation.</remarks>
    /// <param name="key">The key of the element to add. Cannot be null if null keys are not supported by the collection implementation.</param>
    /// <param name="value">The value of the element to add.</param>
    /// <returns>true if the key and value pair was added successfully; otherwise, false. Returns false if an element with the
    /// same key already exists.</returns>
    bool TryAdd(TKey key, TValue value);
}

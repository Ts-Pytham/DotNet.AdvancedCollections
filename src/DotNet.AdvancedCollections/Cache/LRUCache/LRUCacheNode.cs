namespace DotNet.AdvancedCollections.Cache;

/// <summary>
/// Represents a node in a doubly linked list used for an LRU (Least Recently Used) cache, containing a key-value pair
/// and references to adjacent nodes.
/// </summary>
/// <remarks>LRUCacheNode is typically used internally by LRU cache implementations to track the order of usage
/// and facilitate efficient eviction of least recently used items. Each node maintains references to its previous and
/// next nodes, enabling fast updates to the linked list structure.</remarks>
/// <typeparam name="TKey">The type of the key stored in the cache node. Must be non-null.</typeparam>
/// <typeparam name="TValue">The type of the value stored in the cache node. Must be non-null.</typeparam>
/// <param name="key">The key associated with the cache entry. Cannot be null.</param>
/// <param name="value">The value associated with the cache entry. Cannot be null.</param>
internal sealed class LRUCacheNode<TKey, TValue>(TKey key, TValue value)
    where TKey   : notnull
    where TValue : notnull
{
    /// <summary>
    /// Gets the key associated with the cache entry. This property is read-only and is set during initialization.
    /// </summary>
    public TKey Key { get; } = key;

    /// <summary>
    /// Gets or sets the value associated with the current instance.
    /// </summary>
    public TValue Value { get; set; } = value;

    /// <summary>
    /// Gets or sets the previous node in the linked list used by the LRU cache.
    /// <remarks> This property is typically used to traverse or modify the order of nodes within the cache's internal linked list.
    /// Setting this property to <see langword="null"/> indicates that the node is the beginning of the list.</remarks>
    /// </summary>
    public LRUCacheNode<TKey, TValue>? Previous { get; set; } = null;

    /// <summary>
    /// Gets or sets the next node in the linked list used by the LRU cache.
    /// </summary>
    /// <remarks>This property is typically used to traverse or modify the order of nodes within the cache's
    /// internal linked list. Setting this property to <see langword="null"/> indicates that the node is the end of the
    /// list.</remarks>
    public LRUCacheNode<TKey, TValue>? Next { get; set; } = null;
}

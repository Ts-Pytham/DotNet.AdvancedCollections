using System.Diagnostics.CodeAnalysis;

namespace DotNet.AdvancedCollections.Cache;

/// <summary>
/// Represents a fixed-capacity, thread-unsafe cache that stores key-value pairs and evicts the least recently used
/// items when the capacity is exceeded.
/// </summary>
/// <remarks>The LRUCache class implements a least-recently-used (LRU) eviction policy, ensuring that the most
/// recently accessed items remain in the cache while older, less frequently accessed items are removed when the cache
/// reaches its capacity. This class is not thread-safe; external synchronization is required if used concurrently from
/// multiple threads.</remarks>
/// <typeparam name="TKey">The type of keys in the cache. Keys must be non-null and are used to uniquely identify cached values.</typeparam>
/// <typeparam name="TValue">The type of values stored in the cache. Values must be non-null.</typeparam>
public class LRUCache<TKey, TValue> : ICache<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    private readonly Dictionary<TKey, LRUCacheNode<TKey, TValue>> _cache;

    private LRUCacheNode<TKey, TValue>? _head;
    private LRUCacheNode<TKey, TValue>? _tail;

    /// <inheritdoc cref="ICache{TKey, TValue}.Count"/>
    public int Count { get; private set; } = 0;

    /// <inheritdoc cref="ICache{TKey, TValue}.Capacity"/>
    public int Capacity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of key-value pairs the cache can hold. Must be a positive integer. The default is 10.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="capacity"/> is less than or equal to zero.</exception>
    public LRUCache(int capacity = 10)
    {
        _cache = [];
        _head = null;
        _tail = null;
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        Capacity = capacity;
    }

    /// <inheritdoc cref="ICache{TKey, TValue}.Clear"/>
    public void Clear()
    {
        _cache.Clear();
        _head = null;
        _tail = null;
        Count = 0;
    }

    /// <inheritdoc cref="ICache{TKey, TValue}.Put(TKey, TValue)"/>
    public void Put(TKey key, TValue value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            node.Value = value;
            MoveToFront(node);
            return;
        }

        if (Count == Capacity)
            RemoveLast();

        AddFirst(key, value);
    }

    /// <inheritdoc cref="ICache{TKey, TValue}.ContainsKey(TKey)"/>
    public bool ContainsKey(TKey key)
        => _cache.ContainsKey(key);

    /// <inheritdoc cref="ICache{TKey, TValue}.Remove"/>
    public void Remove(TKey key)
    {
        if (!_cache.TryGetValue(key, out var node))
            return;

        RemoveNode(node);
        _cache.Remove(key);
    }

    /// <inheritdoc cref="ICache{TKey, TValue}.TryAdd(TKey, TValue)"/>
    public bool TryAdd(TKey key, TValue value)
    {
        if (_cache.ContainsKey(key))
            return false;

        if (Count == Capacity)
            RemoveLast();

        AddFirst(key, value);

        return true;
    }

    /// <inheritdoc cref="ICache{TKey, TValue}.TryGet(TKey, out TValue)"/>
    public bool TryGet(TKey key, [NotNullWhen(true)] out TValue? value)
    {
        if(_cache.TryGetValue(key, out var node))
        {
            value = node.Value;
            MoveToFront(node);
            return true;
        }

        value = default;

        return false;
    }

    /// <summary>
    /// Adds a new entry to the cache and places it at the front of the least recently used (LRU) list.
    /// </summary>
    /// <remarks>If the cache is empty, the new entry becomes both the head and tail of the LRU list.
    /// Otherwise, the entry is inserted at the head, making it the most recently used. Existing entries with the same
    /// key will be replaced.</remarks>
    /// <param name="key">The key associated with the cache entry to add. Cannot be null.</param>
    /// <param name="value">The value to store in the cache entry.</param>
    private void AddFirst(TKey key, TValue value)
    {
        var newNode = new LRUCacheNode<TKey, TValue>(key, value);

        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }

        _cache[key] = newNode;
        Count++;
    }

    /// <summary>
    /// Removes the last element from the collection.
    /// </summary>
    /// <remarks>If the collection is empty, this method performs no action. After removal, the count of
    /// elements is decremented. This method is typically used to maintain a fixed-size cache or collection by
    /// discarding the least recently added item.</remarks>
    private void RemoveLast()
    {
        if (_tail is null)
            return;
        _cache.Remove(_tail.Key);
        if (_tail.Previous is not null)
        {
            _tail = _tail.Previous;
            _tail.Next = null;
        }
        else
        {
            _head = null;
            _tail = null;
        }
        Count--;
    }

    /// <summary>
    /// Removes the specified node from the linked list used by the LRU cache.
    /// </summary>
    /// <remarks>This method updates the linked list pointers and decrements the cache count. Removing a node
    /// does not affect the stored key-value pairs outside the cache's eviction logic.</remarks>
    /// <param name="node">The node to remove from the cache's linked list. Must not be null.</param>
    private void RemoveNode(LRUCacheNode<TKey, TValue> node)
    {
        if(node.Previous is not null)
            node.Previous.Next = node.Next;
        else
            _head = node.Next;

        if (node.Next is not null)
            node.Next.Previous = node.Previous;
        else
            _tail = node.Previous;

        Count--;
    }

    /// <summary>
    /// Moves the specified cache node to the front of the linked list, updating its position to reflect most recent
    /// access.
    /// </summary>
    /// <remarks>This method is typically used in LRU (Least Recently Used) cache implementations to ensure
    /// that the most recently accessed item is positioned at the front of the list. Moving a node to the front allows
    /// for efficient eviction of least recently used items from the tail of the list.</remarks>
    /// <param name="node">The cache node to move to the front. Must be a valid node within the linked list.</param>
    private void MoveToFront(LRUCacheNode<TKey, TValue> node)
    {
        if (node == _head)
            return;
        if (node == _tail)
        {
            _tail = node.Previous;
            _tail!.Next = null;
        }
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }
        node.Previous = null;
        node.Next = _head;
        _head!.Previous = node;
        _head = node;
    }
}

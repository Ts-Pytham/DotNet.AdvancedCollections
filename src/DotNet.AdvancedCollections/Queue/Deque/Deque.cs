using System.Collections;

namespace DotNet.AdvancedCollections.Queue.Deque;

/// <summary>
/// This class implements a double-ended queue (deque).
/// A deque is a queue that supports adding and removing elements from both ends of the queue.
/// </summary>
/// <typeparam name="T">The type of elements in the deque.</typeparam>
public class Deque<T> : IDeque<T>, IEnumerable<T>
    where T : notnull
{
    /// <summary>
    /// Gets the maximum number of elements the deque can hold.
    /// If the value is -1, the deque can hold an unlimited number of elements.
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    /// Gets the number of elements in the deque.
    /// </summary>
    public int Count { get => _deque.Count; }

    /// <inheritdoc cref="IQueue{T}.IsEmpty"/>
    public bool IsEmpty { get => Count == 0; }

    /// <summary>
    /// Gets a value indicating whether the deque is read-only.
    /// This property always returns false.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// List of elements in the deque.
    /// </summary>
    private readonly List<T> _deque;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deque{T}"/> class.
    /// The deque can hold an unlimited number of elements.
    /// </summary>
    public Deque()
    {
        _deque = [];
        Capacity = -1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Deque{T}"/> class.
    /// The deque can hold a maximum of the specified number of elements.
    /// </summary>
    /// <param name="capacity">The maximum number of elements the deque can hold.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="capacity"/> is less than 1.
    /// </exception>
    public Deque(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);

        _deque = [];
        Capacity = capacity;
    }

    /// <summary>
    /// Adds the specified item to the front of the deque.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        PushFirst(item);
    }

    /// <summary>
    /// Removes all elements from the deque.
    /// </summary>
    public void Clear()
    {
        if (!IsEmpty)
        {
            _deque.Clear();
        }
    }

    /// <summary>
    /// Determines whether the deque contains the specified item.
    /// </summary>
    /// <param name="item">The item to search for in the deque.</param>
    /// <returns>
    /// True if the deque contains the item, false otherwise.
    /// </returns>
    public bool Contains(T item)
    {
        if (IsEmpty)
        {
            return false;
        }
        return _deque.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _deque.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc cref="IQueue{T}.Dequeue"/>
    public T Dequeue()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        var value = _deque[0];
        _deque.RemoveAt(0);
        return value;
    }

    /// <inheritdoc cref="IQueue{T}.TryDequeue(out T)"/>
    public bool TryDequeue(out T? result)
    {
        if (!IsEmpty)
        {
            result = Dequeue();
            return true;
        }
        result = default;

        return false;
    }

    /// <inheritdoc cref="IDeque{T}.PopLast"/>
    public T PopLast()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        var value = _deque[Count - 1];
        _deque.RemoveAt(Count - 1);
        return value;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the deque.
    /// </summary>
    /// <returns>An enumerator for the deque.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _deque.GetEnumerator();
    }

    /// <inheritdoc cref="IQueue{T}.Peek"/>
    public T Peek()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        return _deque[0];
    }

    /// <inheritdoc cref="IQueue{T}.TryPeek(out T)"/>
    public bool TryPeek(out T? result)
    {
        if (!IsEmpty)
        {
            result = _deque[0];
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="IDeque{T}.PeekLast"/>
    public T PeekLast()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        return _deque[Count - 1];
    }

    /// <inheritdoc cref="IDeque{T}.PushFirst(T)"/>
    public void PushFirst(T item)
    {
        if (Capacity != -1 && Capacity == Count)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        _deque.Insert(0, item);
    }

    /// <inheritdoc cref="IDeque{T}.PushLast(T)"/>
    public void PushLast(T item)
    {
        if (Capacity != -1 && Capacity == Count)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        _deque.Insert(Count, item);
    }

    /// <summary>
    /// Removes the first occurrence of the specified item from the deque.
    /// </summary>
    /// <param name="item">The item to remove from the deque.</param>
    /// <returns>
    /// True if the item was found and removed from the deque, false otherwise.
    /// </returns>
    public bool Remove(T item)
    {
        if (IsEmpty)
        {
            return false;
        }

        return _deque.Remove(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the deque.
    /// </summary>
    /// <returns>An enumerator for the deque.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _deque.GetEnumerator();
    }
}
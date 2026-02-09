using DotNet.AdvancedCollections.Utils;
using System.Collections;

namespace DotNet.AdvancedCollections.Queue.PriorityQueue;

/// <summary>
/// This class implements a priority queue data structure.
/// A priority queue is a queue in which each element has an associated priority.
/// Elements with higher priorities are dequeued before elements with lower priorities.
/// </summary>
/// <typeparam name="T">The type of elements in the priority queue.</typeparam>
public class PriorityQueue<T> : IPriorityQueue<T>, IEnumerable<T>, ICollection<T>
    where T : notnull
{
    /// <summary>
    /// list of PriorityObjects that store the elements in the queue along with their priorities.
    /// </summary>
    private readonly List<PriorityObject<T>> _queue;

    public int Count { get => _queue.Count; }

    public bool IsReadOnly { get => false; }

    /// <summary>
    /// Gets the maximum number of elements the <see cref="PriorityQueue{T}"/> can hold.
    /// If the value is -1, the <see cref="PriorityQueue{T}"/> can hold an unlimited number of elements.
    /// </summary>
    public int Capacity
    {
        get;
    }

    /// <inheritdoc cref="IQueue{T}.IsEmpty"/>
    public bool IsEmpty { get => Count == 0; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class with default capacity.
    /// </summary>
    public PriorityQueue()
    {
        _queue = [];
        Capacity = -1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/>  class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of elements that the queue can hold.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified capacity is less than 1.</exception>
    public PriorityQueue(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _queue = new List<PriorityObject<T>>(capacity);
        Capacity = capacity;
    }

    /// <summary>
    /// Adds an element to the <see cref="PriorityQueue{T}"/> with the default priority (0).
    /// </summary>
    /// <param name="item">The element to add to the queue.</param>
    public void Add(T item)
    {
        Enqueue(item, 0);
    }

    /// <summary>
    /// Removes all elements from the queue.
    /// </summary>
    public void Clear()
    {
        if (!IsEmpty)
        {
            _queue.Clear();
        }
    }

    public bool Contains(T item)
    {
        if (IsEmpty)
        {
            return false;
        }

        return ExistsItem(item) != -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _queue.CopyTo([.. array.Select(x => new PriorityObject<T>(value: x, priority: 0))], arrayIndex);
    }

    /// <inheritdoc cref="IQueue{T}.Dequeue"/>
    public T Dequeue()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        var value = _queue[0].Value;
        _queue.RemoveAt(0);
        return value;
    }

    /// <inheritdoc cref="IQueue{T}.TryDequeue(out T?)"/>
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

    /// <inheritdoc cref="IPriorityQueue{T}.Enqueue(T, int)"/>
    public void Enqueue(T item, int priority)
    {
        if (Capacity != -1 && Capacity == Count)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        if (priority < 0)
        {
            throw new NegativeNumberException();
        }
        var value = new PriorityObject<T>(value: item, priority: priority);
        if (IsEmpty)
        {
            _queue.Add(value);
        }
        else
        {
            Enqueue(value);
        }
    }

    /// <summary>
    /// Adds a new element with the specified priority to the priority queue.
    /// The element is inserted into the queue in the correct position based on its priority.
    /// </summary>
    /// <param name="item">The element to add to the priority queue.</param>
    private void Enqueue(PriorityObject<T> item)
    {
        for (int index = 0; index != Count; ++index)
        {
            if (item > _queue[index])
            {
                _queue.Insert(index, item);
                return;
            }
        }
        _queue.Add(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// The enumerator returns the elements in the queue in the order they are dequeued.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the queue.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _queue.Select(x => x.Value).GetEnumerator();
    }

    /// <inheritdoc cref="IQueue{T}.Peek"/>
    public T Peek()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        return _queue[0].Value;
    }

    /// <inheritdoc cref="IQueue{T}.TryPeek(out T)"/>
    public bool TryPeek(out T? result)
    {
        if (!IsEmpty)
        {
            result = Peek();
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Removes the specified element from the priority queue.
    /// If the element is not found in the queue, the method returns false.
    /// If the queue is empty, an IndexOutOfRangeException is thrown.
    /// </summary>
    /// <param name="item">The element to remove from the priority queue.</param>
    /// <returns>True if the element is found and removed from the queue, false otherwise.</returns>
    public bool Remove(T item)
    {
        if (IsEmpty)
            return false;

        int index = ExistsItem(item);

        if (index == -1) return false;

        _queue.RemoveAt(index);

        return true;
    }

    /// <summary>
    /// Determines if the specified element exists in the queue.
    /// If the element exists, the method returns its index in the queue.
    /// Otherwise, the method returns -1.
    /// </summary>
    /// <param name="item">The element to search for in the queue.</param>
    /// <returns>The index of the element in the queue, or -1 if the element is not found.</returns>
    private int ExistsItem(T item)
    {
        for (int i = 0, j = Count - 1; i != Count && j >= 0; ++i, --j)
        {
            if (_queue[i].Value.Equals(item))
            {
                return i;
            }

            if (_queue[j].Value.Equals(item))
            {
                return j;
            }
        }

        return -1;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// The enumerator returns the elements in the queue in the order they are dequeued.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the queue.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _queue.Select(x => x.Value).GetEnumerator();
    }

}

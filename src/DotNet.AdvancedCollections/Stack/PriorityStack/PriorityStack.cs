using DotNet.AdvancedCollections.Exceptions;
using DotNet.AdvancedCollections.Utils;
using System.Collections;

namespace DotNet.AdvancedCollections.Stack.PriorityStack;

/// <summary>
/// This is a PriorityStack class.
/// </summary>
/// <typeparam name="T">The type of data to be stored in the PriorityStack.</typeparam>
public class PriorityStack<T> : IPriorityStack<T>, ICollection<T>, IEnumerable<T>
    where T : notnull
{
    /// <summary>
    /// list of PriorityObjects that store the elements in the stack along with their priorities.
    /// </summary>
    private readonly List<PriorityObject<T>> _stack;

    /// <inheritdoc cref="IStack{T}.Capacity"/>
    public int Capacity { get; }

    public int Count { get => _stack.Count; }

    public bool IsReadOnly => false;

    /// <inheritdoc cref="IStack{T}.IsEmpty"/>
    public bool IsEmpty { get => Count == 0; }

    /// <summary>
    /// Constructs a new instance of the <see cref="PriorityStack{T}"/> class.
    /// </summary>
    public PriorityStack()
    {
        _stack = [];
        Capacity = -1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityStack{T}"/>  class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of elements that the stack can hold.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified capacity is less than 1.</exception>
    public PriorityStack(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);

        _stack = [];
        Capacity = capacity;
    }

    /// <summary>
    /// Adds an element to the stack with the default priority (0).
    /// </summary>
    /// <param name="item">The element to add to the stack.</param>
    public void Add(T item)
    {
        Push(item, 0);
    }

    /// <summary>
    /// Removes all elements from the stack.
    /// </summary>
    public void Clear()
    {
        if (!IsEmpty) 
        { 
            _stack.Clear();
        }
    }

    public bool Contains(T item)
    {
        if (IsEmpty)
            return false;

        return ExistsItem(item) != -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _stack.CopyTo([.. array.Select(value => new PriorityObject<T>(priority: 0, value: value))], arrayIndex);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the stack.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _stack.Select(x => x.Value).Reverse().GetEnumerator();
    }

    /// <inheritdoc cref="IStack{T}.Peek"/>
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new IndexOutOfRangeException();
        }

        return _stack[Count - 1].Value;
    }

    /// <inheritdoc cref="IStack{T}.TryPeek(out T)"/>
    public bool TryPeek(out T? result)
    {
        if (!IsEmpty)
        {
            result = _stack[Count - 1].Value;
            return true;
        }
        result = default;

        return false;
    }

    /// <inheritdoc cref="IStack{T}.Pop"/>
    public T Pop()
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        var value = _stack[Count - 1].Value;
        _stack.RemoveAt(Count - 1);
        return value;
    }

    /// <inheritdoc cref="IStack{T}.TryPop(out T)"/>
    public bool TryPop(out T? result)
    {
        if (!IsEmpty)
        {
            result = Pop();
            return true;
        }
        result = default;

        return false;
    }

    /// <inheritdoc cref="IPriorityStack{T}.Push(T, int)"/>
    public void Push(T item, int priority)
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

        if (_stack.Count == 0)
        {
            _stack.Add(value);
        }
        else
        {
            Push(value);
        }
    }

    /// <summary>
    /// Adds a <see cref="PriorityObject{T}"/> item to a sorted stack while maintaining priority order.
    /// If the item is more prioritized than any other in the stack, it is inserted at the first position.
    /// If it is not more prioritized than any other, it is added to the end of the stack.
    /// </summary>
    /// <param name="item">The item to add to the stack.</param>
    private void Push(PriorityObject<T> item)
    {
        for (int index = 0; index != Count; ++index)
        {
            if (item < _stack[index])
            {
                _stack.Insert(index, item);
                return;
            }
        }
        _stack.Add(item);
    }

    /// <summary>
    /// Removes the specified item from the stack.
    /// </summary>
    /// <param name="item">The item to remove from the stack.</param>
    /// <returns>True if the item was successfully removed from the stack, or false if the item was not found in the stack.</returns>
    public bool Remove(T item)
    {
        if (IsEmpty)
            throw new IndexOutOfRangeException();

        int index = ExistsItem(item);

        if (index == -1) return false;

        _stack.RemoveAt(index);

        return true;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the stack.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _stack.Select(x => x.Value).Reverse().GetEnumerator();
    }

    /// <summary>
    /// Determines if the specified element exists in the stack.
    /// If the element exists, the method returns its index in the stack.
    /// Otherwise, the method returns -1.
    /// </summary>
    /// <param name="item">The element to search for in the stack.</param>
    /// <returns>The index of the element in the stack, or -1 if the element is not found.</returns>
    private int ExistsItem(T item)
    {
        for (int i = 0, j = Count - 1; i != Count && j >= 0; ++i, --j)
        {
            if (_stack[i].Value.Equals(item))
            {
                return i;
            }

            if (_stack[j].Value.Equals(item))
            {
                return j;
            }
        }

        return -1;
    }
}

using System.Collections;

namespace DotNet.AdvancedCollections.List.DoublyLinkedList;

/// <summary>
/// A doubly linked list is a linear data structure in which each element is a separate object 
/// connected to two other elements, usually referred to as the previous and next element.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class DoublyLinkedList<T> : IDoublyLinkedList<T>, IEnumerable<T>, ICollection<T>
    where T : notnull
{
    private Node<T>? _head;
    private Node<T>? _tail;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the index is less than 0 or greater than or equal to the number of elements in the list.
    /// </exception>
    public T this[int index]
    {
        get => Loop(index);
        set => SetValue(index, value);
    }

    /// <summary>
    /// Gets the number of elements contained in the <see cref="DoublyLinkedList{T}"/>.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="DoublyLinkedList{T}"/> is read-only.
    /// </summary>
    /// <returns>Always returns false.</returns>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a value indicating whether access to the doubly linked list is synchronized (thread-safe).
    /// </summary>
    public bool IsSynchronized => false;

    /// <summary>
    /// Constructs a new instance of the <see cref="DoublyLinkedList{T}"/> class.
    /// </summary>
    public DoublyLinkedList()
    {
        Count = 0;
        _head = null;
        _tail = null;
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="DoublyLinkedList{T}"/> class and adds the given item to the list.
    /// </summary>
    /// <param name="item">The item to be added to the list.</param>
    public DoublyLinkedList(T item)
    {
        Count = 0;
        _head = null;
        _tail = null;
        Add(item);
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="DoublyLinkedList{T}"/> class and adds the items from the given enumerable to the list.
    /// </summary>
    /// <param name="values">The items to be added to the list.</param>
    public DoublyLinkedList(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Add(value);
        }
    }

    /// <summary>
    /// Adds an item to the beginning of the list.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    public void Add(T item)
    {
        if (_head is null)
        {
            _head = new Node<T>(item);
            _tail = _head;
        }
        else
        {
            var newNode = new Node<T>(item)
            {
                Next = _head
            };
            _head.Previous = newNode;
            _head = newNode;
        }

        Count++;
    }

    /// <inheritdoc cref="IDoublyLinkedList{T}.AddLast(T)"/>
    public void AddLast(T item)
    {
        if (_head is null || _tail is null)
        {
            _head = new Node<T>(item);
            _tail = _head;
        }
        else
        {
            var newNode = new Node<T>(item);
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
        Count++;
    }

    /// <summary>
    /// Removes all elements from the <see cref="DoublyLinkedList{T}"/>.
    /// </summary>
    public void Clear()
    {
        _head = null;
        _tail = null;
    }

    /// <summary>
    /// Determines whether the <see cref="DoublyLinkedList{T}"/> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="DoublyLinkedList{T}"/>.</param>
    /// <returns>
    ///   <see langword="true"/> if item is found in the <see cref="DoublyLinkedList{T}"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(T item)
    {
        return Find(item) != -1;
    }

    /// <summary>
    /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="arrayIndex"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown if the number of elements in the source <see cref="ICollection{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
    public void CopyTo(T[] array, int arrayIndex)
    {
        GetEnumerable().ToArray().CopyTo(array, arrayIndex);
    }

    /// <inheritdoc cref="IDoublyLinkedList{T}.Find(T)"/>
    public int Find(T item)
    {
        if (_head is null || _tail is null) return -1;

        if (_head.Value.Equals(item)) return 0;

        if (_tail.Value.Equals(item)) return Count - 1;

        var aux = _head.Next;

        for (int i = 1; i != Count - 1; ++i)
        {
            if(aux is null) 
                return -1;

            if (aux.Value.Equals(item))
            {
                return i;
            }
            aux = aux.Next;
        }

        return -1;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        if (_head is null) yield break;
        var aux = _head;
        for (int i = 0; i != Count; ++i)
        {
            yield return aux!.Value;

            aux = aux.Next;
        }
    }

    /// <summary>
    /// Returns an enumerable collection of the elements of the list.
    /// </summary>
    /// <returns>An enumerable collection of the elements of the list.</returns>
    public IEnumerable<T> GetEnumerable()
    {
        if (_head is null) yield break;

        var aux = _head;
        for (int i = 0; i != Count; ++i)
        {
            yield return aux!.Value;

            aux = aux.Next;
        }
    }

    /// <inheritdoc cref="IDoublyLinkedList{T}.GetFirst()"/>
    public T? GetFirst()
    {
        if (_head is null) return default;

        return _head.Value;
    }

    /// <inheritdoc cref="IDoublyLinkedList{T}.GetLast()"/>
    public T? GetLast()
    {
        if (_tail is null) return default;

        return _tail.Value;
    }

    /// <summary>
    /// Removes the specified item from the <see cref="DoublyLinkedList{T}"/>t.
    /// </summary>
    /// <param name="item">The item to be removed from the <see cref="DoublyLinkedList{T}"/>.</param>
    /// <returns>
    /// <see langword="true"/> if the item was found and removed from the list, <see langword="false"/> otherwise.
    /// </returns>
    public bool Remove(T item)
    {
        if (_head is null) return false;

        if (_head.Value.Equals(item))
        {
            _head = _head.Next;
            _head?.Previous = null;
            Count--;
            return true;
        }
        else if (_tail != null && _tail.Value.Equals(item))
        {
            _tail = _tail.Previous;
            _tail?.Next = null;
            Count--;
            return true;
        }
        else
        {
            var aux1 = _head;
            var aux2 = _tail;
            var deleted = false;
            while (aux1 is not null && aux2 is not null)
            {

                if (aux1.Value.Equals(item))
                {
                    aux1.Next?.Previous = aux1.Previous;
                    aux1.Previous?.Next = aux1.Next;
                    Count--;
                    deleted = true;
                }
                else if (aux2.Value.Equals(item))
                {
                    aux2.Next?.Previous = aux2.Previous;
                    aux2.Previous?.Next = aux2.Next;
                    Count--;
                    deleted = true;
                }
                aux1 = aux1.Next;
                aux2 = aux2.Previous;
            }
            return deleted;
        }

    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Retrieves the element at the specified index in the list.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <returns>The element at the specified index in the list.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is outside the bounds of the list.</exception>
    private T Loop(int index)
    {
        if (index > Count || index < 0) throw new IndexOutOfRangeException();

        if (index == 0 && _head is not null) return _head.Value;

        if (index == Count - 1 && _tail is not null) return _tail.Value;

        if (_head is null) throw new IndexOutOfRangeException();

        var aux = _head.Next;

        for (int i = 1; i != Count - 1; ++i)
        {
            if (aux is null)
                break;

            if (index == i)
            {
                return aux.Value;
            }
            aux = aux.Next;
        }

        throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Sets the value at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to set.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
    private void SetValue(int index, T value)
    {
        if (index > Count || index < 0) throw new IndexOutOfRangeException();

        if(_head is null || _tail is null) throw new IndexOutOfRangeException();

        if (index == 0)
        {
            _head.Value = value;
            return;
        }


        if (index == Count - 1)
        {
            _tail.Value = value;
            return;
        }

        var aux = _head.Next;

        for (int i = 1; i != Count - 1; ++i)
        {
            if (aux is null)
                break;

            if (index == i)
            {
                aux.Value = value;
                return;
            }
            aux = aux.Next;
        }
    }

    /// <summary>
    /// Returns a synchronized (thread-safe) wrapper for the current instance of the DoublyLinkedList.
    /// </summary>
    /// <returns>A synchronized wrapper for the current instance of the DoublyLinkedList.</returns>
    public IDoublyLinkedList<T> Synchronized()
    {
        return new SynchronizedDoublyLinkedList(this);
    }

    public static IDoublyLinkedList<T> Synchronized(DoublyLinkedList<T> list)
        => new SynchronizedDoublyLinkedList(list);

    internal class SynchronizedDoublyLinkedList : IDoublyLinkedList<T>
    {
        private readonly DoublyLinkedList<T> _list;
        private readonly object _lock;

        internal SynchronizedDoublyLinkedList(DoublyLinkedList<T> list)
        {
            _list = [.. list.AsEnumerable()];
            _lock = new object();
        }

        public T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    _list[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _list.Count;
                }
            }
        }

        public bool IsReadOnly => _list.IsReadOnly;

        public static bool IsSynchronized => true;

        public void Add(T item)
        {
            lock (_lock)
            {
                _list.Add(item);
            }
        }

        public void AddLast(T item)
        {
            lock (_lock)
            {
                _list.AddLast(item);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _list.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_lock)
            {
                return _list.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _list.GetEnumerator();
            }
        }

        public int Find(T item)
        {
            lock (_lock)
            {
                return _list.Find(item);
            }
        }

        public T? GetFirst()
        {
            lock (_lock)
            {
                return _list.GetFirst();
            }
        }

        public T? GetLast()
        {
            lock (_lock)
            {
                return _list.GetLast();
            }
        }

        public bool Remove(T item)
        {
            lock (_lock)
            {
                return _list.Remove(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_lock)
            {
                return _list.GetEnumerator();
            }
        }
    }
}

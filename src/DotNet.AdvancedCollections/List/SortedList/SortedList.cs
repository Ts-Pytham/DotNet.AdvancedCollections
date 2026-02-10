using DotNet.AdvancedCollections.Concurrent;
using System.Collections;

namespace DotNet.AdvancedCollections.List.SortedList;

/// <summary>
/// Represents a sorted list each time you enter an item in the list.
/// </summary>
/// <typeparam name="T">T is type of element in the list and implements <see cref="IComparable{T}"/>.</typeparam>
public class SortedList<T> : ISortedList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, ISynchronized
    where T : notnull, IComparable<T>
{
    private readonly List<T> _sortedList;

    /// <summary>
    /// Criterion for sorting the list.
    /// </summary>
    public Criterion Criterion { get; private set; }


    public T this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

            return _sortedList[index];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

            if (value.CompareTo(_sortedList[index]) == 0)
            {
                return;
            }

            if (index == 0 && Count == 1)
                _sortedList[index] = value;
            else if (index == 0 && Count > 1)
            {
                if (value.CompareTo(this[1]) <= 0 && Criterion == Criterion.Ascending)
                {
                    _sortedList[index] = value;
                }
                else if (value.CompareTo(this[1]) >= 0 && Criterion == Criterion.Descending)
                {
                    _sortedList[index] = value;
                }
                else
                {
                    _sortedList.RemoveAt(index);
                    Add(value);
                }
            }
            else if (index == Count - 1)
            {
                if (value.CompareTo(this[Count - 2]) >= 0 && Criterion == Criterion.Ascending)
                {
                    _sortedList[index] = value;
                }
                else if (value.CompareTo(this[Count - 2]) <= 0 && Criterion == Criterion.Descending)
                {
                    _sortedList[index] = value;
                }
                else
                {
                    _sortedList.RemoveAt(index);
                    Add(value);
                }
            }
            else
            {
                if (value.CompareTo(this[index - 1]) >= 0 && value.CompareTo(this[index + 1]) <= 0 && Criterion == Criterion.Ascending)
                {
                    _sortedList[index] = value;
                }
                else if (value.CompareTo(this[index - 1]) <= 0 && value.CompareTo(this[index + 1]) >= 0 && Criterion == Criterion.Descending)
                {
                    _sortedList[index] = value;
                }
                else
                {
                    _sortedList.RemoveAt(index);
                    Add(value);
                }
            }

        }
    }

    /// <summary>
    /// Gets the number of elements contained in the sorted list.
    /// </summary>
    public int Count
    {
        get => _sortedList.Count;
    }

    /// <summary>
    /// Gets a value indicating whether the sorted list is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a value indicating whether access to the sorted list is synchronized (thread-safe).
    /// </summary>
    public bool IsSynchronized => false;

    private readonly Lazy<object> _syncRoot = new(() => new object());

    /// <summary>
    /// Gets an object that can be used to synchronize access to the collection.
    /// </summary>
    /// <remarks>Use the object returned by this property when implementing thread-safe operations on the
    /// collection. Synchronization is required when multiple threads access the collection concurrently and at least
    /// one thread modifies the collection.</remarks>
    public object SyncRoot
    {
        get => _syncRoot.Value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SortedList{T}"/> class and initialize the criterion to ascending by default.
    /// </summary>
    public SortedList()
    {
        _sortedList = [];
        Criterion = Criterion.Ascending;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SortedList{T}"/> class and initializes the criterion.
    /// </summary>
    /// <param name="criterion">Can be ascending or descending.</param>
    public SortedList(Criterion criterion)
    {
        _sortedList = [];
        Criterion = criterion;
    }

    /// <summary>
    /// Adds an item and sorts it.
    /// </summary>
    /// <param name="item">The object to be added</param>
    public void Add(T item)
    {
        if (Count == 0)
        {
            _sortedList.Add(item);
        }
        else
        {
            AddSorted(item);
        }
    }
    /// <summary>
    /// Adds an item and sorts it.
    /// </summary>
    /// <param name="item">The object to be added</param>
    private void AddSorted(T item)
    {
        if (Criterion == Criterion.Ascending)
        {
            if (item.CompareTo(_sortedList[0]) == -1) // Initial Position
            {
                _sortedList.Insert(0, item);
            }
            else if (item.CompareTo(_sortedList[Count - 1]) >= 0) // Last Position
            {
                _sortedList.Insert(Count, item);
            }
            else
            {
                for (int index = 1; index != Count; index++)
                {
                    if (item.CompareTo(_sortedList[index]) == -1)
                    {
                        _sortedList.Insert(index, item);
                        return;
                    }
                }

            }
        }
        else
        {
            if (item.CompareTo(_sortedList[0]) >= 0) // Initial Position
            {
                _sortedList.Insert(0, item);
            }
            else if (item.CompareTo(_sortedList[Count - 1]) == -1) // Last Position
            {
                _sortedList.Insert(Count, item);
            }
            else
            {
                for (int index = 1; index != Count; index++)
                {
                    if (item.CompareTo(_sortedList[index]) >= 0)
                    {
                        _sortedList.Insert(index, item);
                        return;
                    }
                }

            }
        }
    }

    /// <summary>
    /// Adds a range of values to the collection in a sorted order.
    /// </summary>
    /// <param name="values">The values to be added to the collection.</param>
    public void AddRange(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Add(value);
        }
    }
    public void Clear()
    {
        _sortedList.Clear();
    }

    public bool Contains(T item)
    {
        return _sortedList.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _sortedList.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _sortedList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _sortedList.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _sortedList.IndexOf(item);
    }

    /// <summary>
    /// Performs a binary search on a sorted list to find a specific element.
    /// </summary>
    /// <param name="item">The element to search for in the list.</param>
    /// <returns>The index of the element in the list if found, or -1 if not found.</returns>
    public int BinarySearch(T item)
    {
        int low = 0;
        int high = _sortedList.Count - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (item.CompareTo(_sortedList[mid]) == 0)
            {
                return mid;
            }
            else if (item.CompareTo(_sortedList[mid]) < 0)
            {
                if (Criterion == Criterion.Ascending)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            else
            {
                if (Criterion == Criterion.Ascending)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
        }

        return -1;
    }

    public bool Remove(T item)
    {
        return _sortedList.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _sortedList.RemoveAt(index);
    }

    /// <summary>
    /// Reverses the order of the elements in the sorted list and changes the Criterion property accordingly.
    /// </summary>
    public void Reverse()
    {
        Criterion = Criterion == Criterion.Ascending ? Criterion.Descending : Criterion.Ascending;

        for (int i = 0, j = Count - 1; i <= j; ++i, --j)
        {
            (_sortedList[j], _sortedList[i]) = (_sortedList[i], _sortedList[j]);
        }
    }

    /// <summary>
    /// Returns a synchronized (thread-safe) wrapper for the current instance of the sorted list.
    /// </summary>
    /// <returns>A synchronized wrapper for the current instance of the sorted list.</returns>
    public ISortedList<T> Synchronized()
    {
        return new SynchronizedSortedList(this);
    }

    internal class SynchronizedSortedList : ISortedList<T>
    {
        private readonly SortedList<T> _sortedList;
        private readonly object _lock;

        internal SynchronizedSortedList(SortedList<T> sortedList)
        {
            _sortedList = sortedList;
            _lock = _sortedList.SyncRoot;
        }

        public T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return _sortedList[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    _sortedList[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _sortedList.Count;
                }
            }
        }

        public bool IsReadOnly => _sortedList.IsReadOnly;

        public static bool IsSynchronized => true;

        public void Add(T item)
        {
            lock (_lock)
            {
                _sortedList.Add(item);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _sortedList.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_lock)
            {
                return _sortedList.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _sortedList.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _sortedList.GetEnumerator();
            }
        }

        public int IndexOf(T item)
        {
            lock (_lock)
            {
                return _sortedList.IndexOf(item);
            }
        }

        public bool Remove(T item)
        {
            lock (_lock)
            {
                return _sortedList.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_lock)
            {
                _sortedList.RemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_lock)
            {
                return _sortedList.GetEnumerator();
            }
        }
    }
}
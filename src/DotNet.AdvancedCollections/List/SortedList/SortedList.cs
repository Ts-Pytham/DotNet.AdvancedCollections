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

            if (Compare(value, _sortedList[index]) == 0)
                return;

            bool validLeft = index == 0 || Compare(_sortedList[index - 1], value) <= 0;
            bool validRight = index == Count - 1 || Compare(value, _sortedList[index + 1]) <= 0;

            if (validLeft && validRight)
            {
                _sortedList[index] = value;
                return;
            }

            _sortedList.RemoveAt(index);
            Add(value);
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
    /// Inserts the specified item into the collection while maintaining the sort order.
    /// </summary>
    /// <remarks>If the collection allows duplicate items, the new item is inserted before any existing items
    /// that compare equal. The sort order is determined by the collection's comparer or the default comparer for the
    /// type.</remarks>
    /// <param name="item">The item to add to the collection. The item is inserted at the position determined by the collection's sort
    /// order.</param>
    public void Add(T item)
    {
        if (Count == 0)
        {
            _sortedList.Add(item);
            return;
        }

        var pos = LowerBound(item);
        _sortedList.Insert(pos, item);
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
        var index = LowerBound(item);

        if(index < Count && _sortedList[index].CompareTo(item) == 0)
        {
            return index;
        }

        return -1;
    }

    /// <summary>
    /// Finds the index of the first element in the sorted list that is not less than the specified item, according to
    /// the current sorting criterion.
    /// </summary>
    /// <remarks>This method performs a binary search to determine the lower bound position for the specified
    /// item. The result can be used as an insertion index to maintain the list's sorted order. The sorting direction is
    /// determined by the current value of the Criterion property.</remarks>
    /// <param name="item">The item to locate in the sorted list. The comparison is performed using the current sorting criterion.</param>
    /// <returns>The zero-based index of the first element that is not less than the specified item. If all elements are less
    /// than the item, returns the index at which the item can be inserted to maintain the sort order.</returns>
    private int LowerBound(T item)
    {
        return Criterion == Criterion.Ascending
            ? LowerBoundAscending(item)
            : LowerBoundDescending(item);
    }

    /// <summary>
    /// Binary search for ascending order - optimized to avoid condition checks in the loop.
    /// </summary>
    private int LowerBoundAscending(T item)
    {
        int low = 0;
        int high = _sortedList.Count;
        
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (_sortedList[mid].CompareTo(item) < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }
        
        return low;
    }

    /// <summary>
    /// Binary search for descending order - optimized to avoid condition checks in the loop.
    /// </summary>
    private int LowerBoundDescending(T item)
    {
        int low = 0;
        int high = _sortedList.Count;
        
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (_sortedList[mid].CompareTo(item) > 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }
        
        return low;
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

    /// <summary>
    /// Compares two values according to the specified sorting criterion.
    /// </summary>
    /// <remarks>
    /// Si el criterio de ordenación es ascendente, el método compara x con y; si es descendente, compara y con x.
    /// Ambos x e y deben implementar IComparable&lt;T&gt;.
    /// </remarks>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>A signed integer that indicates the relative values of x and y: less than zero if x is less than y; zero if x
    /// equals y; greater than zero if x is greater than y. The comparison direction depends on the sorting criterion.</returns>
    private int Compare(T x, T y)
    {
        return Criterion == Criterion.Ascending
            ? x.CompareTo(y)
            : y.CompareTo(x);
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
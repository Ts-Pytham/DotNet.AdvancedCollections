using BenchmarkDotNet.Attributes;
using DotNet.AdvancedCollections.List.SortedList;

namespace DotNet.AdvancedCollections.Benchmarks.Lists;

/// <summary>
/// Comprehensive benchmarks for SortedList operations.
/// Tests all major operations: Add, Contains, BinarySearch, IndexOf, Remove, RemoveAt, and Indexer access.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class SortedListBenchmarks
{
    private int[]? _testData;
    private SortedList<int>? _prePopulatedList;
    private readonly Random _random = new(42);

    [Params(10, 1_000, 10_000, 100_000)]
    public int DataSize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _testData = new int[DataSize];
        for (int i = 0; i < DataSize; i++)
        {
            _testData[i] = _random.Next(0, DataSize * 10);
        }

        // Pre-populate list for read/search/remove operations
        _prePopulatedList = new SortedList<int>();
        for (int i = 0; i < DataSize; i++)
        {
            _prePopulatedList.Add(_testData[i]);
        }
    }

    [Benchmark]
    public void Add()
    {
        var list = new SortedList<int>();
        for (int i = 0; i < DataSize; i++)
        {
            list.Add(_testData![i]);
        }
    }

    [Benchmark]
    public void AddRange()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
    }

    [Benchmark]
    public void BinarySearch_Found()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            int index = _prePopulatedList!.BinarySearch(_testData![i]);
            sum += index;
        }
    }

    [Benchmark]
    public void BinarySearch_NotFound()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            int index = _prePopulatedList!.BinarySearch(DataSize * 20 + i);
            sum += index;
        }
    }

    [Benchmark]
    public void Contains_Found()
    {
        int count = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            if (_prePopulatedList!.Contains(_testData![i]))
                count++;
        }
    }

    [Benchmark]
    public void Contains_NotFound()
    {
        int count = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            if (_prePopulatedList!.Contains(DataSize * 20 + i))
                count++;
        }
    }

    [Benchmark]
    public void IndexOf_Found()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            int index = _prePopulatedList!.IndexOf(_testData![i]);
            sum += index;
        }
    }

    [Benchmark]
    public void IndexOf_NotFound()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            int index = _prePopulatedList!.IndexOf(DataSize * 20 + i);
            sum += index;
        }
    }

    [Benchmark]
    public void IndexerGet()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(DataSize, 1000); i++)
        {
            sum += _prePopulatedList![i % _prePopulatedList.Count];
        }
    }

    [Benchmark]
    public void IndexerSet_SameValue()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            int index = i % list.Count;
            list[index] = list[index]; // Same value, should be fast
        }
    }

    [Benchmark]
    public void Remove_Found()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            list.Remove(_testData![i]);
        }
    }

    [Benchmark]
    public void Remove_NotFound()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        
        for (int i = 0; i < Math.Min(DataSize, 100); i++)
        {
            list.Remove(DataSize * 20 + i);
        }
    }

    [Benchmark]
    public void RemoveAt()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        
        for (int i = 0; i < Math.Min(DataSize, 100) && list.Count > 0; i++)
        {
            list.RemoveAt(0);
        }
    }

    [Benchmark]
    public void Reverse()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        list.Reverse();
    }

    [Benchmark]
    public void Clear()
    {
        var list = new SortedList<int>();
        list.AddRange(_testData!);
        list.Clear();
    }

    [Benchmark]
    public void CopyTo()
    {
        var array = new int[DataSize];
        _prePopulatedList!.CopyTo(array, 0);
    }

    [Benchmark]
    public void Enumerate()
    {
        int sum = 0;
        foreach (var item in _prePopulatedList!)
        {
            sum += item;
        }
    }
}


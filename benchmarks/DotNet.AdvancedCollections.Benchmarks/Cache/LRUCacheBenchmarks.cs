using BenchmarkDotNet.Attributes;
using DotNet.AdvancedCollections.Cache;

namespace DotNet.AdvancedCollections.Benchmarks.Cache;

/// <summary>
/// Comprehensive benchmarks for LRUCache operations.
/// Uses realistic cache sizes (10-100 elements) and access patterns.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class LRUCacheBenchmarks
{
    private int[]? _testKeys;
    private string[]? _testValues;
    private LRUCache<int, string>? _prePopulatedCache;
    private readonly Random _random = new(42);

    // Cache sizes realistic for LRU caches (small by design)
    // Typical LRU caches: 10-100 elements for API responses, session data, etc.
    [Params(10, 50, 100)]
    public int CacheSize { get; set; }

    // Hit rate: percentage of operations that access existing keys
    // 80% is typical for a well-performing cache
    [Params(50, 80)]
    public int HitRatePercent { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // Generate test data - 10x cache size operations
        int operationCount = CacheSize * 10;
        _testKeys = new int[operationCount];
        _testValues = new string[operationCount];

        for (int i = 0; i < operationCount; i++)
        {
            // Simulate realistic cache access pattern
            bool shouldHit = _random.Next(100) < HitRatePercent;
            if (shouldHit && i >= CacheSize)
            {
                // Re-use an existing key (cache hit)
                _testKeys[i] = _testKeys[_random.Next(Math.Max(0, i - CacheSize), i)];
            }
            else
            {
                // New key (cache miss)
                _testKeys[i] = i;
            }

            _testValues[i] = $"value_{i}";
        }

        // Pre-populate cache for read/search operations
        _prePopulatedCache = new LRUCache<int, string>(CacheSize);
        for (int i = 0; i < CacheSize; i++)
        {
            _prePopulatedCache.Put(i, $"value_{i}");
        }
    }

    [Benchmark]
    public void Put()
    {
        var cache = new LRUCache<int, string>(CacheSize);
        
        for (int i = 0; i < _testKeys!.Length; i++)
        {
            cache.Put(_testKeys[i], _testValues![i]);
        }
    }

    [Benchmark]
    public void TryAdd()
    {
        var cache = new LRUCache<int, string>(CacheSize);
        
        for (int i = 0; i < _testKeys!.Length; i++)
        {
            cache.TryAdd(_testKeys[i], _testValues![i]);
        }
    }

    [Benchmark]
    public void TryGet_MixedHits()
    {
        int sum = 0;
        for (int i = 0; i < Math.Min(_testKeys!.Length, 100); i++)
        {
            if (_prePopulatedCache!.TryGet(_testKeys[i % CacheSize], out var value))
                sum += value.Length;
        }
    }

    [Benchmark]
    public void ContainsKey()
    {
        int count = 0;
        for (int i = 0; i < Math.Min(_testKeys!.Length, 100); i++)
        {
            if (_prePopulatedCache!.ContainsKey(i % CacheSize))
                count++;
        }
    }

    [Benchmark]
    public void Remove()
    {
        var cache = new LRUCache<int, string>(CacheSize);
        
        // Populate cache
        for (int i = 0; i < CacheSize; i++)
        {
            cache.Put(i, $"value_{i}");
        }
        
        // Remove half of the items
        for (int i = 0; i < CacheSize / 2; i++)
        {
            cache.Remove(i);
        }
    }

    [Benchmark]
    public void Clear()
    {
        var cache = new LRUCache<int, string>(CacheSize);
        
        // Populate cache
        for (int i = 0; i < CacheSize; i++)
        {
            cache.Put(i, $"value_{i}");
        }
        
        cache.Clear();
    }

    [Benchmark]
    public void FullWorkflow()
    {
        var cache = new LRUCache<int, string>(CacheSize);
        
        // Realistic workflow: mix of Put, TryGet, Remove
        for (int i = 0; i < _testKeys!.Length; i++)
        {
            // Add
            cache.Put(_testKeys[i], _testValues![i]);
            
            // Occasionally read
            if (i % 3 == 0 && i > 0)
            {
                cache.TryGet(_testKeys[i - 1], out var dummy);
            }
            
            // Occasionally remove
            if (i % 10 == 0 && i > CacheSize)
            {
                cache.Remove(_testKeys[i - CacheSize]);
            }
        }
    }
}


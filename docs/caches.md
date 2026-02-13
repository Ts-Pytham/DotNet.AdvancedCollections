# Caches

## LRUCache

`LRUCache<TKey, TValue>` is a fixed-capacity cache that implements the Least Recently Used (LRU) eviction policy. When the cache reaches its capacity, the least recently accessed item is automatically removed to make room for new items.

### When to Use

- **API Response Caching**: Store frequently requested API responses
- **Database Query Results**: Cache expensive database queries
- **Session Data**: Store user session information
- **Computed Results**: Cache expensive calculations or transformations
- **Image/File Metadata**: Store metadata for recently accessed files

### Key Features

- **Fixed Capacity**: Define maximum number of items at creation
- **Automatic Eviction**: Least recently used items are removed when capacity is reached
- **O(1) Operations**: All operations (Put, Get, Remove) run in constant time
- **Generic Implementation**: Supports any non-null key and value types
- **Thread-Unsafe**: Requires external synchronization for concurrent access

### Creating a Cache

```csharp
using DotNet.AdvancedCollections.Cache;

// Create cache with default capacity (10)
var cache1 = new LRUCache<int, string>();

// Create cache with custom capacity
var cache2 = new LRUCache<string, User>(capacity: 100);

// Create cache with complex types
var cache3 = new LRUCache<Guid, OrderDetails>(capacity: 50);
```

### Basic Operations

#### Adding Items

```csharp
var cache = new LRUCache<int, string>(capacity: 3);

// Add items
cache.Put(1, "one");
cache.Put(2, "two");
cache.Put(3, "three");

Console.WriteLine($"Count: {cache.Count}"); // Output: Count: 3
```

#### Retrieving Items

```csharp
// Try to get a value
if (cache.TryGet(1, out string? value))
{
    Console.WriteLine($"Found: {value}"); // Output: Found: one
}
else
{
    Console.WriteLine("Not found");
}

// Check if key exists
bool exists = cache.ContainsKey(2);
Console.WriteLine($"Contains key 2: {exists}"); // Output: Contains key 2: True
```

#### Updating Items

```csharp
// Update existing item
cache.Put(1, "ONE"); // Overwrites "one" with "ONE"

// TryAdd - only adds if key doesn't exist
bool added = cache.TryAdd(1, "uno"); // Returns false - key already exists
Console.WriteLine($"Added: {added}"); // Output: Added: False

bool added2 = cache.TryAdd(4, "four"); // Returns true - new key
Console.WriteLine($"Added: {added2}"); // Output: Added: True
```

#### Removing Items

```csharp
// Remove specific item
cache.Remove(2);

// Clear entire cache
cache.Clear();
Console.WriteLine($"Count after clear: {cache.Count}"); // Output: Count after clear: 0
```

### LRU Eviction Example

```csharp
var cache = new LRUCache<int, string>(capacity: 3);

// Fill cache to capacity
cache.Put(1, "one");
cache.Put(2, "two");
cache.Put(3, "three");

// Access key 1 - makes it "recently used"
cache.TryGet(1, out _);

// Add new item - evicts least recently used (key 2)
cache.Put(4, "four");

Console.WriteLine($"Contains key 1: {cache.ContainsKey(1)}"); // True - was accessed
Console.WriteLine($"Contains key 2: {cache.ContainsKey(2)}"); // False - was evicted
Console.WriteLine($"Contains key 3: {cache.ContainsKey(3)}"); // True - still in cache
Console.WriteLine($"Contains key 4: {cache.ContainsKey(4)}"); // True - just added

/*
Output:
Contains key 1: True
Contains key 2: False
Contains key 3: True
Contains key 4: True
*/
```

### Real-World Example: API Response Cache

```csharp
public class ApiResponseCache
{
    private readonly LRUCache<string, ApiResponse> _cache;
    private readonly HttpClient _httpClient;

    public ApiResponseCache(int cacheSize = 100)
    {
        _cache = new LRUCache<string, ApiResponse>(cacheSize);
        _httpClient = new HttpClient();
    }

    public async Task<ApiResponse> GetDataAsync(string endpoint)
    {
        // Check cache first
        if (_cache.TryGet(endpoint, out var cachedResponse))
        {
            Console.WriteLine($"Cache hit for: {endpoint}");
            return cachedResponse;
        }

        // Cache miss - fetch from API
        Console.WriteLine($"Cache miss for: {endpoint}");
        var response = await FetchFromApiAsync(endpoint);
        
        // Store in cache
        _cache.Put(endpoint, response);
        
        return response;
    }

    private async Task<ApiResponse> FetchFromApiAsync(string endpoint)
    {
        var response = await _httpClient.GetStringAsync(endpoint);
        return new ApiResponse { Data = response, Timestamp = DateTime.UtcNow };
    }
}

public class ApiResponse
{
    public string Data { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
```

### Real-World Example: Database Query Cache

```csharp
public class UserRepository
{
    private readonly LRUCache<int, User> _userCache;
    private readonly DbContext _dbContext;

    public UserRepository(DbContext dbContext, int cacheSize = 50)
    {
        _dbContext = dbContext;
        _userCache = new LRUCache<int, User>(cacheSize);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        // Try cache first
        if (_userCache.TryGet(userId, out var cachedUser))
        {
            return cachedUser;
        }

        // Query database
        var user = await _dbContext.Users.FindAsync(userId);
        
        if (user != null)
        {
            // Store in cache for future requests
            _userCache.Put(userId, user);
        }

        return user;
    }

    public void InvalidateUser(int userId)
    {
        _userCache.Remove(userId);
    }

    public void ClearCache()
    {
        _userCache.Clear();
    }
}
```

### Performance Characteristics

| Operation | Time Complexity | Space Complexity |
|-----------|----------------|------------------|
| Put | O(1) | O(1) |
| TryGet | O(1) | O(1) |
| TryAdd | O(1) | O(1) |
| Remove | O(1) | O(1) |
| ContainsKey | O(1) | O(1) |
| Clear | O(n) | O(1) |

**Memory Usage:**
- Approximately 190 bytes per cached item (includes Dictionary overhead and linked list nodes)
- Total memory = ~190 bytes × capacity

### Best Practices

1. **Choose Appropriate Capacity**
   - Too small: Frequent evictions, poor cache hit rate
   - Too large: Wasted memory
   - Typical values: 10-100 for most use cases

2. **Thread Safety**
   - LRUCache is **not thread-safe**
   - Use external locking for concurrent access:
   ```csharp
   private readonly LRUCache<int, string> _cache = new(100);
   private readonly object _lock = new();

   public void AddToCache(int key, string value)
   {
       lock (_lock)
       {
           _cache.Put(key, value);
       }
   }
   ```

3. **Cache Invalidation**
   - Remove items when underlying data changes
   - Consider time-based expiration (implement wrapper if needed)

4. **Monitoring**
   - Track hit/miss ratio
   - Adjust capacity based on actual usage patterns

### Limitations

- **Not Thread-Safe**: Requires external synchronization
- **No Expiration**: Items don't expire based on time (only on LRU policy)
- **Fixed Capacity**: Cannot grow dynamically
- **No Persistence**: In-memory only

### See Also

- [ICache Interface](../api/DotNet.AdvancedCollections.Cache.ICache-2.md)
- [LRUCacheNode](../api/DotNet.AdvancedCollections.Cache.LRUCacheNode-2.md)

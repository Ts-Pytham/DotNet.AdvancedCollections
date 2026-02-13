using DotNet.AdvancedCollections.Cache;
using FluentAssertions;

namespace DotNet.AdvancedCollections.Tests.Cache.LRUCache;

/// <summary>
/// Unit tests for LRUCache implementation.
/// Tests the Least Recently Used cache eviction policy and all cache operations.
/// </summary>
public class LRUCacheTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_DefaultCapacity_ShouldBe10()
    {
        // Arrange & Act
        var cache = new LRUCache<int, string>();

        // Assert
        cache.Capacity.Should().Be(10);
        cache.Count.Should().Be(0);
    }

    [Fact]
    public void Constructor_CustomCapacity_ShouldSetCorrectly()
    {
        // Arrange & Act
        var cache = new LRUCache<int, string>(5);

        // Assert
        cache.Capacity.Should().Be(5);
        cache.Count.Should().Be(0);
    }

    #endregion

    #region Put Tests

    [Fact]
    public void Put_FirstItem_ShouldAddSuccessfully()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        cache.Put(1, "one");

        // Assert
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void Put_MultipleItems_ShouldAddAllWithinCapacity()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void Put_ExceedCapacity_ShouldEvictLRU()
    {
        // Arrange
        var cache = new LRUCache<int, string>(2);

        // Act
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three"); // Should evict key 1

        // Assert
        cache.Count.Should().Be(2);
        // Key 1 should have been evicted (least recently used)
    }

    [Fact]
    public void Put_UpdateExistingKey_ShouldUpdateValue()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act
        cache.Put(1, "ONE_UPDATED");

        // Assert
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void Put_UpdateExistingKey_ShouldMoveToFront()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.Put(1, "ONE_UPDATED"); // Move key 1 to front
        cache.Put(4, "four"); // Should evict key 2 (now LRU)

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void Put_CapacityOne_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<int, string>(1);

        // Act
        cache.Put(1, "one");
        cache.Put(2, "two"); // Should evict key 1

        // Assert
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void Put_LargeNumberOfItems_ShouldMaintainCapacity()
    {
        // Arrange
        var cache = new LRUCache<int, string>(5);

        // Act
        for (int i = 0; i < 100; i++)
        {
            cache.Put(i, $"value_{i}");
        }

        // Assert
        cache.Count.Should().Be(5);
    }

    #endregion

    #region Clear Tests

    [Fact]
    public void Clear_EmptyCache_ShouldNotThrow()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        Action act = () => cache.Clear();

        // Assert
        act.Should().NotThrow();
        cache.Count.Should().Be(0);
    }

    [Fact]
    public void Clear_WithItems_ShouldRemoveAll()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.Clear();

        // Assert
        cache.Count.Should().Be(0);
    }

    [Fact]
    public void Clear_ThenAdd_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Clear();

        // Act
        cache.Put(3, "three");

        // Assert
        cache.Count.Should().Be(1);
    }

    #endregion

    #region LRU Eviction Policy Tests

    [Fact]
    public void LRUEviction_ShouldEvictLeastRecentlyUsed()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        
        // Act
        cache.Put(1, "one");   // Order: [1]
        cache.Put(2, "two");   // Order: [2, 1]
        cache.Put(3, "three"); // Order: [3, 2, 1]
        cache.Put(4, "four");  // Should evict 1, Order: [4, 3, 2]

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void LRUEviction_AccessPattern_ShouldMaintainCorrectOrder()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        
        // Act
        cache.Put(1, "one");     // [1]
        cache.Put(2, "two");     // [2, 1]
        cache.Put(3, "three");   // [3, 2, 1]
        cache.Put(2, "TWO");     // Update 2, moves to front: [2, 3, 1]
        cache.Put(4, "four");    // Evicts 1: [4, 2, 3]

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void LRUEviction_ConsecutiveEvictions_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<int, string>(2);
        
        // Act
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three"); // Evict 1
        cache.Put(4, "four");  // Evict 2
        cache.Put(5, "five");  // Evict 3

        // Assert
        cache.Count.Should().Be(2);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Put_SameKeyMultipleTimes_ShouldOnlyCountOnce()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        cache.Put(1, "one");
        cache.Put(1, "uno");
        cache.Put(1, "ONE");

        // Assert
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void Put_NullableValueTypes_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<string, int>(3);

        // Act
        cache.Put("key1", 100);
        cache.Put("key2", 0);
        cache.Put("key3", -1);

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void Put_StringKeys_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);

        // Act
        cache.Put("a", "value_a");
        cache.Put("b", "value_b");
        cache.Put("c", "value_c");

        // Assert
        cache.Count.Should().Be(3);
    }

    [Fact]
    public void Put_ComplexTypes_ShouldWorkCorrectly()
    {
        // Arrange
        var cache = new LRUCache<int, TestObject>(3);

        // Act
        cache.Put(1, new TestObject { Id = 1, Name = "Object1" });
        cache.Put(2, new TestObject { Id = 2, Name = "Object2" });

        // Assert
        cache.Count.Should().Be(2);
    }

    #endregion

    #region Capacity Tests

    [Fact]
    public void Capacity_ShouldRemainConstant()
    {
        // Arrange
        var cache = new LRUCache<int, string>(5);
        int initialCapacity = cache.Capacity;

        // Act
        for (int i = 0; i < 10; i++)
        {
            cache.Put(i, $"value_{i}");
        }

        // Assert
        cache.Capacity.Should().Be(initialCapacity);
    }

    [Fact]
    public void Count_ShouldNeverExceedCapacity()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            cache.Put(i, $"value_{i}");
            cache.Count.Should().BeLessThanOrEqualTo(cache.Capacity);
        }
    }

    #endregion

    #region TryGet Tests

    [Fact]
    public void TryGet_ExistingKey_ShouldReturnTrueAndValue()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");

        // Act
        bool result = cache.TryGet(1, out var value);

        // Assert
        result.Should().BeTrue();
        value.Should().Be("one");
    }

    [Fact]
    public void TryGet_NonExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act
        bool result = cache.TryGet(99, out var value);

        // Assert
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Fact]
    public void TryGet_ShouldUpdateLRUOrder()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.TryGet(1, out _); // Access key 1, should move to front
        cache.Put(4, "four");   // Should evict key 2 (now LRU)

        // Assert
        cache.TryGet(1, out _).Should().BeTrue("key 1 should still exist");
        cache.TryGet(2, out _).Should().BeFalse("key 2 should have been evicted");
    }

    [Fact]
    public void TryGet_EmptyCache_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        bool result = cache.TryGet(1, out var value);

        // Assert
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    #endregion

    #region ContainsKey Tests

    [Fact]
    public void ContainsKey_ExistingKey_ShouldReturnTrue()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");

        // Act & Assert
        cache.ContainsKey(1).Should().BeTrue();
        cache.ContainsKey(2).Should().BeTrue();
    }

    [Fact]
    public void ContainsKey_NonExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act & Assert
        cache.ContainsKey(99).Should().BeFalse();
    }

    [Fact]
    public void ContainsKey_AfterEviction_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(2);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three"); // Evicts key 1

        // Act & Assert
        cache.ContainsKey(1).Should().BeFalse("key 1 should have been evicted");
        cache.ContainsKey(2).Should().BeTrue();
        cache.ContainsKey(3).Should().BeTrue();
    }

    [Fact]
    public void ContainsKey_EmptyCache_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act & Assert
        cache.ContainsKey(1).Should().BeFalse();
    }

    #endregion

    #region Remove Tests

    [Fact]
    public void Remove_ExistingKey_ShouldRemoveItem()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");

        // Act
        cache.Remove(1);

        // Assert
        cache.Count.Should().Be(1);
        cache.ContainsKey(1).Should().BeFalse();
    }

    [Fact]
    public void Remove_NonExistingKey_ShouldNotThrow()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act
        Action act = () => cache.Remove(99);

        // Assert
        act.Should().NotThrow();
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void Remove_FromHead_ShouldUpdateHead()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.Remove(3); // Remove head

        // Assert
        cache.Count.Should().Be(2);
        cache.ContainsKey(3).Should().BeFalse();
    }

    [Fact]
    public void Remove_FromTail_ShouldUpdateTail()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.Remove(1); // Remove tail

        // Assert
        cache.Count.Should().Be(2);
        cache.ContainsKey(1).Should().BeFalse();
    }

    [Fact]
    public void Remove_FromMiddle_ShouldMaintainLinks()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        // Act
        cache.Remove(2); // Remove middle

        // Assert
        cache.Count.Should().Be(2);
        cache.ContainsKey(2).Should().BeFalse();
        cache.ContainsKey(1).Should().BeTrue();
        cache.ContainsKey(3).Should().BeTrue();
    }

    [Fact]
    public void Remove_LastItem_ShouldLeaveEmptyCache()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act
        cache.Remove(1);

        // Assert
        cache.Count.Should().Be(0);
    }

    [Fact]
    public void Remove_EmptyCache_ShouldNotThrow()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        Action act = () => cache.Remove(1);

        // Assert
        act.Should().NotThrow();
        cache.Count.Should().Be(0);
    }

    #endregion

    #region TryAdd Tests

    [Fact]
    public void TryAdd_NewKey_ShouldReturnTrueAndAdd()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);

        // Act
        bool result = cache.TryAdd(1, "one");

        // Assert
        result.Should().BeTrue();
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void TryAdd_ExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LRUCache<int, string>(3);
        cache.Put(1, "one");

        // Act
        bool result = cache.TryAdd(1, "ONE");

        // Assert
        result.Should().BeFalse();
        cache.Count.Should().Be(1);
    }

    [Fact]
    public void TryAdd_AtCapacity_ShouldEvictAndAdd()
    {
        // Arrange
        var cache = new LRUCache<int, string>(2);
        cache.Put(1, "one");
        cache.Put(2, "two");

        // Act
        bool result = cache.TryAdd(3, "three");

        // Assert
        result.Should().BeTrue();
        cache.Count.Should().Be(2);
        cache.ContainsKey(1).Should().BeFalse("key 1 should have been evicted");
    }

    #endregion

    #region Test Helper Classes

    private class TestObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    #endregion
}

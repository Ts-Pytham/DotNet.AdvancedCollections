namespace DotNet.AdvancedCollections.Tests.List.SortedList;

public class SortedListTests
{
    [Fact]
    public void Add_WithAscendingOrder_ShouldSortCorrectly()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            20, 10, 40
        };

        // Act
        var firstItem = sortedList[0];

        // Assert
        firstItem.Should().Be(10);
    }

    [Fact]
    public void Add_WithDescendingOrder_ShouldSortCorrectly()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            20, 10, 40
        };

        // Act
        var firstItem = sortedList[0];

        // Assert
        firstItem.Should().Be(40);
    }

    [Fact]
    public void Add_WithAscendingOrder_ShouldMatchExpectedSequence()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            20, 10, 40
        };

        var expectedList = new List<int>
        {
            20, 10, 40
        };

        // Act
        var actualSequence = sortedList;

        // Assert
        actualSequence.Should().BeEquivalentTo(expectedList.Order());
    }

    [Fact]
    public void Add_WithDescendingOrder_ShouldMatchExpectedSequence()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            20, 10, 40
        };

        var expectedList = new List<int>
        {
            20, 10, 40
        };

        // Act
        var actualSequence = sortedList;

        // Assert
        actualSequence.Should().BeEquivalentTo(expectedList.OrderByDescending(x => x));
    }

    [Fact]
    public void Indexer_WhenSettingFirstElement_ShouldReorderCorrectly()
    {
        // Arrange
        var sortedListA = new SortedList<int>(Criterion.Ascending)
        {
            20, 10, 40
        };

        var sortedListD = new SortedList<int>(Criterion.Descending)
        {
            20, 10, 40
        };

        // Act
        sortedListA[0] = 19; // 19, 20, 40
        sortedListD[0] = 25; // 25, 20, 10

        // Assert
        sortedListA.Should().Equal(new List<int>() { 19, 20, 40 });
        sortedListD.Should().Equal(new List<int>() { 25, 20, 10 });

        // Act - Second modification
        sortedListA[0] = 100; // 20, 40, 100
        sortedListD[0] = 0; // 20, 10, 0

        // Assert - Second modification
        sortedListA.Should().Equal(new List<int>() { 20, 40, 100 });
        sortedListD.Should().Equal(new List<int>() { 20, 10, 0 });

    }


    [Fact]
    public void Indexer_WhenSettingLastElement_ShouldReorderCorrectly()
    {
        // Arrange
        var sortedListA = new SortedList<int>(Criterion.Ascending)
        {
            20, 10, 40
        };

        var sortedListD = new SortedList<int>(Criterion.Descending)
        {
            20, 10, 40
        };

        // Act
        sortedListA[2] = 21; // 10, 20, 21
        sortedListD[2] = 19; // 40, 20, 19

        // Assert
        sortedListA.Should().Equal(new List<int>() { 10, 20, 21 });
        sortedListD.Should().Equal(new List<int>() { 40, 20, 19 });

        // Act - Second modification
        sortedListA[2] = 0; // 0, 10, 20
        sortedListD[2] = 94; // 94, 40, 20

        // Assert - Second modification
        sortedListA.Should().Equal(new List<int>() { 0, 10, 20 });
        sortedListD.Should().Equal(new List<int>() { 94, 40, 20 });
    }

    [Fact]
    public void Indexer_WhenSettingMiddleElement_ShouldReorderCorrectly()
    {
        // Arrange
        var sortedListA = new SortedList<int>(Criterion.Ascending)
        {
            20, 10, 40, 59, 100, 0
        };

        var sortedListD = new SortedList<int>(Criterion.Descending)
        {
            20, 10, 40, 59, 100, 0
        };

        // Act
        sortedListA[3] = 105; // 0, 10, 20, 59, 100, 105
        sortedListD[3] = 19; // 100, 59, 40, 19, 10, 0

        // Assert
        sortedListA.Should().Equal(new List<int>() { 0, 10, 20, 59, 100, 105 });
        sortedListD.Should().Equal(new List<int>() { 100, 59, 40, 19, 10, 0 });
    }

    [Fact]
    public void BinarySearch_WithAscendingOrder_ShouldFindExistingItems()
    {
        // Arrange
        SortedList<int> list = [10, 29, 0, -19, -390, 5];

        // Act
        var foundIndex = list.BinarySearch(29);
        var notFoundIndex = list.BinarySearch(500);

        // Assert
        foundIndex.Should().NotBe(-1);
        notFoundIndex.Should().Be(-1);
    }

    [Fact]
    public void BinarySearch_WithDescendingOrder_ShouldFindExistingItems()
    {
        // Arrange
        SortedList<int> list = new(Criterion.Descending) { 10, 29, 0, -19, -390, 5 };

        // Act
        var foundIndex = list.BinarySearch(29);
        var notFoundIndex = list.BinarySearch(500);

        // Assert
        foundIndex.Should().NotBe(-1);
        notFoundIndex.Should().Be(-1);
    }

    [Fact]
    public void Reverse_WithOddNumberOfElements_ShouldReverseOrder()
    {
        // Arrange
        SortedList<int> list =
        [
            30, 9, -1, 8, 2
        ];

        SortedList<int> expectedList = new(Criterion.Descending)
        {
            30, 9, -1, 8, 2
        };

        // Act
        list.Reverse();

        // Assert
        list.Should().Equal(expectedList);
    }

    [Fact]
    public void Reverse_WithEvenNumberOfElements_ShouldReverseOrder()
    {
        // Arrange
        SortedList<int> list =
        [
            30, 9, -1, 8
        ];

        SortedList<int> expectedList = new(Criterion.Descending)
        {
            30, 9, -1, 8
        };

        // Act
        list.Reverse();

        // Assert
        list.Should().Equal(expectedList);
    }

    [Fact]
    public void Add_WithMultipleItems_ShouldSortAutomatically()
    {
        // Arrange & Act
        SortedList<int> list = [59, 10, 2, 5, -2];

        // Assert
        list.Should().Equal([-2, 2, 5, 10, 59]);
    }

    [Fact]
    public void Add_WithParallelThreads_ShouldAddAllItemsSorted()
    {
        // Arrange
        SortedList<int> list = [];
        var synchronizedList = list.Synchronized();
        var expectedList = new SortedList<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int len = 10;
        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = len
        };

        // Act
        Parallel.For(0, len, options, synchronizedList.Add);

        // Assert
        list.Count.Should().Be(len);
        list.Should().Equal(expectedList);
    }

    [Fact]
    public void Indexer_WhenIncrementingValue_ShouldSucceed()
    {
        // Arrange
        var list = new SortedList<int>() { 0 };
        int maxIter = 10;

        // Act
        for (int i = 0; i < maxIter; i++)
            list[0]++;

        // Assert
        list[0].Should().Be(maxIter);
    }

    [Fact]
    public void Add_WithParallelAdds_ShouldAddAllItemsSorted()
    {
        // Arrange
        SortedList<int> list = [];
        var synchronizedList = list.Synchronized();
        int len = 100;
        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = 10
        };

        // Act
        Parallel.For(0, len, options, i =>
        {
            synchronizedList.Add(i);
        });

        // Assert
        list.Count.Should().Be(len);
        list.Distinct().Count().Should().Be(len);
        list.Should().BeInAscendingOrder();
    }

    [Fact]
    public void Add_WithAscendingOrder_WhenItemEqualToFirstElement_ShouldInsertAtBeginning()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            10, 20, 30, 40
        };

        // Act
        sortedList.Add(10);

        // Assert
        sortedList.Should().Equal([10, 10, 20, 30, 40]);
        sortedList[0].Should().Be(10);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithAscendingOrder_WhenItemLessThanFirstElement_ShouldInsertAtBeginning()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            10, 20, 30, 40
        };

        // Act
        sortedList.Add(5);

        // Assert
        sortedList.Should().Equal([5, 10, 20, 30, 40]);
        sortedList[0].Should().Be(5);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithAscendingOrder_WhenItemEqualToLastElement_ShouldInsertAtEnd()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            10, 20, 30, 40
        };

        // Act
        sortedList.Add(40);

        // Assert
        sortedList.Should().Equal([10, 20, 30, 40, 40]);
        sortedList[sortedList.Count - 1].Should().Be(40);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithAscendingOrder_WhenItemGreaterThanLastElement_ShouldInsertAtEnd()
    {
        // Arrange
        var sortedList = new SortedList<int>
        {
            10, 20, 30, 40
        };

        // Act
        sortedList.Add(50);

        // Assert
        sortedList.Should().Equal([10, 20, 30, 40, 50]);
        sortedList[sortedList.Count - 1].Should().Be(50);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithDescendingOrder_WhenItemEqualToFirstElement_ShouldInsertAtBeginning()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            40, 30, 20, 10
        };

        // Act
        sortedList.Add(40);

        // Assert
        sortedList.Should().Equal([40, 40, 30, 20, 10]);
        sortedList[0].Should().Be(40);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithDescendingOrder_WhenItemGreaterThanFirstElement_ShouldInsertAtBeginning()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            40, 30, 20, 10
        };

        // Act
        sortedList.Add(50);

        // Assert
        sortedList.Should().Equal([50, 40, 30, 20, 10]);
        sortedList[0].Should().Be(50);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithDescendingOrder_WhenItemEqualToLastElement_ShouldInsertAtEnd()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            40, 30, 20, 10
        };

        // Act
        sortedList.Add(10);

        // Assert
        sortedList.Should().Equal([40, 30, 20, 10, 10]);
        sortedList[sortedList.Count - 1].Should().Be(10);
        sortedList.Count.Should().Be(5);
    }

    [Fact]
    public void Add_WithDescendingOrder_WhenItemLessThanLastElement_ShouldInsertAtEnd()
    {
        // Arrange
        var sortedList = new SortedList<int>(Criterion.Descending)
        {
            40, 30, 20, 10
        };

        // Act
        sortedList.Add(5);

        // Assert
        sortedList.Should().Equal([40, 30, 20, 10, 5]);
        sortedList[sortedList.Count - 1].Should().Be(5);
        sortedList.Count.Should().Be(5);
    }
}

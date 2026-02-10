namespace DotNet.AdvancedCollections.Tests.List.DoublyLinkedList;

public class DoublyLinkedListTests
{
    [Fact]
    public void Add_WithMultipleItems_ShouldAddToFrontAndMaintainOrder()
    {
        // Arrange
        var list = new DoublyLinkedList<int>
        {
            1, 2, 3
        };

        // Act
        var actualCount = list.Count;
        var actualSequence = list;

        // Assert
        actualCount.Should().Be(3);
        actualSequence.Should().Equal([3, 2, 1]);
    }

    [Fact]
    public void AddLast_WithMultipleItems_ShouldAddToEndAndMaintainOrder()
    {
        // Arrange
        var list = new DoublyLinkedList<int>();

        // Act
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        // Assert
        list.Count.Should().Be(3);
        list.Should().Equal([1, 2, 3]);
    }

    [Fact]
    public void Add_MixingAddAndAddLast_ShouldMaintainCorrectOrder()
    {
        // Arrange
        var list = new DoublyLinkedList<int>();

        // Act
        list.AddLast(1);
        list.Add(2);
        list.AddLast(3);
        list.Add(4);

        // Assert
        list.Count.Should().Be(4);
        list.Should().Equal([4, 2, 1, 3]);
    }

    [Fact]
    public void GetFirstAndLast_WithMultipleItems_ShouldReturnCorrectValues()
    {
        // Arrange
        var list = new DoublyLinkedList<int>();
        list.AddLast(1);
        list.Add(2);
        list.AddLast(3);
        list.Add(4);

        // Act
        var actualFirst = list.GetFirst();
        var actualLast = list.GetLast();

        // Assert
        list.Count.Should().Be(4);
        actualFirst.Should().Be(4);
        actualLast.Should().Be(3);
    }

    [Theory]
    [InlineData("World", true)]
    [InlineData("Hello", true)]
    [InlineData("Pytham", false)]
    [InlineData("Dave", false)]
    public void Contains_WhenCheckingForValue_ShouldReturnExpectedResult(string value, bool expectedResult)
    {
        // Arrange
        var list = new DoublyLinkedList<string>();
        list.AddLast("Hello");
        list.Add("World");
        list.Add("WoW");

        // Act
        var actualResult = list.Contains(value);

        // Assert
        actualResult.Should().Be(expectedResult);
    }

    [Fact]
    public void Remove_WhenItemExists_ShouldRemoveAndReturnTrue()
    {
        // Arrange
        var list = new DoublyLinkedList<int>
        {
            1,
            2,
            3,
            4
        };

        // Act
        var result = list.Remove(2);

        // Assert
        result.Should().BeTrue();
        list.Count.Should().Be(3);
        list.Should().Equal([4, 3, 1]);
    }

    [Theory]
    [InlineData(5, false)]
    [InlineData(0, false)]
    [InlineData(-4, false)]
    public void Remove_WhenItemDoesNotExist_ShouldReturnFalse(int toRemove, bool expectedResult)
    {
        // Arrange
        var list = new DoublyLinkedList<int>
        {
            1,
            2,
            3,
            4
        };

        // Act
        var actualResult = list.Remove(toRemove);

        // Assert
        actualResult.Should().Be(expectedResult);
        list.Count.Should().Be(4);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(3, 10)]
    [InlineData(2, 3)]
    public void Indexer_WhenSettingValue_ShouldUpdateAndBeSearchable(int index, int value)
    {
        // Arrange
        var list = new DoublyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(0);
        list.AddLast(-1);
        list.AddLast(5);

        // Act
        list[index] = value;

        // Assert
        list[index].Should().Be(value);
        list.Find(value).Should().Be(index);
    }

    [Fact]
    public void Indexer_WhenIncrementingValue_ShouldSucceed()
    {
        // Arrange
        var list = new DoublyLinkedList<int>(0);
        int maxIter = 10;

        // Act
        for (int i = 0; i < maxIter; i++)
            list[0] = list[0] + 1;

        // Assert
        list[0].Should().Be(maxIter);
    }

    [Fact]
    public void Add_WithParallelThreads_ShouldAddAllItemsDistinctly()
    {
        // Arrange
        DoublyLinkedList<int> list = [];
        var synchronizedList = list.Synchronized();
        int len = 10;
        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = len
        };

        // Act
        Parallel.For(0, len, options, synchronizedList.Add);

        // Assert
        list.Count.Should().Be(len);
        list.Distinct().Count().Should().Be(len);
    }

    [Fact]
    public void Add_WithParallelAdds_ShouldAddAllItems()
    {
        // Arrange
        DoublyLinkedList<int> list = [];
        var synchronizedList = DoublyLinkedList<int>.Synchronized(list);
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
    }
}

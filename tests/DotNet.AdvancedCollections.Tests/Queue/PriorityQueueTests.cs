using System.Diagnostics.CodeAnalysis;

namespace DotNet.AdvancedCollections.Tests.Queue;

public class PriorityQueueTests
{
    [Fact]
    [SuppressMessage(
    "Usage",
    "CA1806:Do not ignore method results",
    Justification = "Constructor is expected to throw")]
    public void Constructor_WithNegativeCapacity_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var negativeCapacity = -1;

        // Act
        Action act = () => new PriorityQueue<int>(negativeCapacity);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    [SuppressMessage(
    "Usage",
    "CA1806:Do not ignore method results",
    Justification = "Constructor is expected to throw")]
    public void Constructor_WithPositiveCapacity_ShouldCreateInstance()
    {
        // Arrange
        var positiveCapacity = 1;

        // Act
        Action act = () => new PriorityQueue<int>(positiveCapacity);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Enqueue_WithDifferentPriorities_ShouldDequeueHighestPriorityItem()
    {
        // Arrange
        var queue = new PriorityQueue<string>();
        queue.Enqueue("Pytham", 20);
        queue.Enqueue("MrDave", 10);
        queue.Enqueue("Holly" , 50); //High priority

        // Act
        var actualValue = queue.Dequeue();

        // Assert
        actualValue.Should().Be("Holly");
    }

    [Fact]
    public void Enqueue_WithNegativePriority_ShouldThrowNegativeNumberException()
    {
        // Arrange
        var queue = new PriorityQueue<int>();
        var negativePriority = -1;

        // Act
        Action act = () => queue.Enqueue(20, negativePriority);

        // Assert
        act.Should().Throw<NegativeNumberException>();
    }

    [Fact]
    public void Dequeue_WithMultipleItems_ShouldRemoveAndReturnHighestPriorityItem()
    {
        // Arrange
        var queue = new PriorityQueue<float>();
        queue.Enqueue(10f, 0);
        queue.Enqueue(-20f, 5);

        // Act
        var actualValue = queue.Dequeue();

        // Assert
        actualValue.Should().Be(-20f);
        queue.Should().HaveCount(1);
    }

    [Fact]
    public void Peek_WithMultipleItems_ShouldReturnHighestPriorityItemWithoutRemoving()
    {
        // Arrange
        var queue = new PriorityQueue<double>();
        queue.Enqueue(10.493, 0);
        queue.Enqueue(-20.3910, 0);

        // Act
        var actualValue = queue.Peek();

        // Assert
        actualValue.Should().Be(10.493);
        queue.Count.Should().Be(2);
    }

    [Fact]
    public void Contains_WhenItemsExist_ShouldReturnTrue()
    {
        // Arrange
        var queue = new PriorityQueue<int>();
        queue.Enqueue(5, 0);
        queue.Enqueue(10, 0);
        queue.Enqueue(20, 10);
        queue.Enqueue(50, 50);

        // Act & Assert
        queue.Should().Contain(5);
        queue.Should().Contain(50);
    }
}

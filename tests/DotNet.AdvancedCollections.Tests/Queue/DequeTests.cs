using System.Diagnostics.CodeAnalysis;

namespace DotNet.AdvancedCollections.Tests.Queue;

public class DequeTests
{
    [Fact]
    public void Push_WithMultipleItems_ShouldPeekFirstItem()
    {
        // Arrange
        var deque = new Deque<int>
        {
            1, 2, 5
        };

        //First Element: 5.

        // Act
        var actualFirst = deque.Peek();

        // Assert
        actualFirst.Should().Be(5);
    }

    [Fact]
    public void PushLast_WithMultipleItems_ShouldPeekLastItem()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.PushLast(1);
        deque.PushLast(2);
        deque.PushLast(5);

        //Last Element: 5.

        // Act
        var actualLast = deque.PeekLast();

        // Assert
        actualLast.Should().Be(5);
    }

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
        Action act = () => new Deque<int>(negativeCapacity);

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
        Action act = () => new Deque<int>(positiveCapacity);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void PopLast_WithMultipleItems_ShouldRemoveAndReturnLastItem()
    {
        // Arrange
        //Push First
        var deque = new Deque<string>()
        {
            "Hola",
            "D",
            "M"
        };

        // Act
        var actualLast = deque.PopLast();

        // Assert
        actualLast.Should().Be("Hola");
        deque.Count.Should().Be(2);
    }
}

using System.Diagnostics.CodeAnalysis;

namespace DotNet.AdvancedCollections.Tests.Stack;

public class PriorityStackTests
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
        Action act = () => new PriorityStack<int>(negativeCapacity);

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
        Action act = () => new PriorityStack<int>(positiveCapacity);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(new[] { "ho", "la", "ma" }, new[] { 1, 10, 2 }, "la")]
    [InlineData(new[] { "Pytham", "MrDave", "Holly" }, new[] { 20, 10, 50 }, "Holly")]
    [InlineData(new[] { "Do", "Re", "Mi" }, new[] { 0, 0, 0 }, "Mi")]
    public void Push_WithDifferentPriorities_ShouldPeekHighestPriorityItem(string[] items, int[] priority, string expectedValue)
    {
        // Arrange
        int len = items.Length;
        var stack = new PriorityStack<string>();

        // Act
        for (int i = 0; i != len; ++i)
        {
            stack.Push(items[i], priority[i]);
        }
        var actualValue = stack.Peek();

        // Assert
        actualValue.Should().Be(expectedValue);
    }

    [Fact]
    public void Push_WithNegativePriority_ShouldThrowNegativeNumberException()
    {
        // Arrange
        var stack = new PriorityStack<string>();
        var negativePriority = -1;

        // Act
        Action act = () => stack.Push("Hello", negativePriority);

        // Assert
        act.Should().Throw<NegativeNumberException>();
    }

    [Theory]
    [InlineData(new[] { 10f, 0f, 3f }, new[] { 0, 0, 0}, 3f)]
    [InlineData(new[] { 20f, 3.5f, 2.99f }, new[] { 50, 10, 0 }, 20f)]
    [InlineData(new[] { 1.11f, 2.53f, 3.1415f }, new[] { 10, 20, 5 }, 2.53f)]
    [InlineData(new[] { 1.1f, 3f, -0.5f }, new[] { 47, 0, 65 }, -0.5f)]
    public void Pop_WithMultipleItems_ShouldRemoveAndReturnHighestPriorityItem(float[] items, int[] priority, float expectedPopValue)
    {
        // Arrange
        int len = items.Length;
        var stack = new PriorityStack<float>();
        for (int i = 0; i != len; ++i)
        {
            stack.Push(items[i], priority[i]);
        }

        // Act
        var actualPopValue = stack.Pop();

        // Assert
        actualPopValue.Should().Be(expectedPopValue);
        stack.Count.Should().Be(len - 1);
    }

    [Theory]
    [InlineData(new[] { 10f, 0f, 3f }, new[] { 0, 0, 0 }, 3f)]
    [InlineData(new[] { 20f, 3.5f, 2.99f }, new[] { 50, 10, 0 }, 20f)]
    [InlineData(new[] { 1.11f, 2.53f, 3.1415f }, new[] { 10, 20, 5 }, 2.53f)]
    [InlineData(new[] { 1.1f, 3f, -0.5f }, new[] { 47, 0, 65 }, -0.5f)]
    public void Peek_WithMultipleItems_ShouldReturnHighestPriorityItemWithoutRemoving(float[] items, int[] priority, float expectedPeekValue)
    {
        // Arrange
        int len = items.Length;
        var stack = new PriorityStack<float>();
        for (int i = 0; i != len; ++i)
        {
            stack.Push(items[i], priority[i]);
        }

        // Act
        var actualPeekValue = stack.Peek();

        // Assert
        actualPeekValue.Should().Be(expectedPeekValue);
        stack.Count.Should().Be(len);
    }

    [Theory]
    [InlineData(new[] { 10f, 0f, 3f }, new[] { 0, 0, 0 }, 3f)]
    [InlineData(new[] { 20f, 3.5f, 2.99f }, new[] { 50, 10, 0 }, 20f)]
    [InlineData(new[] { 1.11f, 2.53f, 3.1415f }, new[] { 10, 20, 5 }, 2.53f)]
    [InlineData(new[] { 1.1f, 3f, -0.5f }, new[] { 47, 0, 65 }, -0.5f)]
    public void TryPeek_WithItems_ShouldReturnTrueAndHighestPriorityItem(float[] items, int[] priority, float expectedPeekValue)
    {
        // Arrange
        int len = items.Length;
        var stack = new PriorityStack<float>();
        for (int i = 0; i != len; ++i)
        {
            stack.Push(items[i], priority[i]);
        }

        // Act
        var result = stack.TryPeek(out float actualValue);

        // Assert
        result.Should().BeTrue();
        actualValue.Should().Be(expectedPeekValue);
    }
}

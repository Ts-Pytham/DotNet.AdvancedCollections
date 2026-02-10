namespace DotNet.AdvancedCollections.Tests.Tree;

public class BinarySearchTreeTests
{
    [Fact]
    public void Add_WithMultipleElements_ShouldIncreaseCount()
    {
        // Arrange
        BinarySearchTree<int> tree =
        // InOrder default
        [
            9, 1, 0, 19, 4
        ];

        // Act
        var actualCount = tree.Count;

        // Assert
        actualCount.Should().Be(5);
    }

    [Fact] 
    public void Traverse_WithInOrderTraversal_ShouldReturnSortedSequence()
    {
        // Arrange
        BinarySearchTree<int> tree =
        // InOrder default
        [
            9, 1, 0, 19, 4
        ];

        SortedList<int> expectedList =
        [
            9, 1, 0, 19, 4
        ];

        // Act
        var actualSequence = tree;

        // Assert
        actualSequence.Should().Equal(expectedList);

    }

    [Fact]
    public void Traverse_WithPreOrderTraversal_ShouldReturnCorrectSequence()
    {
        // Arrange
        BinarySearchTree<int> tree = new(TraversalType.PreOrder)
        {
            10, 1, 0, 19
        };

        List<int> expectedList =
        [
            10, 1, 0, 19
        ];

        // Act
        var actualSequence = tree;

        // Assert
        actualSequence.Should().Equal(expectedList);

    }

    [Fact]
    public void Traverse_WithPostOrderTraversal_ShouldReturnCorrectSequence()
    {
        // Arrange
        BinarySearchTree<int> tree = new(TraversalType.PostOrder)
        {
            40, 30, 35, 25, 28, 15, 50, 45, 60, 70, 55
        };

        List<int> expectedList =
        [
            15, 28, 25, 35, 30, 45, 55, 70, 60, 50, 40
        ];

        // Act
        var actualSequence = tree;

        // Assert
        actualSequence.Should().Equal(expectedList);

    }

    [Fact]
    public void Contains_WhenItemExists_ShouldReturnTrue()
    {
        // Arrange
        var itemToFind = "Nint";
        BinarySearchTree<string> tree =
        [
            "Hola",
            "Mundo",
            "Nint"
        ];

        // Act
        var containsItem = tree.Contains(itemToFind);

        // Assert
        containsItem.Should().BeTrue();
    }

    [Fact]
    public void RepeatedNodes_WithDuplicateItems_ShouldTrackRepeatedValues()
    {
        // Arrange
        BinarySearchTree<int> tree =
        [
            10, 39, 50, 2, 3, 10, 20
        ];

        // Act
        var repeatedNodes = tree.RepeatedNodes;

        // Assert
        repeatedNodes.Should().ContainKey(10);
        repeatedNodes.Should().NotContainKey(50); // The value 50 is not repeated
    }

    [Fact]
    public void Remove_WhenNodeIsLeaf_ShouldRemoveSuccessfully()
    {
        // Arrange
        BinarySearchTree<int> tree =
        [
            5, 1, -1, 4, 10
        ];

        // Representación
        /*       5
         *    1    10  
         * -1  4
         * 
         * -1 and 4 are leafs
         */

        // Act
        var wasRemoved = tree.Remove(-1);

        // Assert
        wasRemoved.Should().BeTrue();
        tree.Count.Should().Be(4);
    }

    [Fact]
    public void Remove_WhenNodeHasOneChild_ShouldRemoveAndRestructure()
    {
        // Arrange
        // At least has one child node
        BinarySearchTree<int> tree =
        [
            5, 1, -1, 10
        ];

        // Representación
        /*       5
         *    1    10  
         * -1  
         * 
         * 1 have a left child
         */

        SortedList<int> expectedList =
        [
            5, -1, 10
        ];

        // Act
        var wasRemoved = tree.Remove(1);

        // Assert
        wasRemoved.Should().BeTrue();
        tree.Should().NotContain(1);
        tree.Count.Should().Be(3);
        tree.Should().Equal(expectedList);
    }

    [Fact]
    public void Remove_WhenNodeHasTwoChildren_ShouldRemoveAndRestructure()
    {
        // Arrange
        BinarySearchTree<int> tree =
        [
            5, 1, -1, 2, 10
        ];

        // Representación
        /*              5
         *           1       10  
         *      -1      2  
         * 
         * 1 have two childs
         */

        // Act
        var wasRemoved = tree.Remove(1);

        // Assert
        wasRemoved.Should().BeTrue();
        tree.Should().NotContain(1);
        tree.Count.Should().Be(4);
        tree.Should().Equal(new SortedList<int>() { 5, -1, 2, 10 });
    }

    [Fact]
    public void Remove_WhenNodeHasTwoChildrenWithRightSubtree_ShouldRemoveAndRestructure()
    {
        // Arrange
        BinarySearchTree<int> tree =
        [
            5, 1, -1, 2, 10, 12, 14
        ];

        // Representación
        /*                5
         *           1       10  
         *      -1      2       12
         *                          14
         * 
         * 1 have two childs
         */

        // Act
        var wasRemoved = tree.Remove(10);

        // Assert
        wasRemoved.Should().BeTrue();
        tree.Should().NotContain(10);
        tree.Count.Should().Be(6);
        tree.Should().Equal(new SortedList<int>() { 5, -1, 2, 1, 12, 14});
    }
}

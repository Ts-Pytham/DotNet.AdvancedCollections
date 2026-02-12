namespace DotNet.AdvancedCollections.Tests.Graph;

public class AdjacencyMatrixGraphTests
{
    [Fact]
    public void AddVertex_WithThreeVertices_ShouldIncreaseCountToThree()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        var actualVertexCount = graph.VertexCount();

        // Assert
        actualVertexCount.Should().Be(3);
        graph.HasVertex("A").Should().BeTrue();
        graph.HasVertex("B").Should().BeTrue();
        graph.HasVertex("C").Should().BeTrue();
    }

    [Fact]
    public void AddVertex_DuplicateVertex_ShouldNotIncreaseCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        graph.AddVertex("A");
        graph.AddVertex("A");
        var actualVertexCount = graph.VertexCount();

        // Assert
        actualVertexCount.Should().Be(1);
        graph.HasVertex("A").Should().BeTrue();
    }

    [Theory]
    [InlineData("A", "B", 10)]
    [InlineData("C", "D", 3)]
    [InlineData("X", "Y", 100)]
    public void AddEdge_BetweenTwoVertices_ShouldCreateEdgeWithExpectedCost(string vertex1, string vertex2, int expectedCost)
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex(vertex1);
        graph.AddVertex(vertex2);

        // Act
        graph.AddEdge(vertex1, vertex2, expectedCost);

        // Assert
        graph.HasEdge(vertex1, vertex2).Should().BeTrue();
        graph.EdgeCount().Should().Be(1);
        
        var edges = graph.GetEdges(vertex1).ToList();
        edges.Should().HaveCount(1);
        edges[0].Cost.Should().Be(expectedCost);
        edges[0].Predecessor.Should().Be(vertex1);
        edges[0].Sucessor.Should().Be(vertex2);
    }

    [Fact]
    public void AddEdge_WhenSourceVertexDoesNotExist_ShouldThrowArgumentException()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("B");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => graph.AddEdge("A", "B", 10));
        exception.ParamName.Should().Be("v1");
        exception.Message.Should().Contain("Vertex A not found in graph");
    }

    [Fact]
    public void AddEdge_WhenDestinationVertexDoesNotExist_ShouldThrowArgumentException()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => graph.AddEdge("A", "B", 10));
        exception.ParamName.Should().Be("v2");
        exception.Message.Should().Contain("Vertex B not found in graph");
    }

    [Fact]
    public void AddEdge_UpdateExistingEdge_ShouldUpdateCostWithoutIncreasingCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddEdge("A", "B", 10);

        // Act
        graph.AddEdge("A", "B", 20);

        // Assert
        graph.EdgeCount().Should().Be(1);
        var edges = graph.GetEdges("A").ToList();
        edges[0].Cost.Should().Be(20);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    public void HasVertex_WhenVertexExists_ShouldReturnTrue(string vertex)
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex(vertex);

        // Act
        var vertexExists = graph.HasVertex(vertex);

        // Assert
        vertexExists.Should().BeTrue();
    }

    [Fact]
    public void HasVertex_WhenVertexDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act
        var vertexExists = graph.HasVertex("B");

        // Assert
        vertexExists.Should().BeFalse();
    }

    [Fact]
    public void HasEdge_WhenEdgeExists_ShouldReturnTrue()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddEdge("A", "B", 10);

        // Act
        var edgeExists = graph.HasEdge("A", "B");

        // Assert
        edgeExists.Should().BeTrue();
    }

    [Fact]
    public void HasEdge_WhenEdgeDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");

        // Act
        var edgeExists = graph.HasEdge("A", "B");

        // Assert
        edgeExists.Should().BeFalse();
    }

    [Fact]
    public void HasEdge_WhenVerticesDoNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        var edgeExists = graph.HasEdge("A", "B");

        // Assert
        edgeExists.Should().BeFalse();
    }

    [Fact]
    public void GetNeighbors_FromVertexWithMultipleEdges_ShouldReturnAllNeighbors()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");
        graph.AddEdge("A", "B", 5);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("A", "D", 10);

        // Act
        var neighbors = graph.GetNeighbors("A").ToList();

        // Assert
        neighbors.Should().HaveCount(3);
        neighbors.Should().Contain(new[] { "B", "C", "D" });
    }

    [Fact]
    public void GetNeighbors_FromVertexWithNoEdges_ShouldReturnEmpty()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");

        // Act
        var neighbors = graph.GetNeighbors("A").ToList();

        // Assert
        neighbors.Should().BeEmpty();
    }

    [Fact]
    public void GetNeighbors_FromNonExistentVertex_ShouldReturnEmpty()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act
        var neighbors = graph.GetNeighbors("B").ToList();

        // Assert
        neighbors.Should().BeEmpty();
    }

    [Fact]
    public void RemoveVertex_WhenVertexExists_ShouldReturnTrueAndDecreaseCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");

        // Act
        var wasRemoved = graph.RemoveVertex("A");
        var remainingVertices = graph.GetVertices().ToList();

        // Assert
        wasRemoved.Should().BeTrue();
        graph.VertexCount().Should().Be(3);
        remainingVertices.Should().HaveCount(3);
        remainingVertices.Should().Contain(new[] { "B", "C", "D" });
        graph.HasVertex("A").Should().BeFalse();
    }

    [Fact]
    public void RemoveVertex_WhenVertexDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act
        var wasRemoved = graph.RemoveVertex("B");

        // Assert
        wasRemoved.Should().BeFalse();
        graph.VertexCount().Should().Be(1);
    }

    [Fact]
    public void RemoveVertex_WithConnectedEdges_ShouldRemoveVertexAndAllConnectedEdges()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddEdge("A", "B", 10);
        graph.AddEdge("B", "C", 20);
        graph.AddEdge("C", "A", 30);

        // Act
        var wasRemoved = graph.RemoveVertex("B");

        // Assert
        wasRemoved.Should().BeTrue();
        graph.VertexCount().Should().Be(2);
        graph.HasEdge("A", "B").Should().BeFalse();
        graph.HasEdge("B", "C").Should().BeFalse();
        graph.HasEdge("C", "A").Should().BeTrue();
    }

    [Fact]
    public void RemoveEdge_WhenEdgeExists_ShouldReturnTrueAndDecreaseEdgeCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddEdge("A", "B", 20);

        // Act
        var wasRemoved = graph.RemoveEdge("A", "B");

        // Assert
        wasRemoved.Should().BeTrue();
        graph.EdgeCount().Should().Be(0);
        graph.HasEdge("A", "B").Should().BeFalse();
    }

    [Fact]
    public void RemoveEdge_WithMultipleEdges_ShouldRemoveOnlySpecifiedEdge()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddEdge("A", "B", 20);
        graph.AddEdge("B", "C", 0);
        graph.AddEdge("A", "C", 10);

        // Act
        var firstEdgeRemoved = graph.RemoveEdge("A", "B");
        var secondEdgeRemoved = graph.RemoveEdge("A", "C");

        // Assert
        firstEdgeRemoved.Should().BeTrue();
        secondEdgeRemoved.Should().BeTrue();
        graph.EdgeCount().Should().Be(1);
        graph.HasEdge("A", "B").Should().BeFalse();
        graph.HasEdge("A", "C").Should().BeFalse();
        graph.HasEdge("B", "C").Should().BeTrue();
    }

    [Fact]
    public void RemoveEdge_WhenEdgeDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");

        // Act
        var wasRemoved = graph.RemoveEdge("A", "B");

        // Assert
        wasRemoved.Should().BeFalse();
        graph.EdgeCount().Should().Be(0);
    }

    [Fact]
    public void RemoveEdge_WhenVerticesDoNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        var firstRemovalAttempt = graph.RemoveEdge("A", "B");
        var secondRemovalAttempt = graph.RemoveEdge("C", "D");

        // Assert
        firstRemovalAttempt.Should().BeFalse();
        secondRemovalAttempt.Should().BeFalse();
    }

    [Fact]
    public void GetEdges_FromVertexWithMultipleEdges_ShouldReturnAllEdges()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");
        graph.AddEdge("A", "B", 5);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("A", "D", 10);

        // Act
        var edges = graph.GetEdges("A").ToList();

        // Assert
        edges.Should().HaveCount(3);
        edges.Should().Contain(e => e.Predecessor.Equals("A") && e.Sucessor.Equals("B") && e.Cost == 5);
        edges.Should().Contain(e => e.Predecessor.Equals("A") && e.Sucessor.Equals("C") && e.Cost == 2);
        edges.Should().Contain(e => e.Predecessor.Equals("A") && e.Sucessor.Equals("D") && e.Cost == 10);
    }

    [Fact]
    public void GetEdges_FromVertexWithNoEdges_ShouldReturnEmpty()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act
        var edges = graph.GetEdges("A").ToList();

        // Assert
        edges.Should().BeEmpty();
    }

    [Fact]
    public void GetEdges_FromNonExistentVertex_ShouldReturnEmpty()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        var edges = graph.GetEdges("A").ToList();

        // Assert
        edges.Should().BeEmpty();
    }

    [Fact]
    public void GetVertices_WithMultipleVertices_ShouldReturnAllVertices()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");

        // Act
        var vertices = graph.GetVertices().ToList();

        // Assert
        vertices.Should().HaveCount(3);
        vertices.Should().Contain(new[] { "A", "B", "C" });
    }

    [Fact]
    public void GetVertices_WithNoVertices_ShouldReturnEmpty()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        var vertices = graph.GetVertices().ToList();

        // Assert
        vertices.Should().BeEmpty();
    }

    [Fact]
    public void Degree_FromVertexWithMultipleEdges_ShouldReturnCorrectCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");
        graph.AddEdge("A", "B", 5);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("A", "D", 10);

        // Act
        var degree = graph.Degree("A");

        // Assert
        degree.Should().Be(3);
    }

    [Fact]
    public void Degree_FromVertexWithNoEdges_ShouldReturnZero()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");

        // Act
        var degree = graph.Degree("A");

        // Assert
        degree.Should().Be(0);
    }

    [Fact]
    public void Degree_FromNonExistentVertex_ShouldReturnZero()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();

        // Act
        var degree = graph.Degree("A");

        // Assert
        degree.Should().Be(0);
    }

    [Fact]
    public void EdgeCount_WithMultipleEdges_ShouldReturnCorrectCount()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddEdge("A", "B", 10);
        graph.AddEdge("B", "C", 20);
        graph.AddEdge("C", "A", 30);

        // Act
        var edgeCount = graph.EdgeCount();

        // Assert
        edgeCount.Should().Be(3);
    }

    [Fact]
    public void EdgeCount_WithNoEdges_ShouldReturnZero()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        graph.AddVertex("A");
        graph.AddVertex("B");

        // Act
        var edgeCount = graph.EdgeCount();

        // Assert
        edgeCount.Should().Be(0);
    }

    [Fact]
    public void MatrixResize_WhenExceedingInitialCapacity_ShouldResizeAndMaintainData()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<int, int>(initialCapacity: 2);
        
        // Act
        for (int i = 0; i < 20; i++)
        {
            graph.AddVertex(i);
        }
        
        graph.AddEdge(0, 1, 10);
        graph.AddEdge(5, 10, 20);
        graph.AddEdge(15, 19, 30);

        // Assert
        graph.VertexCount().Should().Be(20);
        graph.EdgeCount().Should().Be(3);
        graph.HasEdge(0, 1).Should().BeTrue();
        graph.HasEdge(5, 10).Should().BeTrue();
        graph.HasEdge(15, 19).Should().BeTrue();
    }

    [Fact]
    public void ComplexGraphOperations_ShouldMaintainConsistency()
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<string, int>();
        
        // Add vertices
        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");
        graph.AddVertex("E");
        
        // Add edges
        graph.AddEdge("A", "B", 1);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("B", "D", 3);
        graph.AddEdge("C", "D", 4);
        graph.AddEdge("D", "E", 5);

        // Act & Assert - Initial state
        graph.VertexCount().Should().Be(5);
        graph.EdgeCount().Should().Be(5);
        
        // Remove vertex C (should remove edges A->C and C->D)
        graph.RemoveVertex("C");
        graph.VertexCount().Should().Be(4);
        graph.EdgeCount().Should().Be(3);
        graph.HasEdge("A", "C").Should().BeFalse();
        graph.HasEdge("C", "D").Should().BeFalse();
        
        // Remove edge B->D
        graph.RemoveEdge("B", "D");
        graph.EdgeCount().Should().Be(2);
        
        // Remaining edges should be A->B and D->E
        graph.HasEdge("A", "B").Should().BeTrue();
        graph.HasEdge("D", "E").Should().BeTrue();
        
        // Check neighbors
        var neighborsA = graph.GetNeighbors("A").ToList();
        neighborsA.Should().HaveCount(1);
        neighborsA.Should().Contain("B");
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void LargeGraph_ShouldHandleCorrectly(int vertexCount)
    {
        // Arrange
        var graph = new AdjacencyMatrixGraph<int, int>();
        
        // Act
        for (int i = 0; i < vertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        // Add some edges
        for (int i = 0; i < vertexCount - 1; i++)
        {
            graph.AddEdge(i, i + 1, i);
        }

        // Assert
        graph.VertexCount().Should().Be(vertexCount);
        graph.EdgeCount().Should().Be(vertexCount - 1);
        graph.HasVertex(0).Should().BeTrue();
        graph.HasVertex(vertexCount - 1).Should().BeTrue();
        graph.HasEdge(0, 1).Should().BeTrue();
        graph.HasEdge(vertexCount - 2, vertexCount - 1).Should().BeTrue();
    }
}

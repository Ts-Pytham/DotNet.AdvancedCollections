namespace DotNet.AdvancedCollections.Tests.Graph;

public class GraphTests
{
    [Fact]
    public void AddVertex_WithThreeVertices_ShouldIncreaseCountToThree()
    {
        // Arrange
        var expectedVertexCount = 3;
        var vertexGraph = new Graph<string, int>
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
            new Vertex<string, int>("C"),
        };

        // Act
        var actualVertexCount = vertexGraph.Count;

        // Assert
        actualVertexCount.Should().Be(expectedVertexCount);
        vertexGraph.HasVertex("A").Should().BeTrue();
        vertexGraph.HasVertex("B").Should().BeTrue();
        vertexGraph.HasVertex("C").Should().BeTrue();
    }

    [Theory]
    [InlineData("A", "B", 10)]
    [InlineData("C", "D", 3.3)]
    [InlineData(2, 5, 5d)]
    public void AddEdge_BetweenTwoVertices_ShouldCreateEdgeWithExpectedCost(object vertex1, object vertex2, decimal expectedCost)
    {
        // Arrange
        var sourceVertex = vertex1;
        var destinationVertex = vertex2;
        var edgeGraph = new Graph<object, decimal>
        {
            new Vertex<object, decimal>(sourceVertex),
            new Vertex<object, decimal>(destinationVertex),
        };

        // Act
        edgeGraph.AddEdge(sourceVertex, destinationVertex, expectedCost);
        edgeGraph.TryGetEdge(sourceVertex, destinationVertex, out var retrievedEdge);

        // Assert
        edgeGraph.Count.Should().Be(2);
        retrievedEdge.Should().NotBeNull();
        retrievedEdge!.Cost.Should().Be(expectedCost);
        edgeGraph.HasEdge(sourceVertex, destinationVertex).Should().BeTrue();
    }

    [Theory]
    [InlineData("A")]
    [InlineData('B')]
    [InlineData(4)]
    [InlineData(5.4f)]
    public void HasVertex_WhenVertexExists_ShouldReturnTrue(object vertex)
    {
        // Arrange
        var vertexToCheck = vertex;
        var singleVertexGraph = new Graph<object, int>()
        {
            new Vertex<object, int>(vertexToCheck)
        };

        // Act
        var vertexExists = singleVertexGraph.HasVertex(vertexToCheck);

        // Assert
        singleVertexGraph.Should().HaveCount(1);
        vertexExists.Should().BeTrue();
    }

    [Theory]
    [InlineData("Hello", "Nil", 20)]
    [InlineData('A', 'B', 0)]
    [InlineData(5, 10, 200.504d)]
    public void HasEdge_WhenEdgeExists_ShouldReturnTrue(object vertex, object vertex2, decimal cost)
    {
        // Arrange
        var fromVertex = vertex;
        var toVertex = vertex2;
        var edgeCost = cost;
        var edgeGraph = new Graph<object, decimal>()
        {
            new Vertex<object, decimal>(fromVertex),
            new Vertex<object, decimal>(toVertex)
        };

        // Act
        edgeGraph.AddEdge(fromVertex, toVertex, edgeCost);
        edgeGraph.TryGetEdge(fromVertex, toVertex, out var retrievedEdge);
        var edgeExists = edgeGraph.HasEdge(fromVertex, toVertex);

        // Assert
        retrievedEdge.Should().NotBeNull();
        retrievedEdge!.Cost.Should().Be(edgeCost);
        edgeExists.Should().BeTrue();
    }

    [Fact]
    public void GetSuccessors_FromVertexWithMultipleEdges_ShouldReturnAllSuccessors()
    {
        // Arrange
        var sourceVertexName = "A";
        var expectedSuccessorNames = new[] { "B", "C", "D" };
        var directedGraph = new Graph<string, int>()
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
            new Vertex<string, int>("C"),
            new Vertex<string, int>("D")
        };
        directedGraph.AddEdge("A", "B", 5);
        directedGraph.AddEdge("A", "C", 2);
        directedGraph.AddEdge("A", "D", 10);

        // Act
        var actualSuccessorNames = directedGraph.Successors(sourceVertexName).Select(x => x.VertexName);

        // Assert
        actualSuccessorNames.Should().Equal(expectedSuccessorNames);
    }

    [Fact]
    public void GetPredecessors_FromVertexWithMultipleEdges_ShouldReturnAllPredecessors()
    {
        // Arrange
        var targetVertexName = "B";
        var expectedPredecessorNames = new[] { "A", "C", "D" };
        var directedGraph = new Graph<string, int>()
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
            new Vertex<string, int>("C"),
            new Vertex<string, int>("D")
        };
        directedGraph.AddEdge("A", "B", 5);
        directedGraph.AddEdge("C", "B", 2);
        directedGraph.AddEdge("D", "B", 10);

        // Act
        var actualPredecessorNames = directedGraph.Predecessors(targetVertexName).Select(x => x.VertexName);

        // Assert
        actualPredecessorNames.Should().Equal(expectedPredecessorNames);
    }

    [Fact]
    public void RemoveVertex_WhenVertexExists_ShouldReturnTrueAndDecreaseCount()
    {
        // Arrange
        var vertexToRemove = "A";
        var expectedRemainingVertices = new[] { "B", "C", "D" };
        var graphWithVertices = new Graph<string, int>()
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
            new Vertex<string, int>("C"),
            new Vertex<string, int>("D")
        };

        // Act
        var wasRemoved = graphWithVertices.RemoveVertex(vertexToRemove);
        var remainingVertexNames = graphWithVertices.Select(x => x.VertexName);

        // Assert
        wasRemoved.Should().BeTrue();
        graphWithVertices.Count.Should().Be(3);
        remainingVertexNames.Should().Equal(expectedRemainingVertices);
    }

    [Fact]
    public void RemoveVertex_WhenVertexDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var nonExistentVertex = "B";
        var graphWithSingleVertex = new Graph<string, int>()
        {
            new Vertex<string, int>("A")
        };

        // Act
        var wasRemoved = graphWithSingleVertex.RemoveVertex(nonExistentVertex);

        // Assert
        wasRemoved.Should().BeFalse();
    }

    [Fact]
    public void RemoveEdge_WhenEdgeExists_ShouldReturnTrueAndClearEdges()
    {
        // Arrange
        var fromVertex = "A";
        var toVertex = "B";
        var edgeCost = 20;
        var graphWithEdge = new Graph<string, int>()
        {
            new Vertex<string, int>(fromVertex),
            new Vertex<string, int>(toVertex)
        };
        graphWithEdge.AddEdge(fromVertex, toVertex, edgeCost);

        // Act
        var wasRemoved = graphWithEdge.RemoveEdge(fromVertex, toVertex);

        // Assert
        wasRemoved.Should().BeTrue();
        graphWithEdge.TryGetVertex(fromVertex, out var vertexA);
        graphWithEdge.TryGetVertex(toVertex, out var vertexB);
        vertexA!.Edges.Should().BeEmpty();
        vertexB!.Edges.Should().BeEmpty();
    }

    [Fact]
    public void RemoveEdge_WithMultipleEdges_ShouldRemoveAllSuccessfully()
    {
        // Arrange
        var graphWithMultipleEdges = new Graph<string, int>()
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
            new Vertex<string, int>("C")
        };
        graphWithMultipleEdges.AddEdge("A", "B", 20);
        graphWithMultipleEdges.AddEdge("B", "C", 0);
        graphWithMultipleEdges.AddEdge("A", "C", 10);

        // Act
        var firstEdgeRemoved = graphWithMultipleEdges.RemoveEdge("A", "B");
        var secondEdgeRemoved = graphWithMultipleEdges.RemoveEdge("A", "C");

        // Assert
        firstEdgeRemoved.Should().BeTrue();
        secondEdgeRemoved.Should().BeTrue();
        graphWithMultipleEdges.TryGetVertex("A", out var vertexA);
        graphWithMultipleEdges.TryGetVertex("C", out var vertexC);
        vertexA!.Edges.Should().BeEmpty();
        vertexC!.Edges.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveEdge_WhenEdgeDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var graphWithoutEdges = new Graph<string, int>()
        {
            new Vertex<string, int>("A"),
            new Vertex<string, int>("B"),
        };

        // Act
        var firstRemovalAttempt = graphWithoutEdges.RemoveEdge("A", "B");
        var secondRemovalAttempt = graphWithoutEdges.RemoveEdge("C", "D");

        // Assert
        firstRemovalAttempt.Should().BeFalse();
        secondRemovalAttempt.Should().BeFalse();
    }
}


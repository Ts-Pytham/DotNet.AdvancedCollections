using BenchmarkDotNet.Attributes;
using DotNet.AdvancedCollections.Graph;

namespace DotNet.AdvancedCollections.Benchmarks.Graphs;

/// <summary>
/// Comparison benchmarks between AdjacencyMatrixGraph and Graph (adjacency list) implementations.
/// Tests various graph operations to determine which implementation performs better for different scenarios.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class GraphImplementationComparisonBenchmarks
{
    private readonly Random _random = new(42);

    [Params(10, 100, 500)]
    public int VertexCount { get; set; }

    [Params(2, 5, 10)]
    public int EdgesPerVertex { get; set; }

    // ==================== AddVertex Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("AddVertex")]
    public void AddVertex_AdjacencyList()
    {
        var graph = new Graph<int, int>();
        for (int i = 0; i < VertexCount; i++)
        {
            graph.Add(new Vertex<int, int>(i));
        }
    }

    [Benchmark]
    [BenchmarkCategory("AddVertex")]
    public void AddVertex_AdjacencyMatrix()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
    }

    // ==================== AddEdge Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("AddEdge")]
    public void AddEdge_AdjacencyList()
    {
        var graph = new Graph<int, int>();
        
        for (int i = 0; i < VertexCount; i++)
        {
            graph.Add(new Vertex<int, int>(i));
        }
        
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
    }

    [Benchmark]
    [BenchmarkCategory("AddEdge")]
    public void AddEdge_AdjacencyMatrix()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
    }

    // ==================== HasVertex Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("HasVertex")]
    public bool HasVertex_AdjacencyList()
    {
        var graph = CreateAdjacencyList();
        
        bool result = false;
        for (int i = 0; i < VertexCount; i++)
        {
            result |= graph.HasVertex(i);
        }
        return result;
    }

    [Benchmark]
    [BenchmarkCategory("HasVertex")]
    public bool HasVertex_AdjacencyMatrix()
    {
        var graph = CreateAdjacencyMatrix();
        
        bool result = false;
        for (int i = 0; i < VertexCount; i++)
        {
            result |= graph.HasVertex(i);
        }
        return result;
    }

    // ==================== HasEdge Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("HasEdge")]
    public bool HasEdge_AdjacencyList()
    {
        var graph = CreateAdjacencyList();
        
        bool result = false;
        for (int i = 0; i < VertexCount; i++)
        {
            int target = _random.Next(VertexCount);
            result |= graph.HasEdge(i, target);
        }
        return result;
    }

    [Benchmark]
    [BenchmarkCategory("HasEdge")]
    public bool HasEdge_AdjacencyMatrix()
    {
        var graph = CreateAdjacencyMatrix();
        
        bool result = false;
        for (int i = 0; i < VertexCount; i++)
        {
            int target = _random.Next(VertexCount);
            result |= graph.HasEdge(i, target);
        }
        return result;
    }

    // ==================== GetNeighbors Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("GetNeighbors")]
    public void GetNeighbors_AdjacencyList()
    {
        var graph = CreateAdjacencyList();
        
        for (int i = 0; i < VertexCount; i++)
        {
            var neighbors = graph.GetNeighbors(i);
            var list = neighbors.ToList();
        }
    }

    [Benchmark]
    [BenchmarkCategory("GetNeighbors")]
    public void GetNeighbors_AdjacencyMatrix()
    {
        var graph = CreateAdjacencyMatrix();
        
        for (int i = 0; i < VertexCount; i++)
        {
            var neighbors = graph.GetNeighbors(i);
            var list = neighbors.ToList();
        }
    }

    // ==================== Degree Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Degree")]
    public int Degree_AdjacencyList()
    {
        var graph = CreateAdjacencyList();
        
        int totalDegree = 0;
        for (int i = 0; i < VertexCount; i++)
        {
            totalDegree += graph.Degree(i);
        }
        return totalDegree;
    }

    [Benchmark]
    [BenchmarkCategory("Degree")]
    public int Degree_AdjacencyMatrix()
    {
        var graph = CreateAdjacencyMatrix();
        
        int totalDegree = 0;
        for (int i = 0; i < VertexCount; i++)
        {
            totalDegree += graph.Degree(i);
        }
        return totalDegree;
    }

    // ==================== RemoveEdge Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("RemoveEdge")]
    public void RemoveEdge_AdjacencyList()
    {
        var graph = CreateAdjacencyList();
        
        for (int i = 0; i < VertexCount / 2; i++)
        {
            int target = _random.Next(VertexCount);
            graph.RemoveEdge(i, target);
        }
    }

    [Benchmark]
    [BenchmarkCategory("RemoveEdge")]
    public void RemoveEdge_AdjacencyMatrix()
    {
        var graph = CreateAdjacencyMatrix();
        
        for (int i = 0; i < VertexCount / 2; i++)
        {
            int target = _random.Next(VertexCount);
            graph.RemoveEdge(i, target);
        }
    }

    // ==================== FullWorkflow Benchmarks ====================

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("FullWorkflow")]
    public void FullWorkflow_AdjacencyList()
    {
        var graph = new Graph<int, int>();
        
        // Add vertices
        for (int i = 0; i < VertexCount; i++)
        {
            graph.Add(new Vertex<int, int>(i));
        }
        
        // Add edges
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
        
        // Check edges
        for (int i = 0; i < VertexCount / 4; i++)
        {
            int target = _random.Next(VertexCount);
            bool hasEdge = graph.HasEdge(i, target);
        }
        
        // Get neighbors
        for (int i = 0; i < VertexCount / 4; i++)
        {
            var neighbors = graph.GetNeighbors(i).ToList();
        }
        
        // Remove some edges
        for (int i = 0; i < VertexCount / 4; i++)
        {
            int target = _random.Next(VertexCount);
            graph.RemoveEdge(i, target);
        }
    }

    [Benchmark]
    [BenchmarkCategory("FullWorkflow")]
    public void FullWorkflow_AdjacencyMatrix()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        // Add vertices
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        // Add edges
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
        
        // Check edges
        for (int i = 0; i < VertexCount / 4; i++)
        {
            int target = _random.Next(VertexCount);
            bool hasEdge = graph.HasEdge(i, target);
        }
        
        // Get neighbors
        for (int i = 0; i < VertexCount / 4; i++)
        {
            var neighbors = graph.GetNeighbors(i).ToList();
        }
        
        // Remove some edges
        for (int i = 0; i < VertexCount / 4; i++)
        {
            int target = _random.Next(VertexCount);
            graph.RemoveEdge(i, target);
        }
    }

    // ==================== Helper Methods ====================

    private Graph<int, int> CreateAdjacencyList()
    {
        var graph = new Graph<int, int>();
        
        for (int i = 0; i < VertexCount; i++)
        {
            graph.Add(new Vertex<int, int>(i));
        }
        
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
        
        return graph;
    }

    private AdjacencyMatrixGraph<int, int> CreateAdjacencyMatrix()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
            }
        }
        
        return graph;
    }
}

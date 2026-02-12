using BenchmarkDotNet.Attributes;
using DotNet.AdvancedCollections.Graph;

namespace DotNet.AdvancedCollections.Benchmarks.Graphs;

/// <summary>
/// Benchmarks for AdjacencyMatrixGraph operations to measure performance characteristics.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class AdjacencyMatrixGraphBenchmarks
{
    private AdjacencyMatrixGraph<int, int>? _graph;
    private readonly Random _random = new(42);

    [Params(10, 100, 1000)]
    public int VertexCount { get; set; }

    [Params(2, 5, 10)]
    public int EdgesPerVertex { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        // Add vertices
        for (int i = 0; i < VertexCount; i++)
        {
            _graph.AddVertex(i);
        }
        
        // Add edges
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                _graph.AddEdge(i, target, cost);
            }
        }
    }

    [Benchmark]
    public void AddVertex()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
    }

    [Benchmark]
    public void AddEdge()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        // First add vertices
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        // Then add edges
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
    public bool HasVertex()
    {
        bool result = false;
        for (int i = 0; i < VertexCount; i++)
        {
            result |= _graph!.HasVertex(i);
        }
        return result;
    }

    [Benchmark]
    public bool HasEdge()
    {
        bool result = false;
        for (int i = 0; i < VertexCount / 2; i++)
        {
            int target = _random.Next(VertexCount);
            result |= _graph!.HasEdge(i, target);
        }
        return result;
    }

    [Benchmark]
    public void RemoveVertex()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        // Setup
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
        
        // Benchmark: Remove half the vertices
        for (int i = 0; i < VertexCount / 2; i++)
        {
            graph.RemoveVertex(i);
        }
    }

    [Benchmark]
    public void RemoveEdge()
    {
        var graph = new AdjacencyMatrixGraph<int, int>(VertexCount);
        
        // Setup
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
        
        var edges = new List<(int from, int to)>();
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = 0; j < EdgesPerVertex; j++)
            {
                int target = _random.Next(VertexCount);
                int cost = _random.Next(1, 100);
                graph.AddEdge(i, target, cost);
                edges.Add((i, target));
            }
        }
        
        // Benchmark: Remove edges
        for (int i = 0; i < edges.Count / 2; i++)
        {
            graph.RemoveEdge(edges[i].from, edges[i].to);
        }
    }

    [Benchmark]
    public void GetNeighbors()
    {
        // Benchmark: Get neighbors for multiple vertices
        for (int i = 0; i < VertexCount; i++)
        {
            var neighbors = _graph!.GetNeighbors(i);
            var list = neighbors.ToList(); // Force enumeration
        }
    }

    [Benchmark]
    public void GetEdges()
    {
        // Benchmark: Get edges for multiple vertices
        for (int i = 0; i < VertexCount; i++)
        {
            var edges = _graph!.GetEdges(i);
            var list = edges.ToList(); // Force enumeration
        }
    }

    [Benchmark]
    public void Degree()
    {
        // Benchmark: Get degree for all vertices
        int totalDegree = 0;
        for (int i = 0; i < VertexCount; i++)
        {
            totalDegree += _graph!.Degree(i);
        }
    }

    [Benchmark]
    public void FullWorkflow()
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
}

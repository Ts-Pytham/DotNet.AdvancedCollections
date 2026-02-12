using BenchmarkDotNet.Attributes;
using DotNet.AdvancedCollections.Graph;

namespace DotNet.AdvancedCollections.Benchmarks.Graphs;

/// <summary>
/// Benchmarks for Graph operations to compare different implementations.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class GraphBenchmarks
{
    private IGraph<int, int>? _graph;
    private readonly Random _random = new(42);

    [Params(10, 100, 1000)]
    public int VertexCount { get; set; }

    [Params(2, 5, 10)]
    public int EdgesPerVertex { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _graph = new Graph<int, int>();
        
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
        var graph = new Graph<int, int>();
        for (int i = 0; i < VertexCount; i++)
        {
            graph.AddVertex(i);
        }
    }

    [Benchmark]
    public void AddEdge()
    {
        var graph = new Graph<int, int>();
        
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
        var graph = new Graph<int, int>();
        
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
        var graph = new Graph<int, int>();
        
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
        var graph = new Graph<int, int>();
        
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
        
        // Benchmark: Get neighbors for multiple vertices
        for (int i = 0; i < VertexCount; i++)
        {
            var neighbors = graph.GetNeighbors(i);
            var list = neighbors.ToList(); // Force enumeration
        }
    }

    [Benchmark]
    public void Degree()
    {
        var graph = new Graph<int, int>();
        
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
        
        // Benchmark: Get degree for all vertices
        int totalDegree = 0;
        for (int i = 0; i < VertexCount; i++)
        {
            totalDegree += graph.Degree(i);
        }
    }
}

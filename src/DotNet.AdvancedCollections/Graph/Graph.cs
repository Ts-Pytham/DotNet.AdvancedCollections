using DotNet.AdvancedCollections.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Represents a graph data structure, composed of vertices and edges connecting them.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices in the graph.</typeparam>
/// <typeparam name="TEdge">The type of the edges in the graph (cost).</typeparam>
public class Graph<TVertex, TEdge> 
    : IGraph<TVertex, TEdge>, ICollection<Vertex<TVertex, TEdge>>, IEnumerable<Vertex<TVertex, TEdge>>
        where TEdge : notnull, IComparable<TEdge>
        where TVertex : notnull
{
    /// <summary>
    /// Gets the list of vertices in the graph.
    /// </summary>
    private readonly Dictionary<TVertex, Vertex<TVertex, TEdge>> _vertices;

    /// <summary>
    /// Gets the number of vertices in the graph.
    /// </summary>
    public int Count => _vertices.Count;

    /// <summary>
    /// Gets a value indicating whether the graph is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Constructs a new instance of the graph with no vertices.
    /// </summary>
    public Graph()
    {
        _vertices = [];
    }

    /// <summary>
    /// Constructs a new instance of the graph with a single vertex.
    /// </summary>
    /// <param name="item">The vertex to be added to the graph.</param>
    public Graph(Vertex<TVertex, TEdge> item) : this()
    {
        Add(item);
    }

    /// <summary>
    /// Constructs a new instance of the graph with a collection of vertices.
    /// </summary>
    /// <param name="items">The vertices to be added to the graph.</param>
    public Graph(IEnumerable<Vertex<TVertex, TEdge>> items) : this()
    {
        foreach(var item in items)
        {
            Add(item);
        }
    }

    /// <summary>
    /// Adds a vertex to the graph.
    /// </summary>
    /// <param name="item">The vertex to be added.</param>
    /// <exception cref="ExistentVertexException">Thrown if the vertex already exists in the graph.</exception>
    public void Add(Vertex<TVertex, TEdge> item)
    {
        AddVertex(item.VertexName);
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.AddEdge(TVertex, TVertex, TEdge)"/>
    public void AddEdge(TVertex v1, TVertex v2, TEdge cost)
    {
        if (TryGetVertex(v1, out Vertex<TVertex, TEdge>? vertex) is false || vertex is null)
        {
            throw new NonExistentVertexException();
        }

        if (TryGetVertex(v2, out Vertex<TVertex, TEdge>? vertex2) is false || vertex2 is null)
        {
            throw new NonExistentVertexException();
        }

        var edge = new Edge<TVertex, TEdge>(vertex.VertexName, vertex2.VertexName, cost);

        vertex.AddEdge(edge);
        vertex2.AddEdge(edge);

        vertex.Successors.Add(vertex2);
        vertex2.Predecessors.Add(vertex);
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.AddVertex(TVertex)"/>
    public void AddVertex(TVertex vertex)
    {
        if (HasVertex(vertex))
        {
            throw new ExistentVertexException();
        }

        _vertices[vertex] = new Vertex<TVertex, TEdge>(vertex);
    }

    /// <summary>
    /// Removes all vertices from the graph.
    /// </summary>
    public void Clear()
    {
        _vertices.Clear();
    }

    /// <summary>
    /// Determines whether the graph contains a specific vertex.
    /// </summary>
    /// <param name="item">The vertex to locate in the graph.</param>
    /// <returns><see langword="true"/> if the vertex is found in the graph; otherwise, <see langword="false"/>.</returns>
    public bool Contains(Vertex<TVertex, TEdge> item)
    {
        return _vertices.ContainsKey(item.VertexName);
    }

    /// <summary>
    /// Copies the elements of the graph to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the graph. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(Vertex<TVertex, TEdge>[] array, int arrayIndex)
    {
        _vertices.Values.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the graph.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the graph.</returns>
    public IEnumerator<Vertex<TVertex, TEdge>> GetEnumerator()
    {
        return _vertices.Values.GetEnumerator();
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.HasEdge(TVertex, TVertex)"/>
    public bool HasEdge(TVertex v1, TVertex v2)
        => TryGetEdge(v1, v2, out _);

    /// <summary>
    /// Searches for an edge between two vertices and returns it if found.
    /// </summary>
    /// <param name="v1">The starting vertex of the edge.</param>
    /// <param name="v2">The ending vertex of the edge.</param>
    /// <param name="edge">If found, the edge connecting the two vertices; otherwise, the default value of TEdge.</param>
    public bool TryGetEdge(
        TVertex v1, 
        TVertex v2, 
        [NotNullWhen(true)] out Edge<TVertex, TEdge>? edge)
    {
        edge = null;
        if (TryGetVertex(v1, out Vertex<TVertex, TEdge>? vertex) is false)
        {
            return false;
        }

        edge = vertex.Edges.Find(e =>
            EqualityComparer<TVertex>.Default.Equals(e.Sucessor, v2));

        return edge is not null;
    }

    private bool TryGetEdge(
        TVertex v1, TVertex v2, 
        out Vertex<TVertex, TEdge>? vertex, 
        out Vertex<TVertex, TEdge>? vertex2, 
        [NotNullWhen(true)] out Edge<TVertex, TEdge>? edge)
    {
        edge = null;
        vertex2 = null;

        if(TryGetVertex(v1, out vertex) is false || TryGetVertex(v1, out vertex) is false)
        {
            return false;
        }

        edge = vertex.Edges.Find(e =>
            EqualityComparer<TVertex>.Default.Equals(e.Sucessor, v2));

        return edge is not null;
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.HasVertex(TVertex)"/>
    public bool HasVertex(TVertex vertex)
    {
        return _vertices.ContainsKey(vertex);
    }

    /// <summary>
    /// Determines if the graph has a vertex with the given name.
    /// </summary>
    /// <param name="name">The name of the vertex to search for.</param>
    /// <param name="vertex">The vertex with the given name, if it exists in the graph.</param>
    /// <returns>The vertex with the given name, if it exists in the graph.</returns>
    public bool TryGetVertex(
        TVertex name, 
        [NotNullWhen(true)] out Vertex<TVertex, TEdge>? vertex)
    {
        var exists = _vertices.TryGetValue(name, out vertex);

        return exists is true;
    }

    /// <summary>
    /// Removes a vertex from the graph.
    /// </summary>
    /// <param name="item">The vertex to remove from the graph.</param>
    /// <returns>True if the vertex was successfully removed, false otherwise.</returns>
    public bool Remove(Vertex<TVertex, TEdge> item)
        => RemoveVertex(item.VertexName);

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.RemoveEdge(TVertex, TVertex)"/>
    public bool RemoveEdge(TVertex v1, TVertex v2)
    {
        if(TryGetEdge(v1, v2, 
            out Vertex<TVertex, TEdge>? vertex, 
            out Vertex<TVertex, TEdge>? vertex2, 
            out Edge <TVertex, TEdge>? edge) is false)
        {
            return false;
        }

        var isRemoved = vertex!.Edges.Remove(edge!) && vertex2!.Edges.Remove(edge!);

        if(isRemoved)
        {
            vertex.Successors.Remove(vertex2!);
            vertex2!.Predecessors.Remove(vertex);
        }

        return isRemoved;
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.RemoveVertex(TVertex)"/>
    public bool RemoveVertex(TVertex vertex)
    {
        if (TryGetVertex(vertex, out _) is false)
        {
            return false;
        }

        foreach (var v in _vertices.Values)
        {
            v.Edges.RemoveAll(e =>
                EqualityComparer<TVertex>.Default.Equals(e.Sucessor, vertex) ||
                EqualityComparer<TVertex>.Default.Equals(e.Predecessor, vertex));
            v.Predecessors.RemoveAll(p => EqualityComparer<TVertex>.Default.Equals(p.VertexName, vertex));
            v.Successors.RemoveAll(s => EqualityComparer<TVertex>.Default.Equals(s.VertexName, vertex));
        }

        return _vertices.Remove(vertex);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the graph.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the graph.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _vertices.Keys.GetEnumerator();

    }

    /// <summary>
    /// Returns an enumerable of vertexes that are the successors of the specified vertex in the graph.
    /// </summary>
    /// <param name="v">The vertex whose successors are to be returned.</param>
    /// <returns>An enumerable of vertexes that are the successors of the specified vertex in the graph.</returns>
    /// <exception cref="NonExistentVertexException">Thrown if the specified vertex does not exist in the graph.</exception>
    public IEnumerable<Vertex<TVertex, TEdge>> Successors(TVertex v)
    {
        if(TryGetVertex(v, out Vertex<TVertex, TEdge>? vertex) is false)
        {
            throw new NonExistentVertexException();
        }

        return vertex!.Successors;
    }

    /// <summary>
    /// Returns an enumerable of vertexes that are the predecessors of the specified vertex in the graph.
    /// </summary>
    /// <param name="v">The vertex whose predecessors are to be returned.</param>
    /// <returns>An enumerable of vertexes that are the predecessors of the specified vertex in the graph.</returns>
    /// <exception cref="NonExistentVertexException">Thrown if the specified vertex does not exist in the graph.</exception>
    public IEnumerable<Vertex<TVertex, TEdge>> Predecessors(TVertex v)
    {
        if (TryGetVertex(v, out Vertex<TVertex, TEdge>? vertex) is false)
        {
            throw new NonExistentVertexException();
        }

        return vertex!.Predecessors;
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.VertexCount"/>
    public int VertexCount()
    {
        return _vertices.Count;
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.EdgeCount"/>
    public int EdgeCount()
    {
        return _vertices.Values.Sum(v => v.Edges.Count);
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.GetVertices"/>
    public IEnumerable<TVertex> GetVertices()
    {
        return _vertices.Keys;
    }

    /// <inheritdoc cref="IGraph{TVertex, TEdge}.GetEdges(TVertex)"/>
    public IEnumerable<Edge<TVertex, TEdge>> GetEdges(TVertex vertex)
    {
        if (_vertices.TryGetValue(vertex, out var v))
        {
            return v.Edges;
        }
        return [];
    }
}

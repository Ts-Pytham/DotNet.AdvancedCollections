namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Represents a vertex in a graph, with a name and a list of edges connecting it to other vertices.
/// </summary>
/// <typeparam name="T">The type of the vertex name.</typeparam>
/// <typeparam name="U">The type of the cost associated with the edges connected to the vertex.</typeparam>
/// <remarks>
/// Constructs a new instance of the vertex with the specified name.
/// </remarks>
/// <param name="Vertex">The name of the vertex.</param>
public class Vertex<T, U>(T Vertex) : IEquatable<Vertex<T, U>>
    where U : notnull, IComparable<U>
    where T : notnull
{
    /// <summary>
    /// Gets or sets the name of the vertex.
    /// </summary>
    public T VertexName { get; set; } = Vertex;

    /// <summary>
    /// Gets the list of edges connecting the vertex to other vertices.
    /// </summary>
    public List<Edge<T, U>> Edges { get; } = [];

    public List<Vertex<T, U>> Predecessors { get; } = [];
    
    public List<Vertex<T, U>> Successors { get; } = [];

    /// <summary>
    /// Adds an edge to the list of edges connecting the vertex to other vertices.
    /// </summary>
    /// <param name="edge">The edge to be added.</param>
    public void AddEdge(Edge<T, U> edge)
    {
        Edges.Add(edge);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if(obj is null) return false;

        return Equals(obj as Vertex<T, U>);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(VertexName, Edges);
    }

    /// <summary>
    /// Determines if this vertex is equal to another vertex based on the vertex name and edges.
    /// </summary>
    /// <param name="other">The other vertex to compare with.</param>
    /// <returns>True if the vertices are equal, false otherwise.</returns>
    public bool Equals(Vertex<T, U>? other)
    {
        if(other is null) return false;

        if (other.VertexName.Equals(VertexName) && other.Edges.SequenceEqual(Edges))
        {
            return true;
        }

        return false;
    }
}

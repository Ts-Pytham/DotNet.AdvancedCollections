namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Represents an edge in a graph, connecting two vertices and having a cost associated with it.
/// </summary>
/// <typeparam name="T">The type of the vertices connected by the edge.</typeparam>
/// <typeparam name="U">The type of the cost associated with the edge.</typeparam>
/// <remarks>
/// Initializes a new instance of the Edge class with the specified sucessor vertex, predecessor vertex, and cost.
/// </remarks>
/// <param name="sucessor">The sucessor vertex of the edge.</param>
/// <param name="predecessor">The predecessor vertex of the edge.</param>
/// <param name="cost">The cost of the edge.</param>
public class Edge<T, U>(Vertex<T, U> predecessor, Vertex<T, U> sucessor, U cost)
    where U : notnull, IComparable<U>
{
    /// <summary>
    /// Gets or sets the vertex that the edge connects to.
    /// </summary>
    public Vertex<T, U> Sucessor { get; set; } = sucessor;

    /// <summary>
    /// The predecessor of this vertex in the graph.
    /// </summary>
    public Vertex<T, U> Predecessor { get; set; } = predecessor;

    /// <summary>
    /// Gets or sets the cost of the edge.
    /// </summary>
    public U Cost { get; set; } = cost;
}

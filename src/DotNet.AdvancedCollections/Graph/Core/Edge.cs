namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Represents an edge in a graph, connecting two vertices and having a cost associated with it.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices connected by the edge.</typeparam>
/// <typeparam name="TEdge">The type of the cost associated with the edge.</typeparam>
/// <remarks>
/// Initializes a new instance of the Edge class with the specified sucessor vertex, predecessor vertex, and cost.
/// </remarks>
/// <param name="sucessor">The sucessor vertex of the edge.</param>
/// <param name="predecessor">The predecessor vertex of the edge.</param>
/// <param name="cost">The cost of the edge.</param>
public class Edge<TVertex, TEdge>(
    TVertex predecessor, 
    TVertex sucessor, 
    TEdge cost)
        where TEdge : notnull, IComparable<TEdge>
        where TVertex : notnull
{
    /// <summary>
    /// Gets or sets the successor vertex in the graph.
    /// </summary>
    public TVertex Sucessor { get; set; } = sucessor;
    
    /// <summary>
    /// Gets or sets the predecessor vertex in a traversal or pathfinding operation.
    /// </summary>
    public TVertex Predecessor { get; set; } = predecessor;

    /// <summary>
    /// Gets or sets the cost of the edge.
    /// </summary>
    public TEdge Cost { get; set; } = cost;
}

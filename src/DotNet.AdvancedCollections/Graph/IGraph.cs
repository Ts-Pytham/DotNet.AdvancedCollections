namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Represents a interface graph data structure.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices in the graph.</typeparam>
/// <typeparam name="TEdge">The type of the edges in the graph.</typeparam>
public interface IGraph<TVertex, TEdge>
    where TEdge : notnull, IComparable<TEdge>
    where TVertex : notnull
{
    /// <summary>
    /// Adds a vertex to the graph.
    /// </summary>
    /// <param name="vertex">The vertex to be added.</param>
    void AddVertex(TVertex vertex);

    /// <summary>
    /// Adds an edge to the graph, connecting the two specified vertices.
    /// </summary>
    /// <param name="v1">The first vertex to be connected.</param>
    /// <param name="v2">The second vertex to be connected.</param>
    /// <param name="cost">The cost connecting the two vertices.</param>
    void AddEdge(TVertex v1, TVertex v2, TEdge cost);

    /// <summary>
    /// Removes a vertex from the graph.
    /// </summary>
    /// <param name="vertex">The vertex to be removed.</param>
    /// <returns>True if the vertex was successfully removed, false otherwise.</returns>
    bool RemoveVertex(TVertex vertex);

    /// <summary>
    /// Removes an edge from the graph, disconnecting the two specified vertices.
    /// </summary>
    /// <param name="v1">The first vertex to be disconnected.</param>
    /// <param name="v2">The second vertex to be disconnected.</param>
    /// <returns>True if the edge was successfully removed, false otherwise.</returns>
    bool RemoveEdge(TVertex v1, TVertex v2);

    /// <summary>
    /// Determines whether the graph contains a specific vertex.
    /// </summary>
    /// <param name="vertex">The vertex to be searched for.</param>
    /// <returns><see langword="true"/> if the vertex is found; <see langword="false"/> otherwise.</returns>
    bool HasVertex(TVertex vertex);

    /// <summary>
    /// Determines whether the graph contains an edge connecting the two specified vertices.
    /// </summary>
    /// <param name="v1">The first vertex of the edge.</param>
    /// <param name="v2">The second vertex of the edge.</param>
    /// <returns><see langword="true"/> if the edge is found; <see langword="false"/> otherwise.</returns>
    bool HasEdge(TVertex v1, TVertex v2);

    /// <summary>
    /// Gets the number of vertices in the mesh.
    /// </summary>
    /// <returns>The total number of vertices contained in the mesh.</returns>
    int VertexCount();

    /// <summary>
    /// Returns the total number of edges in the graph.
    /// </summary>
    /// <returns>The number of edges currently present in the graph. Returns 0 if the graph contains no edges.</returns>
    int EdgeCount();

    /// <summary>
    /// Returns an enumerable collection of all vertices in the graph.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{TVertex}"/> containing the vertices of the graph. The collection may be empty if the
    /// graph contains no vertices.</returns>
    IEnumerable<TVertex> GetVertices();

    /// <summary>
    /// Returns a collection of edges that are connected to the specified vertex.
    /// </summary>
    /// <param name="vertex">The vertex for which to retrieve connected edges. Cannot be null.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="Edge{TVertex, TEdge}"/> objects associated with 
    /// the specified vertex. The collection will be empty if the
    /// vertex has no connected edges.</returns>
    IEnumerable<Edge<TVertex, TEdge>> GetEdges(TVertex vertex);

    /// <summary>
    /// Returns an enumerable collection of vertices that are directly connected to the specified vertex.
    /// </summary>
    /// <param name="vertex">The vertex for which to retrieve neighboring vertices. Must be a valid vertex in the graph.</param>
    /// <returns>An enumerable collection of vertices adjacent to the specified vertex. If the vertex has no neighbors, the
    /// collection is empty.</returns>
    IEnumerable<TVertex> GetNeighbors(TVertex vertex);

    /// <summary>
    /// Gets the degree of the specified vertex.
    /// </summary>
    /// <param name="vertex">The vertex for which to calculate the degree. Cannot be null.</param>
    /// <returns>The number of edges incident to the specified vertex.</returns>
    int Degree(TVertex vertex);
}

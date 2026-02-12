namespace DotNet.AdvancedCollections.Graph;

/// <summary>
/// Graph implementation using an adjacency matrix.
/// Uses a bidirectional mapping between generic vertices and matrix indices.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices in the graph.</typeparam>
/// <typeparam name="TEdge">The type of the edges in the graph.</typeparam>
public class AdjacencyMatrixGraph<TVertex, TEdge>(int initialCapacity = 10) : IGraph<TVertex, TEdge>
    where TEdge : notnull, IComparable<TEdge>
    where TVertex : notnull
{
    private TEdge[,] _adjacencyMatrix = new TEdge[initialCapacity, initialCapacity];
    private bool[,] _hasEdge = new bool[initialCapacity, initialCapacity];
    private readonly Dictionary<TVertex, int> _vertexToIndex = [];
    private readonly Dictionary<int, TVertex> _indexToVertex = [];
    private int _vertexCount = 0;
    private int _edgeCount = 0;
    private int _capacity = initialCapacity;

    public void AddVertex(TVertex vertex)
    {
        if (_vertexToIndex.ContainsKey(vertex))
        {
            return;
        }

        if (_vertexCount >= _capacity)
        {
            ResizeMatrix(_capacity * 2);
        }

        _vertexToIndex[vertex] = _vertexCount;
        _indexToVertex[_vertexCount] = vertex;
        _vertexCount++;
    }

    public void AddEdge(TVertex v1, TVertex v2, TEdge cost)
    {
        if (!_vertexToIndex.TryGetValue(v1, out int index1))
        {
            throw new ArgumentException($"Vertex {v1} not found in graph.", nameof(v1));
        }

        if (!_vertexToIndex.TryGetValue(v2, out int index2))
        {
            throw new ArgumentException($"Vertex {v2} not found in graph.", nameof(v2));
        }

        if (!_hasEdge[index1, index2])
        {
            _edgeCount++;
        }

        _adjacencyMatrix[index1, index2] = cost;
        _hasEdge[index1, index2] = true;
    }

    public bool RemoveVertex(TVertex vertex)
    {
        if (!_vertexToIndex.TryGetValue(vertex, out int indexToRemove))
        {
            return false;
        }

        for (int i = 0; i < _vertexCount; i++)
        {
            if (_hasEdge[indexToRemove, i])
            {
                _edgeCount--;
            }
            if (_hasEdge[i, indexToRemove] && i != indexToRemove)
            {
                _edgeCount--;
            }
        }

        if (indexToRemove != _vertexCount - 1)
        {
            int lastIndex = _vertexCount - 1;
            TVertex lastVertex = _indexToVertex[lastIndex];

            _vertexToIndex.Remove(vertex);
            _vertexToIndex[lastVertex] = indexToRemove;
            _indexToVertex.Remove(lastIndex);
            _indexToVertex[indexToRemove] = lastVertex;

            for (int i = 0; i < _vertexCount; i++)
            {
                _adjacencyMatrix[indexToRemove, i] = _adjacencyMatrix[lastIndex, i];
                _adjacencyMatrix[i, indexToRemove] = _adjacencyMatrix[i, lastIndex];
                _hasEdge[indexToRemove, i] = _hasEdge[lastIndex, i];
                _hasEdge[i, indexToRemove] = _hasEdge[i, lastIndex];
            }

            for (int i = 0; i < _vertexCount; i++)
            {
                _adjacencyMatrix[lastIndex, i] = default!;
                _adjacencyMatrix[i, lastIndex] = default!;
                _hasEdge[lastIndex, i] = false;
                _hasEdge[i, lastIndex] = false;
            }
        }
        else
        {
            _vertexToIndex.Remove(vertex);
            _indexToVertex.Remove(indexToRemove);
            
            for (int i = 0; i < _vertexCount; i++)
            {
                _adjacencyMatrix[indexToRemove, i] = default!;
                _adjacencyMatrix[i, indexToRemove] = default!;
                _hasEdge[indexToRemove, i] = false;
                _hasEdge[i, indexToRemove] = false;
            }
        }

        _vertexCount--;
        return true;
    }

    public bool RemoveEdge(TVertex v1, TVertex v2)
    {
        if (!_vertexToIndex.TryGetValue(v1, out int index1) ||
            !_vertexToIndex.TryGetValue(v2, out int index2))
        {
            return false;
        }

        if (_hasEdge[index1, index2])
        {
            _adjacencyMatrix[index1, index2] = default!;
            _hasEdge[index1, index2] = false;
            _edgeCount--;
            return true;
        }

        return false;
    }

    public bool HasVertex(TVertex vertex)
    {
        return _vertexToIndex.ContainsKey(vertex);
    }

    public bool HasEdge(TVertex v1, TVertex v2)
    {
        if (!_vertexToIndex.TryGetValue(v1, out int index1) ||
            !_vertexToIndex.TryGetValue(v2, out int index2))
        {
            return false;
        }

        return _hasEdge[index1, index2];
    }

    public int VertexCount()
    {
        return _vertexCount;
    }

    public int EdgeCount()
    {
        return _edgeCount;
    }

    public IEnumerable<TVertex> GetVertices()
    {
        return _vertexToIndex.Keys;
    }

    public IEnumerable<Edge<TVertex, TEdge>> GetEdges(TVertex vertex)
    {
        if (!_vertexToIndex.TryGetValue(vertex, out int index))
        {
            yield break;
        }

        for (int i = 0; i < _vertexCount; i++)
        {
            if (_hasEdge[index, i])
            {
                yield return new Edge<TVertex, TEdge>(vertex, _indexToVertex[i], _adjacencyMatrix[index, i]);
            }
        }
    }

    public IEnumerable<TVertex> GetNeighbors(TVertex vertex)
    {
        if (!_vertexToIndex.TryGetValue(vertex, out int index))
        {
            yield break;
        }

        for (int i = 0; i < _vertexCount; i++)
        {
            if (_hasEdge[index, i])
            {
                yield return _indexToVertex[i];
            }
        }
    }

    public int Degree(TVertex vertex)
    {
        if (!_vertexToIndex.TryGetValue(vertex, out int index))
        {
            return 0;
        }

        int degree = 0;
        for (int i = 0; i < _vertexCount; i++)
        {
            if (_hasEdge[index, i])
            {
                degree++;
            }
        }

        return degree;
    }

    private void ResizeMatrix(int newCapacity)
    {
        var newMatrix = new TEdge[newCapacity, newCapacity];
        var newHasEdge = new bool[newCapacity, newCapacity];

        for (int i = 0; i < _vertexCount; i++)
        {
            for (int j = 0; j < _vertexCount; j++)
            {
                newMatrix[i, j] = _adjacencyMatrix[i, j];
                newHasEdge[i, j] = _hasEdge[i, j];
            }
        }

        _adjacencyMatrix = newMatrix;
        _hasEdge = newHasEdge;
        _capacity = newCapacity;
    }
}

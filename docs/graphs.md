# Graphs

DotNet.AdvancedCollections provides **two complete graph implementations**, each optimized for different use cases.

## Graph Implementations

### 1. Graph&lt;TVertex, TEdge&gt; (Adjacency List)

A graph implementation using an **adjacency list** representation. Best for **sparse graphs** (few edges relative to vertices).

**Key Features:**
- ? Memory efficient: O(V + E) space complexity
- ? Fast neighbor iteration: O(degree)
- ? Ideal for graph traversal algorithms (BFS, DFS)
- ? Collection initializer support
- ? Support for directed and undirected graphs

**When to use:**
- Your graph is sparse (E < V²/4)
- You frequently iterate over neighbors
- Memory usage is a concern
- You're implementing traversal algorithms

### 2. AdjacencyMatrixGraph&lt;TVertex, TEdge&gt;

A graph implementation using an **adjacency matrix** with bidirectional vertex-to-index mapping. Best for **dense graphs** or frequent edge lookups.

**Key Features:**
- ? Ultra-fast edge checking: O(1)
- ? Fast edge addition/removal: O(1)
- ? Ideal for algorithms requiring frequent edge queries
- ? Auto-resizing matrix when capacity is exceeded

**When to use:**
- Your graph is dense (E > V²/3)
- You frequently check if edges exist
- You know the approximate vertex count in advance
- Edge lookup performance is critical

## Performance Comparison

| Operation | Adjacency List | Adjacency Matrix |
|-----------|---------------|------------------|
| Add Vertex | O(1) | O(1) amortized* |
| Add Edge | O(1) | O(1) |
| Has Edge | O(degree) | **O(1)** ? |
| Get Neighbors | **O(degree)** ? | O(V) |
| Remove Vertex | O(V + E) | O(V) |
| Remove Edge | O(degree) | O(1) |
| Space | **O(V + E)** ?? | O(V²) |

*Requires matrix resize when capacity is exceeded

---

## Graph&lt;TVertex, TEdge&gt; (Adjacency List)

### Creating Graphs

```csharp
using DotNet.AdvancedCollections.Graph;

// Create a graph with vertex type char and edge weight type int
var graph = new Graph<char, int>();

// Add vertices
graph.AddVertex('A');
graph.AddVertex('B');
graph.AddVertex('C');

// Or use collection initializer
var graph2 = new Graph<char, int>
{
    new Vertex<char, int>('A'),
    new Vertex<char, int>('B'),
    new Vertex<char, int>('C')
};
```

### Vertices

```csharp
var graph = new Graph<string, int>();

// Add vertices
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Check if a vertex exists
bool exists = graph.HasVertex("A"); // true

// Remove vertex
graph.RemoveVertex("C");

// Iterate over vertices
foreach (var vertex in graph)
{
    Console.WriteLine(vertex.VertexName);
}
```

### Edges

```csharp
var graph = new Graph<string, int>();
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Add edges with cost/weight
graph.AddEdge("A", "B", 10);
graph.AddEdge("B", "C", 5);
graph.AddEdge("A", "C", 15);

// Check if an edge exists
bool hasEdge = graph.HasEdge("A", "B"); // true

// Remove edge
bool removed = graph.RemoveEdge("A", "B");

// Get the cost/weight of an edge
var cost = graph.GetCost("A", "C"); // 15
```

### Adjacencies

```csharp
var graph = new Graph<char, int>();
graph.AddVertex('A');
graph.AddVertex('B');
graph.AddVertex('C');
graph.AddEdge('A', 'B', 10);
graph.AddEdge('A', 'C', 5);

// Get predecessors (vertices that point to this vertex)
var predecessors = graph.Predecessors('A');
foreach (var vertex in predecessors)
{
    Console.WriteLine(vertex.VertexName);
}

// Get successors (vertices that this vertex points to)
var successors = graph.Successors('A');
foreach (var vertex in successors)
{
    Console.WriteLine(vertex.VertexName);
}
```

## Vertex&lt;TVertex, TEdge&gt;

Represents a vertex in the graph.

```csharp
using DotNet.AdvancedCollections.Graph;

var vertex = new Vertex<char, int>('A');
char name = vertex.VertexName;
```

## Edge&lt;TVertex, TEdge&gt;

Represents an edge between two vertices.

```csharp
using DotNet.AdvancedCollections.Graph;

var source = new Vertex<string, int>("A");
var destination = new Vertex<string, int>("B");
var edge = new Edge<string, int>(source, destination, 10);

var from = edge.From;
var to = edge.To;
int cost = edge.Cost;
```

## Exceptions

The library provides graph-specific exceptions:

- `ExistentVertexException`: Thrown when attempting to add a vertex that already exists
- `NonExistentVertexException`: Thrown when attempting to operate on a non-existent vertex

```csharp
var graph = new Graph<string, int>();

try
{
    graph.AddVertex("A");
    graph.AddVertex("A"); // Throws ExistentVertexException
}
catch (ExistentVertexException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

try
{
    // Throws NonExistentVertexException if "Z" doesn't exist
    graph.AddEdge("A", "Z", 10);
}
catch (NonExistentVertexException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Practical Example

```csharp
using DotNet.AdvancedCollections.Graph;

// Create a social network graph
var socialNetwork = new Graph<string, int>
{
    new Vertex<string, int>("Alice"),
    new Vertex<string, int>("Bob"),
    new Vertex<string, int>("Charlie"),
    new Vertex<string, int>("Diana")
};

// Add connections (friendships) with closeness score
socialNetwork.AddEdge("Alice", "Bob", 10);
socialNetwork.AddEdge("Alice", "Charlie", 8);
socialNetwork.AddEdge("Bob", "Diana", 5);
socialNetwork.AddEdge("Charlie", "Diana", 7);

// Find Alice's friends (successors)
var aliceFriends = socialNetwork.Successors("Alice");
Console.WriteLine($"Alice has {aliceFriends.Count()} friends");
foreach (var friend in aliceFriends)
{
    Console.WriteLine(friend.VertexName);
}

// Check if two people are friends
bool areFriends = socialNetwork.HasEdge("Bob", "Charlie");
Console.WriteLine($"Bob and Charlie are friends: {areFriends}");
```

---

## AdjacencyMatrixGraph&lt;TVertex, TEdge&gt;

The adjacency matrix implementation uses a 2D matrix for edge storage with bidirectional vertex-to-index mapping.

### Creating an Adjacency Matrix Graph

```csharp
using DotNet.AdvancedCollections.Graph;

// Create with default capacity (10)
var graph = new AdjacencyMatrixGraph<char, int>();

// Create with specific initial capacity
var largeGraph = new AdjacencyMatrixGraph<string, double>(initialCapacity: 100);
```

### Adding Vertices

```csharp
var graph = new AdjacencyMatrixGraph<string, int>();

// Add vertices
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Check vertex count
int count = graph.VertexCount(); // 3

// Check if vertex exists
bool exists = graph.HasVertex("A"); // true
```

### Adding and Checking Edges

```csharp
var graph = new AdjacencyMatrixGraph<string, int>(initialCapacity: 10);

graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Add edges with costs/weights
graph.AddEdge("A", "B", 10);
graph.AddEdge("B", "C", 5);
graph.AddEdge("A", "C", 15);

// Check edge existence - O(1) operation!
if (graph.HasEdge("A", "B"))
{
    Console.WriteLine("A is connected to B");
}

// Get edge count
int edgeCount = graph.EdgeCount(); // 3
```

### Getting Neighbors and Edges

```csharp
var graph = new AdjacencyMatrixGraph<char, int>();
graph.AddVertex('A');
graph.AddVertex('B');
graph.AddVertex('C');
graph.AddEdge('A', 'B', 10);
graph.AddEdge('A', 'C', 5);

// Get all neighbors
foreach (var neighbor in graph.GetNeighbors('A'))
{
    Console.WriteLine($"Neighbor: {neighbor}"); // B, C
}

// Get all edges from a vertex
foreach (var edge in graph.GetEdges('A'))
{
    Console.WriteLine($"Edge: {edge.Predecessor} -> {edge.Sucessor}, Cost: {edge.Cost}");
}

// Get vertex degree (number of outgoing edges)
int degree = graph.Degree('A'); // 2
```

### Removing Vertices and Edges

```csharp
var graph = new AdjacencyMatrixGraph<string, int>();
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddEdge("A", "B", 10);
graph.AddEdge("B", "C", 5);

// Remove edge - O(1) operation!
bool removed = graph.RemoveEdge("A", "B"); // true

// Remove vertex (also removes all connected edges)
bool vertexRemoved = graph.RemoveVertex("C"); // true
```

### Getting All Vertices

```csharp
var graph = new AdjacencyMatrixGraph<int, int>();
graph.AddVertex(1);
graph.AddVertex(2);
graph.AddVertex(3);

// Iterate over all vertices
foreach (var vertex in graph.GetVertices())
{
    Console.WriteLine($"Vertex: {vertex}");
}
```

### Practical Example: Route Network

```csharp
using DotNet.AdvancedCollections.Graph;

// Create a city route network
var cityNetwork = new AdjacencyMatrixGraph<string, int>(initialCapacity: 50);

// Add cities
cityNetwork.AddVertex("New York");
cityNetwork.AddVertex("Los Angeles");
cityNetwork.AddVertex("Chicago");
cityNetwork.AddVertex("Houston");
cityNetwork.AddVertex("Phoenix");

// Add routes with distances (km)
cityNetwork.AddEdge("New York", "Chicago", 1145);
cityNetwork.AddEdge("New York", "Houston", 2294);
cityNetwork.AddEdge("Los Angeles", "Phoenix", 600);
cityNetwork.AddEdge("Chicago", "Houston", 1523);
cityNetwork.AddEdge("Houston", "Phoenix", 1645);

// Check if there's a direct route - O(1)!
if (cityNetwork.HasEdge("New York", "Chicago"))
{
    Console.WriteLine("There is a direct route from New York to Chicago");
}

// Find all cities with direct routes from Houston
Console.WriteLine("Cities reachable from Houston:");
foreach (var city in cityNetwork.GetNeighbors("Houston"))
{
    Console.WriteLine($"  - {city}");
}

// Get degree (number of direct routes)
int routes = cityNetwork.Degree("Houston");
Console.WriteLine($"Houston has {routes} direct routes");
```

---

## Choosing the Right Implementation

### Use **Graph (Adjacency List)** when:

- ? Graph is **sparse** (few edges: E < V²/4)
- ? You **iterate neighbors** frequently
- ? **Memory** is limited
- ? Implementing **traversal algorithms** (BFS, DFS)
- ? Vertex count changes frequently

**Example use cases:**
- Social networks (few friends relative to total users)
- Web page links (few links per page)
- Dependency graphs
- File system structures

### Use **AdjacencyMatrixGraph** when:

- ? Graph is **dense** (many edges: E > V²/3)
- ? You **check edge existence** very frequently
- ? You know the **vertex count** in advance
- ? **Edge lookup speed** is critical
- ? Implementing matrix-based algorithms

**Example use cases:**
- Complete or near-complete graphs
- Small dense networks (e.g., tournament brackets)
- Flight route networks (dense connections)
- Algorithms like Floyd-Warshall

### Quick Decision Formula

```csharp
double density = (double)edgeCount / (vertexCount * vertexCount);

if (density > 0.3)
{
    // Dense graph -> Use AdjacencyMatrixGraph
    var graph = new AdjacencyMatrixGraph<TVertex, TEdge>(vertexCount);
}
else
{
    // Sparse graph -> Use Graph
    var graph = new Graph<TVertex, TEdge>();
}
```

---

## Common Types

### Vertex&lt;TVertex, TEdge&gt;

Represents a vertex in the adjacency list graph.

```csharp
var vertex = new Vertex<char, int>('A');
char name = vertex.VertexName;
```

### Edge&lt;TVertex, TEdge&gt;

Represents an edge between two vertices (used by both implementations).

```csharp
// The Edge constructor takes: predecessor, successor, cost
var edge = new Edge<string, int>("A", "B", 10);

string from = edge.Predecessor;  // "A"
string to = edge.Sucessor;       // "B"
int cost = edge.Cost;            // 10
```

---

## Exceptions

Both graph implementations use the same exception types:

- `ExistentVertexException`: Thrown when attempting to add a duplicate vertex
- `NonExistentVertexException`: Thrown when operating on a non-existent vertex

```csharp
var graph = new AdjacencyMatrixGraph<string, int>();

try
{
    graph.AddVertex("A");
    graph.AddVertex("A"); // Would throw in adjacency list, returns silently in matrix
}
catch (ExistentVertexException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

try
{
    // Throws if vertex doesn't exist
    graph.AddEdge("A", "NonExistent", 10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

## Best Practices

### Pre-allocate Capacity (Matrix)

```csharp
// Good: Pre-allocate if you know the size
var graph = new AdjacencyMatrixGraph<int, int>(initialCapacity: 1000);

// Add all vertices first
for (int i = 0; i < 1000; i++)
{
    graph.AddVertex(i);
}

// Then add edges
// This avoids matrix resizing
```

### Batch Operations

```csharp
// Good: Add vertices first, then edges
var graph = new AdjacencyMatrixGraph<string, int>(100);

// Add all vertices
foreach (var city in cities)
{
    graph.AddVertex(city);
}

// Then add all edges
foreach (var route in routes)
{
    graph.AddEdge(route.From, route.To, route.Distance);
}
```

### Cache Degree if Used Frequently

```csharp
// If you need degree frequently, cache it
var degreeCache = new Dictionary<TVertex, int>();

foreach (var vertex in graph.GetVertices())
{
    degreeCache[vertex] = graph.Degree(vertex);
}
```

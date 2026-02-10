# Graphs

DotNet.AdvancedCollections provides a complete graph implementation.

## Graph&lt;T&gt;

A graph that can be directed or undirected, with support for vertices and edges.

### Features

- Support for directed and undirected graphs
- Add and remove vertices and edges
- Get adjacencies and degrees
- Generic for any data type

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

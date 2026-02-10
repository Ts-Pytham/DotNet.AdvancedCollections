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

// Undirected graph
var undirectedGraph = new Graph<string>(isDirected: false);

// Directed graph
var directedGraph = new Graph<int>(isDirected: true);
```

### Vertices

```csharp
var graph = new Graph<string>(isDirected: false);

// Add vertices
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Check if a vertex exists
bool exists = graph.ContainsVertex("A"); // true

// Remove vertex
graph.RemoveVertex("C");

// Get all vertices
var vertices = graph.Vertices;

// Get vertex count
int vertexCount = graph.VertexCount;
```

### Edges

```csharp
var graph = new Graph<string>(isDirected: false);
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");

// Add edges
graph.AddEdge("A", "B");
graph.AddEdge("B", "C");

// Add edge with weight
graph.AddEdge("A", "C", weight: 5.0);

// Check if an edge exists
bool hasEdge = graph.ContainsEdge("A", "B"); // true

// Remove edge
graph.RemoveEdge("A", "B");

// Get all edges
var edges = graph.Edges;

// Get edge count
int edgeCount = graph.EdgeCount;
```

### Adjacencies and Degrees

```csharp
var graph = new Graph<string>(isDirected: false);
graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddEdge("A", "B");
graph.AddEdge("A", "C");

// Get adjacent vertices
var neighbors = graph.GetAdjacentVertices("A"); // ["B", "C"]

// Get degree of a vertex
int degree = graph.GetDegree("A"); // 2

// For directed graphs
var dirGraph = new Graph<int>(isDirected: true);
dirGraph.AddVertex(1);
dirGraph.AddVertex(2);
dirGraph.AddEdge(1, 2);

int inDegree = dirGraph.GetInDegree(2);   // 1
int outDegree = dirGraph.GetOutDegree(1); // 1
```

## Vertex&lt;T&gt;

Represents a vertex in the graph.

```csharp
using DotNet.AdvancedCollections.Graph;

var vertex = new Vertex<string>("A");
string value = vertex.Value;
```

## Edge&lt;T&gt;

Represents an edge between two vertices.

```csharp
using DotNet.AdvancedCollections.Graph;

var source = new Vertex<string>("A");
var destination = new Vertex<string>("B");
var edge = new Edge<string>(source, destination, weight: 1.0);

var from = edge.Source;
var to = edge.Destination;
double weight = edge.Weight;
```

## Exceptions

The library provides graph-specific exceptions:

- `ExistentVertexException`: Thrown when attempting to add a vertex that already exists
- `NonExistentVertexException`: Thrown when attempting to operate on a non-existent vertex

```csharp
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
    graph.AddEdge("A", "Z"); // Throws NonExistentVertexException if "Z" doesn't exist
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
var socialNetwork = new Graph<string>(isDirected: false);

// Add users
socialNetwork.AddVertex("Alice");
socialNetwork.AddVertex("Bob");
socialNetwork.AddVertex("Charlie");
socialNetwork.AddVertex("Diana");

// Add connections (friendships)
socialNetwork.AddEdge("Alice", "Bob");
socialNetwork.AddEdge("Alice", "Charlie");
socialNetwork.AddEdge("Bob", "Diana");
socialNetwork.AddEdge("Charlie", "Diana");

// Find Alice's friends
var aliceFriends = socialNetwork.GetAdjacentVertices("Alice");
Console.WriteLine($"Alice has {aliceFriends.Count()} friends");

// Check if two people are friends
bool areFriends = socialNetwork.ContainsEdge("Bob", "Charlie");
Console.WriteLine($"Bob and Charlie are friends: {areFriends}");
```

# DotNet.AdvancedCollections

Welcome to the **DotNet.AdvancedCollections** documentation, a comprehensive library of advanced data structures for .NET.

## Features

This library provides efficient and well-tested implementations of:

- **Lists**: `DoublyLinkedList<T>`, `SortedList<T>`
- **Queues**: `PriorityQueue<T>`, `Deque<T>`
- **Stacks**: `PriorityStack<T>`
- **Caches**: `LRUCache<TKey, TValue>` - Least Recently Used cache with fixed capacity
- **Trees**: `BinarySearchTree<T>`, `BinaryTreeNode<T>`
- **Graphs**: 
  - `Graph<TVertex, TEdge>` (Adjacency List) - Sparse graphs, memory efficient
  - `AdjacencyMatrixGraph<TVertex, TEdge>` - Dense graphs, O(1) edge lookups

## Getting Started

### Installation

```bash
dotnet add package TsPytham.DotNet.AdvancedCollections
```

### Basic Usage

```csharp
using DotNet.AdvancedCollections.List.DoublyLinkedList;
using DotNet.AdvancedCollections.Queue.PriorityQueue;
using DotNet.AdvancedCollections.Tree.BinarySearchTree;
using DotNet.AdvancedCollections.Cache;
using DotNet.AdvancedCollections.Graph;

// Doubly linked list
var list = new DoublyLinkedList<int>();
list.Add(1);
list.Add(2);
list.Add(3);

// Priority queue
var priorityQueue = new PriorityQueue<string>();
priorityQueue.Enqueue("Task 1", 3);
priorityQueue.Enqueue("Task 2", 1);
var next = priorityQueue.Dequeue(); // Returns "Task 1" (highest priority: 3)

// LRU Cache - Fixed capacity with automatic eviction
var cache = new LRUCache<int, string>(capacity: 3);
cache.Put(1, "one");
cache.Put(2, "two");
cache.Put(3, "three");
cache.TryGet(1, out var value); // "one" - marks as recently used
cache.Put(4, "four"); // Evicts key 2 (least recently used)

// Binary search tree
var bst = new BinarySearchTree<int>();
bst.Add(5);
bst.Add(3);
bst.Add(7);
bool contains = bst.Contains(5); // true

// Graph (Adjacency List) - Best for sparse graphs
var sparseGraph = new Graph<string, int>
{
    new Vertex<string, int>("A"),
    new Vertex<string, int>("B")
};
sparseGraph.AddEdge("A", "B", 10);

// Graph (Adjacency Matrix) - Best for dense graphs, O(1) edge checks
var denseGraph = new AdjacencyMatrixGraph<string, int>();
denseGraph.AddVertex("A");
denseGraph.AddVertex("B");
denseGraph.AddEdge("A", "B", 10);
bool hasEdge = denseGraph.HasEdge("A", "B"); // O(1) operation!
```

## Navigation

- [User Guide](docs/index.md)
- [API Reference](api/index.md)

## License

This project is licensed under the MIT License. See the LICENSE file for details.

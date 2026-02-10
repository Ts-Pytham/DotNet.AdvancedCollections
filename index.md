# DotNet.AdvancedCollections

Welcome to the **DotNet.AdvancedCollections** documentation, a comprehensive library of advanced data structures for .NET.

## Features

This library provides efficient and well-tested implementations of:

- **Lists**: `DoublyLinkedList<T>`, `SortedList<T>`
- **Queues**: `PriorityQueue<T>`, `Deque<T>`
- **Stacks**: `PriorityStack<T>`
- **Trees**: `BinarySearchTree<T>`, `BinaryTreeNode<T>`
- **Graphs**: `Graph<T>`, support for directed and undirected graphs

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

// Binary search tree
var bst = new BinarySearchTree<int>();
bst.Add(5);
bst.Add(3);
bst.Add(7);
bool contains = bst.Contains(5); // true
```

## Navigation

- [User Guide](docs/index.md)
- [API Reference](api/index.md)

## License

This project is licensed under the MIT License. See the LICENSE file for details.

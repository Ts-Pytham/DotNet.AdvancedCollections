# GenericCollectionsExtension

[![](https://img.shields.io/badge/.NET8-red)](https://github.com/Ts-Pytham/GenericCollectionsExtension)
[![](https://img.shields.io/badge/License-MIT-green)](https://github.com/Ts-Pytham/GenericCollectionsExtension/blob/master/LICENSE.txt)
[![](https://img.shields.io/badge/Nuget-1.4.0-blue)](https://www.nuget.org/packages/GenericCollectionsExtension/)
[![PayPal-donate-button](https://img.shields.io/badge/Paypal-donate-orange)](https://paypal.me/johansanchezdl?locale.x=es_XC)

# DotNet.AdvancedCollections

Advanced and complementary data structures for modern .NET.

`DotNet.AdvancedCollections` provides production-ready data structures
that are not included in `System.Collections`, designed with clean APIs,
modern C# features, and performance in mind.

This project is the successor of **GenericCollectionsExtension**,
rewritten to target modern .NET and improve design, safety, and
maintainability.

---

## Why this library?

.NET already offers excellent general-purpose collections, but some data
structures are intentionally left out of the BCL.

This library focuses on:

- Advanced and specialized data structures
- Clean and explicit APIs
- Nullable reference types enabled
- Modern .NET (`net8.0`)
- Practical usage (not academic demos)

If you need trees, graphs, priority-based collections, or custom list
variants, this library is for you.

---

## Target framework

- **.NET 8.0**

> The library targets modern .NET only.\
> Older frameworks and `.NET Standard` are intentionally not supported.

---

## Included data structures

New data structures were added that are derived from the traditional data structures, these are:

- Deque.
- PriorityQueue.
- PriorityStack.
- Binary Search Tree.
- SortedList.
- DoublyLinkedList.
- Graph.

### Queue

A queue is a data structure that stores and retrieves items in a first-in-first-out (FIFO) manner.
This means that the first item that is added to the queue is also the first item that is removed from the queue.
Queues are useful for storing and managing data in applications where items need to be processed in the order in which they are received.
For example, a queue could be used to hold tasks that need to be executed in the order in which they are received, or to hold messages that need to be sent in the order in which they were received.

#### Queue Type:

- Deque: Is a double-ended queue, which is a type of data structure that allows items to be added and removed from either end of the queue.
  This means that items can be added to the front or back of the queue, and can also be removed from the front or back of the queue.

**Example in code:**

```C#
Deque<int> deque = new();
deque.PushFirst(1);
deque.PushFirst(2);
deque.PushFirst(5);

Console.WriteLine($"Count: {deque.Count}");
foreach(var item in deque)
{
    Console.WriteLine(item);
}

/*
Output:
Count: 3
5
2
1
*/

```

- Priority Queue: Is a data structure in which each item has a priority associated with it, and items are stored and retrieved in order of their priority.
  This means that when items are added to the queue, they are automatically placed in the correct position based on their priority, and when items are removed from the queue, the item with the highest priority is always selected first.

**Example in code:**

```C#
PriorityQueue<int> priorityQueue = new();
priorityQueue.Enqueue(10, 5);
priorityQueue.Enqueue(50, 2);
priorityQueue.Enqueue(55, 2);
priorityQueue.Enqueue(20, 3);

foreach (int item in priorityQueue)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Peek: {priorityQueue.Peek()}");
int contains = 51;
Console.WriteLine($"Contains: {contains}, {priorityQueue.Contains(contains)}");


/*
Output:
10
20
50
55
Peek: 10
Contains: 51, False

*/
```

### Stack

A stack is a data structure that stores and retrieves items in a last-in-first-out (LIFO) manner.
This means that the last item that is added to the stack is also the first item that is removed from the stack.
Stacks are useful for storing and managing data in applications where items need to be processed in the reverse order in which they are received.

- Priority Stack: Is a type of data structure that combines the characteristics of a stack and a priority queue.
  Like a stack, a priority stack stores and retrieves items in a last-in-first-out (LIFO) manner, but also allows items to be added and removed based on their priority.
  This means that items can be added to the top of the stack in any order, but when items are removed from the stack, the item with the highest priority is always selected first.

**Example in code:**

```C#

PriorityStack<int> stack = new()
{
    4, 5, 3, 5
};

Console.WriteLine($"Count: {stack.Count}");

Console.WriteLine($"Value: {stack.Peek()}");

foreach(var item in stack)
{
    Console.WriteLine(item);
}

/*
Count: 4
Value: 5
5
3
5
4
*/
```

### List

Is a data structure that stores a collection of items in a specific order. Lists allow items to be added, removed, and accessed by their position in the list. They are often used to store collections of data that need to be organized and accessed in a specific order.

- Sorted List: Is a data structure that is similar to a regular list, but maintains its items in a sorted order. This means that when items are added to the list, they are automatically placed in the correct position based on their sorting order. Sorted lists are useful in situations where it is necessary to access items in a specific order, such as in applications that require efficient search and retrieval of data.

- DoublyLinkedList: Is a data structure that consists of a set of nodes, where each node has two pointers: one pointing to the previous node in the list and one pointing to the next node in the list. This allows for efficient insertion and deletion of elements from the list, as well as the ability to traverse the list in both directions (from the beginning to the end and from the end to the beginning).

**Example of SortedList in code:**

```C#
//Descending

SortedList<int> sortedList = new(Criterion.Descending)
{
    1,
    0,
    -1,
    495,
    1,
    19,
    5
};

Console.WriteLine($"total items: {sortedList.Count}");
sortedList.ToList().ForEach(Console.WriteLine);

/*
total items: 7
495
19
5
1
1
0
-1
*/
```

```C#
//Ascending

SortedList<int> sortedList = new()
{
    1,
    0,
    -1,
    495,
    1,
    19,
    5
};

Console.WriteLine($"total items: {sortedList.Count}");
sortedList.ToList().ForEach(Console.WriteLine);

/*
total items: 7
-1
0
1
1
5
19
495
*/
```

##### NOTE

This implementation is different from the one implemented in System.Collections.Generic because this implementation does not use a key, and any class to be used must inherit the IComparable interface. Very important to look at this implementation and documentation.

### Tree

Is a data structure consisting of linked nodes. Nodes can have one or more children and each node can have a relationship with other nodes in the tree.

- Binary search tree: Is a type of data tree in which each node has a value that is smaller than all the values in its left subtree and larger than all the values in its right subtree. This allows for fast and efficient search in the tree.

**Example in code:**

```C#
BinarySearchTree<int> tree = new() { 5, 3, 4, 7, 6, 9, 8, 1, 2, 0, 14, 15};

Console.WriteLine($"Count: {tree.Count}");
Console.WriteLine($"Contains 5: {tree.Contains(5)}");
Console.WriteLine($"Contains 12: {tree.Contains(12)}");
var inOrder = tree.InOrder();
Console.WriteLine($"Count InOrder: {inOrder.Count()}");

foreach(var item in inOrder)
{
    Console.WriteLine(item);
}

/*
Count: 12
Contains 5: True
Contains 12: False
Count InOrder: 12
0
1
2
3
4
5
6
7
8
9
14
15
*/
```

### Graph

A graph is a data structure that consists of a finite set of vertices (also known as nodes) and a set of edges connecting these vertices. The vertices represent the entities being modeled, and the edges represent the relationships between them. Graphs are often used to represent networks, such as social networks, communication networks, and transportation networks. They are also used in computer science to represent data structures such as trees and maps.

**Example in code:**

```C#
var graph = new Graph<char, int>
{
    new Vertex<char, int>('A'),
    new Vertex<char, int>('B'),
    new Vertex<char, int>('C')
};

graph.AddEdge('A', 'B', 10);
graph.AddEdge('A', 'C', 5);

foreach(var vertex in graph)
{
    Console.WriteLine($"{vertex.VertexName}: {string.Join(" ", graph.Successors(vertex.VertexName).Select(x => x.VertexName))}");
}

/*
A: B C
B:
C:
*/
```

## Project status

This project is under active development.\
Public APIs may evolve until the first stable release (`v1.0`).

---

## History

This project is the modern successor of **GenericCollectionsExtension**.

---

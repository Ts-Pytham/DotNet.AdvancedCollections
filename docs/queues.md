# Queues

DotNet.AdvancedCollections provides advanced queue implementations.

## PriorityQueue&lt;T&gt;

A priority queue where elements are dequeued according to their priority.

### Features

- Elements with higher priority are processed first
- Efficient heap-based implementation
- Generic support for any type

### Usage Example

```csharp
using DotNet.AdvancedCollections.Queue.PriorityQueue;

var queue = new PriorityQueue<string>();

// Enqueue with priorities
queue.Enqueue("Low priority task", 1);
queue.Enqueue("High priority task", 10);
queue.Enqueue("Medium priority task", 5);

// Dequeue (returns "High priority task")
var next = queue.Dequeue();

// Peek at the next item without dequeuing
var peek = queue.Peek();

// Check if empty
bool isEmpty = queue.IsEmpty;

// Get element count
int count = queue.Count;
```

## Deque&lt;T&gt;

A double-ended queue that allows insertion and removal at both ends.

### Features

- O(1) operations at both ends
- Can be used as a queue or stack
- Efficient implementation

### Usage Example

```csharp
using DotNet.AdvancedCollections.Queue.Deque;

var deque = new Deque<int>();

// Add to front
deque.PushFirst(1);
deque.PushFirst(2);
deque.PushFirst(5);

// Add to back
deque.PushLast(10);

// Check count
Console.WriteLine($"Count: {deque.Count}");

// Iterate
foreach (var item in deque)
{
    Console.WriteLine(item);
}

// Remove from front (Dequeue)
var first = deque.Dequeue();

// Remove from back
var last = deque.PopLast();

// Peek at elements without removing
var peekFirst = deque.Peek();
var peekLast = deque.PeekLast();
```

## Comparison

| Operation | PriorityQueue | Deque |
|-----------|---------------|-------|
| Enqueue | O(log n) | O(1) |
| Dequeue | O(log n) | O(1) |
| Peek | O(1) | O(1) |
| Order | By priority | FIFO/LIFO |

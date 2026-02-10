# Lists

DotNet.AdvancedCollections provides several list implementations with special features.

## DoublyLinkedList&lt;T&gt;

A doubly linked list that allows efficient bidirectional navigation.

### Features

- O(1) insertion/deletion at both ends
- Forward and backward navigation
- Support for concurrent synchronization

### Usage Example

```csharp
using DotNet.AdvancedCollections.List.DoublyLinkedList;

var list = new DoublyLinkedList<int>();

// Add elements
list.Add(1);
list.Add(2);
list.Add(3);

// Insert at the beginning
list.AddFirst(0);

// Insert at the end
list.AddLast(4);

// Iterate
foreach (var item in list)
{
    Console.WriteLine(item);
}

// Navigate backward
var current = list.Tail;
while (current != null)
{
    Console.WriteLine(current.Value);
    current = current.Previous;
}
```

## SortedList&lt;T&gt;

A list that maintains its elements sorted automatically.

### Features

- Maintains order automatically
- Supports ascending and descending sort criteria
- Efficient element search

### Usage Example

```csharp
using DotNet.AdvancedCollections.List.SortedList;

// Ascending sorted list
var sortedList = new SortedList<int>(Criterion.Ascending);
sortedList.Add(3);
sortedList.Add(1);
sortedList.Add(2);
// The list is sorted: [1, 2, 3]

// Descending sorted list
var descList = new SortedList<string>(Criterion.Descending);
descList.Add("Charlie");
descList.Add("Alice");
descList.Add("Bob");
// The list is sorted: ["Charlie", "Bob", "Alice"]
```

## Synchronization

Both lists implement `ISynchronized` for thread-safe operations:

```csharp
var list = new DoublyLinkedList<int>();

// Synchronize operations
lock (list.SyncRoot)
{
    list.Add(1);
    list.Add(2);
}
```

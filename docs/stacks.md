# Stacks

DotNet.AdvancedCollections provides advanced stack implementations.

## PriorityStack&lt;T&gt;

A priority stack where elements are popped according to their priority instead of LIFO.

### Features

- Elements with higher priority are popped first
- Efficient implementation
- Familiar API similar to Stack&lt;T&gt;

### Usage Example

```csharp
using DotNet.AdvancedCollections.Stack.PriorityStack;

var stack = new PriorityStack<string>();

// Push with priorities
stack.Push("Low priority task", 1);
stack.Push("High priority task", 10);
stack.Push("Medium priority task", 5);

// Pop (returns "High priority task")
var next = stack.Pop();

// Peek at the next item without popping
var peek = stack.Peek();

// Check if empty
bool isEmpty = stack.IsEmpty;

// Get element count
int count = stack.Count;

// Clear the stack
stack.Clear();
```

## Use Cases

The priority stack is useful in scenarios such as:

- Task processing systems with priorities
- Algorithms requiring prioritized element access
- Event management with different importance levels
- Process schedulers

## Practical Example

```csharp
using DotNet.AdvancedCollections.Stack.PriorityStack;

public class TaskProcessor
{
    private readonly PriorityStack<Task> _tasks = new();

    public void AddTask(Task task, int priority)
    {
        _tasks.Push(task, priority);
    }

    public async Task ProcessNextTask()
    {
        if (!_tasks.IsEmpty)
        {
            var task = _tasks.Pop();
            await task.ExecuteAsync();
        }
    }

    public int PendingTasks => _tasks.Count;
}
```

## Comparison with Standard Stack&lt;T&gt;

| Feature | PriorityStack&lt;T&gt; | Stack&lt;T&gt; |
|---------|------------------------|----------------|
| Pop order | By priority | LIFO |
| Push complexity | O(log n) | O(1) |
| Pop complexity | O(log n) | O(1) |
| Memory usage | Higher | Lower |

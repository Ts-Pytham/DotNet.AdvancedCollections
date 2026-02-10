# Trees

DotNet.AdvancedCollections provides binary tree implementations.

## BinarySearchTree&lt;T&gt;

A binary search tree (BST) that maintains elements in sorted order and allows efficient searches.

### Features

- Efficient search, insertion, and deletion
- In-order, pre-order, and post-order traversals
- Generic with support for any comparable type

### Usage Example

```csharp
using DotNet.AdvancedCollections.Tree.BinarySearchTree;

var bst = new BinarySearchTree<int>();

// Add elements
bst.Add(5);
bst.Add(3);
bst.Add(7);
bst.Add(1);
bst.Add(9);

// Search for elements
bool exists = bst.Contains(7); // true

// Remove elements
bst.Remove(3);

// Get tree height
int height = bst.Height;

// Check if empty
bool isEmpty = bst.IsEmpty;

// Get node count
int count = bst.Count;
```

## Traversals

The tree supports different traversal types:

```csharp
using DotNet.AdvancedCollections.Tree;

var bst = new BinarySearchTree<int>();
bst.Add(5);
bst.Add(3);
bst.Add(7);

// In-order traversal (left, root, right)
// Result: [3, 5, 7]
foreach (var item in bst.Traverse(TraversalType.InOrder))
{
    Console.WriteLine(item);
}

// Pre-order traversal (root, left, right)
// Result: [5, 3, 7]
foreach (var item in bst.Traverse(TraversalType.PreOrder))
{
    Console.WriteLine(item);
}

// Post-order traversal (left, right, root)
// Result: [3, 7, 5]
foreach (var item in bst.Traverse(TraversalType.PostOrder))
{
    Console.WriteLine(item);
}
```

## BinaryTreeNode&lt;T&gt;

Individual node of a binary tree with references to left and right children.

### Usage Example

```csharp
using DotNet.AdvancedCollections.Tree.BinaryNode;

// Create nodes manually
var root = new BinaryTreeNode<int>(10);
var left = new BinaryTreeNode<int>(5);
var right = new BinaryTreeNode<int>(15);

root.Left = left;
root.Right = right;

// Access values
int value = root.Value;
var leftChild = root.Left;
var rightChild = root.Right;
```

## Complexity

| Operation | Average case | Worst case |
|-----------|--------------|------------|
| Search | O(log n) | O(n) |
| Insert | O(log n) | O(n) |
| Delete | O(log n) | O(n) |
| Traversal | O(n) | O(n) |

**Note**: The worst case O(n) occurs when the tree is unbalanced (similar to a linked list).

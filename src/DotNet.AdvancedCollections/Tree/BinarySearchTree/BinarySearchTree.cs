using DotNet.AdvancedCollections.Tree.BinaryNode;
using System.Collections;

namespace DotNet.AdvancedCollections.Tree.BinarySearchTree;

/// <summary>
/// Represents a <see cref="BinarySearchTree{T}"/> implementation.
/// </summary>
/// <typeparam name="T">T is type of element in the list and implements <see cref="IComparable{T}"/>.</typeparam>
public class BinarySearchTree<T> : IBinarySearchTree<T>, IEnumerable<T>
    where T : notnull, IComparable<T>
{
    /// <summary>
    /// Number of data in the tree.
    /// </summary>
    public int Count { get; private set; }

    public bool IsReadOnly => false;

    /// <summary>
    /// Tree root.
    /// </summary>
    private IBinaryTreeNode<T>? _root;

    /// <inheritdoc cref="IBinarySearchTree{T}.Traversal"/>
    public TraversalType Traversal { get; set; }

    /// <summary>
    /// Represents a property that returns a dictionary containing the number of times each element appears in the tree.
    /// </summary>
    public Dictionary<T, int> RepeatedNodes { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class. 
    /// <para>The traversal type is set to InOrder by default.</para>
    /// </summary>
    public BinarySearchTree()
    {
        RepeatedNodes = [];
        Traversal = TraversalType.InOrder;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class with the specified traversal type.
    /// </summary>
    /// <param name="traversal">The traversal type to use for the tree.</param>
    public BinarySearchTree(TraversalType traversal)
    {
        RepeatedNodes = [];
        Traversal = traversal;
    }

    /// <summary>
    /// Adds the specified item to the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    /// <param name="item">The item to add to the tree.</param>
    public void Add(T item)
    {
        if (_root is null)
        {
            _root = new BinaryTreeNode<T>(item);
        }
        else
        {
            Add(item, _root);
        }
        Count++;
    }

    /// <summary>
    /// This method adds the specified item of type <typeparamref name="T"/> to a <see cref="BinarySearchTree{T}"/>. 
    /// The root parameter specifies the root node of the tree, which should be of type IBinaryTreeNode.
    /// </summary>
    /// <param name="item">This is the item to be added to the <see cref="BinarySearchTree{T}"/>. It should be of the type specified by the T type parameter.</param>
    /// <param name="root"> where T is the same type as the item parameter. This parameter specifies where in the tree the item should be added.</param>
    private void Add(T item, IBinaryTreeNode<T> root)
    {
        if (item.CompareTo(root.Data) > 0) // Item Greater than root.Data
        {
            if (root.RightChild is null)
            {
                root.RightChild = new BinaryTreeNode<T>(item, root);
            }
            else
            {
                Add(item, root.RightChild);
            }
        }
        else if (item.CompareTo(root.Data) < 0) // Item less than root.Data
        {
            if (root.LeftChild is null)
            {
                root.LeftChild = new BinaryTreeNode<T>(item, root);
            }
            else
            {
                Add(item, root.LeftChild);
            }
        }
        else //Repeated Value
        {
            if (RepeatedNodes.TryGetValue(item, out int value))
            {
                RepeatedNodes[item] = ++value;
            }
            else
            {
                RepeatedNodes.Add(item, 1);
            }
        }
    }

    /// <summary>
    /// Deletes all data from the tree.
    /// </summary>
    public void Clear()
    {
        _root = null;
    }

    /// <summary>
    /// This method checks whether the given item exists in the tree by calling the Search method and checking if the result is not `null`.
    /// </summary>
    /// <param name="item">The item to search for in the tree.</param>
    /// <returns>A boolean value indicating whether the item was found in the tree.</returns>
    public bool Contains(T item)
    {
        return Search(_root, item) is not null;
    }

    /// <summary>
    /// This method performs a depth-first search (DFS) for the given item in the <see cref="BinarySearchTree{T}"/>. 
    /// It returns a reference to the node containing the item if it is found, or null if the item is not found.
    /// </summary>
    /// <param name="r">The root of the <see cref="BinarySearchTree{T}"/> to search.</param>
    /// <param name="item">The item to search for in the <see cref="BinarySearchTree{T}"/>.</param>
    /// <returns>A reference to the node containing the item if it is found, or null if the item is not found.</returns>
    private IBinaryTreeNode<T>? Search(IBinaryTreeNode<T>? r, T item)
    {
        if (_root is null)
        {
            return null;
        }

        if (r is null)
        {
            return null;
        }

        if (r.Data.CompareTo(item) == 0)
        {
            return r;
        }
        else if (r.Data.CompareTo(item) > 0) // root.Data Greater than item
        {
            if (r.LeftChild is null)
            {
                return null;
            }
            else
            {
                return Search(r.LeftChild, item);
            }
        }
        else
        {
            if (r.RightChild is null)
            {
                return null;
            }
            else
            {
                return Search(r.RightChild, item);
            }
        }

    }

    /// <summary>
    /// This method removes the given item from the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    /// <param name="item">The item to remove from the <see cref="BinarySearchTree{T}"/>.</param>
    /// <returns>A boolean value indicating whether the item was found and removed from the tree.</returns>
    public bool Remove(T item)
    {
        var node = Search(_root, item);

        if (node is null)
        {
            return false;
        }

        var parent = node.Parent;

        if (node.LeftChild is null && node.RightChild is null) //Node is a leaf
        {
            if (parent is null) // Root does not parent!
            {
                _root = null;
                Count = 0;
                return true;
            }

            if (parent.LeftChild == node)
            {
                parent.LeftChild = null;
            }
            else if (parent.RightChild == node)
            {
                parent.RightChild = null;
            }
            else
            {
                throw new InvalidOperationException("The parent node does not have the current node as a child.");
            }
        }
        else if (node.LeftChild is null || node.RightChild is null) // At least one subtree is null
        {
            if (parent is null) // Is a root
            {
                _root = node.LeftChild is not null ? node.LeftChild : node.RightChild;
                Count--;
                return true;
            }

            if (node.LeftChild is not null && parent.LeftChild == node)
            {
                parent.LeftChild = node.LeftChild;
            }
            else if (node.RightChild is not null && parent.LeftChild == node)
            {
                parent.LeftChild = node.RightChild;
            }
            else if (node.LeftChild is not null && parent.RightChild == node)
            {
                parent.RightChild = node.LeftChild;
            }
            else
            {
                parent.RightChild = node.RightChild;
            }
        }
        else // Probably has two sub-trees
        {
            var inorder = InOrder().ToList();
            var toReplace = inorder[inorder.IndexOf(item) + 1];
            var nodeToReplace = Search(node, toReplace)
                ?? throw new InvalidOperationException("The node to replace was not found in the tree.");

            if (nodeToReplace.RightChild is not null) // two cases: because the node to be replaced has more information and is the one following the node to be deleted.
            {
                node.RightChild = nodeToReplace.RightChild;
            }
            else // The node to replace is a leaf
            {
                Remove(toReplace);
                Count++;
            }
            node.Data = nodeToReplace.Data;
            if (parent is not null) // If parent is null, the sub-tree is root
            {
                if (parent.RightChild == node)
                {
                    parent.RightChild = node;
                }
                else
                {
                    parent.LeftChild = node;
                }
            }

        }

        Count--;

        return true;
    }

    /// <summary>
    /// This method copies the elements in the <see cref="BinarySearchTree{T}"/> to the given array, starting at the specified index in the array.
    /// </summary>
    /// <param name="array">The array to copy the elements of the <see cref="BinarySearchTree{T}"/> to.</param>
    /// <param name="arrayIndex">The index in the array at which to start copying elements.</param>
    public void CopyTo(T[] array, int arrayIndex)
    {
        GetEnumerable().ToArray().CopyTo(array, arrayIndex);
    }

    /// <inheritdoc cref="IBinarySearchTree{T}.IsEmpty"/>
    public bool IsEmpty()
    {
        return _root is null;
    }

    /// <summary>
    /// This method returns an enumerator for the elements in the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    /// <returns>An enumerator for the elements in the <see cref="BinarySearchTree{T}"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// This method returns an enumerator for the elements in the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    /// <returns>An enumerator for the elements in the <see cref="BinarySearchTree{T}"/>.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerable().GetEnumerator();
    }

    /// <summary>
    /// This method returns an enumerable for the elements in the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    /// <returns>An enumerable for the elements in the <see cref="BinarySearchTree{T}"/>.</returns>
    public IEnumerable<T> GetEnumerable()
    {
        if (Traversal == TraversalType.PreOrder)
        {
            return PreOrder();
        }
        else if (Traversal == TraversalType.InOrder)
        {
            return InOrder();
        }

        return PostOrder();
    }

    /// <summary>
    /// This method returns an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in in-order traversal order.
    /// </summary>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in in-order traversal order.</returns>
    public IEnumerable<T> InOrder()
    {
        if (_root is not null)
        {
            return BinarySearchTree<T>.InOrder(_root);
        }

        return [];
    }

    /// <summary>
    /// This method recursively generates an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in in-order traversal order.
    /// </summary>
    /// <param name="r">The root of the subtree to generate the in-order traversal for.</param>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in in-order traversal order.</returns>
    private static IEnumerable<T> InOrder(IBinaryTreeNode<T>? r)
    {
        if (r is not null)
        {
            return BinarySearchTree<T>.InOrder(r.LeftChild).Append(r.Data)
                .Union(BinarySearchTree<T>.InOrder(r.RightChild));                                    
        }

        return [];
    }

    /// <summary>
    /// This method returns an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in pre-order traversal order.
    /// </summary>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in pre-order traversal order.</returns>
    public IEnumerable<T> PreOrder()
    {
        if (_root is not null)
        {
            return PreOrder(_root);
        }

        return [];
    }

    /// <summary>
    /// This method recursively generates an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in pre-order traversal order.
    /// </summary>
    /// <param name="r">The root of the subtree to generate the pre-order traversal for.</param>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in pre-order traversal order.</returns>
    private static IEnumerable<T> PreOrder(IBinaryTreeNode<T>? r)
    {
        if (r is not null)
        {
            List<T> list = [r.Data];

            return list.Union(PreOrder(r.LeftChild))
                .Union(PreOrder(r.RightChild));
        }

        return [];
    }

    /// <summary>
    /// This method returns an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in post-order traversal order.
    /// </summary>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in post-order traversal order.</returns>
    public IEnumerable<T> PostOrder()
    {
        if (_root is not null)
        {
            return PostOrder(_root);
        }

        return [];
    }

    /// <summary>
    /// This method recursively generates an enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in post-order traversal order.
    /// </summary>
    /// <param name="r">The root of the subtree to generate the post-order traversal for.</param>
    /// <returns>An enumerable collection of the elements in the <see cref="BinarySearchTree{T}"/> in post-order traversal order.</returns>
    private static IEnumerable<T> PostOrder(IBinaryTreeNode<T>? r)
    {
        if (r is not null)
        {
            return PostOrder(r.LeftChild).Union(PostOrder(r.RightChild))
                .Append(r.Data);
        }
        return [];
    }
}

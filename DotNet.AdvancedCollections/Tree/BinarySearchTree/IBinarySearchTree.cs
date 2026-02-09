namespace DotNet.AdvancedCollections.Tree.BinarySearchTree;

/// <summary>
/// Represents BinarySearchTree Interface.
/// </summary>
public interface IBinarySearchTree<T> : ICollection<T>
{
    /// <summary>
    /// Traversal type.
    /// InOrder, PostOrder and PreOrder.
    /// </summary>
    TraversalType Traversal { get; set; }

    /// <summary>
    /// Check if the tree is empty.
    /// </summary>
    /// <returns>If the tree is empty, returns true, otherwise returns false.</returns>
    bool IsEmpty();
}

namespace DotNet.AdvancedCollections.Tree;

/// <summary>
/// This enum type defines the different types of traversal order for a binary tree.
/// </summary>
public enum TraversalType
{
    /// <summary>
    /// In-order traversal visits the left child of a node, then the node itself, and finally the right child of the node.
    /// This results in the nodes being visited in ascending order if the binary tree is a search tree.
    /// </summary>
    InOrder,

    /// <summary>
    /// Pre-order traversal visits the node itself, then the left child of the node, and finally the right child of the node.
    /// </summary>
    PreOrder,

    /// <summary>
    /// Post-order traversal visits the left child of a node, then the right child of the node, and finally the node itself.
    /// </summary>
    PostOrder,
}

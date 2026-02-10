namespace DotNet.AdvancedCollections.Tree.BinaryNode;

/// <summary>
/// Represents the implementation Binary tree node.
/// </summary>
/// <typeparam name="T">T is generic.</typeparam>
internal class BinaryTreeNode<T> : IBinaryTreeNode<T>
    where T : notnull
{
    public IBinaryTreeNode<T>? LeftChild { get; set; }
    public IBinaryTreeNode<T>? RightChild { get; set; }
    public IBinaryTreeNode<T>? Parent { get; set; }
    public T Data { get; set; }

    /// <summary>
    /// Initializes a new instance of the T:GenericCollectionsExtension.Tree.BinaryTreeNode class.
    /// </summary>
    /// <param name="data">Data to be added to node.</param>
    public BinaryTreeNode(T data)
    {
        Data = data;
        LeftChild = null;
        RightChild = null;
        Parent = null;
    }

    /// <summary>
    /// Initializes a new instance of the T:GenericCollectionsExtension.Tree.BinaryTreeNode class.
    /// </summary>
    /// <param name="data">Data to be added to node.</param>
    /// <param name="parent">Node parent.</param>
    public BinaryTreeNode(T data, IBinaryTreeNode<T> parent)
    {
        Data = data;
        LeftChild = null;
        RightChild = null;
        Parent = parent;
    }
}

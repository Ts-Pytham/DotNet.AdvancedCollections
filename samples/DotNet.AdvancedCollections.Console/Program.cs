using DotNet.AdvancedCollections.Graph;
using DotNet.AdvancedCollections.List.DoublyLinkedList;
using DotNet.AdvancedCollections.List.SortedList;
using DotNet.AdvancedCollections.Queue.Deque;
using DotNet.AdvancedCollections.Queue.PriorityQueue;
using DotNet.AdvancedCollections.Stack.PriorityStack;
using DotNet.AdvancedCollections.Tree.BinarySearchTree;

//SortedList<int> sortedList = new(Criterion.Descending)
//{
//    1,
//    0,
//    -1,
//    495,
//    1,
//    19,
//    5
//};

//sortedList.Reverse();
//Console.WriteLine($"total items: {sortedList.Count}");
//sortedList.ToList().ForEach(Console.WriteLine);

//Console.WriteLine("PriorityQueue: ");

//PriorityQueue<int> priorityQueue = [];
//priorityQueue.Enqueue(10, 5);
//priorityQueue.Enqueue(50, 2);
//priorityQueue.Enqueue(55, 2);
//priorityQueue.Enqueue(20, 3);

//foreach (int item in priorityQueue)
//{
//    Console.WriteLine(item);
//}

//Console.WriteLine($"Peek: {priorityQueue.Peek()}");
//int contains = 51;
//Console.WriteLine($"Contains: {contains}, {priorityQueue.Contains(contains)}");

//Console.WriteLine("Deque: ");

//Deque<int> deque = new();
//deque.PushFirst(1);
//deque.PushFirst(2);
//deque.PushFirst(5);

//Console.WriteLine($"Count: {deque.Count}");
//foreach (var item in deque)
//{
//    Console.WriteLine(item);
//}


//Console.WriteLine("Priority Stack: ");

//PriorityStack<int> stack = new()
//{
//    4, 5, 3, 5
//};

//Console.WriteLine($"Count: {stack.Count}");

//Console.WriteLine($"Value: {stack.Peek()}");

//foreach (var item in stack)
//{
//    Console.WriteLine(item);
//}


//Console.WriteLine("Binary Search Tree");

//BinarySearchTree<int> tree = new() { 5, 3, 4, 7, 6, 9, 8, 1, 2, 0, 14, 15 };

//Console.WriteLine($"Count: {tree.Count}");
//Console.WriteLine($"Contains 5: {tree.Contains(5)}");
//Console.WriteLine($"Contains 12: {tree.Contains(12)}");
//var inOrder = tree.InOrder();
//Console.WriteLine($"Count InOrder: {inOrder.Count()}");

//foreach (var item in inOrder)
//{
//    Console.WriteLine(item);
//}

//var postOrder = tree.PostOrder();
//Console.WriteLine($"Count PostOrder: {postOrder.Count()}");

//foreach (var item in postOrder)
//{
//    Console.WriteLine(item);
//}

//var preOrder = tree.PreOrder();
//Console.WriteLine($"Count PreOrder: {preOrder.Count()}");

//foreach (var item in preOrder)
//{
//    Console.WriteLine(item);
//}
//int delete = 5;
//Console.WriteLine($"Delete leaf: {tree.Remove(delete)}, Value: {delete}");
//Console.WriteLine($"Count: {tree.Count}");

//preOrder = tree.PreOrder();
//Console.WriteLine($"Count PreOrder: {preOrder.Count()}");

//foreach (var item in preOrder)
//{
//    Console.WriteLine(item);
//}

//Console.WriteLine("DoublyLinkedList: ");
//var doubly = new DoublyLinkedList<int>();

//doubly.Add(1);
//doubly.Add(2);
//doubly.Add(3);

//Console.WriteLine($"Count: {doubly.Count}\n");
//for (int i = 0; i != doubly.Count; i++)
//{
//    Console.WriteLine(doubly[i]);
//}


var graph = new Graph<char, int>
{
    new Vertex<char, int>('A'),
    new Vertex<char, int>('B'),
    new Vertex<char, int>('C')
};

graph.AddEdge('A', 'B', 10);
graph.AddEdge('A', 'C', 5);

foreach (var vertex in graph)
{
    Console.WriteLine($"{vertex.VertexName}: {string.Join(" ", graph.Predecessors(vertex.VertexName).Select(x => x.VertexName))}");
}